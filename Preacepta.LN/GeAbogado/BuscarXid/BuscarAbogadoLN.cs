using Preacepta.AD.CasosTipo.BuscarXid;
using Preacepta.AD.GeAbogado.BuscarXid;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.GeAbogado.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeAbogado.BuscarXid
{
    public class BuscarAbogadoLN : IBuscarAbogadoLN
    {
        private readonly IBuscarAbogadoAD _buscar;
        private readonly IObtenerDatosAbogadoLN _obtenerDatosLN;

        public BuscarAbogadoLN(IBuscarAbogadoAD buscar,
             IObtenerDatosAbogadoLN obtnerDatosLN)
        {
            _buscar = buscar;
            _obtenerDatosLN = obtnerDatosLN;
        }

        public async Task<GeAbogadoDTO?> buscar(int id)
        {
            try
            {
                TGeAbogado? resultadoBusqueda = await _buscar.buscar(id);
                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró la el tipo de abogado.");
                    return null;
                }
                GeAbogadoDTO obtenerDatos = _obtenerDatosLN.ObtenerDeDB(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarAbogadoLN: {ex.Message}");
                return null;
            }


        }
    }
}
