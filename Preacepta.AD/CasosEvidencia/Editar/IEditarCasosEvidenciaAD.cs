using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.CasosEvidencia.Editar
{
    public interface IEditarCasosEvidenciaAD
    {
        Task<int> Editar(TCasosEvidencia editar);
    }
}
