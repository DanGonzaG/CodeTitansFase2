using Preacepta.AD.CasosTipo.Editar;
using Preacepta.AD.DocsAutorizacionRevisionExpediente.Editar;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.DocsAutorizacionRevisionExpediente.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsAutorizacionRevisionExpediente.Editar
{
    public class EditarDocsAutorizacionRevisionExpedienteLN : IEditarDocsAutorizacionRevisionExpedienteLN
    {
        private readonly IEditarDocsAutorizacionRevisionExpedienteAD _editar;
        private readonly IObtenerDocsAutorizacionRevisionExpedienteLN _obtenerDatosLN;

        public EditarDocsAutorizacionRevisionExpedienteLN(IEditarDocsAutorizacionRevisionExpedienteAD editar,
            IObtenerDocsAutorizacionRevisionExpedienteLN obtenerDatos)
        {
            _editar = editar;
            _obtenerDatosLN = obtenerDatos;
        }

        public async Task<int> Editar(DocsAutorizacionRevisionExpedienteDTO editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                int bandera = await _editar.Editar(_obtenerDatosLN.ObtenerDeFront(editar));
                return bandera;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarDocsAutorizacionRevisionExpedienteLN: {ex.Message}");
                return 0;
            }
        }
    }
}
