using Preacepta.AD.CasosTipo.BuscarXid;
using Preacepta.AD.DocsCompraventaFinca.BuscarXid;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsCompraventaFinca.Eliminar
{
    public class EliminarDocsCompraventaFincaAD : IEliminarDocsCompraventaFincaAD
    {
        private readonly Contexto _contexto;
        private readonly IBuscarDocsCompraventaFincaAD _buscarXid;

        public EliminarDocsCompraventaFincaAD(Contexto contexto, IBuscarDocsCompraventaFincaAD buscarXid)
        {
            _contexto = contexto;
            _buscarXid = buscarXid;
        }

        public async Task<int> Eliminar(int id)
        {

            try
            {
                TDocsCompraventaFinca? encontrado = await _buscarXid.buscar(id);
                if (encontrado == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                _contexto.TDocsCompraventaFincas.Remove(encontrado);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarDocsCompraventaFincaAD: {ex.Message}");
                return -1;
            }

        }
    }
}
