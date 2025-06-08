using Preacepta.AD.CasosTipo.Crear;
using Preacepta.AD.DocsMarcaVehiculo.Crear;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.DocsMarcaVehiculo.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsMarcaVehiculo.Crear
{
    public class CrearDocsMarcaVehiculoLN : ICrearDocsMarcaVehiculoLN
    {
        private readonly ICrearDocsMarcaVehiculoAD _crear;
        private readonly IObtenerDatosDocsMarcaVehiculoLN _obtenerDatosLN;

        public CrearDocsMarcaVehiculoLN(ICrearDocsMarcaVehiculoAD crear,
            IObtenerDatosDocsMarcaVehiculoLN obtenerDatosLN)
        {
            _crear = crear;
            _obtenerDatosLN = obtenerDatosLN;
        }

        public async Task<int> Crear(DocsMarcaVehiculoDTO crear)
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
                    Console.WriteLine("Conversion de DocsMarcaVehiculoDTO fallido");
                    return 0;
                }
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearDocsMarcaVehiculoLN{ex.Message}");
                return -1;
            }
        }
    }
}
