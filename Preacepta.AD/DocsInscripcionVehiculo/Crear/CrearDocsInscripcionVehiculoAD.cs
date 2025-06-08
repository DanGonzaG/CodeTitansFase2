using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsInscripcionVehiculo.Crear
{
    public class CrearDocsInscripcionVehiculoAD : ICrearDocsInscripcionVehiculoAD
    {
        private readonly Contexto _contexto;

        public CrearDocsInscripcionVehiculoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(TDocsInscripcionVehiculo crear)
        {
            if (crear == null)
            {
                Console.WriteLine("El objeto recibo fue nulo");
                return -1;
            }
            try
            {
                await _contexto.TDocsInscripcionVehiculos.AddAsync(crear);
                int guardado = await _contexto.SaveChangesAsync();
                return guardado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearDocsInscripcionVehiculoAD {ex.Message}");
                return 0;
            }


        }
    }
}
