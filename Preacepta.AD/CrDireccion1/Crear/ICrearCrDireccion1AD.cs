using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.CrDireccion1.Crear
{
    public interface ICrearCrDireccion1AD
    {
        Task<int> crearProvincia(TCrProvincia crear);
        Task<int> crearCanton(TCrCantone crear);
        Task<int> crearDistrito(TCrDistrito crear);
    }
}
