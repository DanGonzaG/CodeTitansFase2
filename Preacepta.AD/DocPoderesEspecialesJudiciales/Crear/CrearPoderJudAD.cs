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
            if (poderJud == null) return -1;

            try
            {
                await _contexto.TDocsPoderesEspecialesJudiciales.AddAsync(poderJud);
                await _contexto.SaveChangesAsync();
                return poderJud.IdDoc; 
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine($"Error en CrearPoderJudAD: {msg}");
                return 0;
            }
        }

    }
}
