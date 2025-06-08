using Preacepta.AD.CasosTipo.Listar;
using Preacepta.AD.DocsAutorizacionRevisionExpediente.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsAutorizacionRevisionExpediente.Listar
{
    public class ListarDocsAutorizacionRevisionExpedienteLN : IListarDocsAutorizacionRevisionExpedienteLN
    {
        private readonly IListarDocsAutorizacionRevisionExpedienteAD _listar;

        public ListarDocsAutorizacionRevisionExpedienteLN(IListarDocsAutorizacionRevisionExpedienteAD listar)
        {
            _listar = listar;
        }

        public async Task<List<DocsAutorizacionRevisionExpedienteDTO>> listar()
        {
            List<DocsAutorizacionRevisionExpedienteDTO> lista = await _listar.listar();
            return lista;
        }
    }
}
