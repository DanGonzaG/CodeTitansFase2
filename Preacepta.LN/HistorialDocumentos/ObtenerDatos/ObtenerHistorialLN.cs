using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.HistorialDocumentos.ObtenerDatos
{
    public class ObtenerHistorialLN : IObtenerHistorialLN
    {
        public HistorialDocumentoDTO ObtenerDeDB(THistorialDocumento datos)
        {
            return new HistorialDocumentoDTO
            {
                Id = datos.Id,
                Fecha = datos.Fecha.ToString("dd-MM-yyyy"),
                Cliente = datos.Cliente,
                Abogado = datos.Abogado,
                TipoDocumento = datos.TipoDocumento,
                IdDocumento = datos.IdDocumento,
                Titulo = datos.Titulo,

                ClienteNavigation = datos.ClienteNavigation,
                AbogadoNavigation = datos.AbogadoNavigation
            };
        }

        public THistorialDocumento ObtenerDeFrontCrear(HistorialDocumentoDTO datos)
        {
            return new THistorialDocumento
            {
                Fecha = DateTime.Now,
                Cliente = datos.Cliente,
                Abogado = datos.Abogado,
                TipoDocumento = (datos.TipoDocumento ?? string.Empty).Trim(),
                IdDocumento = datos.IdDocumento,
                Titulo = (datos.Titulo ?? string.Empty).Trim()
            };
        }

        public THistorialDocumento ObtenerDeFrontEditar(HistorialDocumentoDTO datos)
        {
            return new THistorialDocumento
            {
                Id = datos.Id,
                Fecha = ParseFecha(dtoFecha: datos.Fecha) ?? DateTime.Today,
                Cliente = datos.Cliente,
                Abogado = datos.Abogado,
                TipoDocumento = (datos.TipoDocumento ?? string.Empty).Trim(),
                IdDocumento = datos.IdDocumento,
                Titulo = (datos.Titulo ?? string.Empty).Trim(),

                ClienteNavigation = datos.ClienteNavigation,
                AbogadoNavigation = datos.AbogadoNavigation
            };
        }

        // ——— helpers ———
        private static DateTime? ParseFecha(string? dtoFecha)
        {
            if (string.IsNullOrWhiteSpace(dtoFecha)) return null;

            string[] formatos = { "dd-MM-yyyy", "dd-MM-yyyy HH:mm" };
            if (DateTime.TryParseExact(dtoFecha.Trim(), formatos,
                                       CultureInfo.InvariantCulture,
                                       DateTimeStyles.None, out var f))
            {
                return f.Date; 
            }
            return null;
        }
    }
}
