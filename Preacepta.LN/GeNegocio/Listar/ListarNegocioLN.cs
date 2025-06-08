using Preacepta.AD.GeNegocio.Listar;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeNegocio.Listar
{
    public class ListarNegocioLN : IListarNegocioLN
    {
        private readonly IListarNegocioAD _listar;

        public ListarNegocioLN(IListarNegocioAD listar)
        {
            _listar = listar;
        }

        public async Task<List<GeNegocioDTO>> listar()
        {
            List<GeNegocioDTO> lista = await _listar.listar();
            return lista;
        }
    }
}
