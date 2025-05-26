using Preacepta.AD.GePersona.Eliminar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GePersona.Eliminar
{
    public class EliminarPersonaLN : IEliminarPersonaLN
    {
        private readonly IEliminarPersonaAD _eliminarPersonaAD;

        public EliminarPersonaLN (IEliminarPersonaAD eliminarPersonaAD) 
        {
            _eliminarPersonaAD = eliminarPersonaAD;
        }

        public async Task<int> eliminar (int id) 
        {
            if (id < 0) 
            {
                Console.WriteLine("el valor de id en menor a 1");
                return 0;
            }
            try 
            {
                int bandera = await _eliminarPersonaAD.eliminar(id);
                return bandera;
            } 
            catch (Exception ex) 
            {
                Console.WriteLine($"Error en: EliminarPersonaLN {ex.Message}");
                return -1;
            }
           

        }
    }
}
