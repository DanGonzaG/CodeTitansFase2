using System.Globalization;
using System.Net;
using System.Net.Mail;

namespace Preacepta.UI.Services
{
    public class NotificadorCorreoCasos
    {
        public static async Task EnviarCorreoConclusionCaso(
            List<string> correos,
            DateTime fechaConclusion,
            string nombreCliente,
            string nombreAbogado
        )
        {
            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(
                    "d.gon.guerrero@gmail.com",
                    "oiup tfoc roio sbei"
                ),
                EnableSsl = true
            };

            var cultura = new CultureInfo("es-CR");
            string fechaCR = fechaConclusion.ToString("dddd dd 'de' MMMM 'de' yyyy 'a las' HH:mm", cultura);

            foreach (var correo in correos)
            {
                try
                {
                    var innerUrl = $"/Home/UsuarioAutenticado" +
                                   $"?correo={Uri.EscapeDataString(correo)}" +
                                   $"&redirectTo={Uri.EscapeDataString("/TTestimonios/TestimonialForm")}";

                    var loginUrl = $"https://localhost:7065/Identity/Account/Login?ReturnUrl={Uri.EscapeDataString(innerUrl)}";

                    var cuerpo =
                    $@"Saludos {nombreCliente},

                    Le informamos que su caso ha concluido el {fechaCR}.
                    Abogado a cargo: {nombreAbogado}.

                    Nos gustaría conocer su experiencia. Por favor deje su testimonio aquí:
                    {loginUrl}

                    (Debe iniciar sesión para acceder al formulario.)";

                    var mail = new MailMessage("d.gon.guerrero@gmail.com", correo)
                    {
                        Subject = "Conclusión de su caso — Déjenos su testimonio",
                        Body = cuerpo,
                        IsBodyHtml = false
                    };

                    await smtp.SendMailAsync(mail);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error enviando correo a {correo}: {ex.Message}");
                }
            }
        }
    }
}
