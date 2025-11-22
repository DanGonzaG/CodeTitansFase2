using Preacepta.LN.GePersona.BuscarXid;

namespace Preacepta.UI.Extensions
{
    public static class CedulaExtensions
    {       
        public static string LimpiarCedula (this string cedula) 
        {
            if (string.IsNullOrWhiteSpace(cedula))
                throw new ArgumentException("La cédula no puede estar vacía.");

            if(cedula.Equals("Sin número de identificación")) 
            {
                string numero = Guid.NewGuid().ToString();                
                cedula = numero;
            }

            return cedula.Replace("-", "");
        }
    }
}
