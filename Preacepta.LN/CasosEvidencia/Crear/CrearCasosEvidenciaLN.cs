using Preacepta.AD.CasosEvidencia.Crear;
using Preacepta.LN.CasosEvidencia.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosEvidencia.Crear
{
    public class CrearCasosEvidenciaLN : ICrearCasosEvidenciaLN
    {
        private readonly ICrearCasosEvidenciaAD _crear;
        private readonly IObtnerDatosCasoEvidenciaLN _obtenerDatosLN;

        public CrearCasosEvidenciaLN(ICrearCasosEvidenciaAD crear,
            IObtnerDatosCasoEvidenciaLN obtenerDatosLN)
        {
            _crear = crear;
            _obtenerDatosLN = obtenerDatosLN;
        }

        public async Task<int> Crear(CasosEvidenciaDTO crear)
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
                    Console.WriteLine("Conversion de CasosEvidenciaDTO fallido");
                    return 0;
                }
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearCasosEvidenciaLN{ex.Message}");
                return -1;
            }
        }
    }
}
