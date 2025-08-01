﻿using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                CedulaDeudorNavigation = pagare.CedulaDeudorNavigation,
                CedulaFiadorNavigation = pagare.CedulaFiadorNavigation,
                LugarPagoNavigation = pagare.LugarPagoNavigation
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TDocsPagare ObtenerDeFront(DocsPagareDTO pagareDTO)
        {
            if (!DateOnly.TryParseExact(pagareDTO.FechaFirma, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fechaFirma))
            {
                throw new FormatException($"FechaFirma inválida: {pagareDTO.FechaFirma}");
            }

            if (!TimeOnly.TryParseExact(pagareDTO.HoraFirma, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var horaFirma))
            {
                throw new FormatException($"HoraFirma inválida: {pagareDTO.HoraFirma}");
            }

            if (!DateOnly.TryParseExact(pagareDTO.FechaVencimiento, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fechaVencimiento))
            {
                throw new FormatException($"FechaVencimiento inválida: {pagareDTO.FechaVencimiento}");
            }

            return new TDocsPagare
            {
                IdDocumento = pagareDTO.IdDocumento,
                MontoNumerico = pagareDTO.MontoNumerico,
                CedulaDeudor = pagareDTO.CedulaDeudor,
                SociedadDeudor = pagareDTO.SociedadDeudor,
                CedulaJuridicaSociedad = pagareDTO.CedulaJuridicaSociedad,
                AcreedorNombre = pagareDTO.AcreedorNombre,
                CedulaJuridicaAcreedor = pagareDTO.CedulaJuridicaAcreedor,
                AcreedorDomicilio = pagareDTO.AcreedorDomicilio,
                FechaFirma = DateOnly.ParseExact(pagareDTO.FechaFirma, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                HoraFirma = TimeOnly.ParseExact(pagareDTO.HoraFirma, "HH:mm", CultureInfo.InvariantCulture),
                FechaVencimiento = DateOnly.ParseExact(pagareDTO.FechaVencimiento, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                InteresFormula = pagareDTO.InteresFormula,
                InteresTasaActual = pagareDTO.InteresTasaActual,
                InteresBase = pagareDTO.InteresBase,
                LugarPago = pagareDTO.LugarPago,
                CedulaFiador = pagareDTO.CedulaFiador,
                UbicacionFirma = pagareDTO.UbicacionFirma,
                CedulaDeudorNavigation = pagareDTO.CedulaDeudorNavigation,
                CedulaFiadorNavigation = pagareDTO.CedulaFiadorNavigation,
                LugarPagoNavigation = pagareDTO.LugarPagoNavigation
            };
        }
    }
}
