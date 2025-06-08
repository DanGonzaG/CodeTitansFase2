using Preacepta.AD.DocsPagare.Crear;
using Preacepta.LN.DocsPagare.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsPagare.Crear
{
    public class CrearPagareLN : ICrearPagareLN
    {
        private readonly ICrearPagareAD _crearPagareAD;
        private readonly IObtenerDatosPagareLN _obtenerDatosPagareLN;

        public CrearPagareLN (ICrearPagareAD crearPagareAD, IObtenerDatosPagareLN obtenerDatosPagareLN)
        {
            _crearPagareAD = crearPagareAD;
            _obtenerDatosPagareLN = obtenerDatosPagareLN;
        }

        public async Task<int> crear(DocsPagareDTO pagareDTO)
        {
            if (pagareDTO == null)
            {
                Console.WriteLine("Error: Objeto nulo.");
                return 0;
            }
            try
            {
                int bandera = await _crearPagareAD.crear(_obtenerDatosPagareLN.ObtenerDeFront(pagareDTO));
                if (bandera == null)
                {
                    Console.WriteLine("Fallido");
                    return 0;
                }
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearPagareLN{ex.Message}");
                return -1;
            }
        }

    }
}
