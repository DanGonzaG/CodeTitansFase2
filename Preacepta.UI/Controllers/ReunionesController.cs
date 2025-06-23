using Microsoft.AspNetCore.Mvc;
using Preacepta.LN.Videollamada;
using System;
using System.Threading.Tasks;

namespace Preacepta.UI.Controllers
{
    public class ReunionesController : Controller
    {
        [HttpGet]
        public IActionResult Crear()
        {
            return PartialView("~/Views/Reuniones/_ProgramarReunionModal.cshtml");
        }

        // Acción que recibe la petición AJAX para crear la reunión
        [HttpPost]
        public async Task<IActionResult> CrearReunion([FromBody] ReunionesRequest request)
        {
            try
            {
                var auth = new ZoomAuthService();
                var token = await auth.ObtenerAccessTokenAsync();

                var servicio = new ZoomMeetingService();
                var url = await servicio.CrearReunionProgramadaAsync(token, request.FechaInicio, request.Duracion, request.Tema);

                return Json(new { success = true, url });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
    }

    // Clase para recibir los datos del POST JSON
    public class ReunionesRequest
    {
        public DateTime FechaInicio { get; set; }
        public int Duracion { get; set; }
        public string Tema { get; set; }
    }
}
