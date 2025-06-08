using Preacepta.AD.DocsOpcionCompraventaVehiculo.Buscar;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsOpcionCompraventaVehiculo.Eliminar
{
    public class EliminarDocCVAD : IEliminarDocCVAD
    {
        private readonly Contexto _contexto;
        private readonly IBuscarDocCVAD _buscarDocCVAD;

        public EliminarDocCVAD(Contexto contexto, IBuscarDocCVAD buscarDocCVAD)
        {
            _contexto = contexto;
            _buscarDocCVAD = buscarDocCVAD;
        }

        public async Task<int> eliminar(int id)
        {
            try
            {
                TDocsOpcionCompraventaVehiculo? encontrado = await _buscarDocCVAD.buscar(id);
                if (encontrado == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                _contexto.TDocsOpcionCompraventaVehiculos.Remove(encontrado);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarTestimonioAD: {ex.Message}");
                return -1;
            }
        }

    }
}
