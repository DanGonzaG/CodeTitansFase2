using Preacepta.AD.DocsTipoVehiculo.Buscar;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsTipoVehiculo.Eliminar
{
    public class EliminarTipoVehiculoAD : IEliminarTipoVehiculoAD
    {
        private readonly Contexto _contexto;
        private readonly IBuscarTipoVehiculoAD _buscar;

        public EliminarTipoVehiculoAD(Contexto contexto,
            IBuscarTipoVehiculoAD buscar)
        {
            _buscar = buscar;
            _contexto = contexto;
        }

        public async Task<int> eliminar(int id)
        {
            try
            {
                TDocsTipoVehiculo? encontrado = await _buscar.buscar(id);
                if (encontrado == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                _contexto.TDocsTipoVehiculos.Remove(encontrado);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarTipoVehiculoAD: {ex.Message}");
                return -1;
            }
        }

    }
}
