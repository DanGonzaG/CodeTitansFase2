using Preacepta.AD;
using Preacepta.AD.HistorialDocumentos.Crear;
using Preacepta.LN.HistorialDocumentos.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.HistorialDocumentos.Crear
{
    public class CrearHistorialLN : ICrearHistorialLN
    {
        private readonly ICrearHistorialAD _crearAD;
        private readonly IObtenerHistorialLN _mapper; // usa el nombre de tu mapper

        public CrearHistorialLN(ICrearHistorialAD crearAD,
                                IObtenerHistorialLN mapper)
        {
            _crearAD = crearAD;
            _mapper = mapper;
        }

        public async Task<int> Crear(HistorialDocumentoDTO dto)
        {
            if (dto == null) return 0;

            dto.TipoDocumento = (dto.TipoDocumento ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(dto.TipoDocumento) || dto.IdDocumento <= 0)
                return 0;

            var entidad = _mapper.ObtenerDeFrontCrear(dto);

            return await _crearAD.crear(entidad);
        }
    }
}
