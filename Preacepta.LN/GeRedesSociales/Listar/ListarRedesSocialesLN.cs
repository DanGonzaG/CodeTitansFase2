using Preacepta.AD.GeRedesSociales.Listar;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeRedesSociales.Listar
{
    public class ListarRedesSocialesLN : IListarRedesSocialesLN
    {
        private readonly IListarRedesSocialesAD _listar;

        public ListarRedesSocialesLN(IListarRedesSocialesAD listar)
        {
            _listar = listar;
        }

        public async Task<List<GeRedesSocialeDTO>> listar()
        {
            List<GeRedesSocialeDTO> lista = await _listar.listar();
            return lista;
        }
    }
}
