using Preacepta.AD.Testimonios.Buscar;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Testimonios.Eliminar
{
    public class EliminarTestAD : IEliminarTestAD
    {
        private readonly Contexto _contexto;
        private readonly IBuscarTestAD _buscar;

        public EliminarTestAD(Contexto contexto, IBuscarTestAD buscarTestAD)
        {
            _contexto = contexto;
            _buscar = buscarTestAD;
        }

        public async Task<int> eliminar(int id)
        {
            try
            {
                TTestimonio? encontrado = await _buscar.buscar(id);
                if (encontrado == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                _contexto.TTestimonios.Remove(encontrado);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarTestimonioAD: {ex.Message}");
                return -1;
            }
        }
    }
}
