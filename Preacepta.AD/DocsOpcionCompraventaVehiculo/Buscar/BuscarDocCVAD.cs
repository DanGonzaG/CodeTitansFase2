using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsOpcionCompraventaVehiculo.Buscar
{
    public class BuscarDocCVAD : IBuscarDocCVAD
    {
        private readonly Contexto _contexto;

        public BuscarDocCVAD (Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<TDocsOpcionCompraventaVehiculo?> buscar(int id)
        {
            try
            {
                var lista = await _contexto.TDocsOpcionCompraventaVehiculos.FindAsync(id);
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarDocCVAD, no se encontro id: {ex.Message}");
                return null;
            }
        }

    }
}
