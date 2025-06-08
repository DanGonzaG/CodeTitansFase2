using Preacepta.AD.Testimonios.Buscar;
using Preacepta.AD.Testimonios.Crear;
using Preacepta.LN.Testimonios.Buscar;
using Preacepta.LN.Testimonios.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Testimonios.Crear
{
    public class CrearTestLN : ICrearTestLN
    {
        private readonly ICrearTestAD _crearTestAD;
        private readonly IObtenerDatosTestLN _obtenerDatosTestLN;

        public CrearTestLN(ICrearTestAD crearTestAD, IObtenerDatosTestLN obtenerDatosTestLN)
        {
            _crearTestAD = crearTestAD;
            _obtenerDatosTestLN = obtenerDatosTestLN;
        }

        public async Task<int> crear(TTestimonioDTO testDTO)
        {
            if (testDTO == null)
            {
                Console.WriteLine("Error: Objeto nulo.");
                return 0;
            }
            try
            {
                int bandera = await _crearTestAD.crear(_obtenerDatosTestLN.ObtenerDeFront(testDTO));
                if (bandera == null)
                {
                    Console.WriteLine("Conversion de CrearTestLN fallido");
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
