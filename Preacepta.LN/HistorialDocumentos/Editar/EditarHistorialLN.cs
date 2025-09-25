using Preacepta.AD.HistorialDocumentos.Editar;
using Preacepta.LN.HistorialDocumentos.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.HistorialDocumentos.Editar
{
    public class EditarHistorialLN : IEditarHistorialLN
    {
        private readonly IEditarHistorialAD _editarAD;
        private readonly IObtenerHistorialLN _mapper;

        public EditarHistorialLN(IEditarHistorialAD editarAD,
                                 IObtenerHistorialLN mapper)
        {
            _editarAD = editarAD;
            _mapper = mapper;
        }

        public async Task<int> Editar(HistorialDocumentoDTO editar)
        {
            if (editar == null) return 0;

            try
            {
                var entidad = _mapper.ObtenerDeFrontEditar(editar);
                var bandera = await _editarAD.Editar(entidad);
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarHistorialLN: {ex.Message}");
                return 0;
            }
        }
    }
}
