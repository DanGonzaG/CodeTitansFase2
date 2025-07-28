namespace Preacepta.UI.Services.MensajesPersonalizados
{
    public interface IValidacionesResetPassword
    {
        ListaDeErrores Ejecutar(string email, string password, string token);
    }
}
