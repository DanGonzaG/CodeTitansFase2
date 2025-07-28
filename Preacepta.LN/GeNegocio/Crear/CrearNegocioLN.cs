using Preacepta.AD.GeNegocio.Crear;
using Preacepta.LN.GeNegocio.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeNegocio.Crear
{
    public class CrearNegocioLN : ICrearNegocioLN
    {
        private readonly ICrearNegocioAD _crear;
        private readonly IObtenerDatosNegocioLN _obtenerDatosLN;

        public CrearNegocioLN(ICrearNegocioAD crear,
            IObtenerDatosNegocioLN obtenerDatosLN)
        {
            _crear = crear;
            _obtenerDatosLN = obtenerDatosLN;
        }

        public async Task<int> Crear(GeNegocioDTO crear)
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
                    Console.WriteLine("Conversion de GeNegocioDTO fallido");
                    return 0;
                }
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearNegocioLN{ex.Message}");
                return -1;
            }

        }
    }
}
