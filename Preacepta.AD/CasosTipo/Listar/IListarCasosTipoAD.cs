using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.AD.CasosTipo.Listar
{
    public interface IListarCasosTipoAD
    {
        Task<List<CasosTipoDTO>> listar();
    }
}
