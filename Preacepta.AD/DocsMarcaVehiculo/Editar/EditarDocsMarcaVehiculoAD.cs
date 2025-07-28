using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsMarcaVehiculo.Editar
{
    public class EditarDocsMarcaVehiculoAD : IEditarDocsMarcaVehiculoAD
    {
        private readonly Contexto _contexto;
        public EditarDocsMarcaVehiculoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> Editar(TDocsMarcaVehiculo editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                _contexto.TDocsMarcaVehiculos.Update(editar);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarDocsMarcaVehiculoAD : {ex.Message}");
                return -1;
            }

        }
    }
}
