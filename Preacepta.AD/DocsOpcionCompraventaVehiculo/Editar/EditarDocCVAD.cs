using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsOpcionCompraventaVehiculo.Editar
{
    public class EditarDocCVAD : IEditarDocCVAD
    {
        private readonly Contexto _contexto;

        public EditarDocCVAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> editar(TDocsOpcionCompraventaVehiculo docCV)
        {
            if (docCV == null)
            {
                return 0;
            }

            try
            {
                _contexto.TDocsOpcionCompraventaVehiculos.Update(docCV);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarDocCVAD : {ex.Message}");
                return -1;
            }
        }

    }
}
