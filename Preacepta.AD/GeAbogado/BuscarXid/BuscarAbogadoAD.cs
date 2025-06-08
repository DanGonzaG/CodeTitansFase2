using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GeAbogado.BuscarXid
{
    public class BuscarAbogadoAD : IBuscarAbogadoAD
    {
        private readonly Contexto _contexto;

        public BuscarAbogadoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<TGeAbogado?> buscar(int id)
        {
            try
            {
                var lista = await _contexto.TGeAbogados
                .Include(t => t.CJuridicaNavigation)
                .Include(t => t.CedulaNavigation)
                .Include(t => t.IdTipoAbogadoNavigation)
                .FirstOrDefaultAsync(m => m.Cedula == id);
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarAbogadoAD metodo buscar, no se encontro carnet: {ex.Message}");
                return null;
            }
        }

        public async Task<TGeAbogado?> buscarXcarnet(int carnet)
        {
            try
            {
                var lista = await _contexto.TGeAbogados
                .Include(t => t.CJuridicaNavigation)
                .Include(t => t.CedulaNavigation)
                .Include(t => t.IdTipoAbogadoNavigation)
                .FirstOrDefaultAsync(m => m.Carnet == carnet);
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarAbogadoAD metodo buscarXcarnet, no se encontro carnet: {ex.Message}");
                return null;
            }

        }
    }
}
