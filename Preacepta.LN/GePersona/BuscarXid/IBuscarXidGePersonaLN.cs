using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GePersona.BuscarXid
{
    public interface IBuscarXidGePersonaLN
    {
        Task<GePersonaDTO?> buscar(int id);
        Task<GePersonaDTO?> buscarXnumCedula(string id);
        Task<bool?> buscarXnumCedulaBOOLEAN(string id);
        Task<GePersonaDTO?> buscarXcorreo(string correo);
        Task<GePersonaDTO?> buscarXtelefono1(string telefono);
        Task<GePersonaDTO?> buscarXtelefono2(string telefono);
    }
}
