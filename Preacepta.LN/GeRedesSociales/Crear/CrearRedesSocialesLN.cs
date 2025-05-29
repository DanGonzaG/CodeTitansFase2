using Preacepta.AD.CasosTipo.Crear;
using Preacepta.AD.GeRedesSociales.Crear;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.GeRedesSociales.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeRedesSociales.Crear
{
    public class CrearRedesSocialesLN : ICrearRedesSocialesLN
    {
        private readonly ICrearRedesSocialesAD _crear;
        private readonly IObtenerDatosRedesSocialesLN _obtenerDatosLN;

        public CrearRedesSocialesLN(ICrearRedesSocialesAD crear,
            IObtenerDatosRedesSocialesLN obtenerDatosLN)
        {
            _crear = crear;
            _obtenerDatosLN = obtenerDatosLN;
        }

        public async Task<int> Crear(GeRedesSocialeDTO crear)
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
                    Console.WriteLine("Conversion de GeRedesSocialeDTO fallido");
                    return 0;
                }
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearRedesSocialesLN{ex.Message}");
                return -1;
            }
        }
    }
}
