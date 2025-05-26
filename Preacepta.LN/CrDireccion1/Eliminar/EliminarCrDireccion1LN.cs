using Preacepta.AD.CasosTipo.Eliminar;
using Preacepta.AD.CrDireccion1.Eliminar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CrDireccion1.Eliminar
{
    public class EliminarCrDireccion1LN : IEliminarCrDireccion1LN
    {
        private readonly IEliminarCrDireccion1AD _eliminar;

        public EliminarCrDireccion1LN(IEliminarCrDireccion1AD eliminar)
        {
            _eliminar = eliminar;
        }

        public async Task<int> EliminarProvincia(int id)
        {
            if (id < 0)
            {
                Console.WriteLine("el valor de id en menor a 1");
                return 0;
            }
            try
            {
                int bandera = await _eliminar.EliminarProvincia(id);
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en: EliminarCrDireccion1LN-EliminarProvincia {ex.Message}");
                return -1;
            }
        }

        public async Task<int> EliminarCanton(int id)
        {
            if (id < 0)
            {
                Console.WriteLine("el valor de id en menor a 1");
                return 0;
            }
            try
            {
                int bandera = await _eliminar.EliminarCanton(id);
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en: EliminarCrDireccion1LN-EliminarCanton {ex.Message}");
                return -1;
            }
        }

        public async Task<int> EliminarDistrito(int id)
        {
            if (id < 0)
            {
                Console.WriteLine("el valor de id en menor a 1");
                return 0;
            }
            try
            {
                int bandera = await _eliminar.EliminarDistrito(id);
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en: EliminarCrDireccion1LN-EliminarDistrito {ex.Message}");
                return -1;
            }
        }
    }
}
