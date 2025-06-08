using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosEtapa.Editar
{
    public interface IEditarCasosEtapasLN
    {
        Task<int> Editar(CasosEtapaDTO editar);
    }
}
