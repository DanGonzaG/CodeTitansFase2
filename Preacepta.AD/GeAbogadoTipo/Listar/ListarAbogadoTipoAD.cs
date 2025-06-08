using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.AD.GeAbogadoTipo.Listar
{
    public class ListarAbogadoTipoAD : IListarAbogadoTipoAD
    {
        private readonly Contexto _contexto;

        public ListarAbogadoTipoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<GeAbogadoTipoDTO>> listar()
        {
            try
            {
                return await _contexto.TGeAbogadoTipos.Select(tipoAbogado => new GeAbogadoTipoDTO
                {
                    IdTipoAbogado = tipoAbogado.IdTipoAbogado,
                    Nombre = tipoAbogado.Nombre,
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos {ex.Message}");
                return new List<GeAbogadoTipoDTO>();
            }

        }
    }
}

