using Preacepta.AD.CrDireccion1.Crear;
using Preacepta.LN.CrDireccion1.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CrDireccion1.Crear
{
    public class CrearCrDireccion1LN : ICrearCrDireccion1LN
    {
        private readonly ICrearCrDireccion1AD _crear;
        private readonly IObtenerDatosDireccion1LN _obtenerDatosLN;

        public CrearCrDireccion1LN(ICrearCrDireccion1AD crear,
            IObtenerDatosDireccion1LN obtenerDatosLN)
        {
            _crear = crear;
            _obtenerDatosLN = obtenerDatosLN;
        }

        public async Task<int> CrearProvincia(CrProvinciaDTO crear)
        {
            if (crear == null)
            {
                Console.WriteLine("Error: Objeto nulo.");
                return 0;
            }
            try
            {
                int bandera = await _crear.crearProvincia(_obtenerDatosLN.ObtenerDeFrontProvincias(crear));
                if (bandera == null)
                {
                    Console.WriteLine("Conversion de CrProvinciaDTO fallido");
                    return 0;
                }
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearCrDireccion1LN-CrearProvincia{ex.Message}");
                return -1;
            }
        }

        public async Task<int> CrearCanton(CrCantonDTO crear)
        {
            if (crear == null)
            {
                Console.WriteLine("Error: Objeto nulo.");
                return 0;
            }
            try
            {
                int bandera = await _crear.crearCanton(_obtenerDatosLN.ObtenerDeFrontCanton(crear));
                if (bandera == null)
                {
                    Console.WriteLine("Conversion de CrCantonDTO fallido");
                    return 0;
                }
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearCrDireccion1LN-crearCanton{ex.Message}");
                return -1;
            }
        }

        public async Task<int> CrearDistrito(CrDistritoDTO crear)
        {
            if (crear == null)
            {
                Console.WriteLine("Error: Objeto nulo.");
                return 0;
            }
            try
            {
                int bandera = await _crear.crearDistrito(_obtenerDatosLN.ObtenerDeFrontDistrito(crear));
                if (bandera == null)
                {
                    Console.WriteLine("Conversion de CrDistritoDTO fallido");
                    return 0;
                }
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearCrDireccion1LN-CrearDistrito{ex.Message}");
                return -1;
            }
        }
    }
}
