using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocPoderesEspecialesJudiciales.Buscar
{
    public class BuscarPoderJudAD : IBuscarPoderJudAD
    {
        private readonly Contexto _contexto;

        public BuscarPoderJudAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<TDocsPoderesEspecialesJudiciale?> buscar(int id)
        {
            try
            {
                var lista = await _contexto.TDocsPoderesEspecialesJudiciales.FindAsync(id);
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarPoderJudAD, no se encontro id: {ex.Message}");
                return null;
            }
        }

    }
}
