using Preacepta.AD.GePersona.Listar;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GePersona.Listar
{
    public class ListarGePersonaLN : IListarGePersonaLN
    {
        private readonly IListarGePersonaAD _listarGePersonaAD;

        public ListarGePersonaLN(IListarGePersonaAD listarGePersonaAD)
        {
            _listarGePersonaAD = listarGePersonaAD;
        }

        public async Task<List<GePersonaDTO>> listar()
        {
            List<GePersonaDTO> lista = await _listarGePersonaAD.listar();
            return lista;
        }
    }
}
