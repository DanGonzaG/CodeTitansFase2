using Preacepta.AD.CasosTipo.BuscarXid;
using Preacepta.AD.DocsAutorizacionRevisionExpediente.BuscarXid;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsAutorizacionRevisionExpediente.Eliminar
{
    public class EliminarDocsAutorizacionRevisionExpedienteAD : IEliminarDocsAutorizacionRevisionExpedienteAD
    {
        private readonly Contexto _contexto;
        private readonly IBuscarDocsAutorizacionRevisionExpedienteAD _buscarXid;

        public EliminarDocsAutorizacionRevisionExpedienteAD(Contexto contexto, IBuscarDocsAutorizacionRevisionExpedienteAD buscarXid)
        {
            _contexto = contexto;
            _buscarXid = buscarXid;
        }

        public async Task<int> Eliminar(int id)
        {

            try
            {
                TDocsAutorizacionRevisionExpediente? encontrado = await _buscarXid.buscar(id);
                if (encontrado == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                _contexto.TDocsAutorizacionRevisionExpedientes.Remove(encontrado);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarDocsAutorizacionRevisionExpedienteAD: {ex.Message}");
                return -1;
            }

        }
    }
}
