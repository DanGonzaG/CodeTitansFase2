using Preacepta.AD.DocsPagare.Crear;
using Preacepta.LN.DocsPagare.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsPagare.Crear
{
    public class CrearPagareLN : ICrearPagareLN
    {
        private readonly ICrearPagareAD _crearPagareAD;
        private readonly IObtenerDatosPagareLN _obtenerDatosPagareLN;

        public CrearPagareLN(
            ICrearPagareAD crearPagareAD,
            IObtenerDatosPagareLN obtenerDatosPagareLN)
        {
            _crearPagareAD = crearPagareAD;
            _obtenerDatosPagareLN = obtenerDatosPagareLN;
        }

        public async Task<int> crear(DocsPagareDTO dto)
        {
            if (dto == null)
            {
                Console.WriteLine("Error: Objeto nulo.");
                return 0;
            }

            try
            {
                var entidad = _obtenerDatosPagareLN.ObtenerDeFront(dto);
                var idNuevo = await _crearPagareAD.crear(entidad);
                if (idNuevo <= 0)
                {
                    Console.WriteLine("Creación no exitosa.");
                    return 0;
                }
                return idNuevo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearPagareLN.crear: {ex.Message}");
                return -1;
            }
        }
    }
}
