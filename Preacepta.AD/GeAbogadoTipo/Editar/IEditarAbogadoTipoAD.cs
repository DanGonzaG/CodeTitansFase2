using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GeAbogadoTipo.Editar
{
    public interface IEditarAbogadoTipoAD
    {
        Task<int> editar(TGeAbogadoTipo geAbogadoTipo);
    }
}
