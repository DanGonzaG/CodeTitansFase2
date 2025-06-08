using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeRedesSociales.Editar
{
    public interface IEditarRedesSocialesLN
    {
        Task<int> Editar(GeRedesSocialeDTO editar);
    }
}
