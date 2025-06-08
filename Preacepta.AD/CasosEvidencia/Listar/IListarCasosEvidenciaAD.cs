using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.AD.CasosEvidencia.Listar
{
    public interface IListarCasosEvidenciaAD
    {
        Task<List<CasosEvidenciaDTO>> listar();
    }
}
