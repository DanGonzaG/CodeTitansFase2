using Preacepta.AD.GeAbogado.BuscarXid;
using Preacepta.LN.GeAbogado.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

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
                Console.WriteLine($"Error en BuscarAbogadoLNmetodo buscar: {ex.Message}");
                return null;
            }
        }

        public async Task<GeAbogadoDTO?> buscarXcarnet(int carnet)
        {
            try
            {
                TGeAbogado? resultadoBusqueda = await _buscar.buscarXcarnet(carnet);
                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró el carnet del abogado.");
                    return null;
                }
                GeAbogadoDTO obtenerDatos = _obtenerDatosLN.ObtenerDeDB(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarAbogadoLN metodo buscarXcarnet: {ex.Message}");
                return null;
            }
        }
    }
}
