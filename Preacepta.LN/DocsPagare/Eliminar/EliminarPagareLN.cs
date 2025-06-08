using Preacepta.AD.DocsPagare.Eliminar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsPagare.Eliminar
{
    public class EliminarPagareLN : IEliminarPagareLN
    {
        private IEliminarPagareAD _eliminar;

        public EliminarPagareLN(IEliminarPagareAD eliminar)
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
                Console.WriteLine($"Error en: EliminarPagareLN {ex.Message}");
                return -1;
            }
        }

    }
}
