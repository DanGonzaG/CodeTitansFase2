using Preacepta.AD.CasosTipo.Listar;
using Preacepta.AD.GeRedesSociales.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
