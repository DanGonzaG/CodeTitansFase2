using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.CasosEtapa.BuscarXid
{
    public class BuscarCasosEtapasAD : IBuscarCasosEtapasAD
    {
        private readonly Contexto _contexto;

        public BuscarCasosEtapasAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<TCasosEtapa?> buscar(int id)
        {
            try
            {
                var lista = await _contexto.TCasosEtapas
                .Include(t => t.IdCasoNavigation)
                .FirstOrDefaultAsync(m => m.IdEtapaPl == id);
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarCasosEtapasAD, no se encontro id: {ex.Message}");
                return null;
            }

        }
    }
}
