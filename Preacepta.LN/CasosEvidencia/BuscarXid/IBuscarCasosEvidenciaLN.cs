
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosEvidencia.BuscarXid
{
    public interface IBuscarCasosEvidenciaLN
    {
        Task<CasosEvidenciaDTO?> buscar(int id);
    }
}
