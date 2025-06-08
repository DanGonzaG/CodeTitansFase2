using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeRedesSociales.BuscarXid
{
    public interface IBuscarRedesSocialesLN
    {
        Task<GeRedesSocialeDTO?> buscar(int id);
    }
}
