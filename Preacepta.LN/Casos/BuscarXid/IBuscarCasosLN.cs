using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.Casos.BuscarXid
{
    public interface IBuscarCasosLN
    {
        Task<CasoDTO?> buscar(int id);
    }
}
