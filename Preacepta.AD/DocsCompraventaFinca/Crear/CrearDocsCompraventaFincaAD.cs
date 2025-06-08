using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsCompraventaFinca.Crear
{
    public class CrearDocsCompraventaFincaAD : ICrearDocsCompraventaFincaAD
    {
        private readonly Contexto _contexto;

        public CrearDocsCompraventaFincaAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(TDocsCompraventaFinca crear)
        {
            if (crear == null)
            {
                Console.WriteLine("El objeto recibo fue nulo");
                return -1;
            }
            try
            {
                await _contexto.TDocsCompraventaFincas.AddAsync(crear);
                int guardado = await _contexto.SaveChangesAsync();
                return guardado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearDocsCompraventaFincaAD {ex.Message}");
                return 0;
            }


        }
    }
}
