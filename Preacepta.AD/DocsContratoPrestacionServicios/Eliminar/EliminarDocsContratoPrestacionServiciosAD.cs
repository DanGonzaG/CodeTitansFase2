using Preacepta.AD.CasosTipo.BuscarXid;
using Preacepta.AD.DocsContratoPrestacionServicios.BuscarXid;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsContratoPrestacionServicios.Eliminar
{
    public class EliminarDocsContratoPrestacionServiciosAD : IEliminarDocsContratoPrestacionServiciosAD
    {
        private readonly Contexto _contexto;
        private readonly IBuscarDocsContratoPrestacionServiciosAD _buscarXid;

        public EliminarDocsContratoPrestacionServiciosAD(Contexto contexto, IBuscarDocsContratoPrestacionServiciosAD buscarXid)
        {
            _contexto = contexto;
            _buscarXid = buscarXid;
        }

        public async Task<int> Eliminar(int id)
        {

            try
            {
                TDocsContratoPrestacionServicio? encontrado = await _buscarXid.buscar(id);
                if (encontrado == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                _contexto.TDocsContratoPrestacionServicios.Remove(encontrado);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarDocsContratoPrestacionServiciosAD: {ex.Message}");
                return -1;
            }

        }
    }
}
