using Preacepta.AD.Casos.BuscarXid;
using Preacepta.AD.CasosTipo.BuscarXid;
using Preacepta.LN.Casos.ObtenerDatos;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Casos.BuscarXid
{
    public class BuscarCasosLN : IBuscarCasosLN
    {
        private readonly IBuscarCasosAD _buscar;
        private readonly IObtenerDatosCasoLN _obtenerDatosLN;

        public BuscarCasosLN(IBuscarCasosAD buscar,
             IObtenerDatosCasoLN obtnerDatosLN)
        {
            _buscar = buscar;
            _obtenerDatosLN = obtnerDatosLN;
        }

        public async Task<CasoDTO?> buscar(int id)
        {
            try
            {
                TCaso? resultadoBusqueda = await _buscar.buscar(id);
                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró la el tipo de abogado.");
                    return null;
                }
                CasoDTO obtenerDatos = _obtenerDatosLN.ObtenerDeDB(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarCasosLN: {ex.Message}");
                return null;
            }
        }
    }
}
