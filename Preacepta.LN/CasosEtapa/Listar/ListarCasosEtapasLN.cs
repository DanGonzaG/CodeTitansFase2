using Preacepta.AD.CasosEtapa.Listar;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosEtapa.Listar
{
    public class ListarCasosEtapasLN : IListarCasosEtapasLN
    {
        private readonly IListarCasosEtapasAD _listar;

        public ListarCasosEtapasLN(IListarCasosEtapasAD listar)
        {
            _listar = listar;
        }

        public async Task<List<CasosEtapaDTO>> listar()
        {
            List<CasosEtapaDTO> lista = await _listar.listar();
            return lista;
        }

        public async Task<List<CasosEtapaDTO>> listarXcaso(int id)
        {
            List<CasosEtapaDTO> lista = await _listar.listarXcaso(id);
            return lista;
        }


    }
}
