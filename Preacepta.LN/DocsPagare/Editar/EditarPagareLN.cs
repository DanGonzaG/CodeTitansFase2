using Preacepta.AD.DocsPagare.Editar;
using Preacepta.LN.DocsPagare.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
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
            if (pagareDTO == null || pagareDTO.IdDocumento <= 0)
            {
                Console.WriteLine("EditarPagareLN: DTO nulo o IdDocumento inválido.");
                return 0;
            }

            try
            {
                var entidad = _obtenerDatosPagareLN.ObtenerDeFront(pagareDTO);
                var filas = await _editarPagareAD.editar(entidad);
                return filas;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarPagareLN: {ex.Message}");
                return 0;
            }
        }
    }
}
