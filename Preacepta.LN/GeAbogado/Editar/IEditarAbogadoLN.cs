using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeAbogado.Editar
{
    public interface IEditarAbogadoLN
    {
        Task<int> Editar(GeAbogadoDTO editar);
    }
}
