using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsPagare.Crear
{
    public class CrearPagareAD : ICrearPagareAD
    {
        private readonly Contexto _contexto;

        public CrearPagareAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(TDocsPagare pagare)
        {
            if (pagare == null)
            {
                Console.WriteLine("El objeto recibo fue nulo");
                return -1;
            }
            try
            {
                await _contexto.TDocsPagares.AddAsync(pagare);
                int guardado = await _contexto.SaveChangesAsync();
                return guardado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearTestAD {ex.Message}");
                return 0;
            }
        }
    }
}
