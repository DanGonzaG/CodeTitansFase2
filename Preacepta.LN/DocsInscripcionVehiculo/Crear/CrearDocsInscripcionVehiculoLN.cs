using Preacepta.AD.CasosTipo.Crear;
using Preacepta.AD.DocsInscripcionVehiculo.Crear;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.DocsInscripcionVehiculo.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsInscripcionVehiculo.Crear
{
    public class CrearDocsInscripcionVehiculoLN : ICrearDocsInscripcionVehiculoLN
    {
        private readonly ICrearDocsInscripcionVehiculoAD _crear;
        private readonly IObtenerDatosDocsInscripcionVehiculoTipoLN _obtenerDatosLN;

        public CrearDocsInscripcionVehiculoLN(ICrearDocsInscripcionVehiculoAD crear,
            IObtenerDatosDocsInscripcionVehiculoTipoLN obtenerDatosLN)
        {
            _crear = crear;
            _obtenerDatosLN = obtenerDatosLN;
        }

        public async Task<int> Crear(DocsInscripcionVehiculoDTO crear)
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
                    Console.WriteLine("Conversion de DocsInscripcionVehiculoDTO fallido");
                    return 0;
                }
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearDocsInscripcionVehiculoLN{ex.Message}");
                return -1;
            }
        }
    }
}
