using Preacepta.AD.DocsPagare.Editar;
using Preacepta.LN.DocsPagare.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsPagare.Editar
{
    public class EditarPagareLN : IEditarPagareLN
    {
        private readonly IEditarPagareAD _editarPagareAD;
        private readonly IObtenerDatosPagareLN _obtenerDatosPagareLN;

        public EditarPagareLN(IEditarPagareAD editarPagareAD, IObtenerDatosPagareLN obtenerDatosPagareLN)
        {
            _editarPagareAD = editarPagareAD;
            _obtenerDatosPagareLN = obtenerDatosPagareLN;
        }

        public async Task<int> editar(DocsPagareDTO pagareDTO)
        {
            if (pagareDTO == null)
            {
                return 0;
            }

            try
            {
                int bandera = await _editarPagareAD.editar(_obtenerDatosPagareLN.ObtenerDeFront(pagareDTO));
                return bandera;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarPagareLN: {ex.Message}");
                return 0;
            }
        }

    }
}
