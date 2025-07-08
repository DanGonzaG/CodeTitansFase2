using Microsoft.AspNetCore.Mvc;
using Preacepta.LN.Videollamada;
using System;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Linq;

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

                // Enviar correos (puedes usar SMTP, SendGrid, etc.)
                var participantes = request.Participantes?.Split(',').Select(c => c.Trim()).ToList();

                await EnviarCorreosManual(participantes, url, request.Tema, request.FechaInicio);

                return Json(new { success = true, url });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

    private async Task EnviarCorreosManual(List<string> correos, string url, string tema, DateTime fecha)
    {
        var smtp = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("valeria2024.43@gmail.com", "rkvd tmlh txrh attg"),
            EnableSsl = true
        };

        foreach (var correo in correos)
        {
            var mail = new MailMessage("tucorreo@gmail.com", correo)
            {
                Subject = $"Invitación a reunión: {tema}",
                Body = $"Hola, estás invitado a una reunión el {fecha:G}\n\nEnlace de Zoom: {url}"
            };

            await smtp.SendMailAsync(mail);
        }
    }
}
    // Clase para recibir los datos del POST JSON
    public class ReunionesRequest
    {
        public DateTime FechaInicio { get; set; }
        public int Duracion { get; set; }
        public string Tema { get; set; }
        public string Participantes { get; set; }
    }
}