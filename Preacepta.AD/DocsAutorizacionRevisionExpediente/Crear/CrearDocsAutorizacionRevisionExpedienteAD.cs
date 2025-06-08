using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsAutorizacionRevisionExpediente.Crear
{
    public class CrearDocsAutorizacionRevisionExpedienteAD : ICrearDocsAutorizacionRevisionExpedienteAD
    {
        private readonly Contexto _contexto;

        public CrearDocsAutorizacionRevisionExpedienteAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(TDocsAutorizacionRevisionExpediente crear)
        {
            if (crear == null)
            {
                Console.WriteLine("El objeto recibo fue nulo");
                return -1;
            }
            try
            {
                await _contexto.TDocsAutorizacionRevisionExpedientes.AddAsync(crear);
                int guardado = await _contexto.SaveChangesAsync();
                return guardado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearDocsAutorizacionRevisionExpedienteAD {ex.Message}");
                return 0;
            }


        }
    }
}
