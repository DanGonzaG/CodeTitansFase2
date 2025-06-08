using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.CasosTipo.BuscarXid
{
    public interface IBuscarCasosTiposAD
    {
        Task<TCasosTipo?> buscar(int id);
    }
}
