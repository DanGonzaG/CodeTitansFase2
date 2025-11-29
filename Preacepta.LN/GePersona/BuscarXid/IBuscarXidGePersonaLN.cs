using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GePersona.BuscarXid
{
    public interface IBuscarXidGePersonaLN
    {
        //Metodos para retornar objetos completos
        Task<GePersonaDTO?> buscar(int id);
        Task<GePersonaDTO?> buscarXnumCedula(string id);
        Task<GePersonaDTO?> buscarXcorreo(string correo);
        Task<GePersonaDTO?> buscarXtelefono1(string telefono);
        Task<GePersonaDTO?> buscarXtelefono2(string telefono);

        //Metodo Booleanos para corroborar existencia del objeto

        Task<bool?> buscarXnumCedulaBOOLEAN(string id);
        Task<bool?> buscarXcorreoBOOLEAN(string id);
        Task<bool?> buscarXtelefono1BOOLEAN(string id);
        Task<bool?> buscarXtelefono2BOOLEAN(string id);
    }
}
