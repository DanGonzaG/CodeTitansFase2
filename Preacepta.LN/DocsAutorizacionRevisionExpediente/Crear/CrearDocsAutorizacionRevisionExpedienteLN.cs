using Preacepta.AD.CasosTipo.Crear;
using Preacepta.AD.DocsAutorizacionRevisionExpediente.Crear;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.DocsAutorizacionRevisionExpediente.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsAutorizacionRevisionExpediente.Crear
{
    public class CrearDocsAutorizacionRevisionExpedienteLN : ICrearDocsAutorizacionRevisionExpedienteLN
    {
        private readonly ICrearDocsAutorizacionRevisionExpedienteAD _crear;
        private readonly IObtenerDocsAutorizacionRevisionExpedienteLN _obtenerDatosLN;

        public CrearDocsAutorizacionRevisionExpedienteLN(ICrearDocsAutorizacionRevisionExpedienteAD crear,
            IObtenerDocsAutorizacionRevisionExpedienteLN obtenerDatosLN)
        {
            _crear = crear;
            _obtenerDatosLN = obtenerDatosLN;
        }

        public async Task<int> Crear(DocsAutorizacionRevisionExpedienteDTO crear)
        {
            if (crear == null)
            {
                Console.WriteLine("Error: Objeto nulo.");
                return 0;
            }
            try
            {
                int bandera = await _crear.crear(_obtenerDatosLN.ObtenerDeFront(crear));
                if (bandera == null)
                {
                    Console.WriteLine("Conversion de DocsAutorizacionRevisionExpedienteDTO fallido");
                    return 0;
                }
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearDocsAutorizacionRevisionExpedienteLN{ex.Message}");
                return -1;
            }
        }
    }
}
