using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CitasTipo.Listar
{
    public class ListarCitasTipoAD : IListarCitasTipoAD
    {
        private readonly Contexto _contexto;

        public ListarCitasTipoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<CitasTipoDTO>> listar()
        {
            try
            {
                return await _contexto.TCitasTipos.Select(tipo => new CitasTipoDTO
                {
                    Id = tipo.Id,
                    Nombre = tipo.Nombre

                }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos {ex.Message}");
                return new List<CitasTipoDTO>();
            }

        }
    }
}
