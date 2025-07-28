using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsCombustible.Listar
{
    public class ListarDocsCombustibleAD : IListarDocsCombustibleAD
    {
        private readonly Contexto _contexto;

        public ListarDocsCombustibleAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<DocsCombustibleDTO>> listar()
        {
            try
            {
                return await _contexto.TDocsCombustibles.Select(lista => new DocsCombustibleDTO
                {
                    Id = lista.Id,
                    Nombre = lista.Nombre,
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos {ex.Message}");
                return new List<DocsCombustibleDTO>();
            }

        }
    }
}
