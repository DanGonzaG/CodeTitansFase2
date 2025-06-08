using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.CrDireccion1.BuscarXid
{
    public interface IBuscarCrDireccion1AD
    {
        Task<TCrProvincia?> buscarProvincias(int id);
        Task<TCrCantone?> buscarCantones(int id);
        Task<TCrDistrito?> buscarDistritos(int id);
    }
}
