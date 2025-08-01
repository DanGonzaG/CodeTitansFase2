﻿using Preacepta.AD.CasosTipo.Eliminar;
using Preacepta.AD.DocsInscripcionVehiculo.Eliminar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsInscripcionVehiculo.Eliminar
{
    public class EliminarDocsInscripcionVehiculoLN : IEliminarDocsInscripcionVehiculoLN
    {
        private readonly IEliminarDocsInscripcionVehiculoAD _eliminar;

        public EliminarDocsInscripcionVehiculoLN(IEliminarDocsInscripcionVehiculoAD eliminar)
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
                Console.WriteLine($"Error en: EliminarDocsInscripcionVehiculoLN {ex.Message}");
                return -1;
            }
        }
    }
}
