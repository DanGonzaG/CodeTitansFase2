using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocPoderesEspecialesJudiciales.Crear
{
    public class CrearPoderJudAD : ICrearPoderJudAD
    {
        private readonly Contexto _contexto;

        public CrearPoderJudAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(TDocsPoderesEspecialesJudiciale poderJud)
        {
            if (poderJud == null)
            {
                Console.WriteLine("El objeto recibo fue nulo");
                return -1;
            }
            try
            {
                await _contexto.TDocsPoderesEspecialesJudiciales.AddAsync(poderJud);
                int guardado = await _contexto.SaveChangesAsync();
                return guardado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearPoderJudAD {ex.Message}");
                return 0;
            }
        }

    }
}
