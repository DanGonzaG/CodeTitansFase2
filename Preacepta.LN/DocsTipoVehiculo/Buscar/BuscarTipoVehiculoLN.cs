using Preacepta.AD.DocsTipoVehiculo.Buscar;
using Preacepta.LN.DocsTipoVehiculo.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsTipoVehiculo.Buscar
{
    public class BuscarTipoVehiculoLN : IBuscarTipoVehiculoLN
    {
        private readonly IBuscarTipoVehiculoAD _buscar;
        private readonly IObtenerDatosTipoVehiculoLN _obtenerDatos;

        public BuscarTipoVehiculoLN(IBuscarTipoVehiculoAD buscar,
            IObtenerDatosTipoVehiculoLN obtenerDatos)
        {
            _buscar = buscar;
            _obtenerDatos = obtenerDatos;
        }

        public async Task<DocsTipoVehiculoDTO?> buscar(int id)
        {
            try
            {
                // Cambiar tipo aquí
                TDocsTipoVehiculo? resultadoBusqueda = await _buscar.buscar(id);

                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró el tipo de testimonio.");
                    return null;
                }
                DocsTipoVehiculoDTO obtenerDatos = _obtenerDatos.ObtenerDeDB(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarTipoVehiculoLN: {ex.Message}");
                return null;
            }
        }

    }
}
