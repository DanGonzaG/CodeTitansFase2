using Preacepta.AD.CasosTipo.Crear;
using Preacepta.AD.DocsCompraventaFinca.Crear;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.DocsCompraventaFinca.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsCompraventaFinca.Crear
{
    public class CrearDocsCompraventaFincaLN : ICrearDocsCompraventaFincaLN
    {
        private readonly ICrearDocsCompraventaFincaAD _crear;
        private readonly IObtenerDatosDocsCompraventaFincaLN _obtenerDatosLN;

        public CrearDocsCompraventaFincaLN(ICrearDocsCompraventaFincaAD crear,
            IObtenerDatosDocsCompraventaFincaLN obtenerDatosLN)
        {
            _crear = crear;
            _obtenerDatosLN = obtenerDatosLN;
        }

        public async Task<int> Crear(DocsCompraventaFincaDTO crear)
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
                    Console.WriteLine("Conversion de DocsCompraventaFincaDTO fallido");
                    return 0;
                }
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearDocsCompraventaFincaLN{ex.Message}");
                return -1;
            }
        }
    }
}
