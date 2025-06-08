using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeAbogadoTipo.Editar
{
    public interface IEditarAbogadoTipoLN
    {
        Task<int> editar(GeAbogadoTipoDTO geAbogadoTipoDTO);
    }
}
