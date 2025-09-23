using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsPagare.Buscar
{
    public class BuscarPagareAD : IBuscarPagareAD
    {
        private readonly Contexto _contexto;

        public BuscarPagareAD(Contexto context)
        {
            _contexto = context;
        }

        public async Task<TDocsPagare?> buscar(int id)
        {
            try
            {
                var entity = await _contexto.TDocsPagares
                    .AsNoTracking()
                    .Include(x => x.CedulaDeudorNavigation)
                    .Include(x => x.CedulaFiadorNavigation)
                    .Include(x => x.LugarPagoNavigation)
                    .FirstOrDefaultAsync(x => x.IdDocumento == id);

                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarPagareAD (id={id}): {ex.Message}");
                return null;
            }
        }
    }
}
