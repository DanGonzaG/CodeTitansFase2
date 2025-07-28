using Preacepta.AD.CasosTipo.BuscarXid;
using Preacepta.AD.DocsCombustible.BuscarXid;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.DocsCombustible.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsCombustible.BuscarXid
{
    public class BuscarDocsCombustibleLN : IBuscarDocsCombustibleLN
    {
        private readonly IBuscarDocsCombustibleAD _buscar;
        private readonly IObtenerDatosDocsCombustibleLN _obtenerDatosLN;

        public BuscarDocsCombustibleLN(IBuscarDocsCombustibleAD buscar,
             IObtenerDatosDocsCombustibleLN obtnerDatosLN)
        {
            _buscar = buscar;
            _obtenerDatosLN = obtnerDatosLN;
        }

        public async Task<DocsCombustibleDTO?> buscar(int id)
        {
            try
            {
                TDocsCombustible? resultadoBusqueda = await _buscar.buscar(id);
                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró la el tipo de abogado.");
                    return null;
                }
                DocsCombustibleDTO obtenerDatos = _obtenerDatosLN.ObtenerDeDB(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarDocsCombustibleLN: {ex.Message}");
                return null;
            }
        }
    }
}
