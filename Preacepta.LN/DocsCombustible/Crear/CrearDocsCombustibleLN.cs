using Preacepta.AD.CasosTipo.Crear;
using Preacepta.AD.DocsCombustible.Crear;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.DocsCombustible.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsCombustible.Crear
{
    public class CrearDocsCombustibleLN : ICrearDocsCombustibleLN
    {
        private readonly ICrearDocsCombustibleAD _crear;
        private readonly IObtenerDatosDocsCombustibleLN _obtenerDatosLN;

        public CrearDocsCombustibleLN(ICrearDocsCombustibleAD crear,
            IObtenerDatosDocsCombustibleLN obtenerDatosLN)
        {
            _crear = crear;
            _obtenerDatosLN = obtenerDatosLN;
        }

        public async Task<int> Crear(DocsCombustibleDTO crear)
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
                    Console.WriteLine("Conversion de DocsCombustibleDTO fallido");
                    return 0;
                }
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearDocsCombustibleLN{ex.Message}");
                return -1;
            }
        }
    }
}
