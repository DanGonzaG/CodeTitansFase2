using Preacepta.AD.CitasTipo.Crear;
using Preacepta.LN.CitasTipo.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CitasTipo.Crear

{
    public class CrearCitasTipoLN : ICrearCitasTipoLN
    {
        private readonly ICrearCitasTipoAD _crear;
        private readonly IObtenerDatosCitasTipoLN _obtenerDatosLN;

        public CrearCitasTipoLN(ICrearCitasTipoAD crear,
            IObtenerDatosCitasTipoLN obtenerDatosLN)
        {
            _crear = crear;
            _obtenerDatosLN = obtenerDatosLN;
        }

        public async Task<int> Crear(CitasTipoDTO crear)
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
                    Console.WriteLine("Conversion de CitasTipoDTO fallido");
                    return 0;
                }
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearCitasTipoLN{ex.Message}");
                return -1;
            }

        }
    }
}
