using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CrDireccion1.Listar
{
    public interface IListarCrDireccion1LN
    {
        Task<List<CrProvinciaDTO>> listarProvincias();
        Task<List<CrCantonDTO>> listarCantones();
        Task<List<CrDistritoDTO>> listarDistritos();

    }
}
