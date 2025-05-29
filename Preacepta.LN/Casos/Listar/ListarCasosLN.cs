using Preacepta.AD.Casos.Listar;
using Preacepta.AD.CasosTipo.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
