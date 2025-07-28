using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsAutorizacionRevisionExpediente.Eliminar
{
    public interface IEliminarDocsAutorizacionRevisionExpedienteAD
    {
        Task<int> Eliminar(int id);
    }
}
