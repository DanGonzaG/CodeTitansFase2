using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeRedesSociales.Editar
{
    public class EditarRedesSocialesAD : IEditarRedesSocialesAD
    {
        private readonly Contexto _contexto;
        public EditarRedesSocialesAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> Editar(TGeRedesSociale editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                _contexto.TGeRedesSociales.Update(editar);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarRedesSocialesAD : {ex.Message}");
                return -1;
            }
        }
    }
}
