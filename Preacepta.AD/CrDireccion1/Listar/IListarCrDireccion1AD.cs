using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.AD.CrDireccion1.Listar
{
    public interface IListarCrDireccion1AD
    {
        Task<List<CrProvinciaDTO>> listarProvincias();
        Task<List<CrCantonDTO>> listarCantones();
        Task<List<CrDistritoDTO>> listarDistritos();
    }
}
