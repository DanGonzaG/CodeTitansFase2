
using Preacepta.AD.Citas.Eliminar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Citas.Eliminar
{
    public class EliminarCitasLN : IEliminarCitasLN
    {
        private readonly IEliminarCitasAD _eliminar;

        public EliminarCitasLN(IEliminarCitasAD eliminar)
        {
            _eliminar = eliminar;
        }

        public async Task<int> Eliminar(int id)
        {
            if (id < 0)
            {
                Console.WriteLine("el valor de id en menor a 1");
                return 0;
            }
            try
            {
                int bandera = await _eliminar.Eliminar(id);
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en: EliminarCitasLN {ex.Message}");
                return -1;
            }


        }
    }
}
