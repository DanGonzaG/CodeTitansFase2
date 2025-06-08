using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.Casos.BuscarXid
{
    public interface IBuscarCasosAD
    {
        Task<TCaso?> buscar(int id);
    }
}
