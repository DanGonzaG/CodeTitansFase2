using Preacepta.AD.DocPoderesEspecialesJudiciales.Editar;
using Preacepta.LN.DocPoderesEspecialesJudiciales.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocPoderesEspecialesJudiciales.Editar
{
    public class EditarPoderJudLN : IEditarPoderJudLN
    {
        private readonly IEditarPoderJudAD _editar;
        private readonly IObtenerDatosPoderJudLN _obtenerDatos;

        public EditarPoderJudLN(IEditarPoderJudAD editar, IObtenerDatosPoderJudLN obtenerDatos)
        {
            _editar = editar;
            _obtenerDatos = obtenerDatos;
        }

        public async Task<int> editar(DocsPoderesEspecialesJudicialeDTO poderJudDTO)
        {
            if (poderJudDTO == null)
            {
                return 0;
            }

            try
            {
                int bandera = await _editar.editar(_obtenerDatos.ObtenerDeFront(poderJudDTO));
                return bandera;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarTestLN: {ex.Message}");
                return 0;
            }
        }

    }
}
