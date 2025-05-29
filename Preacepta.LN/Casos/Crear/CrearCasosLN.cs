using Preacepta.AD.Casos.Crear;
using Preacepta.AD.CasosTipo.Crear;
using Preacepta.LN.Casos.ObtenerDatos;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Casos.Crear
{
    public class CrearCasosLN : ICrearCasosLN
    {
        private readonly ICrearCasosAD _crear;
        private readonly IObtenerDatosCasoLN _obtenerDatosLN;

        public CrearCasosLN(ICrearCasosAD crear,
            IObtenerDatosCasoLN obtenerDatosLN)
        {
            _crear = crear;
            _obtenerDatosLN = obtenerDatosLN;
        }

        public async Task<int> Crear(CasoDTO crear)
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
                    Console.WriteLine("Conversion de CrearCasosLN-CasoDTO fallido");
                    return 0;
                }
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearCasosLN{ex.Message}");
                return -1;
            }
        }
    }
}
