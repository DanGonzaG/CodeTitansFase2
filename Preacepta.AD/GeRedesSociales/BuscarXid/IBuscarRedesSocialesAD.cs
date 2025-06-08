using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GeRedesSociales.BuscarXid
{
    public interface IBuscarRedesSocialesAD
    {
        Task<TGeRedesSociale?> buscar(int id);
    }
}
