using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsAutorizacionRevisionExpediente.Eliminar
{
    public interface IEliminarDocsAutorizacionRevisionExpedienteLN
    {
        Task<int> Eliminar(int id);
    }
}
