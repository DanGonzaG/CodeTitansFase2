using Preacepta.AD.CasosTipo.BuscarXid;
using Preacepta.AD.DocsMarcaVehiculo.BuscarXid;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.DocsMarcaVehiculo.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsMarcaVehiculo.BuscarXid
{
    public class BuscarDocsMarcaVehiculoLN : IBuscarDocsMarcaVehiculoLN
    {
        private readonly IBuscarDocsMarcaVehiculoAD _buscar;
        private readonly IObtenerDatosDocsMarcaVehiculoLN _obtenerDatosLN;

        public BuscarDocsMarcaVehiculoLN(IBuscarDocsMarcaVehiculoAD buscar,
             IObtenerDatosDocsMarcaVehiculoLN obtnerDatosLN)
        {
            _buscar = buscar;
            _obtenerDatosLN = obtnerDatosLN;
        }

        public async Task<DocsMarcaVehiculoDTO?> buscar(int id)
        {
            try
            {
                TDocsMarcaVehiculo? resultadoBusqueda = await _buscar.buscar(id);
                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró la el tipo de abogado.");
                    return null;
                }
                DocsMarcaVehiculoDTO obtenerDatos = _obtenerDatosLN.ObtenerDeDB(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarDocsMarcaVehiculoLN: {ex.Message}");
                return null;
            }
        }
    }
}
