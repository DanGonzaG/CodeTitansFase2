using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GePersona.BuscarXid
{
    public interface IBuscarXidGePersonaLN
    {
        Task<GePersonaDTO?> buscar(int id);
        Task<GePersonaDTO?> buscarXcorreo(string correo);
    }
}
