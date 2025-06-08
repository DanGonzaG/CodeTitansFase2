using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosEvidencia.Editar
{
    public interface IEditarCasosEvidenciaLN
    {
        Task<int> Editar(CasosEvidenciaDTO editar);
    }
}
