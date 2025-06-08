using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeAbogado.Listar
{
    public interface IListarAbogadoLN
    {
        Task<List<GeAbogadoDTO>> listar();
    }
}
