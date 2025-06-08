using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GeAbogadoTipo.BuscarXid
{
    public interface IBuscarAbogadoTipoAD
    {
        Task<TGeAbogadoTipo?> buscar(int id);
    }
}
