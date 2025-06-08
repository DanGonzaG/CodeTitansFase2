using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsAutorizacionRevisionExpediente.Editar
{
    public class EditarDocsAutorizacionRevisionExpedienteAD : IEditarDocsAutorizacionRevisionExpedienteAD
    {
        private readonly Contexto _contexto;
        public EditarDocsAutorizacionRevisionExpedienteAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> Editar(TDocsAutorizacionRevisionExpediente editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                _contexto.TDocsAutorizacionRevisionExpedientes.Update(editar);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarDocsAutorizacionRevisionExpedienteAD : {ex.Message}");
                return -1;
            }

        }
    }
}
