using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Testimonios.Crear
{
    public class CrearTestAD : ICrearTestAD
    {
        private readonly Contexto _contexto;

        public CrearTestAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(TTestimonio test)
        {
            if (test == null)
            {
                Console.WriteLine("El objeto recibo fue nulo");
                return -1;
            }
            try
            {
                await _contexto.TTestimonios.AddAsync(test);
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
