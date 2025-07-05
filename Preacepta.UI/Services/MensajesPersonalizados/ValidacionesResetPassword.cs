namespace Preacepta.UI.Services.MensajesPersonalizados
{
    public class ValidacionesResetPassword : IValidacionesResetPassword
    {
        public ListaDeErrores Ejecutar(string email, string password, string token)
        {
            var resultado = new ListaDeErrores();

            if (string.IsNullOrWhiteSpace(token))
            {
                resultado.Errores.Add(new MensajesErrorPersonalizados
                {
                    //Codigo = "TokenInvalido",
                    Codigo = "InvalidToken",
                    Descripcion = "El token es inválido o ha expirado."
                });
            }

            if (password.Length < 6)
            {
                resultado.Errores.Add(new MensajesErrorPersonalizados
                {
                    Codigo = "PasswordCorta",
                    Descripcion = "La contraseña debe tener al menos 6 caracteres."
                });
            }

            resultado.Exitoso = resultado.Errores.Count == 0;
            return resultado;
        }

    }
}
