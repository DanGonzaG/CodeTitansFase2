using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsMarcaVehiculo.Listar
{
    public class ListarDocsMarcaVehiculoAD : IListarDocsMarcaVehiculoAD
    {
        private readonly Contexto _contexto;

        public ListarDocsMarcaVehiculoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<DocsMarcaVehiculoDTO>> listar()
        {
            try
            {
                return await _contexto.TDocsMarcaVehiculos.Select(lista => new DocsMarcaVehiculoDTO
                {
                    Id = lista.Id,
                    Nombre = lista.Nombre
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos {ex.Message}");
                return new List<DocsMarcaVehiculoDTO>();
            }

        }
    }
}
