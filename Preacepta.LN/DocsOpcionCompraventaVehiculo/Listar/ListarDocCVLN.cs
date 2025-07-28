using Preacepta.AD.DocsOpcionCompraventaVehiculo.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsOpcionCompraventaVehiculo.Listar
{
    public class ListarDocCVLN : IListarDocCVLN
    {
        private readonly IListarDocCVAD _listarDocCVAD;
        
        public ListarDocCVLN(IListarDocCVAD listarDocCVAD)
        {
            _listarDocCVAD = listarDocCVAD;
        }

        public async Task<List<DocsOpcionCompraventaVehiculoDTO>> Listar()
        {
            try
            {
                List<DocsOpcionCompraventaVehiculoDTO> lista = await _listarDocCVAD.Listar();
                if (lista == null || !lista.Any())
                {
                    Console.WriteLine("No se encontraron testimonios");
                }

                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar testimonios: {ex.Message}");
                return new List<DocsOpcionCompraventaVehiculoDTO>();
            }
        }

    }
}
