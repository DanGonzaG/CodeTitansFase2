using Preacepta.AD.CasosTipo.Editar;
using Preacepta.AD.DocsContratoPrestacionServicios.Editar;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.DocsContratoPrestacionServicios.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsContratoPrestacionServicios.Editar
{
    public class EditarDocsContratoPrestacionServiciosLN : IEditarDocsContratoPrestacionServiciosLN
    {
        private readonly IEditarDocsContratoPrestacionServiciosAD _editar;
        private readonly IObtenerDatosDocsContratoPrestacionServiciosLN _obtenerDatosLN;

        public EditarDocsContratoPrestacionServiciosLN(IEditarDocsContratoPrestacionServiciosAD editar,
            IObtenerDatosDocsContratoPrestacionServiciosLN obtenerDatos)
        {
            _editar = editar;
            _obtenerDatosLN = obtenerDatos;
        }

        public async Task<int> Editar(DocsContratoPrestacionServicioDTO editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                int bandera = await _editar.Editar(_obtenerDatosLN.ObtenerDeFront(editar));
                return bandera;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarDocsContratoPrestacionServiciosLN: {ex.Message}");
                return 0;
            }
        }
    }
}
