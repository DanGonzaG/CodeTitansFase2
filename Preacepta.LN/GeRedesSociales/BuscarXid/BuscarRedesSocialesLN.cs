using Preacepta.AD.CasosTipo.BuscarXid;
using Preacepta.AD.GeRedesSociales.BuscarXid;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.GeRedesSociales.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeRedesSociales.BuscarXid
{
    public class BuscarRedesSocialesLN : IBuscarRedesSocialesLN
    {
        private readonly IBuscarRedesSocialesAD _buscar;
        private readonly IObtenerDatosRedesSocialesLN _obtenerDatosLN;

        public BuscarRedesSocialesLN(IBuscarRedesSocialesAD buscar,
             IObtenerDatosRedesSocialesLN obtnerDatosLN)
        {
            _buscar = buscar;
            _obtenerDatosLN = obtnerDatosLN;
        }

        public async Task<GeRedesSocialeDTO?> buscar(int id)
        {
            try
            {
                TGeRedesSociale? resultadoBusqueda = await _buscar.buscar(id);
                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró la el tipo de Rede Social.");
                    return null;
                }
                GeRedesSocialeDTO obtenerDatos = _obtenerDatosLN.ObtenerDeDB(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarRedesSocialesLN: {ex.Message}");
                return null;
            }
        }
    }
}
