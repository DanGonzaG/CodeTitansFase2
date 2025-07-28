using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CrDireccion1.BuscarXid
{
    public interface IBuscarCrDireccion1LN
    {
        Task<CrProvinciaDTO?> buscarProvincia(int id);
        Task<CrCantonDTO?> buscarCanton(int id);
        Task<CrDistritoDTO?> buscarDistrito(int id);
    }
}
