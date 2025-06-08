using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.AD.CasosEtapa.Listar
{
    public class ListarCasosEtapasAD : IListarCasosEtapasAD
    {
        private readonly Contexto _contexto;

        public ListarCasosEtapasAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<CasosEtapaDTO>> listar()
        {
            try
            {
                return await _contexto.TCasosEtapas.Select(lista => new CasosEtapaDTO
                {
                    IdEtapaPl = lista.IdEtapaPl,
                    Nombre = lista.Nombre,
                    Fecha = lista.Fecha.ToString("dd-MM-yyyy"),
                    Descripcion = lista.Descripcion,
                    IdCaso = lista.IdCaso,
                    IdCasoNavigation = lista.IdCasoNavigation,
                    Activo = lista.Activo
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos {ex.Message}");
                return new List<CasosEtapaDTO>();
            }

        }

        public async Task<List<CasosEtapaDTO>> listarXcaso(int id)
        {
            try
            {
                return await _contexto.TCasosEtapas.Where(a => a.IdCaso == id).Select(lista => new CasosEtapaDTO
                {
                    IdEtapaPl = lista.IdEtapaPl,
                    Nombre = lista.Nombre,
                    Fecha = lista.Fecha.ToString("dd-MM-yyyy"),
                    Descripcion = lista.Descripcion,
                    IdCaso = lista.IdCaso,
                    IdCasoNavigation = lista.IdCasoNavigation,
                    Activo = lista.Activo
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos {ex.Message}");
                return new List<CasosEtapaDTO>();
            }

        }
    }
}
