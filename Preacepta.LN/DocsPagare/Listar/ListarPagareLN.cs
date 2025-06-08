using Preacepta.AD.DocsPagare.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsPagare.Listar
{
    public class ListarPagareLN : IListarPagareLN
    {
        private readonly IListarPagareAD _listarPagareAD;

        public ListarPagareLN(IListarPagareAD listarPagareAD)
        {
            _listarPagareAD = listarPagareAD;
        }

        public async Task<List<DocsPagareDTO>> Listar()
        {
            try
            {
                List<DocsPagareDTO> lista = await _listarPagareAD.Listar();
                if (lista == null || !lista.Any())
                {
                    Console.WriteLine("No se encontraron Pagare");
                }

                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar Pagare: {ex.Message}");
                return new List<DocsPagareDTO>();
            }
        }

    }
}
