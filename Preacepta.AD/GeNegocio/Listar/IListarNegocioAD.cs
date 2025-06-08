using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.AD.GeNegocio.Listar
{
    public interface IListarNegocioAD
    {
        Task<List<GeNegocioDTO>> listar();
    }
}
