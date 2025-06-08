using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeNegocio.Editar
{
    public interface IEditarNegocioLN
    {
        Task<int> Editar(GeNegocioDTO editar);
    }
}
