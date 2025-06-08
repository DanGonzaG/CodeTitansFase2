
using Preacepta.AD.CitasTipo.Eliminar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CitasTipo.Eliminar
{
    public class EliminarCitasTipoLN : IEliminarCitasTipoLN
    {
        private readonly IEliminarCitasTipoAD _eliminar;

        public EliminarCitasTipoLN(IEliminarCitasTipoAD eliminar)
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
                Console.WriteLine($"Error en: EliminarCitasTipoLN {ex.Message}");
                return -1;
            }


        }
    }
}
