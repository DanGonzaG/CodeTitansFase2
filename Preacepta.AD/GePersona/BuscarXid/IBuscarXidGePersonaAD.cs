using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GePersona.BuscarXid
{
    public interface IBuscarXidGePersonaAD
    {
        Task<TGePersona?> buscar(int id);

        Task<TGePersona?> buscarXcorreo(string correo);
    }
}
