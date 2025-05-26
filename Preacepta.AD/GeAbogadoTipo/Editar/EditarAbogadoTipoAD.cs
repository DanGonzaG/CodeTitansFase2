using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeAbogadoTipo.Editar
{
    public class EditarAbogadoTipoAD : IEditarAbogadoTipoAD
    {
        private readonly Contexto _contexto;
        public EditarAbogadoTipoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> editar(TGeAbogadoTipo geAbogadoTipo)
        {
            if (geAbogadoTipo == null)
            {
                return 0;
            }

            try
            {
                _contexto.TGeAbogadoTipos.Update(geAbogadoTipo);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarAbogadoTipoAD : {ex.Message}");
                return -1;
            }

        }
    }
}
