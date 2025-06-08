using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.AD.GeAbogadoTipo.Listar
{
    public interface IListarAbogadoTipoAD
    {
        Task<List<GeAbogadoTipoDTO>> listar();
    }
}
