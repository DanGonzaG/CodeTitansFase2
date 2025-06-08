using Preacepta.AD.Casos.Crear;
using Preacepta.LN.Casos.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;

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
                int bandera = await _crear.crear(_obtenerDatosLN.ObtenerDeFrontCrear(crear));
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
