using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GeAbogado.BuscarXid
{
    public interface IBuscarAbogadoAD
    {
        Task<TGeAbogado?> buscar(int id);
        Task<TGeAbogado?> buscarXcarnet(int carnet);
    }
}
