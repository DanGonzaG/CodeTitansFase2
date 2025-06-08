using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosTipo.BuscarXid
{
    public interface IBuscarCasosTiposLN
    {
        Task<CasosTipoDTO?> buscar(int id);
    }
}
