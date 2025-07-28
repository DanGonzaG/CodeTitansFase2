using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsCombustible.Editar
{
    public class EditarDocsCombustibleAD : IEditarDocsCombustibleAD
    {
        private readonly Contexto _contexto;
        public EditarDocsCombustibleAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> Editar(TDocsCombustible editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                _contexto.TDocsCombustibles.Update(editar);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarDocsCombustibleAD : {ex.Message}");
                return -1;
            }

        }
    }
}
