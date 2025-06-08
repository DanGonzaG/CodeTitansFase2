
using Preacepta.AD.CitasTipo.BuscarXid;
using Preacepta.LN.CitasTipo.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CitasTipo.BuscarXid
{
    public class BuscarCitasTipoLN : IBuscarCitasTipoLN
    {
        private readonly IBuscarCitasTipoAD _buscar;
        private readonly IObtenerDatosCitasTipoLN _obtenerDatosLN;

        public BuscarCitasTipoLN(IBuscarCitasTipoAD buscar,
             IObtenerDatosCitasTipoLN obtnerDatosLN)
        {
            _buscar = buscar;
            _obtenerDatosLN = obtnerDatosLN;
        }

        public async Task<CitasTipoDTO?> buscar(int id)
        {
            try
            {
                TCitasTipo? resultadoBusqueda = await _buscar.buscar(id);
                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró la cita.");
                    return null;
                }
                CitasTipoDTO obtenerDatos = _obtenerDatosLN.ObtenerDeDB(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarCitasTipoLN: {ex.Message}");
                return null;
            }


        }
    }
}
