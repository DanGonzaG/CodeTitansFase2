using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeNegocio.Listar
{
    public class ListarNegocioAD : IListarNegocioAD
    {
        private readonly Contexto _contexto;

        public ListarNegocioAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<GeNegocioDTO>> listar()
        {
            try
            {
                return await _contexto.TGeNegocios.Select(lista => new GeNegocioDTO
                {
                    CJuridica = lista.CJuridica,
                    Nombre = lista.Nombre,
                    Email = lista.Email,
                    FechaConsolidacion = lista.FechaConsolidacion.ToString("dd-MM-yyyy"),
                    Representante = lista.Representante,
                    Telefono = lista.Telefono,
                    Direccion1 = lista.Direccion1,
                    Direccion2 = lista.Direccion2,
                    
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos {ex.Message}");
                return new List<GeNegocioDTO>();
            }

        }
    }
}
