using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GePersona.BuscarXid
{
    public interface IBuscarXidGePersonaAD
    {
        Task<TGePersona?> buscar(int id);

        Task<TGePersona?> buscarXcorreo(string correo);

        Task<TGePersona?> buscarXtelefono1(string telefono);

        Task<TGePersona?> buscarXtelefono2(string telefono);
    }
}
