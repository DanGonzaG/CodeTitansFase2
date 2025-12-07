using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Preacepta.LN.DocumentosCita;
using Preacepta.LN.BitacoraEventos.Crear;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Preacepta.AD.DocumentosCitas.DocumentosCitas;
using Microsoft.AspNetCore.Identity;
using Preacepta.UI.Data;
using System;


namespace Preacepta.Web.Controllers
{
    public class DocumentosCitaController : Controller
    {
        private readonly IDocumentosCitaLN _documentosLN;
        private readonly ICrearEventosLN _bitacoraLN;
        private readonly UserManager<IdentityUser> _userManager;


        public DocumentosCitaController(UserManager<IdentityUser> userManager, IDocumentosCitaLN documentosLN, ICrearEventosLN bitacoraLN)
        {
            _documentosLN = documentosLN;
            _bitacoraLN = bitacoraLN;
            _userManager = userManager;
        }

        private string ObtenerNombreCorto(string nombreArchivo, int maxLength = 50)
        {
            if (string.IsNullOrEmpty(nombreArchivo)) return string.Empty;

            if (nombreArchivo.Length > maxLength)
                return nombreArchivo.Substring(0, maxLength - 3) + "...";

            return nombreArchivo;
        }
        [HttpGet]
        [Authorize(Roles = "Abogado,Gestor")]
        public async Task<IActionResult> Listar(
     int idCita,
     [FromServices] IDocumentosCitaLN documentosLN
 )
        {
            var documentos = documentosLN.ObtenerPorCita(idCita);

            var usuarioId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(usuarioId))
                return Unauthorized();

            var clavePublica = await documentosLN.ObtenerClavePublicaUsuario(usuarioId);

            ViewBag.IdCita = idCita;
            ViewBag.UsuarioId = usuarioId;
            ViewBag.PublicKeyPem = clavePublica?.PublicKeyPem ?? "";

            return PartialView("_ListarPartial", documentos);
        }

