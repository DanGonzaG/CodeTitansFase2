using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GeNegocio.Crear
{
    public interface ICrearNegocioAD
    {
        Task<int> crear(TGeNegocio crear);
    }
}
