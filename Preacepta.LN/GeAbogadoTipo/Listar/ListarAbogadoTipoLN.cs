using Preacepta.AD.GeAbogadoTipo.Listar;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeAbogadoTipo.Listar
{
    public class ListarAbogadoTipoLN : IListarAbogadoTipoLN
    {

        private readonly IListarAbogadoTipoAD _listar;

        public ListarAbogadoTipoLN(IListarAbogadoTipoAD listar)
        {
            _listar = listar;
        }

        public async Task<List<GeAbogadoTipoDTO>> listar()
        {
            List<GeAbogadoTipoDTO> lista = await _listar.listar();
            return lista;
        }
    }

}

