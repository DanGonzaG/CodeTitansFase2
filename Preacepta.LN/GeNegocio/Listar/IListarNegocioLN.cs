using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeNegocio.Listar
{
    public interface IListarNegocioLN
    {
        Task<List<GeNegocioDTO>> listar();
    }
}
