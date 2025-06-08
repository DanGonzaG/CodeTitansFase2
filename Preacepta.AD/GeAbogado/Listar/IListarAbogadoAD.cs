using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.AD.GeAbogado.Listar
{
    public interface IListarAbogadoAD
    {
        Task<List<GeAbogadoDTO>> listar();
    }
}
