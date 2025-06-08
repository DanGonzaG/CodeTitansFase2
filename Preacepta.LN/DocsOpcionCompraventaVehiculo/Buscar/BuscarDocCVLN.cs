using Preacepta.AD.DocsOpcionCompraventaVehiculo.Buscar;
using Preacepta.LN.DocsOpcionCompraventaVehiculo.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsOpcionCompraventaVehiculo.Buscar
{
    public class BuscarDocCVLN : IBuscarDocCVLN
    {
        private readonly IBuscarDocCVAD _buscarDocCVAD;
        private readonly IObtenerDatosDocsCV _obtenerDatosDocsCV;

        public BuscarDocCVLN(IBuscarDocCVAD buscarDocCVAD, IObtenerDatosDocsCV obtenerDatosDocsCV)
        {
            _buscarDocCVAD = buscarDocCVAD;
            _obtenerDatosDocsCV = obtenerDatosDocsCV;
        }

        public async Task<DocsOpcionCompraventaVehiculoDTO?> buscar(int id)
        {
            try
            {
                // Cambiar tipo aquí
                TDocsOpcionCompraventaVehiculo? resultadoBusqueda = await _buscarDocCVAD.buscar(id);

                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró el tipo de Compra o venta.");
                    return null;
                }
                DocsOpcionCompraventaVehiculoDTO obtenerDatos = _obtenerDatosDocsCV.ObtenerDeDB(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarDocCVLN: {ex.Message}");
                return null;
            }

        }
    }
}
