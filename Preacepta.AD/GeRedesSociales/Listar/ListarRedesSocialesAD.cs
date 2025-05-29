using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeRedesSociales.Listar
{
    public class ListarRedesSocialesAD : IListarRedesSocialesAD
    {
        private readonly Contexto _contexto;

        public ListarRedesSocialesAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<GeRedesSocialeDTO>> listar()
        {
            try
            {
                return await _contexto.TGeRedesSociales.Select(lista => new GeRedesSocialeDTO
                {
                    Cedula = lista.Cedula,
                    CedulaNavigation = lista.CedulaNavigation,
                    IdRs = lista.IdRs,
                    LinkRedSocila = lista.LinkRedSocila,
                    

                }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos {ex.Message}");
                return new List<GeRedesSocialeDTO>();
            }

        }
    }
}
