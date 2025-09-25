using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsPagare.Editar
{
    public class EditarPagareAD : IEditarPagareAD
    {
        private readonly Contexto _contexto;

        public EditarPagareAD(Contexto contexto)
        {
            _contexto = contexto;
        }


        public async Task<int> editar(TDocsPagare pagare)
        {
            if (pagare == null || pagare.IdDocumento <= 0)
            {
                Console.WriteLine("EditarPagareAD: DTO nulo o IdDocumento inválido.");
                return 0;
            }

            try
            {
                var existente = await _contexto.TDocsPagares.FindAsync(pagare.IdDocumento);
                if (existente == null)
                {
                    Console.WriteLine($"EditarPagareAD: No se encontró el pagaré Id={pagare.IdDocumento}.");
                    return 0;
                }

                existente.MontoNumerico = pagare.MontoNumerico;
                existente.CedulaDeudor = pagare.CedulaDeudor;
                existente.SociedadDeudor = pagare.SociedadDeudor;
                existente.CedulaJuridicaSociedad = pagare.CedulaJuridicaSociedad;
                existente.AcreedorNombre = pagare.AcreedorNombre;
                existente.CedulaJuridicaAcreedor = pagare.CedulaJuridicaAcreedor;
                existente.AcreedorDomicilio = pagare.AcreedorDomicilio;
                existente.FechaFirma = pagare.FechaFirma;
                existente.HoraFirma = pagare.HoraFirma;          
                existente.FechaVencimiento = pagare.FechaVencimiento;
                existente.InteresFormula = pagare.InteresFormula;
                existente.InteresTasaActual = pagare.InteresTasaActual;
                existente.InteresBase = pagare.InteresBase;
                existente.LugarPago = pagare.LugarPago;
                existente.CedulaFiador = pagare.CedulaFiador;
                existente.UbicacionFirma = pagare.UbicacionFirma;
                existente.CedulaAbogado = pagare.CedulaAbogado;
                existente.TipoSociedad = pagare.TipoSociedad;
                existente.UbicacionSociedad = pagare.UbicacionSociedad;

                return await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarPagareAD: {ex.Message}");
                return 0;
            }
        }
    }
}
