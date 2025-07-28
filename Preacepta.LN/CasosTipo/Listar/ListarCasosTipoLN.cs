using Preacepta.AD.CasosTipo.Listar;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosTipo.Listar
{
    public class ListarCasosTipoLN : IListarCasosTipoLN
    {
        private readonly IListarCasosTipoAD _listar;

        public ListarCasosTipoLN(IListarCasosTipoAD listar)
        {
            _listar = listar;
        }

        public async Task<List<CasosTipoDTO>> listar()
        {
            List<CasosTipoDTO> lista = await _listar.listar();
            return lista;
        }
    }
}
