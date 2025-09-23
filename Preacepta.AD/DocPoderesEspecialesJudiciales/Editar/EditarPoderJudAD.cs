using Microsoft.EntityFrameworkCore;
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

        // using Microsoft.EntityFrameworkCore;

        public async Task<int> editar(TDocsPoderesEspecialesJudiciale poderJud)
        {
            if (poderJud == null || poderJud.IdDoc <= 0) return 0;

            _contexto.TDocsPoderesEspecialesJudiciales.Attach(poderJud);

            // marca SOLO lo que realmente quieras cambiar:
            _contexto.Entry(poderJud).Property(x => x.IdAbogado).IsModified = true;

            // si envías NumCausa desde arriba:
            // _contexto.Entry(poderJud).Property(x => x.NumCausa).IsModified = true;

            return await _contexto.SaveChangesAsync();
        }

    }
}
