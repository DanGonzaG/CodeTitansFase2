using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Preacepta.AD;
using Preacepta.LN.DocumentosCita;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Preacepta.Web.Controllers
{
    public class DocumentosCitaController : Controller
    {
        private readonly IDocumentosCitaLN _documentosLN;
        private readonly Contexto _contexto;

        public DocumentosCitaController(IDocumentosCitaLN documentosLN, Contexto contexto)
        {
            _documentosLN = documentosLN;
            _contexto = contexto;
        }

        // GET: Documentos de una cita
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
            {
                return Json(new { success = false, message = "Debes seleccionar un archivo." });
            }

            try
            {
                var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "citas", idCita.ToString());
                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                var nombreArchivo = Path.GetFileName(archivo.FileName);
                var rutaArchivo = Path.Combine(carpeta, nombreArchivo);

                using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                {
                    await archivo.CopyToAsync(stream);
                }

                var documento = new TDocumentosCita
                {
                    IdCita = idCita,
                    NombreArchivo = nombreArchivo,
                    RutaArchivo = $"/uploads/citas/{idCita}/{nombreArchivo}",
                    FechaSubida = DateTime.Now,
                    Descargar = false
                };

                _contexto.TDocumentosCita.Add(documento);
                await _contexto.SaveChangesAsync();

                return Json(new { success = true, message = "Documento subido correctamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Hubo un error al subir el archivo: {ex.Message}" });
            }
        }



        [Authorize(Roles = "Abogado,Gestor")]
        [HttpPost]
        public async Task<IActionResult> ActualizarPermisoDescarga([FromBody] DocumentosCitaDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("DTO vacío.");
                }

                if (dto.Id <= 0)
                {
                    return BadRequest("Id inválido.");
                }
                await _documentosLN.ActualizarPermisoDescargaAsync(dto.Id, dto.Descargar);

                var documento = await _documentosLN.ObtenerPorIdAsync(dto.Id);

                if (documento == null)
                {
                    return NotFound("Documento no encontrado.");
                }

                return Json(new { success = true });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar permiso de descarga: {ex.Message}");
                return BadRequest("Error actualizando permiso");
            }
        }

        [Authorize(Roles = "Abogado,Gestor")]
        [HttpGet]
        public async Task<IActionResult> Descargar(int id)
        {
            var documento = await _documentosLN.ObtenerPorIdAsync(id);
            if (documento == null)
            {
                return NotFound("Documento no encontrado");
            }

            if (!documento.Descargar)
            {
                return Forbid("No tienes permiso para descargar este documento.");
            }

            var ruta = documento.RutaArchivo; 
            if (string.IsNullOrEmpty(ruta) || !System.IO.File.Exists(ruta))
            {
                return NotFound("El archivo no existe o la ruta es incorrecta.");
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(ruta);

            var nombreArchivo = documento.NombreArchivo ?? "archivo.pdf";

            // Establecer cabeceras para evitar caché
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, proxy-revalidate");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");

            return File(bytes, "application/octet-stream", nombreArchivo);
        }
        [Authorize(Roles = "Abogado,Gestor")]
        [HttpPost]
        public async Task<IActionResult> ActualizarPermisoDescargaBatch([FromBody] List<DocumentosCitaDTO> documentos)
        {
            try
            {
                if (documentos == null || !documentos.Any())
                    return BadRequest("Sin datos para actualizar.");

                foreach (var dto in documentos)
                {
                    var doc = await _contexto.TDocumentosCita.FindAsync(dto.Id);
                    if (doc != null)
                    {
                        doc.Descargar = dto.Descargar;
                    }
                }
                await _contexto.SaveChangesAsync();

                return Json(new { success = true, redirectUrl = "/Citas/Calendar" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        
        [Authorize(Roles = "Abogado,Gestor")]
        [HttpPost]
        public async Task<IActionResult> Deshabilitar(int id)
        {
            try
            {
                var deshabilitado = await _documentosLN.DeshabilitarAsync(id); // Nuevo método en tu capa lógica
                if (!deshabilitado)
                    return Json(new { success = false, message = "Documento no encontrado." });

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
                    return BadRequest("Sin datos para actualizar.");

                foreach (var dto in documentos)
                {
                    var doc = await _contexto.TDocumentosCita.FindAsync(dto.Id);
                    if (doc != null)
                    {
                        doc.Descargar = dto.Descargar;
                        doc.Activo = dto.Activo;
                    }
                }
                await _contexto.SaveChangesAsync();

                return Json(new { success = true, redirectUrl = "/Citas/Calendar" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}
