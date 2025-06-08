using Preacepta.AD.CasosTipo.Listar;
using Preacepta.AD.DocsCompraventaFinca.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsCompraventaFinca.Listar
{
    public class ListarDocsCompraventaFincaLN : IListarDocsCompraventaFincaLN
    {
        private readonly IListarDocsCompraventaFincaAD _listar;

        public ListarDocsCompraventaFincaLN(IListarDocsCompraventaFincaAD listar)
        {
            _listar = listar;
        }

        public async Task<List<DocsCompraventaFincaDTO>> listar()
        {
            List<DocsCompraventaFincaDTO> lista = await _listar.listar();
            return lista;
        }
    }
}
