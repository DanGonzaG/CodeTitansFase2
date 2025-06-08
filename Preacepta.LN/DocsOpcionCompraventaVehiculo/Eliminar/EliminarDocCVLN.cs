using Preacepta.AD.DocsOpcionCompraventaVehiculo.Eliminar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsOpcionCompraventaVehiculo.Eliminar
{
    public class EliminarDocCVLN : IEliminarDocCVLN
    {
        private readonly IEliminarDocCVAD _eliminarDocCV;

        public EliminarDocCVLN(IEliminarDocCVAD eliminarDocCV)
        {
            _eliminarDocCV = eliminarDocCV;
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
                int bandera = await _eliminarDocCV.eliminar(id);
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en: EliminarDocCVLN {ex.Message}");
                return -1;
            }
        }
    }
}
