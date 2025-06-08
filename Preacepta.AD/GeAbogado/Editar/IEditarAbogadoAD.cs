using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GeAbogado.Editar
{
    public interface IEditarAbogadoAD
    {
        Task<int> Editar(TGeAbogado editar);
    }
}
