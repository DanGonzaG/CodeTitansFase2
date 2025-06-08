using Preacepta.AD.CasosTipo.BuscarXid;
using Preacepta.AD.DocsContratoPrestacionServicios.BuscarXid;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.DocsContratoPrestacionServicios.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsContratoPrestacionServicios.BuscarXid
{
    public class BuscarDocsContratoPrestacionServiciosLN : IBuscarDocsContratoPrestacionServiciosLN
    {
        private readonly IBuscarDocsContratoPrestacionServiciosAD _buscar;
        private readonly IObtenerDatosDocsContratoPrestacionServiciosLN _obtenerDatosLN;

        public BuscarDocsContratoPrestacionServiciosLN(IBuscarDocsContratoPrestacionServiciosAD buscar,
             IObtenerDatosDocsContratoPrestacionServiciosLN obtnerDatosLN)
        {
            _buscar = buscar;
            _obtenerDatosLN = obtnerDatosLN;
        }

        public async Task<DocsContratoPrestacionServicioDTO?> buscar(int id)
        {
            try
            {
                TDocsContratoPrestacionServicio? resultadoBusqueda = await _buscar.buscar(id);
                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró la el tipo de documento prestacion de servicios.");
                    return null;
                }
                DocsContratoPrestacionServicioDTO obtenerDatos = _obtenerDatosLN.ObtenerDeDB(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarDocsContratoPrestacionServiciosLN: {ex.Message}");
                return null;
            }
        }
    }
}
