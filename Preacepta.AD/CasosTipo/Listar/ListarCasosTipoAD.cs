using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.AD.CasosTipo.Listar
{
    public class ListarCasosTipoAD : IListarCasosTipoAD
    {
        private readonly Contexto _contexto;

        public ListarCasosTipoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<CasosTipoDTO>> listar()
        {
            try
            {
                return await _contexto.TCasosTipos.Select(lista => new CasosTipoDTO
                {
                    IdTipoCaso = lista.IdTipoCaso,
                    Nombre = lista.Nombre,
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos {ex.Message}");
                return new List<CasosTipoDTO>();
            }

        }
    }
}
