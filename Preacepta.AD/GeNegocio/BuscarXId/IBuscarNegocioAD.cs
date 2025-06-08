using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GeNegocio.BuscarXId
{
    public interface IBuscarNegocioAD
    {
        Task<TGeNegocio?> buscar(int id);
    }
}
