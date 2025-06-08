using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                var lista = await _contexto.TDocsPagares.FindAsync(id);
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarPagareAD, no se encontro id: {ex.Message}");
                return null;
            }
        }
    }
}
