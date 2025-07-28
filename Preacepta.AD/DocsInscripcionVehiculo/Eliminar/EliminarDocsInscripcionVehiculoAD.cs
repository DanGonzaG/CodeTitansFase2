using Preacepta.AD.CasosTipo.BuscarXid;
using Preacepta.AD.DocsInscripcionVehiculo.BuscarXid;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsInscripcionVehiculo.Eliminar
{
    public class EliminarDocsInscripcionVehiculoAD : IEliminarDocsInscripcionVehiculoAD
    {
        private readonly Contexto _contexto;
        private readonly IBuscarDocsInscripcionVehiculoAD _buscarXid;

        public EliminarDocsInscripcionVehiculoAD(Contexto contexto, IBuscarDocsInscripcionVehiculoAD buscarXid)
        {
            _contexto = contexto;
            _buscarXid = buscarXid;
        }

        public async Task<int> Eliminar(int id)
        {

            try
            {
                TDocsInscripcionVehiculo? encontrado = await _buscarXid.buscar(id);
                if (encontrado == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                _contexto.TDocsInscripcionVehiculos.Remove(encontrado);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarDocsInscripcionVehiculoAD: {ex.Message}");
                return -1;
            }

        }
    }
}
