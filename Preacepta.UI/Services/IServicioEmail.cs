namespace Preacepta.UI.Services
{
    public interface IServicioEmail
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
        Task BuzonPreacepta(string email, string subject, string htmlMessage);
    }
}
