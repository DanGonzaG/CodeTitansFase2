using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsAutorizacionRevisionExpediente.ObtenerDatos
{
    public interface IObtenerDocsAutorizacionRevisionExpedienteLN
    {
        DocsAutorizacionRevisionExpedienteDTO ObtenerDeDB(TDocsAutorizacionRevisionExpediente datos);
        TDocsAutorizacionRevisionExpediente ObtenerDeFront(DocsAutorizacionRevisionExpedienteDTO datos);
    }
}
