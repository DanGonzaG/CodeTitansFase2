using Preacepta.Modelos.AbstraccionesBD;
using System;
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
                Console.WriteLine("CrearPagareAD: El objeto recibido fue nulo.");
                return 0;
            }

            try
            {
                await _contexto.TDocsPagares.AddAsync(pagare);
                await _contexto.SaveChangesAsync();

                return pagare.IdDocumento;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearPagareAD: {ex.Message}");
                return 0;
            }
        }
    }
}
