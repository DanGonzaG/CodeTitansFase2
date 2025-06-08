using Preacepta.AD.GeAbogadoTipo.Crear;
using Preacepta.LN.GeAbogadoTipo.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeAbogadoTipo.Crear
{
    public class CrearAbogadoTipoLN : ICrearAbogadoTipoLN
    {
        private readonly ICrearAbogadoTipoAD _crear;
        private readonly IObtenerDatosAbogadoTipoLN _obtenerDatosLN;

        public CrearAbogadoTipoLN(ICrearAbogadoTipoAD crearAbogadoTipoAD,
            IObtenerDatosAbogadoTipoLN obtenerDatosLN)
        {
            _crear = crearAbogadoTipoAD;
            _obtenerDatosLN = obtenerDatosLN;
        }

        public async Task<int> crear(GeAbogadoTipoDTO geAbogadoTipoDTO)
        {
            if (geAbogadoTipoDTO == null)
            {
                Console.WriteLine("Error: Objeto nulo.");
                return 0;
            }
            try
            {
                int bandera = await _crear.crear(_obtenerDatosLN.ObtenerDeFront(geAbogadoTipoDTO));
                if (bandera == null)
                {
                    Console.WriteLine("Conversion de AbogadoTipoDTO fallido");
                    return 0;
                }
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearAbogadoTipoLN{ex.Message}");
                return -1;
            }

        }
    }
}
