using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.Casos.Editar
{
    public interface IEditarCasosAD
    {
        Task<int> Editar(TCaso editar);
    }
}
