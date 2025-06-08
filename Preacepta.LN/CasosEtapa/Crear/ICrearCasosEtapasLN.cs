using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosEtapa.Crear
{
    public interface ICrearCasosEtapasLN
    {
        Task<int> Crear(CasosEtapaDTO crear);
    }
}
