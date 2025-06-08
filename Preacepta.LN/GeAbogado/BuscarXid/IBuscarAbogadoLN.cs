using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeAbogado.BuscarXid
{
    public interface IBuscarAbogadoLN
    {
        Task<GeAbogadoDTO?> buscar(int id);
        Task<GeAbogadoDTO?> buscarXcarnet(int carnet);
    }
}
