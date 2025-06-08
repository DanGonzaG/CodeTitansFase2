using Microsoft.AspNetCore.Components.Forms;
using Preacepta.AD.Testimonios.Editar;
using Preacepta.LN.Testimonios.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Preacepta.LN.Testimonios.Editar
{
    public class EditarTestLN : IEditarTestLN
    {
        private readonly IEditarTestAD _editarTetsAD;
        private readonly IObtenerDatosTestLN _obtenerDatosTestLN;

        public EditarTestLN(IEditarTestAD editarTestAD,
            IObtenerDatosTestLN obtenerDatosTestLN)
        {
            _editarTetsAD = editarTestAD;
            _obtenerDatosTestLN = obtenerDatosTestLN;
        }

        public async Task<int> editar(TTestimonioDTO testDTO)
        {
            if (testDTO == null)
            {
                return 0;
            }

            try
            {
                int bandera = await _editarTetsAD.editar(_obtenerDatosTestLN.ObtenerDeFront(testDTO));
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
