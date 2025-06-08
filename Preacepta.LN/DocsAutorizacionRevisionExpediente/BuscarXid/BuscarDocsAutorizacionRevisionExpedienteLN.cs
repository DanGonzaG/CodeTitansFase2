using Preacepta.AD.CasosTipo.BuscarXid;
using Preacepta.AD.DocsAutorizacionRevisionExpediente.BuscarXid;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.DocsAutorizacionRevisionExpediente.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsAutorizacionRevisionExpediente.BuscarXid
{
    public class BuscarDocsAutorizacionRevisionExpedienteLN : IBuscarDocsAutorizacionRevisionExpedienteLN
    {
        private readonly IBuscarDocsAutorizacionRevisionExpedienteAD _buscar;
        private readonly IObtenerDocsAutorizacionRevisionExpedienteLN _obtenerDatosLN;

        public BuscarDocsAutorizacionRevisionExpedienteLN(IBuscarDocsAutorizacionRevisionExpedienteAD buscar,
             IObtenerDocsAutorizacionRevisionExpedienteLN obtnerDatosLN)
        {
            _buscar = buscar;
            _obtenerDatosLN = obtnerDatosLN;
        }

        public async Task<DocsAutorizacionRevisionExpedienteDTO?> buscar(int id)
        {
            try
            {
                TDocsAutorizacionRevisionExpediente? resultadoBusqueda = await _buscar.buscar(id);
                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró la el tipo de abogado.");
                    return null;
                }
                DocsAutorizacionRevisionExpedienteDTO obtenerDatos = _obtenerDatosLN.ObtenerDeDB(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarDocsAutorizacionRevisionExpedienteLN: {ex.Message}");
                return null;
            }
        }
    }
}
