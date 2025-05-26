using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CasosTipo.Editar
{
    public class EditarCasosTiposAD : IEditarCasosTiposAD
    {
        private readonly Contexto _contexto;
        public EditarCasosTiposAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> Editar(TCasosTipo editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                _contexto.TCasosTipos.Update(editar);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarCasosTiposAD : {ex.Message}");
                return -1;
            }

        }
    }
}
