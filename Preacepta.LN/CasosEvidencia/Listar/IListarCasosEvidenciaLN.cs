using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosEvidencia.Listar
{
    public interface IListarCasosEvidenciaLN
    {
        Task<List<CasosEvidenciaDTO>> listar();
    }
}
