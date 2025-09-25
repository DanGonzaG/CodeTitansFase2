using Preacepta.AD.DocsPagare.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
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
                var lista = await _listarPagareAD.Listar();

                if (lista == null || !lista.Any())
                {
                    Console.WriteLine("No se encontraron Pagarés.");
                }

                return lista ?? new List<DocsPagareDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar Pagarés: {ex.Message}");
                return new List<DocsPagareDTO>();
            }
        }
    }
}
