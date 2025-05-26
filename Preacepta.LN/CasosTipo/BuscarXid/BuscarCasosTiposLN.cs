using Preacepta.AD.CasosTipo.BuscarXid;
using Preacepta.AD.GeAbogadoTipo.BuscarXid;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.GeAbogadoTipo.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CasosTipo.BuscarXid
{
    public class BuscarCasosTiposLN : IBuscarCasosTiposLN
    {
        private readonly IBuscarCasosTiposAD _buscar;
        private readonly IObtenerDatosCasosTipoLN _obtenerDatosLN;

        public BuscarCasosTiposLN(IBuscarCasosTiposAD buscar,
             IObtenerDatosCasosTipoLN obtnerDatosLN)
        {
            _buscar = buscar;
            _obtenerDatosLN = obtnerDatosLN;
        }

        public async Task<CasosTipoDTO?> buscar(int id)
        {
            try
            {
                TCasosTipo? resultadoBusqueda = await _buscar.buscar(id);
                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró la el tipo de abogado.");
                    return null;
                }
                CasosTipoDTO obtenerDatos = _obtenerDatosLN.ObtenerDeDB(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarCasosTiposLN: {ex.Message}");
                return null;
            }
        }
    }
}
