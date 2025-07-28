using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GeNegocio.Editar
{
    public interface IEditarNegocioAD
    {
        Task<int> Editar(TGeNegocio editar);
    }
}
