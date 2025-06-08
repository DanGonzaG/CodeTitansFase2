using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeAbogadoTipo.Listar
{
    public interface IListarAbogadoTipoLN
    {
        Task<List<GeAbogadoTipoDTO>> listar();
    }
}
