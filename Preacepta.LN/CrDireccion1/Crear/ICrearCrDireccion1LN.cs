using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CrDireccion1.Crear
{
    public interface ICrearCrDireccion1LN
    {
        Task<int> CrearProvincia(CrProvinciaDTO crear);
        Task<int> CrearCanton(CrCantonDTO crear);
        Task<int> CrearDistrito(CrDistritoDTO crear);
    }
}
