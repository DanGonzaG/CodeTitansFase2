
using Preacepta.AD.Citas.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Citas.Listar

{
    public class ListarCitasLN : IListarCitasLN
    {
        private readonly IListarCitasAD _listar;

        public ListarCitasLN(IListarCitasAD listar)
        {
            _listar = listar;
        }

        public async Task<List<CitasDTO>> listar()
        {
            List<CitasDTO> lista = await _listar.listar();
            return lista;
        }
    }
}
