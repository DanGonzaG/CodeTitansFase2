using Preacepta.AD.Citas.Crear;
using Preacepta.LN.Citas.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Citas.Crear

{
    public class CrearCitasLN : ICrearCitasLN
    {
        private readonly ICrearCitasAD _crear;
        private readonly IObtenerDatosCitasLN _obtenerDatosLN;

        public CrearCitasLN(ICrearCitasAD crear,
            IObtenerDatosCitasLN obtenerDatosLN)
        {
            _crear = crear;
            _obtenerDatosLN = obtenerDatosLN;
        }

        public async Task<int> crear(CitasDTO crear)
        {
            if (crear == null)
            {
                Console.WriteLine("Error: Objeto nulo.");
                return 0;
            }
            try
            {
                int bandera = await _crear.crear(_obtenerDatosLN.ObtenerDeFront(crear));
                if (bandera <= 0)
                {
                    Console.WriteLine("Fallo al guardar la cita.");
                    return 0;
                }
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearCitasLN{ex.Message}");
                return -1;
            }

        }
    }
}
