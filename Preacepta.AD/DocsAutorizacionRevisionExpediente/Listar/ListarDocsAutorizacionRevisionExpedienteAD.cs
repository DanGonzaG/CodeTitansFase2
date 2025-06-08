using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsAutorizacionRevisionExpediente.Listar
{
    public class ListarDocsAutorizacionRevisionExpedienteAD : IListarDocsAutorizacionRevisionExpedienteAD
    {
        private readonly Contexto _contexto;

        public ListarDocsAutorizacionRevisionExpedienteAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<DocsAutorizacionRevisionExpedienteDTO>> listar()
        {
            try
            {
                return await _contexto.TDocsAutorizacionRevisionExpedientes.Select(lista => new DocsAutorizacionRevisionExpedienteDTO
                {
                    CedulaAsistente = lista.CedulaAsistente,
                    CedulaAsistenteNavigation = lista.CedulaAsistenteNavigation,
                    CedulaAbogado = lista.CedulaAbogado,
                    CedulaAbogadoNavigation = lista.CedulaAbogadoNavigation,
                    CedulaImputado = lista.CedulaImputado,
                    CedulaImputadoNavigation = lista.CedulaImputadoNavigation,
                    IdDocumento = lista.IdDocumento,
                    Delito = lista.Delito,
                    Expediente = lista.Expediente,
                    Ofendido = lista.Ofendido
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos {ex.Message}");
                return new List<DocsAutorizacionRevisionExpedienteDTO>();
            }

        }
    }
}
