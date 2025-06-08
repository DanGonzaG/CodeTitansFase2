using Preacepta.AD.CasosTipo.BuscarXid;
using Preacepta.AD.DocsInscripcionVehiculo.BuscarXid;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.DocsInscripcionVehiculo.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsInscripcionVehiculo.BuscarXid
{
    public class BuscarDocsInscripcionVehiculoLN : IBuscarDocsInscripcionVehiculoLN
    {
        private readonly IBuscarDocsInscripcionVehiculoAD _buscar;
        private readonly IObtenerDatosDocsInscripcionVehiculoTipoLN _obtenerDatosLN;

        public BuscarDocsInscripcionVehiculoLN(IBuscarDocsInscripcionVehiculoAD buscar,
             IObtenerDatosDocsInscripcionVehiculoTipoLN obtnerDatosLN)
        {
            _buscar = buscar;
            _obtenerDatosLN = obtnerDatosLN;
        }

        public async Task<DocsInscripcionVehiculoDTO?> buscar(int id)
        {
            try
            {
                TDocsInscripcionVehiculo? resultadoBusqueda = await _buscar.buscar(id);
                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró la el tipo docs incripcion vehiculo.");
                    return null;
                }
                DocsInscripcionVehiculoDTO obtenerDatos = _obtenerDatosLN.ObtenerDeDB(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarDocsInscripcionVehiculoLN: {ex.Message}");
                return null;
            }
        }
    }
}
