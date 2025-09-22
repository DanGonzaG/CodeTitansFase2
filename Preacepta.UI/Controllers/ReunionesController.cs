using Microsoft.AspNetCore.Mvc;
using Preacepta.LN.Videollamada;
using System;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Linq;
using static Preacepta.UI.Controllers.ReunionesController;
using Microsoft.AspNetCore.Authorization;

namespace Preacepta.UI.Controllers
{
    public class ReunionesController : Controller
    {
        [Authorize(Roles = "Abogado,Gestor")]
        [HttpGet]
        public IActionResult Crear()
        {
            return PartialView("~/Views/Reuniones/_ProgramarReunionModal.cshtml");
        }

        [Authorize(Roles = "Abogado,Gestor")]
        [HttpPost]
        public async Task<IActionResult> CrearReunion([FromBody] ReunionesRequest request)
        {
            try
            {
                var auth = new ZoomAuthService();
                var token = await auth.ObtenerAccessTokenAsync();

                var servicio = new ZoomMeetingService();
                var resultado = await servicio.CrearReunionProgramadaAsync(token, request.FechaInicio, request.Duracion, request.Tema);

                var url = resultado.JoinUrl;
                var meetingId = resultado.MeetingId;

                var participantes = request.Participantes?.Split(',').Select(c => c.Trim()).ToList();

                if (participantes != null)
                {
                    ReunionesStore.MeetingParticipantes[meetingId] = participantes;
                    await EnviarCorreosManual(participantes, url, request.Tema, request.FechaInicio);
                }
                return Json(new { success = true, url });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        [Authorize(Roles = "Abogado,Gestor")]
        public static class ReunionesStore
        {
            public static Dictionary<long, List<string>> MeetingParticipantes = new();
        }
        private async Task EnviarCorreosManual(List<string> correos, string url, string tema, DateTime fecha)
    {
        var smtp = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("d.gon.guerrero@gmail.com", "oiup tfoc roio sbei"),
            EnableSsl = true
        };

        foreach (var correo in correos)
        {
            var mail = new MailMessage("d.gon.guerrero@gmail.com", correo)
            {
                Subject = $"Invitación a reunión: {tema}",
                Body = $"Hola, estás invitado a una reunión el {fecha:G}\n\nEnlace de Zoom: {url}"
            };

            await smtp.SendMailAsync(mail);
        }
    }

        [Authorize(Roles = "Abogado,Gestor")]
        public class ReunionesRequest
    {
        public DateTime FechaInicio { get; set; }
        public int Duracion { get; set; }
        public string Tema { get; set; }
        public string Participantes { get; set; }
    }
}
    [Authorize(Roles = "Abogado,Gestor")]
    [ApiController]
    [Route("api/zoom/webhook")]
    public class ZoomWebhookController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RecibirWebhook([FromBody] dynamic data)
        {
            
            if (data.@event == "endpoint.url_validation")
            {
                return Ok(new
                {
                    plainToken = data.payload.plainToken,
                    encryptedToken = data.payload.encryptedToken
                });
            }

            
            if (data.@event == "meeting.ended")
            {
                string topic = data.payload.@object.topic;
                string fecha = data.payload.@object.end_time;
                long meetingId = (long)data.payload.@object.id;
                Console.WriteLine($"Webhook meeting ended - meetingId: {meetingId}");
           
                if (ReunionesStore.MeetingParticipantes.TryGetValue(meetingId, out List<string> correos))
                {
                    Console.WriteLine($"Participantes encontrados: {string.Join(", ", correos)}");
                    foreach (var correo in correos)
                    {
                        await EnviarCorreoTestimonio(correo, topic, fecha);
                        Console.WriteLine($"Correo enviado a: {correo}");
                    }
                }
                else
                {
                    Console.WriteLine("❌ No se encontraron participantes para el meeting ID: " + meetingId);
                }
            }

            return Ok();
        }

        [Authorize(Roles = "Abogado,Gestor")]
        private async Task EnviarCorreoTestimonio(string correoDestino, string tema, string fecha)
        {
            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("valeria2024.43@gmail.com", "rkvd tmlh txrh attg"),
                EnableSsl = true
            };

            var mail = new MailMessage("valeria2024.43@gmail.com", correoDestino)
            {
                Subject = $"Gracias por asistir a la reunión: {tema}",
                Body = $"Hola,\n\nGracias por tu tiempo el {fecha}.\n" +
                       $"Nos gustaría conocer tu opinión. Por favor deja tu testimonio aquí:\n\n" +
                       $"https://localhost:7065/TTestimonios/Testimonials"
            };

            await smtp.SendMailAsync(mail);
            Console.WriteLine("✅ Correo enviado correctamente a " + correoDestino);
        }
    }

}