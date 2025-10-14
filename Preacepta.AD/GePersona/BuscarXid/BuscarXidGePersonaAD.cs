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
    }
}
