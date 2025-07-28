using Preacepta.AD.CasosTipo.Listar;
using Preacepta.AD.DocsCombustible.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsCombustible.Listar
{
    public class ListarDocsCombustibleLN : IListarDocsCombustibleLN
    {
        private readonly IListarDocsCombustibleAD _listar;

        public ListarDocsCombustibleLN(IListarDocsCombustibleAD listar)
        {
            _listar = listar;
        }

        public async Task<List<DocsCombustibleDTO>> listar()
        {
            List<DocsCombustibleDTO> lista = await _listar.listar();
            return lista;
        }
    }
}
