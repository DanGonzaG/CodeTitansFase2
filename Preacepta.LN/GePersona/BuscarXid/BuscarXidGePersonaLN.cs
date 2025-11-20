using Preacepta.AD.GePersona.BuscarXid;
using Preacepta.LN.GePersona.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GePersona.BuscarXid
{
    public class BuscarXidGePersonaLN : IBuscarXidGePersonaLN
    {
        private readonly IBuscarXidGePersonaAD _buscarXidGePersonaAD;
        private readonly IObtenerDatosLN _obtenerDatosLN;

        public BuscarXidGePersonaLN(IBuscarXidGePersonaAD buscarXidGePersonaAD, IObtenerDatosLN obtnerDatosLN)
        {
            _buscarXidGePersonaAD = buscarXidGePersonaAD;
            _obtenerDatosLN = obtnerDatosLN;
        }

        public async Task<GePersonaDTO?> buscar(int id)
        {
            try
            {
                TGePersona? gePersona = await _buscarXidGePersonaAD.buscar(id);
                if (gePersona == null)
                {
                    Console.WriteLine("No se encontró la persona.");
                    return null;
                }
                GePersonaDTO gePersonaDTO = _obtenerDatosLN.ObtenerDeDB(gePersona);
                return gePersonaDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarXidGePersonaLN: {ex.Message}");
                return null;
            }


        }

        public async Task<GePersonaDTO?> buscarXnumCedula(string id)
        {
            try
            {
                TGePersona? gePersona = await _buscarXidGePersonaAD.buscarXnumCedula(id);
                if (gePersona == null)
                {
                    Console.WriteLine("No se encontró la persona.");
                    return null;
                }
                GePersonaDTO gePersonaDTO = _obtenerDatosLN.ObtenerDeDB(gePersona);
                return gePersonaDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarXidGePersonaLN: {ex.Message}");
                return null;
            }
        }

        public async Task<bool?> buscarXnumCedulaBOOLEAN(string id)
        {
            try
            {
                bool? gePersona = await _buscarXidGePersonaAD.buscarXnumCedulaBOOLEAN(id);
                if (gePersona == false)
                {
                    Console.WriteLine("No se encontró la persona.");
                    return false;
                }                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en metodo buscarXnumCedulaBOOLEAN de clase BuscarXidGePersonaLN: {ex.Message}");
                return null;
            }
        }

        public async Task<GePersonaDTO?> buscarXcorreo(string correo)
        {
            try
            {
                TGePersona? gePersona = await _buscarXidGePersonaAD.buscarXcorreo(correo);
                if (gePersona == null)
                {
                    Console.WriteLine("No se encontró la persona.");
                    return null;
                }
                GePersonaDTO gePersonaDTO = _obtenerDatosLN.ObtenerDeDB(gePersona);
                return gePersonaDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarXidGePersonaLN: {ex.Message}");
                return null;
            }
        }

        public async Task<GePersonaDTO?> buscarXtelefono1(string telefono)
        {
            try
            {
                TGePersona? gePersona = await _buscarXidGePersonaAD.buscarXtelefono1(telefono);
                if (gePersona == null)
                {
                    Console.WriteLine("No se encontró la persona.");
                    return null;
                }
                GePersonaDTO gePersonaDTO = _obtenerDatosLN.ObtenerDeDB(gePersona);
                return gePersonaDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarXidGePersonaLN: {ex.Message}");
                return null;
            }
        }

        public async Task<GePersonaDTO?> buscarXtelefono2(string telefono)
        {
            try
            {
                TGePersona? gePersona = await _buscarXidGePersonaAD.buscarXtelefono2(telefono);
                if (gePersona == null)
                {
                    Console.WriteLine("No se encontró la persona.");
                    return null;
                }
                GePersonaDTO gePersonaDTO = _obtenerDatosLN.ObtenerDeDB(gePersona);
                return gePersonaDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarXidGePersonaLN: {ex.Message}");
                return null;
            }
        }
    }
}
