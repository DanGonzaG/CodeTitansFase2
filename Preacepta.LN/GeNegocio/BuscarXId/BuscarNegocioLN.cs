using Preacepta.AD.CasosTipo.BuscarXid;
using Preacepta.AD.GeNegocio.BuscarXId;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.GeNegocio.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeNegocio.BuscarXId
{
    public class BuscarNegocioLN : IBuscarNegocioLN
    {
        private readonly IBuscarNegocioAD _buscar;
        private readonly IObtenerDatosNegocioLN _obtenerDatosLN;

        public BuscarNegocioLN(IBuscarNegocioAD buscar,
             IObtenerDatosNegocioLN obtnerDatosLN)
        {
            _buscar = buscar;
            _obtenerDatosLN = obtnerDatosLN;
        }

        public async Task<GeNegocioDTO?> buscar(int id)
        {
            try
            {
                TGeNegocio? resultadoBusqueda = await _buscar.buscar(id);
                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró la el tipo de abogado.");
                    return null;
                }
                GeNegocioDTO obtenerDatos = _obtenerDatosLN.ObtenerDeDB(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarNegocioLN: {ex.Message}");
                return null;
            }


        }
    }
}
