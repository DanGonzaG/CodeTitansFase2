using Preacepta.AD.CasosEtapa.Listar;
using Preacepta.AD.CasosTipo.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CasosEtapa.Listar
{
    public class ListarCasosEtapasLN : IListarCasosEtapasLN
    {
        private readonly IListarCasosEtapasAD _listar;

        public ListarCasosEtapasLN(IListarCasosEtapasAD listar)
        {
            _listar = listar;
        }

        public async Task<List<CasosEtapaDTO>> listar()
        {
            List<CasosEtapaDTO> lista = await _listar.listar();
            return lista;
        }
    }
}
