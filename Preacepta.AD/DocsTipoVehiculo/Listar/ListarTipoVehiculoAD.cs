using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsTipoVehiculo.Listar
{
    public class ListarTipoVehiculoAD : IListarTipoVehiculoAD
    {
        private readonly Contexto _contexto;

        public ListarTipoVehiculoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<DocsTipoVehiculoDTO>> listar2()
        {
            List<DocsTipoVehiculoDTO> lista = await (from tipo in _contexto.TDocsTipoVehiculos
                                select new DocsTipoVehiculoDTO
                                {
                                    Id = tipo.Id,
                                    Nombre = tipo.Nombre,
                                    TDocsInscripcionVehiculos = tipo.TDocsInscripcionVehiculos,
                                    TDocsOpcionCompraventaVehiculos = tipo.TDocsOpcionCompraventaVehiculos
                                }).ToListAsync();
            return lista;
        }

        public async Task<List<DocsTipoVehiculoDTO>> Listar()
        {


            try
            {
                return await _contexto.TDocsTipoVehiculos.Select(tipo => new DocsTipoVehiculoDTO
                {
                    Id = tipo.Id,
                    Nombre = tipo.Nombre,
                    TDocsInscripcionVehiculos = tipo.TDocsInscripcionVehiculos,
                    TDocsOpcionCompraventaVehiculos = tipo.TDocsOpcionCompraventaVehiculos
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos {ex.Message}");
                return new List<DocsTipoVehiculoDTO>();
            }
        }
    }
}
