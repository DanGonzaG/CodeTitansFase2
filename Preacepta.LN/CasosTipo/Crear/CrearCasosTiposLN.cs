using Preacepta.AD.CasosTipo.Crear;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosTipo.Crear
{
    public class CrearCasosTiposLN : ICrearCasosTiposLN
    {
        private readonly ICrearCasosTiposAD _crear;
        private readonly IObtenerDatosCasosTipoLN _obtenerDatosLN;

        public CrearCasosTiposLN(ICrearCasosTiposAD crear,
            IObtenerDatosCasosTipoLN obtenerDatosLN)
        {
            _crear = crear;
            _obtenerDatosLN = obtenerDatosLN;
        }

        public async Task<int> Crear(CasosTipoDTO crear)
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
                    Console.WriteLine("Conversion de AbogadoTipoDTO fallido");
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
