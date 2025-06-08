using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CitasTipo.Editar
{
    public class EditarCitasTipoAD : IEditarCitasTipoAD
    {
        private readonly Contexto _contexto;
        public EditarCitasTipoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> editar(TCitasTipo editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                _contexto.TCitasTipos.Update(editar);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarCitasTipoAD : {ex.Message}");
                return -1;
            }

        }
    }
}