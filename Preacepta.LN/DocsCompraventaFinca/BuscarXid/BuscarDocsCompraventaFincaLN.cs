using Preacepta.AD.CasosTipo.BuscarXid;
using Preacepta.AD.DocsCompraventaFinca.BuscarXid;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.DocsCompraventaFinca.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsCompraventaFinca.BuscarXid
{
    public class BuscarDocsCompraventaFincaLN : IBuscarDocsCompraventaFincaLN
    {
        private readonly IBuscarDocsCompraventaFincaAD _buscar;
        private readonly IObtenerDatosDocsCompraventaFincaLN _obtenerDatosLN;

        public BuscarDocsCompraventaFincaLN(IBuscarDocsCompraventaFincaAD buscar,
             IObtenerDatosDocsCompraventaFincaLN obtnerDatosLN)
        {
            _buscar = buscar;
            _obtenerDatosLN = obtnerDatosLN;
        }

        public async Task<DocsCompraventaFincaDTO?> buscar(int id)
        {
            try
            {
                TDocsCompraventaFinca? resultadoBusqueda = await _buscar.buscar(id);
                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró la el tipo de compraventafinca.");
                    return null;
                }
                DocsCompraventaFincaDTO obtenerDatos = _obtenerDatosLN.ObtenerDeDB(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarDocsCompraventaFincaLN: {ex.Message}");
                return null;
            }
        }
    }
}
