using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Casos.Listar
{
    public class ListarCasosAD : IListarCasosAD
    {
        private readonly Contexto _contexto;

        public ListarCasosAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<CasoDTO>> listar()
        {
            try
            {
                return await _contexto.TCasos.Select(lista => new CasoDTO
                {
                    IdCaso = lista.IdCaso,
                    Fecha = lista.Fecha.ToString("dd-MM-yyyy"),
                    IdTipoCaso = lista.IdTipoCaso,
                    Descripcion = lista.Descripcion,
                    IdAbogado = lista.IdAbogado,
                    IdCliente = lista.IdCliente,
                    Activo = lista.Activo,
                    IdAbogadoNavigation = lista.IdAbogadoNavigation,
                    IdClienteNavigation = lista.IdClienteNavigation,
                    IdTipoCasoNavigation = lista.IdTipoCasoNavigation,
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos, clase ListarCasosAD {ex.Message}");
                return new List<CasoDTO>();
            }

        }
    }
}
