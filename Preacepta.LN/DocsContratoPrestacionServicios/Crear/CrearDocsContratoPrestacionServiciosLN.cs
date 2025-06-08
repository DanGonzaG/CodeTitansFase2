using Preacepta.AD.CasosTipo.Crear;
using Preacepta.AD.DocsContratoPrestacionServicios.Crear;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.DocsContratoPrestacionServicios.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsContratoPrestacionServicios.Crear
{
    public class CrearDocsContratoPrestacionServiciosLN : ICrearDocsContratoPrestacionServiciosLN
    {
        private readonly ICrearDocsContratoPrestacionServiciosAD _crear;
        private readonly IObtenerDatosDocsContratoPrestacionServiciosLN _obtenerDatosLN;

        public CrearDocsContratoPrestacionServiciosLN(ICrearDocsContratoPrestacionServiciosAD crear,
            IObtenerDatosDocsContratoPrestacionServiciosLN obtenerDatosLN)
        {
            _crear = crear;
            _obtenerDatosLN = obtenerDatosLN;
        }

        public async Task<int> Crear(DocsContratoPrestacionServicioDTO crear)
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
                    Console.WriteLine("Conversion de CrearDocsContratoPrestacionServiciosLN fallido");
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
