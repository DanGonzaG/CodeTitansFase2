using Preacepta.AD.CasosTipo.Listar;
using Preacepta.AD.GeNegocio.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
