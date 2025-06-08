
using Preacepta.AD.CitasTipo.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CitasTipo.Listar

{
    public class ListarCitasTipoLN : IListarCitasTipoLN
    {
        private readonly IListarCitasTipoAD _listar;

        public ListarCitasTipoLN(IListarCitasTipoAD listar)
        {
            _listar = listar;
        }

        public async Task<List<CitasTipoDTO>> listar()
        {
            List<CitasTipoDTO> lista = await _listar.listar();
            return lista;
        }
    }
}
