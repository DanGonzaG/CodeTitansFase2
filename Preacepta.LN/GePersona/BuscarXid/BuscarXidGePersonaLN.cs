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

        //Metodos para obtener objeto Persona Completo

        #region Buscar Persona x PK
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
        #endregion

        #region Buscar persona X Número de Cédula
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
        #endregion

        #region Buscar Persona xCorreo
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
        #endregion

        #region Buscar Persona X Telefono1
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
        #endregion

        #region Buscar Persona x Telefono2
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
        #endregion


        //Metodo Booleanos para corroborar existencia del objeto

        #region Bool Numero de Cedula
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
        #endregion

        #region Bool x Correo
        public async Task<bool?> buscarXcorreoBOOLEAN(string id)
        {
            try
            {
                bool? gePersona = await _buscarXidGePersonaAD.buscarXcorreoBOOLEAN(id);
                if (gePersona == false)
                {
                    Console.WriteLine("No se encontró la persona.");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en metodo buscarXcorreoBOOLEAN de clase BuscarXidGePersonaLN: {ex.Message}");
                return null;
            }
        }
        #endregion

        #region Bool x Telefono1
        public async Task<bool?> buscarXtelefono1BOOLEAN(string id)
        {
            try
            {
                bool? gePersona = await _buscarXidGePersonaAD.buscarXtelefono1BOOLEAN(id);
                if (gePersona == false)
                {
                    Console.WriteLine("No se encontró la persona.");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en metodo buscarXtelefono1BOOLEAN de clase BuscarXidGePersonaLN: {ex.Message}");
                return null;
            }
        }
        #endregion

        #region Bool Numero de Telefono2
        public async Task<bool?> buscarXtelefono2BOOLEAN(string id)
        {
            try
            {
                bool? gePersona = await _buscarXidGePersonaAD.buscarXtelefono2BOOLEAN(id);
                if (gePersona == false)
                {
                    Console.WriteLine("No se encontró la persona.");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en metodo buscarXtelefono2BOOLEAN de clase BuscarXidGePersonaLN: {ex.Message}");
                return null;
            }
        }
        #endregion
    }
}
