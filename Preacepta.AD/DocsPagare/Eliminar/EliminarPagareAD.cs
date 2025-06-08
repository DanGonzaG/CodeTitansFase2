using Preacepta.AD.DocsPagare.Buscar;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            try
            {
                TDocsPagare? encontrado = await _buscar.buscar(id);
                if (encontrado == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                _contexto.TDocsPagares.Remove(encontrado);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarPagareAD: {ex.Message}");
                return -1;
            }
        }

    }
}
