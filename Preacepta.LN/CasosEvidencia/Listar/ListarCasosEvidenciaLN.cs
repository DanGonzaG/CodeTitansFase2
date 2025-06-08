using Preacepta.AD.CasosEvidencia.Listar;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosEvidencia.Listar
{
    public class ListarCasosEvidenciaLN : IListarCasosEvidenciaLN
    {
        private readonly IListarCasosEvidenciaAD _listar;

        public ListarCasosEvidenciaLN(IListarCasosEvidenciaAD listar)
        {
            _listar = listar;
        }

        public async Task<List<CasosEvidenciaDTO>> listar()
        {
            List<CasosEvidenciaDTO> lista = await _listar.listar();
            return lista;
        }
    }
}

