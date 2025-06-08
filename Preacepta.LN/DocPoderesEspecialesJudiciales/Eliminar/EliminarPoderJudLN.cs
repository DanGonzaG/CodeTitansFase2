using Preacepta.AD.DocPoderesEspecialesJudiciales.Eliminar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocPoderesEspecialesJudiciales.Eliminar
{
    public class EliminarPoderJudLN : IEliminarPoderJudLN
    {
        private readonly IEliminarPoderJudAD _eliminar;

        public EliminarPoderJudLN(IEliminarPoderJudAD eliminar)
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
                Console.WriteLine($"Error en: EliminarPoderJudLN {ex.Message}");
                return -1;
            }
        }
    }
}
