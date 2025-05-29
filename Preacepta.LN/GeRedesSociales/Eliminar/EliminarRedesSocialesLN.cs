using Preacepta.AD.CasosTipo.Eliminar;
using Preacepta.AD.GeRedesSociales.Eliminar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeRedesSociales.Eliminar
{
    public class EliminarRedesSocialesLN : IEliminarRedesSocialesLN
    {
        private readonly IEliminarRedesSocialesAD _eliminar;

        public EliminarRedesSocialesLN(IEliminarRedesSocialesAD eliminar)
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
                Console.WriteLine($"Error en: EliminarRedesSocialesLN {ex.Message}");
                return -1;
            }
        }
    }
}
