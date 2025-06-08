using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.Casos.Crear
{
    public interface ICrearCasosLN
    {
        Task<int> Crear(CasoDTO crear);
    }
}