        [HttpPost("SubirCifrado")]
        [Authorize(Roles = "Abogado,Gestor")]
        public async Task<IActionResult> SubirDocumentoCifrado(
            [FromForm] SubirDocumentoCifradoRequest request,
            [FromServices] IDocumentosCitaAD documentosCitaAD)
        {
            try
            {
                bool existe = await documentosCitaAD.ExisteCitaAsync(request.IdCita);
                if (!existe)
                    return Json(new { success = false, message = $"No existe la cita con Id {request.IdCita}" });

                if (request.ArchivoCifrado == null || request.ArchivoCifrado.Length == 0)
                    return Json(new { success = false, message = "Archivo no recibido." });

                var nombreArchivo = string.IsNullOrWhiteSpace(request.NombreArchivo)
                    ? request.ArchivoCifrado.FileName
                    : request.NombreArchivo;

                if (string.IsNullOrWhiteSpace(nombreArchivo))
                    return Json(new { success = false, message = "Nombre de archivo inválido." });

                using var ms = new MemoryStream();
                await request.ArchivoCifrado.CopyToAsync(ms);

                var documento = new TDocumentosCita
                {
                    IdCita = request.IdCita,
                    NombreArchivo = nombreArchivo,
                    RutaArchivo = $"/uploads/citas/{request.IdCita}/{nombreArchivo}",
                    FechaSubida = DateTime.Now,
                    Activo = true,
                    Descargar = true,
                    OwnerId = request.OwnerId,
                    IV = request.IV,
                    Algoritmo = request.Algoritmo ?? "AES-GCM-256",
                    ContentType = request.ContentType,
                    ArchivoCifrado = ms.ToArray()
                };

                await documentosCitaAD.InsertarDocumentoCifradoAsync(documento);

                await documentosCitaAD.GuardarEncryptedKeyAsync(new TDocumentoKey
                {
                    DocumentoId = documento.Id,
                    UsuarioId = request.OwnerId,
                    EncryptedKeyBase64 = request.EncryptedKeyBase64,
                    IV = request.IV,
                    Activo = true,
                    FechaCreacion = DateTime.Now
                });

                var usuarios = await documentosCitaAD.ObtenerUsuariosDeCitaAsync(request.IdCita, _userManager);

                foreach (var u in usuarios.Where(u => u != request.OwnerId))
                {
                    await documentosCitaAD.GuardarEncryptedKeyAsync(new TDocumentoKey
                    {
                        DocumentoId = documento.Id,
                        UsuarioId = u,
                        EncryptedKeyBase64 = request.EncryptedKeyBase64,
                        IV = request.IV,
                        Activo = true,
                        FechaCreacion = DateTime.Now
                    });
                }

                return Json(new
                {
                    success = true,
                    message = "Documento cifrado subido correctamente.",
                    DocumentoId = documento.Id
                });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException?.Message ?? ex.Message;
                return Json(new { success = false, message = $"Error al subir archivo: {msg}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GuardarClavePublicaAutomatica([FromBody] PublicKeyDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.PublicKeyBase64))
                return BadRequest("Clave pública vacía");

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            // Usar la capa LÓGICA, no acceso a datos directo
            _documentosLN.GuardarClavePublicaUsuario(userId, dto.PublicKeyBase64);

            return Ok(new { success = true });
        }

        public class PublicKeyDTO
        {
            public string PublicKeyBase64 { get; set; }
        }



        public class PublicKeyRequest
        {
            public string publicKeyBase64 { get; set; }
        }


        [HttpGet]
        public IActionResult TieneClavePublica()
        {
            var usuarioId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(usuarioId))
                return Unauthorized();

            bool existe = _documentosLN.ExisteClavePublica(usuarioId);
            return Ok(new { existe });
        }



        [HttpGet("DescargarCifrado/{id}")]
        [Authorize(Roles = "Abogado,Cliente")]
        public async Task<IActionResult> DescargarCifrado(
     int id,
     [FromServices] IDocumentosCitaAD documentosCitaAD,
     [FromServices] IDocumentosCitaLN documentosLN)
        {
            var usuarioId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(usuarioId))
                return Unauthorized();

            // Obtener documento
            var documento = await _documentosLN.ObtenerPorIdAsync(id);
            if (documento == null)
                return NotFound("Documento no encontrado.");

            if (!documento.Descargar)
                return Forbid("No tienes permiso para descargar este documento.");

            // Obtener clave AES cifrada asociada al usuario
            var keyData = await documentosLN.ObtenerKeyDocumentoUsuario(id, usuarioId);

            if (keyData == null)
                return NotFound("No se encontró la clave cifrada del documento para este usuario.");
            //  VALIDAR QUE EXISTEN DATOS DE CIFRADO 
            if (string.IsNullOrEmpty(documento.IV))
                return BadRequest(new { mensaje = "El documento no tiene IV guardado (dañado o forma antigua)." });

            if (string.IsNullOrEmpty(keyData.EncryptedKeyBase64))
                return BadRequest(new { mensaje = "No existe clave cifrada asociada al usuario para este documento." });
            if (string.IsNullOrWhiteSpace(documento.IV))
                return BadRequest(new { mensaje = "El documento no tiene IV guardado (dañado o forma antigua)." });

            // Leer archivo físico
            /*var rutaFisica = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                documento.RutaArchivo.TrimStart('/')
            );

            if (!System.IO.File.Exists(rutaFisica))
                return NotFound("El archivo físico no existe.");

            var fileBytes = documento.ArchivoCifrado;*/
            var fileBytes = documento.ArchivoCifrado;

            if (fileBytes == null || fileBytes.Length == 0)
                return BadRequest("El documento no tiene datos cifrados (posiblemente fue subido antes del cambio).");

            try
            {
                var accion = $"Descarga E2EE del archivo '{documento.NombreArchivo}'";
                await _bitacoraLN.RegistrarBitacoraAsync(usuarioId, "T_DocumentosCita", accion, documento.Id);
            }
            catch { }

            // Devolver datos necesarios al cliente para descifrar
            return Json(new
            {
                DocumentoId = documento.Id,
                NombreArchivo = documento.NombreArchivo,
                ContentType = documento.ContentType ?? "application/octet-stream",
                IV = documento.IV,
                EncryptedKeyBase64 = keyData.EncryptedKeyBase64,
                archivoBase64 = Convert.ToBase64String(documento.ArchivoCifrado),
            });
        }

        /* [HttpPost]
         [Authorize(Roles = "Abogado,Gestor")]
         public async Task<IActionResult> Subir(int idCita, IFormFile archivo)
         {
             if (archivo == null || archivo.Length == 0)
                 return Json(new { success = false, message = "Debes seleccionar un archivo." });

             try
             {
                 var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "citas", idCita.ToString());
                 if (!Directory.Exists(carpeta))
                     Directory.CreateDirectory(carpeta);

                 var nombreArchivo = Path.GetFileName(archivo.FileName);
                 var rutaArchivo = Path.Combine(carpeta, nombreArchivo);

                 using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                     await archivo.CopyToAsync(stream);

                 await _documentosLN.SubirDocumentoAsync(idCita, nombreArchivo, $"/uploads/citas/{idCita}/{nombreArchivo}");

                 var usuario = User.Identity?.Name ?? "Desconocido";
                 var nombreCorto = ObtenerNombreCorto(nombreArchivo);
                 var accion = $"Se adjuntó el documento '{nombreCorto}' a la cita {idCita}";
                 await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_DocumentosCita", accion, idCita);

                 return Json(new { success = true, message = "Documento subido correctamente." });
             }
             catch (Exception ex)
             {
                 var mensaje = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                 return Json(new { success = false, message = $"Hubo un error al subir el archivo: {mensaje}" });
             }
         }*/

        [HttpPost]
        [Authorize(Roles = "Abogado,Gestor")]
        public async Task<IActionResult> ActualizarPermisoDescarga([FromBody] DocumentosCitaDTO dto)
        {
            if (dto == null || dto.Id <= 0)
                return BadRequest("Datos inválidos.");

            try
            {
                var documento = await _documentosLN.ObtenerPorIdAsync(dto.Id);
                if (documento == null)
                    return NotFound("Documento no encontrado.");

                await _documentosLN.ActualizarPermisoDescargaAsync(dto.Id, dto.Descargar);

                var usuario = User.Identity?.Name ?? "Desconocido";
                var nombreCorto = ObtenerNombreCorto(documento.NombreArchivo);
                var accion = dto.Descargar
                    ? $"Otorgó permiso de descarga al documento '{nombreCorto}'"
                    : $"Revocó permiso de descarga al documento '{nombreCorto}'";

                await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_DocumentosCita", accion, dto.Id);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error actualizando permiso: {ex.Message}");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Abogado,Cliente")]
        public async Task<IActionResult> Descargar(int id)
        {
            var documento = await _documentosLN.ObtenerPorIdAsync(id);
            if (documento == null)
                return NotFound("Documento no encontrado.");

            if (!documento.Descargar)
                return Forbid("No tienes permiso para descargar este documento.");

            var rutaFisica = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", documento.RutaArchivo.TrimStart('/'));
            if (!System.IO.File.Exists(rutaFisica))
                return NotFound("El archivo no existe o la ruta es incorrecta.");

            var usuario = User.Identity?.Name ?? "Desconocido";
            var nombreCorto = ObtenerNombreCorto(documento.NombreArchivo ?? "documento.pdf");
            var accion = $"Descargó el documento '{nombreCorto}'";

            try
            {

                await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_DocumentosCita", accion, documento.Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error al registrar bitácora: {ex.Message}");
            }

            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, proxy-revalidate");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");


            var stream = new FileStream(rutaFisica, FileMode.Open, FileAccess.Read);
            return File(stream, "application/octet-stream", documento.NombreArchivo);
        }

