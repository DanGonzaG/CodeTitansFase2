using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Globalization;

namespace Preacepta.LN.DocsPagare.ObtenerDatos
{
    public class ObtenerDatosPagareLN : IObtenerDatosPagareLN
    {
        public DocsPagareDTO ObtenerDeDB(TDocsPagare pagare)
        {
            return new DocsPagareDTO
            {
                IdDocumento = pagare.IdDocumento,
                MontoNumerico = pagare.MontoNumerico,
                CedulaDeudor = pagare.CedulaDeudor,
                SociedadDeudor = pagare.SociedadDeudor,
                CedulaJuridicaSociedad = pagare.CedulaJuridicaSociedad,
                AcreedorNombre = pagare.AcreedorNombre,
                CedulaJuridicaAcreedor = pagare.CedulaJuridicaAcreedor,
                AcreedorDomicilio = pagare.AcreedorDomicilio,

                FechaFirma = pagare.FechaFirma.ToString("yyyy-MM-dd"),
                HoraFirma = pagare.HoraFirma.ToString("HH:mm"),
                FechaVencimiento = pagare.FechaVencimiento.ToString("yyyy-MM-dd"),

                InteresFormula = pagare.InteresFormula,
                InteresTasaActual = pagare.InteresTasaActual,
                InteresBase = pagare.InteresBase,

                LugarPago = pagare.LugarPago,
                CedulaFiador = pagare.CedulaFiador,
                UbicacionFirma = pagare.UbicacionFirma,

                CedulaAbogado = pagare.CedulaAbogado,

                TipoSociedad = pagare.TipoSociedad,
                UbicacionSociedad = pagare.UbicacionSociedad,

                CedulaDeudorNavigation = pagare.CedulaDeudorNavigation,
                CedulaFiadorNavigation = pagare.CedulaFiadorNavigation,
                LugarPagoNavigation = pagare.LugarPagoNavigation
            };
        }

        public TDocsPagare ObtenerDeFront(DocsPagareDTO dto)
        {
            if (!DateOnly.TryParseExact(dto.FechaFirma, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fechaFirma))
                fechaFirma = DateOnly.FromDateTime(DateTime.Today);

            if (!TimeOnly.TryParseExact(dto.HoraFirma, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var horaFirma))
                horaFirma = TimeOnly.FromDateTime(DateTime.Now);

            if (!DateOnly.TryParseExact(dto.FechaVencimiento, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fechaVencimiento))
                fechaVencimiento = DateOnly.FromDateTime(DateTime.Today.AddMonths(1));

            return new TDocsPagare
            {
                IdDocumento = dto.IdDocumento,
                MontoNumerico = dto.MontoNumerico,
                CedulaDeudor = dto.CedulaDeudor,
                SociedadDeudor = dto.SociedadDeudor,
                CedulaJuridicaSociedad = dto.CedulaJuridicaSociedad,
                AcreedorNombre = dto.AcreedorNombre,
                CedulaJuridicaAcreedor = dto.CedulaJuridicaAcreedor,
                AcreedorDomicilio = dto.AcreedorDomicilio,

                FechaFirma = fechaFirma,
                HoraFirma = horaFirma,
                FechaVencimiento = fechaVencimiento,

                InteresFormula = dto.InteresFormula,
                InteresTasaActual = dto.InteresTasaActual,
                InteresBase = dto.InteresBase,

                LugarPago = dto.LugarPago,
                CedulaFiador = dto.CedulaFiador,
                UbicacionFirma = dto.UbicacionFirma,

                CedulaAbogado = dto.CedulaAbogado,

                TipoSociedad = dto.TipoSociedad,
                UbicacionSociedad = dto.UbicacionSociedad,

                CedulaDeudorNavigation = dto.CedulaDeudorNavigation,
                CedulaFiadorNavigation = dto.CedulaFiadorNavigation,
                LugarPagoNavigation = dto.LugarPagoNavigation
            };
        }
    }
}
