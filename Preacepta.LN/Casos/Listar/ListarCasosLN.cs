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

        public async Task<List<CasoDTO>> listarXabogado(int cedula)
        {
            List<CasoDTO> lista = await _listar.listarXabogado(cedula);
            return lista;
        }

        public async Task<List<CasoDTO>> listarXcliente(int cedula)
        {
            List<CasoDTO> lista = await _listar.listarXcliente(cedula);
            return lista;
        }

        public async Task<CasoDTO> listarXultimaFecha(int cedula)
        {
            CasoDTO lista = await _listar.listarXultimaFecha(cedula);
            return lista;
        }
    }
}
