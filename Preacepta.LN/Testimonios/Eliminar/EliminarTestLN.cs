using Preacepta.AD.Testimonios.Eliminar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Testimonios.Eliminar
{
    public class EliminarTestLN : IEliminarTestLN
    {
        private readonly IEliminarTestAD _eliminarTestAD;

        public EliminarTestLN(IEliminarTestAD eliminarTestAD)
        {
            _eliminarTestAD = eliminarTestAD;
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
                int bandera = await _eliminarTestAD.eliminar(id);
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en: EliminarTestLN {ex.Message}");
                return -1;
            }
        }
    }
}
