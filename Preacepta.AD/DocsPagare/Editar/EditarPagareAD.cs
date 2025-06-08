using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsPagare.Editar
{
    public class EditarPagareAD : IEditarPagareAD
    {
        private readonly Contexto _contexto;

        public EditarPagareAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> editar(TDocsPagare pagare)
        {
            if (pagare == null)
            {
                return 0;
            }

            try
            {
                _contexto.TDocsPagares.Update(pagare);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarPagareAD : {ex.Message}");
                return -1;
            }
        }

    }
}
