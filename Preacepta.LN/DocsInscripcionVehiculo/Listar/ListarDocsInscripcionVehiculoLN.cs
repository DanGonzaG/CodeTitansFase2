using Preacepta.AD.CasosTipo.Listar;
using Preacepta.AD.DocsInscripcionVehiculo.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsInscripcionVehiculo.Listar
{
    public class ListarDocsInscripcionVehiculoLN : IListarDocsInscripcionVehiculoLN
    {
        private readonly IListarDocsInscripcionVehiculoAD _listar;

        public ListarDocsInscripcionVehiculoLN(IListarDocsInscripcionVehiculoAD listar)
        {
            _listar = listar;
        }

        public async Task<List<DocsInscripcionVehiculoDTO>> listar()
        {
            List<DocsInscripcionVehiculoDTO> lista = await _listar.listar();
            return lista;
        }
    }
}
