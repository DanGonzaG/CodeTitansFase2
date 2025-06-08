using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosEtapa.Listar
{
    public interface IListarCasosEtapasLN
    {
        Task<List<CasosEtapaDTO>> listar();
        Task<List<CasosEtapaDTO>> listarXcaso(int id);
    }
}
