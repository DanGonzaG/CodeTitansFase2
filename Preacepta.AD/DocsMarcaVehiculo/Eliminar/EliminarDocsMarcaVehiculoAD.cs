using Preacepta.AD.CasosTipo.BuscarXid;
using Preacepta.AD.DocsMarcaVehiculo.BuscarXid;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsMarcaVehiculo.Eliminar
{
    public class EliminarDocsMarcaVehiculoAD : IEliminarDocsMarcaVehiculoAD
    {
        private readonly Contexto _contexto;
        private readonly IBuscarDocsMarcaVehiculoAD _buscarXid;

        public EliminarDocsMarcaVehiculoAD(Contexto contexto, IBuscarDocsMarcaVehiculoAD buscarXid)
        {
            _contexto = contexto;
            _buscarXid = buscarXid;
        }

        public async Task<int> Eliminar(int id)
        {

            try
            {
                TDocsMarcaVehiculo? encontrado = await _buscarXid.buscar(id);
                if (encontrado == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                _contexto.TDocsMarcaVehiculos.Remove(encontrado);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarDocsMarcaVehiculoAD: {ex.Message}");
                return -1;
            }

        }
    }
}
