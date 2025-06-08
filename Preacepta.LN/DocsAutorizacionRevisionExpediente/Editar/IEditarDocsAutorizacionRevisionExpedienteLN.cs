using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsAutorizacionRevisionExpediente.Editar
{
    public interface IEditarDocsAutorizacionRevisionExpedienteLN
    {
        Task<int> Editar(DocsAutorizacionRevisionExpedienteDTO editar);
    }
}
