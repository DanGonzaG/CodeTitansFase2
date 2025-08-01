﻿using Preacepta.AD.GeAbogado.Editar;
using Preacepta.LN.GeAbogado.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeAbogado.Editar
{
    public class EditarAbogadoLN : IEditarAbogadoLN
    {
        private readonly IEditarAbogadoAD _editar;
        private readonly IObtenerDatosAbogadoLN _obtenerDatosLN;

        public EditarAbogadoLN(IEditarAbogadoAD editar,
            IObtenerDatosAbogadoLN obtenerDatos)
        {
            _editar = editar;
            _obtenerDatosLN = obtenerDatos;
        }

        public async Task<int> Editar(GeAbogadoDTO editar)
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
                Console.WriteLine($"Error en EditarAbogadoLN: {ex.Message}");
                return 0;

            }




        }
    }
}
