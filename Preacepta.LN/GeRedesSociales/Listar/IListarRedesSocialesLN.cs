using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeRedesSociales.Listar
{
    public interface IListarRedesSocialesLN
    {
        Task<List<GeRedesSocialeDTO>> listar();
    }
}
