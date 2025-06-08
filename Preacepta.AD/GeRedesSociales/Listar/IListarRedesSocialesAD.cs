using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.AD.GeRedesSociales.Listar
{
    public interface IListarRedesSocialesAD
    {
        Task<List<GeRedesSocialeDTO>> listar();
    }
}
