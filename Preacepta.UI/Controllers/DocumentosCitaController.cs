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

namespace Preacepta.Web.Controllers
{
    public class DocumentosCitaController : Controller
    {
        private readonly IDocumentosCitaLN _documentosLN;
        private readonly ICrearEventosLN _bitacoraLN;

        public DocumentosCitaController(IDocumentosCitaLN documentosLN, ICrearEventosLN bitacoraLN)
        {
            _documentosLN = documentosLN;
            _bitacoraLN = bitacoraLN;
        }

        // Método para obtener nombre corto de archivo (máx 50 caracteres)
        private string ObtenerNombreCorto(string nombreArchivo, int maxLength = 50)
        {
            if (string.IsNullOrEmpty(nombreArchivo)) return string.Empty;

            if (nombreArchivo.Length > maxLength)
                return nombreArchivo.Substring(0, maxLength - 3) + "...";

            return nombreArchivo;
        }

        [Authorize(Roles = "Abogado,Gestor")]
        public IActionResult Listar(int idCita)
        {
            var documentos = _documentosLN.ObtenerPorCita(idCita);
            ViewBag.IdCita = idCita;
            return PartialView("_ListarPartial", documentos);
        }

        [HttpPost]
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
        }

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
        [Authorize(Roles = "Abogado,Gestor")]
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

            var bytes = await System.IO.File.ReadAllBytesAsync(rutaFisica);
            var nombreArchivo = documento.NombreArchivo ?? "documento.pdf";

            var usuario = User.Identity?.Name ?? "Desconocido";
            var nombreCorto = ObtenerNombreCorto(nombreArchivo);
            var accion = $"Descargó el documento '{nombreCorto}'";

            await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_DocumentosCita", accion, documento.Id);


            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, proxy-revalidate");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");

            return File(bytes, "application/octet-stream", nombreArchivo);
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
            if (documentos == null || !documentos.Any())
                return BadRequest("Sin datos para actualizar.");

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

            return Json(new { success = true, redirectUrl = "/Citas/Calendar" });
        }
    }
}
