using Preacepta.AD.DocsPagare.Buscar;
using Preacepta.LN.DocsPagare.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsPagare.Buscar
{
    public class BuscarPagareLN : IBuscarPagareLN
    {
        private readonly IBuscarPagareAD _buscarPagare;
        private readonly IObtenerDatosPagareLN _obtenerDatosPagare;

        public BuscarPagareLN(
            IBuscarPagareAD buscarPagare,
            IObtenerDatosPagareLN obtenerDatosPagare)
        {
            _buscarPagare = buscarPagare;
            _obtenerDatosPagare = obtenerDatosPagare;
        }

        public async Task<DocsPagareDTO?> buscar(int id)
        {
            try
            {
                TDocsPagare? entity = await _buscarPagare.buscar(id);
                if (entity == null)
                {
                    Console.WriteLine("No se encontró el Pagaré solicitado.");
                    return null;
                }

                // Asegúrate que ObtenerDeDB mapee CedulaAbogado y HoraFirma correctamente
                DocsPagareDTO dto = _obtenerDatosPagare.ObtenerDeDB(entity);
                return dto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarPagareLN (id={id}): {ex.Message}");
                return null;
            }
        }
    }
}
