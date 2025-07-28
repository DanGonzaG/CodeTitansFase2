using Preacepta.AD.CasosTipo.Eliminar;
using Preacepta.AD.DocsContratoPrestacionServicios.Eliminar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsContratoPrestacionServicios.Eliminar
{
    public class EliminarDocsContratoPrestacionServiciosLN : IEliminarDocsContratoPrestacionServiciosLN
    {
        private readonly IEliminarDocsContratoPrestacionServiciosAD _eliminar;

        public EliminarDocsContratoPrestacionServiciosLN(IEliminarDocsContratoPrestacionServiciosAD eliminar)
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
                Console.WriteLine($"Error en: EliminarDocsContratoPrestacionServiciosLN {ex.Message}");
                return -1;
            }
        }
    }
}
