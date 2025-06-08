using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsAutorizacionRevisionExpediente.Listar
{
    public interface IListarDocsAutorizacionRevisionExpedienteLN
    {
        Task<List<DocsAutorizacionRevisionExpedienteDTO>> listar();
    }
}
