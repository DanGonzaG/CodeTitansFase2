using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosTipo.Editar
{
    public interface IEditarCasosTiposLN
    {
        Task<int> Editar(CasosTipoDTO editar);
    }
}
