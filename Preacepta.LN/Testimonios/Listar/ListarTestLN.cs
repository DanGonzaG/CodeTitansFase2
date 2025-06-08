using Preacepta.AD.Testimonios.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Testimonios.Listar
{
    public class ListarTestLN : IListarTestLN
    {
        private readonly IListarTestAD _listarTestAD;

        public ListarTestLN(IListarTestAD listarTestAD)
        {
            _listarTestAD = listarTestAD;
        }

        public async Task<List<TTestimonioDTO>> Listar()
        {
            try
            {
                List<TTestimonioDTO> lista = await _listarTestAD.Listar();
                if (lista == null || !lista.Any())
                {
                    Console.WriteLine("No se encontraron testimonios");
                }

                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar testimonios: {ex.Message}");
                return new List<TTestimonioDTO>();
            }
        }

        public async Task<List<TTestimonioDTO>> ListarTodosSinFiltro()
        {
            try
            {
                List<TTestimonioDTO> lista = await _listarTestAD.listar2();
                if (lista == null || !lista.Any())
                {
                    Console.WriteLine("No se encontraron testimonios");
                }

                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar testimonios sin filtro: {ex.Message}");
                return new List<TTestimonioDTO>();
            }
        }

    }
}
