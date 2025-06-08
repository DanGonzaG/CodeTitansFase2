using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsTipoVehiculo.Crear
{
    public class CrearTipoVehiculoAD : ICrearTipoVehiculoAD
    {
        private readonly Contexto _contexto;

        public CrearTipoVehiculoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(TDocsTipoVehiculo tipovehiculo)
        {
            if (tipovehiculo == null)
            {
                Console.WriteLine("El objeto recibo fue nulo");
                return -1;
            }
            try
            {
                await _contexto.TDocsTipoVehiculos.AddAsync(tipovehiculo);
                int guardado = await _contexto.SaveChangesAsync();
                return guardado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearTipoVehiculoAD {ex.Message}");
                return 0;
            }
        }

    }
}
