using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocPoderesEspecialesJudiciales.Editar
{
    public class EditarPoderJudAD : IEditarPoderJudAD
    {
        private readonly Contexto _contexto;

        public EditarPoderJudAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> editar(TDocsPoderesEspecialesJudiciale poderJud)
        {
            if (poderJud == null)
            {
                return 0;
            }

            try
            {
                _contexto.TDocsPoderesEspecialesJudiciales.Update(poderJud);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarPoderJudAD : {ex.Message}");
                return -1;
            }
        }

    }
}
