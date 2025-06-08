using Preacepta.AD.DocsTipoVehiculo.Eliminar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsTipoVehiculo.Eliminar
{
    public class EliminarTipoVehiculoLN : IEliminarTipoVehiculoLN
    {
        private readonly IEliminarTipoVehiculoAD _eliminar;

        public EliminarTipoVehiculoLN(IEliminarTipoVehiculoAD eliminar)
        {
            _eliminar = eliminar;
        }

        public async Task<int> eliminar(int id)
        {
            if (id < 0)
            {
                Console.WriteLine("el valor de id en menor a 1");
                return 0;
            }
            try
            {
                int bandera = await _eliminar.eliminar(id);
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en: EliminarTipoVehiculoLN {ex.Message}");
                return -1;
            }
        }

    }
}
