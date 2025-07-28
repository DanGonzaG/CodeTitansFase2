using Humanizer;
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
        public IActionResult Listar(int idCita)
        {
            var documentos = _documentosLN.ObtenerPorCita(idCita);
            ViewBag.IdCita = idCita;
            return PartialView("_ListarPartial", documentos);
        }

        [HttpPost]
        public async Task<IActionResult> Subir(int idCita, IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                return PartialView("_ListarPartial", new List<DocumentosCitaDTO>());
            }

            Console.WriteLine($"Archivo recibido: {archivo.FileName}, Tamaño: {archivo.Length}");

            var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "citas", idCita.ToString());
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            var nombreArchivo = Path.GetFileName(archivo.FileName);
            var rutaArchivo = Path.Combine(carpeta, nombreArchivo);

            try
            {
                // Guardar el archivo en el sistema de archivos
                using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                {
                    await archivo.CopyToAsync(stream);
                }

                Console.WriteLine($"Archivo guardado en: {rutaArchivo}");

                // Crear el objeto DocumentoCita y guardarlo en la base de datos
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
                var documentos = _documentosLN.ObtenerPorCita(idCita);

                return RedirectToAction("Calendar", "Citas");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al subir el archivo: {ex.Message}");
                return Json(new { success = false, message = $"Hubo un error al subir el archivo: {ex.Message}" });
            }
        }



        [HttpPost]
        public async Task<IActionResult> ActualizarPermisoDescarga([FromBody] DocumentosCitaDTO dto)
        {
            try
            {
                // Verifica que el DTO llegue correctamente
                if (dto == null)
                {
                    return BadRequest("DTO vacío.");
                }

                // Validar que el Id sea válido
                if (dto.Id <= 0)
                {
                    return BadRequest("Id inválido.");
                }

                // Realizar la actualización
                await _documentosLN.ActualizarPermisoDescargaAsync(dto.Id, dto.Descargar);

                // Obtener el documento actualizado
                var documento = await _documentosLN.ObtenerPorIdAsync(dto.Id);

                if (documento == null)
                {
                    return NotFound("Documento no encontrado.");
                }

                // Responder con la ID de la cita
                // Asegúrate de que la respuesta sea JSON en caso de éxito
                return Json(new { success = true });

            }
            catch (Exception ex)
            {
                // Capturar cualquier error
                Console.WriteLine($"Error al actualizar permiso de descarga: {ex.Message}");
                return BadRequest("Error actualizando permiso");
            }
        }


        [HttpGet]
        public async Task<IActionResult> Descargar(int id)
        {
            // Obtener el documento por su Id
            var documento = await _documentosLN.ObtenerPorIdAsync(id);
            if (documento == null)
            {
                return NotFound("Documento no encontrado");
            }

            // Validar si el permiso de descarga está habilitado
            if (!documento.Descargar)
            {
                return Forbid("No tienes permiso para descargar este documento.");
            }

            // Obtener la ruta del archivo desde el DTO
            var ruta = documento.RutaArchivo; // Asumo que esta es la ruta en el sistema de archivos
            if (string.IsNullOrEmpty(ruta) || !System.IO.File.Exists(ruta))
            {
                return NotFound("El archivo no existe o la ruta es incorrecta.");
            }

            // Leer el archivo
            var bytes = await System.IO.File.ReadAllBytesAsync(ruta);

            // Si quieres mostrar el nombre del archivo, podrías almacenarlo también en el DTO
            var nombreArchivo = documento.NombreArchivo ?? "archivo.pdf";

            // Establecer cabeceras para evitar caché
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, proxy-revalidate");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");

            // Devolver el archivo al usuario con un tipo MIME genérico
            return File(bytes, "application/octet-stream", nombreArchivo);
        }

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

                // Después de actualizar los permisos de descarga, redirigir a la lista de documentos de la cita
                return Json(new { success = true, redirectUrl = "/Citas/Calendar" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}
