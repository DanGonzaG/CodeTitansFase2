using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CrDireccion1.Editar
{
    public interface IEditarCrDireccion1LN
    {
        Task<int> EditarProvincia(CrProvinciaDTO editar);

        Task<int> EditarCanton(CrCantonDTO editar);
        Task<int> EditarDistrito(CrDistritoDTO editar);
    }
}
