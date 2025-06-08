using Preacepta.AD.DocPoderesEspecialesJudiciales.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocPoderesEspecialesJudiciales.Listar
{
    public class ListarPoderJudLN : IListarPoderJudLN
    {
        private readonly IListarPoderJudAD _listar;

        public ListarPoderJudLN(IListarPoderJudAD listar)
        {
            _listar = listar;
        }

        public async Task<List<DocsPoderesEspecialesJudicialeDTO>> Listar()
        {
            try
            {
                List<DocsPoderesEspecialesJudicialeDTO> lista = await _listar.Listar();
                if (lista == null || !lista.Any())
                {
                    Console.WriteLine("No se encontraron Poderes");
                }

                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ListarPoderJudLN: {ex.Message}");
                return new List<DocsPoderesEspecialesJudicialeDTO>();
            }
        }

    }
}
