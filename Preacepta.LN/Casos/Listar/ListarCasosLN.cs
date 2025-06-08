using Preacepta.AD.Casos.Listar;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.Casos.Listar
{
    public class ListarCasosLN : IListarCasosLN
    {
        private readonly IListarCasosAD _listar;

        public ListarCasosLN(IListarCasosAD listar)
        {
            _listar = listar;
        }

        public async Task<List<CasoDTO>> listar()
        {
            List<CasoDTO> lista = await _listar.listar();
            return lista;
        }
    }
}
