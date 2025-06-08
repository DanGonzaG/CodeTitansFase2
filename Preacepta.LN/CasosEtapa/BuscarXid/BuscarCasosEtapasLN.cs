using Preacepta.AD.CasosEtapa.BuscarXid;
using Preacepta.LN.CasosEtapa.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosEtapa.BuscarXid
{
    public class BuscarCasosEtapasLN : IBuscarCasosEtapasLN
    {
        private readonly IBuscarCasosEtapasAD _buscar;
        private readonly IObtnerDatosCasoEtapaLN _obtenerDatosLN;

        public BuscarCasosEtapasLN(IBuscarCasosEtapasAD buscar,
             IObtnerDatosCasoEtapaLN obtnerDatosLN)
        {
            _buscar = buscar;
            _obtenerDatosLN = obtnerDatosLN;
        }

        public async Task<CasosEtapaDTO?> buscar(int id)
        {
            try
            {
                TCasosEtapa? resultadoBusqueda = await _buscar.buscar(id);
                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró la el tipo de La etapa de proceso legal.");
                    return null;
                }
                CasosEtapaDTO obtenerDatos = _obtenerDatosLN.ObtenerDeDB(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarCasosEtapasLN: {ex.Message}");
                return null;
            }
        }
    }
}
