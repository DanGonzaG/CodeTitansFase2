using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeAbogado.Listar
{
    public class ListarAbogadoAD : IListarAbogadoAD
    {
        private readonly Contexto _contexto;

        public ListarAbogadoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<GeAbogadoDTO>> listar()
        {
            try
            {
                return await _contexto.TGeAbogados.Select(lista => new GeAbogadoDTO
                {
                    Cedula = lista.Cedula,
                    CedulaNavigation = lista.CedulaNavigation,
                    CJuridicaNavigation = lista.CJuridicaNavigation,
                    IdTipoAbogado = lista.IdTipoAbogado,
                    IdTipoAbogadoNavigation = lista.IdTipoAbogadoNavigation,
                    Carnet = lista.Carnet,
                    CJuridica = lista.CJuridica,                    
                    
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos {ex.Message}");
                return new List<GeAbogadoDTO>();
            }

        }
    }
}
