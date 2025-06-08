
using Preacepta.AD.Citas.BuscarXid;
using Preacepta.LN.Citas.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Citas.BuscarXid
{
    public class BuscarCitasLN : IBuscarCitasLN
    {
        private readonly IBuscarCitasAD _buscar;
        private readonly IObtenerDatosCitasLN _obtenerDatosLN;

        public BuscarCitasLN(IBuscarCitasAD buscar,
             IObtenerDatosCitasLN obtnerDatosLN)
        {
            _buscar = buscar;
            _obtenerDatosLN = obtnerDatosLN;
        }

        public async Task<CitasDTO?> buscar(int id)
        {
            try
            {
                TCita? resultadoBusqueda = await _buscar.buscar(id);
                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró la cita.");
                    return null;
                }
                CitasDTO obtenerDatos = _obtenerDatosLN.ObtenerDeDB(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarCitasLN: {ex.Message}");
                return null;
            }


        }
    }
}
