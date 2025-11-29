using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GePersona.BuscarXid
{
    public class BuscarXidGePersonaAD : IBuscarXidGePersonaAD
    {
        private readonly Contexto _contexto;

        public BuscarXidGePersonaAD(Contexto contexto)
        {
            _contexto = contexto;
        }


        //Metodos para retornar objetos completos
        #region Buscar x PK cedula
        public async Task<TGePersona?> buscar(int id)
        {
            try
            {  
                var tGePersona = await _contexto.TGePersonas
                    .Include(t => t.Direccion1Navigation)
                    .ThenInclude(a => a.IdCatonNavigation)                    
                    .FirstOrDefaultAsync(m => m.Cedula == id);
                return tGePersona;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarXidGePersonaAD, no se encontro id: {ex.Message}");
                return null;
            }
        }
        #endregion

        #region Buscar x Numero de Cedula
        public async Task<TGePersona?> buscarXnumCedula(string id)
        {
            try
            {
                var tGePersona = await _contexto.TGePersonas
                    .Include(t => t.Direccion1Navigation)
                    .ThenInclude(a => a.IdCatonNavigation)                    
                    .FirstOrDefaultAsync(m => m.NumCedula == id);
                return tGePersona;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarXidGePersonaAD, no se encontro id: {ex.Message}");
                return null;
            }
        }
        #endregion

        #region buscar X correo
        public async Task<TGePersona?> buscarXcorreo(string correo)
        {
            try
            {
                var tGePersona = await _contexto.TGePersonas
                    .Include(t => t.Direccion1Navigation)
                    .ThenInclude(a => a.IdCatonNavigation)
                    .ThenInclude (b => b.IdProvinciaNavigation)
                    .FirstOrDefaultAsync(m => m.Email == correo);
                return tGePersona;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarXidGePersonaAD, no se encontro id: {ex.Message}");
                return null;
            }

        }
        #endregion

        #region Buscar X telefono1
        public async Task<TGePersona?> buscarXtelefono1(string telefono)
        {
            try
            {
                var tGePersona = await _contexto.TGePersonas
                    .Include(t => t.Direccion1Navigation)
                    .ThenInclude(a => a.IdCatonNavigation)
                    .ThenInclude(b => b.IdProvinciaNavigation)
                    .FirstOrDefaultAsync(m => m.Telefono1 == telefono);
                return tGePersona;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarXidGePersonaAD, no se encontro id: {ex.Message}");
                return null;
            }
        }
        #endregion

        #region Buscar X telefono2
        public async Task<TGePersona?> buscarXtelefono2(string telefono)
        {
            try
            {
                var tGePersona = await _contexto.TGePersonas
                    .Include(t => t.Direccion1Navigation)
                    .ThenInclude(a => a.IdCatonNavigation)
                    .ThenInclude(b => b.IdProvinciaNavigation)
                    .FirstOrDefaultAsync(m => m.Telefono2 == telefono);
                return tGePersona;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarXidGePersonaAD, no se encontro id: {ex.Message}");
                return null;
            }
        }
        #endregion


        //Metodo Booleanos para corroborar existencia del objeto

        #region Bool X Numero de cedula
        public async Task<bool?> buscarXnumCedulaBOOLEAN(string id)
        {
            try
            {
                var tGePersona = await _contexto.TGePersonas
                    .FirstOrDefaultAsync(m => m.NumCedula == id);
                if (tGePersona == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarXidGePersonaAD, metodo buscarXnumCedulaBOOLEAN, no se encontro id: {ex.Message}");
                return null;
            }
        }
        #endregion

        #region Bool X Correo
        public async Task<bool?> buscarXcorreoBOOLEAN(string correo)
        {
            try
            {
                var tGePersona = await _contexto.TGePersonas
                    .FirstOrDefaultAsync(m => m.Email == correo);
                if (tGePersona == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarXidGePersonaAD, metodo buscarXcorreoBOOLEAN, no se encontro id: {ex.Message}");
                return null;
            }
        }
        #endregion

        #region Bool Telefono1
        public async Task<bool?> buscarXtelefono1BOOLEAN(string telefono)
        {
            try
            {
                var tGePersona = await _contexto.TGePersonas
                    .FirstOrDefaultAsync(m => m.Telefono1 == telefono);
                if (tGePersona == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarXidGePersonaAD, metodo buscarXtelefono1BOOLEAN, no se encontro id: {ex.Message}");
                return null;
            }
        }
        #endregion

        #region Bool Telefono 2
        public async Task<bool?> buscarXtelefono2BOOLEAN(string telefono)
        {
            try
            {
                var tGePersona = await _contexto.TGePersonas
                    .FirstOrDefaultAsync(m => m.Telefono2 == telefono);
                if (tGePersona == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarXidGePersonaAD, metodo buscarXtelefono2BOOLEAN, no se encontro id: {ex.Message}");
                return null;
            }
        }
        #endregion
    }
}
