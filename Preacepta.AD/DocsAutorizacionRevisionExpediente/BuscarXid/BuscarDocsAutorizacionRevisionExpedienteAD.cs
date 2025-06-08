using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsAutorizacionRevisionExpediente.BuscarXid
{
    public class BuscarDocsAutorizacionRevisionExpedienteAD : IBuscarDocsAutorizacionRevisionExpedienteAD
    {
        private readonly Contexto _contexto;

        public BuscarDocsAutorizacionRevisionExpedienteAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<TDocsAutorizacionRevisionExpediente?> buscar(int id)
        {
            try
            {
                var lista = await _contexto.TDocsAutorizacionRevisionExpedientes
                .Include(t => t.CedulaAbogadoNavigation)
                .Include(t => t.CedulaAsistenteNavigation)
                .Include(t => t.CedulaImputadoNavigation)
                .FirstOrDefaultAsync(m => m.IdDocumento == id); ;
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarDocsAutorizacionRevisionExpedienteAD, no se encontro id: {ex.Message}");
                return null;
            }

        }
    }
}
