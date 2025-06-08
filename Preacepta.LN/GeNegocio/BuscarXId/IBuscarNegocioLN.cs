using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeNegocio.BuscarXId
{
    public interface IBuscarNegocioLN
    {
        Task<GeNegocioDTO?> buscar(int id);
    }
}
