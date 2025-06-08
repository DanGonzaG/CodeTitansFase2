using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsAutorizacionRevisionExpediente.BuscarXid
{
    public interface IBuscarDocsAutorizacionRevisionExpedienteLN
    {
        Task<DocsAutorizacionRevisionExpedienteDTO?> buscar(int id);
    }
}
