using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsAutorizacionRevisionExpediente.Editar
{
    public interface IEditarDocsAutorizacionRevisionExpedienteAD
    {
        Task<int> Editar(TDocsAutorizacionRevisionExpediente editar);
    }
}
