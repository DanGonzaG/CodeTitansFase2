using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            List<DocsPagareDTO> lista = await (from doc in _contexto.TDocsPagares
                                               select new DocsPagareDTO
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
                                                   HoraFirma = doc.FechaFirma.ToString("HH:mm"),
                                                   FechaVencimiento = doc.FechaVencimiento.ToString("yyyy-MM-dd"),
                                                   InteresFormula = doc.InteresFormula,
                                                   InteresTasaActual = doc.InteresTasaActual,
                                                   InteresBase = doc.InteresBase,
                                                   LugarPago = doc.LugarPago,
                                                   CedulaFiador = doc.CedulaFiador,
                                                   UbicacionFirma = doc.UbicacionFirma,
                                                   CedulaDeudorNavigation = doc.CedulaDeudorNavigation,
                                                   CedulaFiadorNavigation = doc.CedulaFiadorNavigation,
                                                   LugarPagoNavigation = doc.LugarPagoNavigation
                                               }).ToListAsync();
            return lista;
        }

        public async Task<List<DocsPagareDTO>> Listar()
        {
            try
            {
                // 1) Trae TODO desde la BD
                var raws = await _contexto.TDocsPagares
                                         .AsNoTracking()
                                         .ToListAsync();

                // 2) Proyecta en memoria y formatea strings
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

                    // >>> Aquí usa la propiedad HoraFirma (TimeOnly),
                    //     no FechaFirma, para formatear la hora:
                    FechaFirma = doc.FechaFirma.ToString("yyyy-MM-dd"),
                    HoraFirma = doc.HoraFirma.ToString("HH:mm"),
                    FechaVencimiento = doc.FechaVencimiento.ToString("yyyy-MM-dd"),

                    InteresFormula = doc.InteresFormula,
                    InteresTasaActual = doc.InteresTasaActual,
                    InteresBase = doc.InteresBase,
                    LugarPago = doc.LugarPago,
                    CedulaFiador = doc.CedulaFiador,
                    UbicacionFirma = doc.UbicacionFirma,

                    CedulaDeudorNavigation = doc.CedulaDeudorNavigation,
                    CedulaFiadorNavigation = doc.CedulaFiadorNavigation,
                    LugarPagoNavigation = doc.LugarPagoNavigation
                })
                .ToList();

                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos {ex.Message}");
                return new List<DocsPagareDTO>();
            }
        }
    }
}
