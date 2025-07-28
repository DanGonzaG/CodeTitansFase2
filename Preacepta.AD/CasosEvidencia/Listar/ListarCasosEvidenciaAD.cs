using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.AD.CasosEvidencia.Listar
{
    public class ListarCasosEvidenciaAD : IListarCasosEvidenciaAD
    {
        private readonly Contexto _contexto;

        public ListarCasosEvidenciaAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<CasosEvidenciaDTO>> listar()
        {
            try
            {
                return await _contexto.TCasosEvidencias.Select(lista => new CasosEvidenciaDTO
                {
                    IdEvidencia = lista.IdEvidencia,
                    IdCaso = lista.IdCaso,
                    Archivo = lista.Archivo,
                    Titulo = lista.Titulo,
                    IdCasoNavigation = lista.IdCasoNavigation,
                    IdCaso1 = lista.IdCaso1

                }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos ListarCasosEvidenciaAD {ex.Message}");
                return new List<CasosEvidenciaDTO>();
            }

        }
    }
}
