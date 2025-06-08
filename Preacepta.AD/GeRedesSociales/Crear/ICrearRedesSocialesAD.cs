using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GeRedesSociales.Crear
{
    public interface ICrearRedesSocialesAD
    {
        Task<int> crear(TGeRedesSociale crear);
    }
}
