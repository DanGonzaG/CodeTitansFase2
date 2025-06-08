using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.CasosEvidencia.Crear
{
    public interface ICrearCasosEvidenciaAD
    {
        Task<int> crear(TCasosEvidencia crear);
    }
}
