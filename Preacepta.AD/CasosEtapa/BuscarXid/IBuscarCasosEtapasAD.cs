using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.CasosEtapa.BuscarXid
{
    public interface IBuscarCasosEtapasAD
    {
        Task<TCasosEtapa?> buscar(int id);
    }
}
