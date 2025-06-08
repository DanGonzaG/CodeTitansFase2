using Preacepta.AD.CasosTipo.Listar;
using Preacepta.AD.DocsMarcaVehiculo.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsMarcaVehiculo.Listar
{
    public class ListarDocsMarcaVehiculoLN : IListarDocsMarcaVehiculoLN
    {
        private readonly IListarDocsMarcaVehiculoAD _listar;

        public ListarDocsMarcaVehiculoLN(IListarDocsMarcaVehiculoAD listar)
        {
            _listar = listar;
        }

        public async Task<List<DocsMarcaVehiculoDTO>> listar()
        {
            List<DocsMarcaVehiculoDTO> lista = await _listar.listar();
            return lista;
        }
    }
}
