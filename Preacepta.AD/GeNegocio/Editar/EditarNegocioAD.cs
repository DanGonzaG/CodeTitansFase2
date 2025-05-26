using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeNegocio.Editar
{
    public class EditarNegocioAD : IEditarNegocioAD
    {
        private readonly Contexto _contexto;
        public EditarNegocioAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> Editar(TGeNegocio editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                _contexto.TGeNegocios.Update(editar);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarNegocioAD : {ex.Message}");
                return -1;
            }

        }
    }
}
