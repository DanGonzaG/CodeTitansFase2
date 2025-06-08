using Preacepta.AD.CasosTipo.Eliminar;
using Preacepta.AD.DocsCombustible.Eliminar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsCombustible.Eliminar
{
    public class EliminarDocsCombustibleLN : IEliminarDocsCombustibleLN
    {
        private readonly IEliminarDocsCombustibleAD _eliminar;

        public EliminarDocsCombustibleLN(IEliminarDocsCombustibleAD eliminar)
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
                Console.WriteLine($"Error en: EliminarDocsCombustibleLN {ex.Message}");
                return -1;
            }
        }
    }
}
