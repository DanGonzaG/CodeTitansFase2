using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocPoderesEspecialesJudiciales.Listar
{
    public class ListarPoderJudAD : IListarPoderJudAD
    {
        private readonly Contexto _contexto;

        public ListarPoderJudAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<DocsPoderesEspecialesJudicialeDTO>> listar2()
        {
            List<DocsPoderesEspecialesJudicialeDTO> lista = await (from doc in _contexto.TDocsPoderesEspecialesJudiciales
                                select new DocsPoderesEspecialesJudicialeDTO
                                {
                                    IdDoc = doc.IdDoc,
                                    Fecha = doc.Fecha.ToString("yyyy-MM-dd HH:mm"),
                                    IdAbogado = doc.IdAbogado,
                                    IdCliente = doc.IdCliente,
                                    Texto = doc.Texto,
                                    IdAbogadoNavigation = doc.IdAbogadoNavigation,
                                    IdClienteNavigation = doc.IdClienteNavigation
                                }).ToListAsync();
            return lista;
        }

        public async Task<List<DocsPoderesEspecialesJudicialeDTO>> Listar()
        {
            try
            {
                return await _contexto.TDocsPoderesEspecialesJudiciales.Select(doc => new DocsPoderesEspecialesJudicialeDTO
                {
                    IdDoc = doc.IdDoc,
                    Fecha = doc.Fecha.ToString("yyyy-MM-dd HH:mm"),
                    IdAbogado = doc.IdAbogado,
                    IdCliente = doc.IdCliente,
                    Texto = doc.Texto,
                    IdAbogadoNavigation = doc.IdAbogadoNavigation,
                    IdClienteNavigation = doc.IdClienteNavigation
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos {ex.Message}");
                return new List<DocsPoderesEspecialesJudicialeDTO>();
            }
        }
    }
}
