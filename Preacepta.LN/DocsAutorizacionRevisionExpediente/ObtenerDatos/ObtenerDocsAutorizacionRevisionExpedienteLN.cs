using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsAutorizacionRevisionExpediente.ObtenerDatos
{
    public class ObtenerDocsAutorizacionRevisionExpedienteLN : IObtenerDocsAutorizacionRevisionExpedienteLN
    {
        public DocsAutorizacionRevisionExpedienteDTO ObtenerDeDB(TDocsAutorizacionRevisionExpediente datos)
        {
            return new DocsAutorizacionRevisionExpedienteDTO
            {
                CedulaAsistente = datos.CedulaAsistente,
                CedulaAsistenteNavigation = datos.CedulaAsistenteNavigation,
                CedulaAbogado = datos.CedulaAbogado,
                CedulaAbogadoNavigation = datos.CedulaAbogadoNavigation,
                CedulaImputado = datos.CedulaImputado,
                CedulaImputadoNavigation = datos.CedulaImputadoNavigation,
                IdDocumento = datos.IdDocumento,
                Delito = datos.Delito,
                Expediente = datos.Expediente,
                Ofendido = datos.Ofendido
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TDocsAutorizacionRevisionExpediente ObtenerDeFront(DocsAutorizacionRevisionExpedienteDTO datos)
        {
            return new TDocsAutorizacionRevisionExpediente
            {
                CedulaAsistente = datos.CedulaAsistente,
                CedulaAsistenteNavigation = datos.CedulaAsistenteNavigation,
                CedulaAbogado = datos.CedulaAbogado,
                CedulaAbogadoNavigation = datos.CedulaAbogadoNavigation,
                CedulaImputado = datos.CedulaImputado,
                CedulaImputadoNavigation = datos.CedulaImputadoNavigation,
                IdDocumento = datos.IdDocumento,
                Delito = datos.Delito,
                Expediente = datos.Expediente,
                Ofendido = datos.Ofendido
            };
        }
    }
}
