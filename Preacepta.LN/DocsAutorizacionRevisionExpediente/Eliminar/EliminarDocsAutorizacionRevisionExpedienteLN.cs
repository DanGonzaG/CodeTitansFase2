using Preacepta.AD.CasosTipo.Eliminar;
using Preacepta.AD.DocsAutorizacionRevisionExpediente.Eliminar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsAutorizacionRevisionExpediente.Eliminar
{
    public class EliminarDocsAutorizacionRevisionExpedienteLN : IEliminarDocsAutorizacionRevisionExpedienteLN
    {
        private readonly IEliminarDocsAutorizacionRevisionExpedienteAD _eliminar;

        public EliminarDocsAutorizacionRevisionExpedienteLN(IEliminarDocsAutorizacionRevisionExpedienteAD eliminar)
        {
            _eliminar = eliminar;
        }

        public async Task<int> Eliminar(int id)
        {
            if (id < 0)
            {
                Console.WriteLine("el valor de id en menor a 1");
                return 0;
            }
            try
            {
                int bandera = await _eliminar.Eliminar(id);
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en: EliminarDocsAutorizacionRevisionExpedienteLN {ex.Message}");
                return -1;
            }
        }
    }
}
