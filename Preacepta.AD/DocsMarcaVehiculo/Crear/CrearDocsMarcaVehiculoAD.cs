using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsMarcaVehiculo.Crear
{
    public class CrearDocsMarcaVehiculoAD : ICrearDocsMarcaVehiculoAD
    {
        private readonly Contexto _contexto;

        public CrearDocsMarcaVehiculoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(TDocsMarcaVehiculo crear)
        {
            if (crear == null)
            {
                Console.WriteLine("El objeto recibo fue nulo");
                return -1;
            }
            try
            {
                await _contexto.TDocsMarcaVehiculos.AddAsync(crear);
                int guardado = await _contexto.SaveChangesAsync();
                return guardado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearDocsMarcaVehiculoAD {ex.Message}");
                return 0;
            }


        }
    }
}
