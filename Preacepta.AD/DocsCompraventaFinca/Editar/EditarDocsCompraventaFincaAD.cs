using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsCompraventaFinca.Editar
{
    public class EditarDocsCompraventaFincaAD : IEditarDocsCompraventaFincaAD
    {
        private readonly Contexto _contexto;
        public EditarDocsCompraventaFincaAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> Editar(TDocsCompraventaFinca editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                _contexto.TDocsCompraventaFincas.Update(editar);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarDocsCompraventaFincaAD : {ex.Message}");
                return -1;
            }

        }
    }
}
