using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.CasosEvidencia.BuscarXid
{
    public interface IBuscarCasosEvidenciaAD
    {
        Task<TCasosEvidencia?> buscar(int id);
    }
}
