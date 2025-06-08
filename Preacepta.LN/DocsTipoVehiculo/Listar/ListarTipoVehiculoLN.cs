using Preacepta.AD.DocsTipoVehiculo.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsTipoVehiculo.Listar
{
    public class ListarTipoVehiculoLN : IListarTipoVehiculoLN
    {
        private readonly IListarTipoVehiculoAD _listar;

        public ListarTipoVehiculoLN(IListarTipoVehiculoAD listar)
        {
            _listar = listar;
        }

        public async Task<List<DocsTipoVehiculoDTO>> Listar()
        {
            try
            {
                List<DocsTipoVehiculoDTO> lista = await _listar.Listar();
                if (lista == null || !lista.Any())
                {
                    Console.WriteLine("No se encontraron Tipo vehiculo");
                }

                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al ListarTipoVehiculoLN: {ex.Message}");
                return new List<DocsTipoVehiculoDTO>();
            }
        }

    }
}
