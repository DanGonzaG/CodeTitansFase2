using Microsoft.EntityFrameworkCore;
using Preacepta.AD.DocsPagare.Buscar;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsPagare.Eliminar
{
    public class EliminarPagareAD : IEliminarPagareAD
    {
        private readonly Contexto _contexto;
        private readonly IBuscarPagareAD _buscar;

        public EliminarPagareAD(Contexto contexto, IBuscarPagareAD buscar)
        {
            _contexto = contexto;
            _buscar = buscar;
        }

        public async Task<int> eliminar(int id)
        {
            if (id <= 0) return 0;

            try
            {
                var encontrado = await _buscar.buscar(id); 
                if (encontrado == null) return 0;

                encontrado.CedulaDeudorNavigation = null;
                encontrado.CedulaFiadorNavigation = null;
                encontrado.CedulaAbogadoNavigation = null;
                encontrado.LugarPagoNavigation = null;

                _contexto.Attach(encontrado);
                _contexto.Entry(encontrado).State = EntityState.Deleted;

                return await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"EliminarPagareAD: conflicto de FK al eliminar id={id}. Detalle: {dbEx.Message}");
                return -2;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EliminarPagareAD: error inesperado al eliminar id={id}. Detalle: {ex.Message}");
                return -1;
            }
        }
    }
}
