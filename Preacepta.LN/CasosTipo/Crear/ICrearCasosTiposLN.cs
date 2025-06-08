using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosTipo.Crear
{
    public interface ICrearCasosTiposLN
    {
        Task<int> Crear(CasosTipoDTO crear);
    }
}
