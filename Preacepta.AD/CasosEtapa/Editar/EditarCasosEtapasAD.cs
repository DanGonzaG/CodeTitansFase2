using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CasosEtapa.Editar
{
    public class EditarCasosEtapasAD : IEditarCasosEtapasAD
    {
        private readonly Contexto _contexto;
        public EditarCasosEtapasAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> Editar(TCasosEtapa editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                _contexto.TCasosEtapas.Update(editar);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarCasosEtapasAD : {ex.Message}");
                return -1;
            }
        }
    }
}
