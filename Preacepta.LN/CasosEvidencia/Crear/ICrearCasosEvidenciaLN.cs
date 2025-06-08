using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosEvidencia.Crear
{
    public interface ICrearCasosEvidenciaLN
    {
        Task<int> Crear(CasosEvidenciaDTO crear);
    }
}
