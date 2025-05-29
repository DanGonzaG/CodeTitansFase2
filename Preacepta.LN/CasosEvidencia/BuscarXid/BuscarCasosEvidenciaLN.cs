using Preacepta.AD.CasosEtapa.BuscarXid;
using Preacepta.AD.CasosEvidencia.BuscarXid;
using Preacepta.LN.CasosEtapa.ObtenerDatos;
using Preacepta.LN.CasosEvidencia.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CasosEvidencia.BuscarXid
{
    public class BuscarCasosEvidenciaLN : IBuscarCasosEvidenciaLN
    {

        private readonly IBuscarCasosEvidenciaAD _buscar;
        private readonly IObtnerDatosCasoEvidenciaLN _obtenerDatosLN;

        public BuscarCasosEvidenciaLN(IBuscarCasosEvidenciaAD buscar,
             IObtnerDatosCasoEvidenciaLN obtnerDatosLN)
        {
            _buscar = buscar;
            _obtenerDatosLN = obtnerDatosLN;
        }

        public async Task<CasosEvidenciaDTO?> buscar(int id)
        {
            try
            {
                TCasosEvidencia? resultadoBusqueda = await _buscar.buscar(id);
                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró la el tipo de la evidencia del caso.");
                    return null;
                }
                CasosEvidenciaDTO obtenerDatos = _obtenerDatosLN.ObtenerDeDB(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarCasosEvidenciaLN: {ex.Message}");
                return null;
            }
        }
    }
}
