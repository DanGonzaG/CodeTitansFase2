using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsCompraventaFinca.ObtenerDatos
{
    public class ObtenerDatosDocsCompraventaFincaLN : IObtenerDatosDocsCompraventaFincaLN
    {
        public DocsCompraventaFincaDTO ObtenerDeDB(TDocsCompraventaFinca datos)
        {
            return new DocsCompraventaFincaDTO
            {
                AreaFincaM2 = datos.AreaFincaM2,
                CantonFinca = datos.CantonFinca,
                CedulaAbogado = datos.CedulaAbogado,
                CedulaAbogadoNavigation = datos.CedulaAbogadoNavigation,
                CedulaComprador = datos.CedulaComprador,
                CedulaCompradorNavigation = datos.CedulaCompradorNavigation,
                CedulaVendedor = datos.CedulaVendedor,
                CedulaVendedorNavigation = datos.CedulaVendedorNavigation,
                ColindaEste = datos.ColindaEste,
                ColindaNorte = datos.ColindaNorte,
                ColindaOeste = datos.ColindaOeste,
                ColindaSur = datos.ColindaSur,
                DistritoFinca = datos.DistritoFinca,
                DistritoFincaNavigation = datos.DistritoFincaNavigation,
                FechaFirma = datos.FechaFirma,
                FormaPago = datos.FormaPago,
                HoraFirma = datos.HoraFirma,
                IdDocumento = datos.IdDocumento,
                LugarFirma = datos.LugarFirma,
                LugarFirmaNavigation = datos.LugarFirmaNavigation,
                MatriculaFinca = datos.MatriculaFinca,
                MedioPago = datos.MedioPago,
                MontoVenta = datos.MontoVenta,
                NaturalezaFinca = datos.NaturalezaFinca,
                NumeroEscritura = datos.NumeroEscritura,
                OrigenFondos = datos.OrigenFondos,
                PartidoFinca = datos.PartidoFinca,
                PlanoCatastrado = datos.PlanoCatastrado,
                ProvinciaFinca = datos.ProvinciaFinca
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TDocsCompraventaFinca ObtenerDeFront(DocsCompraventaFincaDTO datos)
        {
            return new TDocsCompraventaFinca
            {
                AreaFincaM2 = datos.AreaFincaM2,
                CantonFinca = datos.CantonFinca,
                CedulaAbogado = datos.CedulaAbogado,
                CedulaAbogadoNavigation = datos.CedulaAbogadoNavigation,
                CedulaComprador = datos.CedulaComprador,
                CedulaCompradorNavigation = datos.CedulaCompradorNavigation,
                CedulaVendedor = datos.CedulaVendedor,
                CedulaVendedorNavigation = datos.CedulaVendedorNavigation,
                ColindaEste = datos.ColindaEste,
                ColindaNorte = datos.ColindaNorte,
                ColindaOeste = datos.ColindaOeste,
                ColindaSur = datos.ColindaSur,
                DistritoFinca = datos.DistritoFinca,
                DistritoFincaNavigation = datos.DistritoFincaNavigation,
                FechaFirma = datos.FechaFirma,
                FormaPago = datos.FormaPago,
                HoraFirma = datos.HoraFirma,
                IdDocumento = datos.IdDocumento,
                LugarFirma = datos.LugarFirma,
                LugarFirmaNavigation = datos.LugarFirmaNavigation,
                MatriculaFinca = datos.MatriculaFinca,
                MedioPago = datos.MedioPago,
                MontoVenta = datos.MontoVenta,
                NaturalezaFinca = datos.NaturalezaFinca,
                NumeroEscritura = datos.NumeroEscritura,
                OrigenFondos = datos.OrigenFondos,
                PartidoFinca = datos.PartidoFinca,
                PlanoCatastrado = datos.PlanoCatastrado,
                ProvinciaFinca = datos.ProvinciaFinca
            };
        }
    }
}
