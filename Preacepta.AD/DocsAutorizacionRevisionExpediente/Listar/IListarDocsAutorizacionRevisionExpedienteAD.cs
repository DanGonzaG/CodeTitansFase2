using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsAutorizacionRevisionExpediente.Listar
{
    public interface IListarDocsAutorizacionRevisionExpedienteAD
    {
        Task<List<DocsAutorizacionRevisionExpedienteDTO>> listar();
    }
}
