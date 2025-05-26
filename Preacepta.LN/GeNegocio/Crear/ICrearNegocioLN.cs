using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeNegocio.Crear
{
    public interface ICrearNegocioLN
    {
        Task<int> Crear(GeNegocioDTO crear);
    }
}
