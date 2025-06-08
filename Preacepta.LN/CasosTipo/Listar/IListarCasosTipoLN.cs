using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosTipo.Listar
{
    public interface IListarCasosTipoLN
    {
        Task<List<CasosTipoDTO>> listar();
    }
}
