using Preacepta.AD.DocsOpcionCompraventaVehiculo.Crear;
using Preacepta.LN.DocsOpcionCompraventaVehiculo.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsOpcionCompraventaVehiculo.Crear
{
    public class CrearDocCVLN : ICrearDocCVLN
    {
        private readonly ICrearDocCVAD _crearDocCVAD;
        private readonly IObtenerDatosDocsCV _obtenerDatos;

        public CrearDocCVLN(ICrearDocCVAD crearDocCVAD,
            IObtenerDatosDocsCV obtenerDatos)
        {
            _crearDocCVAD = crearDocCVAD;
            _obtenerDatos = obtenerDatos;
        }

        public async Task<int> crear(DocsOpcionCompraventaVehiculoDTO docCVDTO)
        {
            if (docCVDTO == null)
            {
                Console.WriteLine("Error: Objeto nulo.");
                return 0;
            }
            try
            {
                int bandera = await _crearDocCVAD.crear(_obtenerDatos.ObtenerDeFront(docCVDTO));
                if (bandera == null)
                {
                    Console.WriteLine("Conversion de AbogadoTipoDTO fallido");
                    return 0;
                }
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearCasosTiposLN{ex.Message}");
                return -1;
            }
        }

    }
}
