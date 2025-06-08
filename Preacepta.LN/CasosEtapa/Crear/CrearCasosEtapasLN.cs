using Preacepta.AD.CasosEtapa.Crear;
using Preacepta.LN.CasosEtapa.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosEtapa.Crear
{
    public class CrearCasosEtapasLN : ICrearCasosEtapasLN
    {
        private readonly ICrearCasosEtapasAD _crear;
        private readonly IObtnerDatosCasoEtapaLN _obtenerDatosLN;

        public CrearCasosEtapasLN(ICrearCasosEtapasAD crear,
            IObtnerDatosCasoEtapaLN obtenerDatosLN)
        {
            _crear = crear;
            _obtenerDatosLN = obtenerDatosLN;
        }

        public async Task<int> Crear(CasosEtapaDTO crear)
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
                    Console.WriteLine("Conversion de CasosEtapaDTO fallido");
                    return 0;
                }
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearCasosEtapasLN{ex.Message}");
                return -1;
            }
        }
    }
}
