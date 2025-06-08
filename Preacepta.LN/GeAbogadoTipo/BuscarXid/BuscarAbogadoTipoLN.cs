using Preacepta.AD.GeAbogadoTipo.BuscarXid;
using Preacepta.LN.GeAbogadoTipo.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeAbogadoTipo.BuscarXid
{
    public class BuscarAbogadoTipoLN : IBuscarAbogadoTipoLN
    {
        private readonly IBuscarAbogadoTipoAD _buscar;
        private readonly IObtenerDatosAbogadoTipoLN _obtenerDatosLN;

        public BuscarAbogadoTipoLN(IBuscarAbogadoTipoAD buscarAbogadoTipoAD,
            IObtenerDatosAbogadoTipoLN obtnerDatosLN)
        {
            _buscar = buscarAbogadoTipoAD;
            _obtenerDatosLN = obtnerDatosLN;
        }

        public async Task<GeAbogadoTipoDTO?> buscar(int id)
        {
            try
            {
                TGeAbogadoTipo? gePersona = await _buscar.buscar(id);
                if (gePersona == null)
                {
                    Console.WriteLine("No se encontró la el tipo de abogado.");
                    return null;
                }
                GeAbogadoTipoDTO gePersonaDTO = _obtenerDatosLN.ObtenerDeDB(gePersona);
                return gePersonaDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarAbogadoTipoLN: {ex.Message}");
                return null;
            }


        }
    }
}
