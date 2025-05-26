using Preacepta.AD.CasosTipo.BuscarXid;
using Preacepta.AD.CrDireccion1.BuscarXid;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.CrDireccion1.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CrDireccion1.BuscarXid
{
    public class BuscarCrDireccion1LN : IBuscarCrDireccion1LN
    {
        private readonly IBuscarCrDireccion1AD _buscar;
        private readonly IObtenerDatosDireccion1LN _obtenerDatosLN;

        public BuscarCrDireccion1LN(IBuscarCrDireccion1AD buscar,
             IObtenerDatosDireccion1LN obtnerDatosLN)
        {
            _buscar = buscar;
            _obtenerDatosLN = obtnerDatosLN;
        }

        public async Task<CrProvinciaDTO?> buscarProvincia(int id)
        {
            try
            {
                TCrProvincia? resultadoBusqueda = await _buscar.buscarProvincias(id);
                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró la el tipo de abogado.");
                    return null;
                }
                CrProvinciaDTO obtenerDatos = _obtenerDatosLN.ObtenerDeDBProvincias(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarCrDireccion1LN-buscarProvincia: {ex.Message}");
                return null;
            }
        }

        public async Task<CrCantonDTO?> buscarCanton(int id)
        {
            try
            {
                TCrCantone? resultadoBusqueda = await _buscar.buscarCantones(id);
                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró la el tipo de abogado.");
                    return null;
                }
                CrCantonDTO obtenerDatos = _obtenerDatosLN.ObtenerDeDBCanton(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarCrDireccion1LN-buscarCanton: {ex.Message}");
                return null;
            }
        }

        public async Task<CrDistritoDTO?> buscarDistrito(int id)
        {
            try
            {
                TCrDistrito? resultadoBusqueda = await _buscar.buscarDistritos(id);
                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró la el tipo de abogado.");
                    return null;
                }
                CrDistritoDTO obtenerDatos = _obtenerDatosLN.ObtenerDeDBDistrito(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarCrDireccion1LN-buscarDistrito: {ex.Message}");
                return null;
            }
        }
    }
}
