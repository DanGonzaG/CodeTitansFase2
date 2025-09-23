using Preacepta.AD.HistorialDocumentos.BuscarXid;
using Preacepta.LN.HistorialDocumentos.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.HistorialDocumentos.BuscarXid
{
    public class BuscarHistorialLN : IBuscarHistorialLN
    {
        private readonly IBuscarHistorialAD _ad;
        private readonly IObtenerHistorialLN _mapper;

        public BuscarHistorialLN(IBuscarHistorialAD ad,
                                 IObtenerHistorialLN mapper)
        {
            _ad = ad;
            _mapper = mapper;
        }

        public async Task<HistorialDocumentoDTO?> Buscar(int id)
        {
            try
            {
                THistorialDocumento? item = await _ad.Buscar(id);
                return item is null ? null : _mapper.ObtenerDeDB(item);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BuscarHistorialLN.Buscar error: {ex.Message}");
                return null;
            }
        }

        public async Task<HistorialDocumentoDTO?> BuscarPorDocumento(string tipoDocumento, int idDocumento)
        {
            try
            {
                THistorialDocumento? item = await _ad.BuscarPorDocumento(tipoDocumento, idDocumento);
                return item is null ? null : _mapper.ObtenerDeDB(item);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BuscarHistorialLN.BuscarPorDocumento error: {ex.Message}");
                return null;
            }
        }

        public async Task<int> BuscarXidDocumento(int idDocumento, string nombreTipoDocumento)
        {
            int resultado = await _ad.BuscarXidDocumento(idDocumento, nombreTipoDocumento);
            return resultado;
        }
    }
}
