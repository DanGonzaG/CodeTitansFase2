using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Preacepta.AD;
using Preacepta.LN.DocumentosCita;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.Web.Controllers
{
    public class DocumentosCitaController : Controller
    {
        private readonly IDocumentosCitaLN _documentosLN;

        public DocumentosCitaController(IDocumentosCitaLN documentosLN)
        {
            _documentosLN = documentosLN;
        }

        // GET: Documentos de una cita
        public IActionResult Listar(int idCita)
        {
            var documentos = _documentosLN.ObtenerPorCita(idCita);
            ViewBag.IdCita = idCita;
            return View(documentos);
        }

        // POST: Subir archivo
        [HttpPost]
        public IActionResult Subir(int idCita, IFormFile archivo)
        {
            if (archivo != null && archivo.Length > 0)
            {
                _documentosLN.SubirArchivo(idCita, archivo);
            }

            return RedirectToAction("Listar", new { idCita });
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarPermisoDescarga(int idDocumento, bool? permitirDescarga)
        {
            try
            {
                bool permiso = permitirDescarga ?? false;
                await _documentosLN.ActualizarPermisoDescargaAsync(idDocumento, permiso);
                // Necesitarás saber a qué cita pertenece para redirigir:
                var documento = await _documentosLN.ObtenerPorIdAsync(idDocumento);
                return RedirectToAction("Listar", new { idCita = documento.IdCita });
            }
            catch
            {
                return BadRequest("Error actualizando permiso");
            }
        }
    }
}