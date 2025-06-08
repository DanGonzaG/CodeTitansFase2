using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosEtapa.BuscarXid
{
    public interface IBuscarCasosEtapasLN
    {
        Task<CasosEtapaDTO?> buscar(int id);
    }
}
