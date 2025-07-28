using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsOpcionCompraventaVehiculo.Crear
{
    public class CrearDocCVAD : ICrearDocCVAD
    {
        private readonly Contexto _contexto;

        public CrearDocCVAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(TDocsOpcionCompraventaVehiculo docCV)
        {
            if (docCV == null)
            {
                Console.WriteLine("El objeto recibo fue nulo");
                return -1;
            }
            try
            {
                await _contexto.TDocsOpcionCompraventaVehiculos.AddAsync(docCV);
                int guardado = await _contexto.SaveChangesAsync();
                return guardado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearDocCVAD {ex.Message}");
                return 0;
            }
        }

    }
}
