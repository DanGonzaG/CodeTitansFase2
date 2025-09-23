using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsPagare.Listar
{
    public class ListarPagareAD : IListarPagareAD
    {
        private readonly Contexto _contexto;

        public ListarPagareAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<DocsPagareDTO>> listar2()
        {
            return await _contexto.TDocsPagares
                .AsNoTracking()
                .Select(doc => new DocsPagareDTO
                {
                    IdDocumento = doc.IdDocumento,
                    MontoNumerico = doc.MontoNumerico,
                    CedulaDeudor = doc.CedulaDeudor,
                    SociedadDeudor = doc.SociedadDeudor,
                    CedulaJuridicaSociedad = doc.CedulaJuridicaSociedad,
                    AcreedorNombre = doc.AcreedorNombre,
                    CedulaJuridicaAcreedor = doc.CedulaJuridicaAcreedor,
                    AcreedorDomicilio = doc.AcreedorDomicilio,

                    FechaFirma = doc.FechaFirma.ToString("yyyy-MM-dd"),
                    HoraFirma = doc.HoraFirma.ToString("HH:mm"),
                    FechaVencimiento = doc.FechaVencimiento.ToString("yyyy-MM-dd"),

                    InteresFormula = doc.InteresFormula,
                    InteresTasaActual = doc.InteresTasaActual,
                    InteresBase = doc.InteresBase,
                    LugarPago = doc.LugarPago,
                    CedulaFiador = doc.CedulaFiador,
                    UbicacionFirma = doc.UbicacionFirma,

                    CedulaAbogado = doc.CedulaAbogado,

                    TipoSociedad = doc.TipoSociedad,
                    UbicacionSociedad = doc.UbicacionSociedad,

                    CedulaDeudorNavigation = doc.CedulaDeudorNavigation,
                    CedulaFiadorNavigation = doc.CedulaFiadorNavigation,
                    LugarPagoNavigation = doc.LugarPagoNavigation
                })
                .ToListAsync();
        }

        public async Task<List<DocsPagareDTO>> Listar()
        {
            try
            {
                var raws = await _contexto.TDocsPagares
                    .AsNoTracking()
                    .ToListAsync();

                var lista = raws.Select(doc => new DocsPagareDTO
                {
                    IdDocumento = doc.IdDocumento,
                    MontoNumerico = doc.MontoNumerico,
                    CedulaDeudor = doc.CedulaDeudor,
                    SociedadDeudor = doc.SociedadDeudor,
                    CedulaJuridicaSociedad = doc.CedulaJuridicaSociedad,
                    AcreedorNombre = doc.AcreedorNombre,
                    CedulaJuridicaAcreedor = doc.CedulaJuridicaAcreedor,
                    AcreedorDomicilio = doc.AcreedorDomicilio,

                    FechaFirma = doc.FechaFirma.ToString("yyyy-MM-dd"),
                    HoraFirma = doc.HoraFirma.ToString("HH:mm"),
                    FechaVencimiento = doc.FechaVencimiento.ToString("yyyy-MM-dd"),

                    InteresFormula = doc.InteresFormula,
                    InteresTasaActual = doc.InteresTasaActual,
                    InteresBase = doc.InteresBase,
                    LugarPago = doc.LugarPago,
                    CedulaFiador = doc.CedulaFiador,
                    UbicacionFirma = doc.UbicacionFirma,

                    CedulaAbogado = doc.CedulaAbogado,

                    TipoSociedad = doc.TipoSociedad,
                    UbicacionSociedad = doc.UbicacionSociedad,

                    CedulaDeudorNavigation = doc.CedulaDeudorNavigation,
                    CedulaFiadorNavigation = doc.CedulaFiadorNavigation,
                    LugarPagoNavigation = doc.LugarPagoNavigation
                })
                .ToList();

                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos: {ex.Message}");
                return new List<DocsPagareDTO>();
            }
        }
    }
}
