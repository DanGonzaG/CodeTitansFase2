using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.AD.CasosEtapa.Listar
{
    public interface IListarCasosEtapasAD
    {
        Task<List<CasosEtapaDTO>> listar();
        Task<List<CasosEtapaDTO>> listarXcaso(int id);
    }
}
