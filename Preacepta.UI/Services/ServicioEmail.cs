using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace Preacepta.UI.Services
{
    public class ServicioEmail : IServicioEmail
    {
        private readonly IConfiguration _config;

        public ServicioEmail(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync (string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_config["EmailSettings:From"]));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            using var client = new SmtpClient();
            await client.ConnectAsync(_config["EmailSettings:SmtpServer"], int.Parse(_config["EmailSettings:Port"]), true);
            await client.AuthenticateAsync(_config["EmailSettings:Username"], _config["EmailSettings:Password"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

        }

        public async Task BuzonPreacepta(string email,string subject, string htmlMessage)
        {
            

            var message = new MimeMessage();
            if (!MailboxAddress.TryParse(email, out var correoRemitente))
            {
                throw new FormatException("La dirección de correo no es valida.");
            }
            message.From.Add(MailboxAddress.Parse(email));
            message.To.Add(MailboxAddress.Parse(_config["EmailSettings:From"]));            
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            try 
            {
                using var client = new SmtpClient();
                await client.ConnectAsync(_config["EmailSettings:SmtpServer"], int.Parse(_config["EmailSettings:Port"]), SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync(_config["EmailSettings:Username"], _config["EmailSettings:Password"]);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"Error en EditarCasosTiposLN: {ex.Message}");
            }
            

        }
    }
}
