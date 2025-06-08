using Preacepta.AD.DocsPagare.Buscar;
using Preacepta.LN.DocsPagare.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsPagare.Buscar
{
    public class BuscarPagareLN : IBuscarPagareLN
    {
        private readonly IBuscarPagareAD _buscarPagare;
        private readonly IObtenerDatosPagareLN _obtenerDatosPagare;

        public BuscarPagareLN(IBuscarPagareAD buscarPagare,
            IObtenerDatosPagareLN obtenerDatosPagare)
        {
            _buscarPagare = buscarPagare;
            _obtenerDatosPagare = obtenerDatosPagare;
        }

        public async Task<DocsPagareDTO?> buscar(int id)
        {
            try
            {
                // Cambiar tipo aquí
                TDocsPagare? resultadoBusqueda = await _buscarPagare.buscar(id);

                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró el tipo de Compra o venta.");
                    return null;
                }
                DocsPagareDTO obtenerDatos = _obtenerDatosPagare.ObtenerDeDB(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarTestimonioLN: {ex.Message}");
                return null;
            }

        }

    }
}