        [Authorize(Roles = "Abogado,Gestor")]
        [HttpPost]
        public async Task<IActionResult> ActualizarPermisoDescargaBatch([FromBody] List<DocumentosCitaDTO> documentos)
        {
            if (documentos == null || !documentos.Any())
                return BadRequest("Sin datos para actualizar.");

            var usuario = User.Identity?.Name ?? "Desconocido";

            foreach (var dto in documentos)
            {
                var doc = await _documentosLN.ObtenerPorIdAsync(dto.Id);
                if (doc != null && doc.Descargar != dto.Descargar)
                {
                    await _documentosLN.ActualizarPermisoDescargaAsync(dto.Id, dto.Descargar);

                    var nombreCorto = ObtenerNombreCorto(doc.NombreArchivo);
                    var accion = dto.Descargar
                        ? $"Otorgó permiso de descarga al documento '{nombreCorto}'"
                        : $"Revocó permiso de descarga al documento '{nombreCorto}'";

                    await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_DocumentosCita", accion, dto.Id);
                }
            }

            return Json(new { success = true, redirectUrl = "/Citas/Calendar" });
        }

        [Authorize(Roles = "Abogado,Gestor")]
        [HttpPost]
        public async Task<IActionResult> Deshabilitar(int id)
        {
            try
            {
                var documento = await _documentosLN.ObtenerPorIdAsync(id);
                if (documento == null)
                    return Json(new { success = false, message = "Documento no encontrado." });

                bool cambioActivo = documento.Activo;
                await _documentosLN.DeshabilitarAsync(id);

                if (cambioActivo)
                {
                    var usuario = User.Identity?.Name ?? "Desconocido";
                    var nombreCorto = ObtenerNombreCorto(documento.NombreArchivo);
                    await _bitacoraLN.RegistrarBitacoraAsync(
                        usuario,
                        "T_DocumentosCita",
                        $"Documento '{nombreCorto}' deshabilitado",
                        id
                    );
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = "Abogado")]
        [HttpPost]
        public async Task<IActionResult> ActualizarPermisosBatch([FromBody] List<DocumentosCitaDTO> documentos)
        {
            try
            {
                if (documentos == null || !documentos.Any())
                    return Json(new { success = false, message = "Lista vacía." });

                var usuario = User.Identity?.Name ?? "Desconocido";

                foreach (var dto in documentos)
                {
                    var doc = await _documentosLN.ObtenerPorIdAsync(dto.Id);
                    if (doc == null) continue;

                    bool cambioDescarga = doc.Descargar != dto.Descargar;
                    bool cambioActivo = doc.Activo != dto.Activo;

                    doc.Descargar = dto.Descargar;
                    doc.Activo = dto.Activo;

                    var nombreCorto = ObtenerNombreCorto(doc.NombreArchivo);

                    if (cambioDescarga)
                    {
                        var accion = dto.Descargar
                            ? $"Otorgó permiso de descarga al documento '{nombreCorto}'"
                            : $"Revocó permiso de descarga al documento '{nombreCorto}'";

                        await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_DocumentosCita", accion, doc.Id);
                    }

                    if (cambioActivo)
                    {
                        var accion = dto.Activo
                            ? $"Documento '{nombreCorto}' habilitado"
                            : $"Documento '{nombreCorto}' deshabilitado";

                        await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_DocumentosCita", accion, doc.Id);
                    }
                }

                await _documentosLN.ActualizarBatchAsync(documentos);

                return Json(new { success = true });

            }
            catch (Exception ex)
            {

                return Json(new { success = false, error = ex.Message, detalle = ex.StackTrace });
            }
        }


    }
}
