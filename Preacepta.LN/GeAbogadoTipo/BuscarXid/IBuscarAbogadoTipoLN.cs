using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeAbogadoTipo.BuscarXid
{
    public interface IBuscarAbogadoTipoLN
    {
        Task<GeAbogadoTipoDTO?> buscar(int id);
    }
}
