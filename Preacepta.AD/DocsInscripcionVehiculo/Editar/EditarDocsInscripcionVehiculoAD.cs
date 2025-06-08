using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsInscripcionVehiculo.Editar
{
    public class EditarDocsInscripcionVehiculoAD : IEditarDocsInscripcionVehiculoAD
    {
        private readonly Contexto _contexto;
        public EditarDocsInscripcionVehiculoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> Editar(TDocsInscripcionVehiculo editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                _contexto.TDocsInscripcionVehiculos.Update(editar);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarDocsInscripcionVehiculoAD : {ex.Message}");
                return -1;
            }

        }
    }
}
