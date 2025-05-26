using Preacepta.AD.CasosTipo.Crear;
using Preacepta.AD.GeAbogado.Crear;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.GeAbogado.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeAbogado.Crear
{
    public class CrearAbogadoLN : ICrearAbogadoLN
    {
        private readonly ICrearAbogadoAD _crear;
        private readonly IObtenerDatosAbogadoLN _obtenerDatosLN;

        public CrearAbogadoLN(ICrearAbogadoAD crear,
           IObtenerDatosAbogadoLN obtenerDatosLN)
        {
            _crear = crear;
            _obtenerDatosLN = obtenerDatosLN;
        }

        public async Task<int> Crear(GeAbogadoDTO crear)
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
                    Console.WriteLine("Conversion de GeAbogadoDTO fallido");
                    return 0;
                }
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearAbogadoLN{ex.Message}");
                return -1;
            }

        }
    }
}
