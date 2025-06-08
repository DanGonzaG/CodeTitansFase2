using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.CrDireccion1.Editar
{
    public interface IEditarCrDireccion1AD
    {
        Task<int> EditarProvincia(TCrProvincia editar);
        Task<int> EditarCanton(TCrCantone editar);
        Task<int> EditarDistrito(TCrDistrito editar);
    }
}
