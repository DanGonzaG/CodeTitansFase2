using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsContratoPrestacionServicios.Listar
{
    public class ListarDocsContratoPrestacionServiciosAD : IListarDocsContratoPrestacionServiciosAD
    {
        private readonly Contexto _contexto;

        public ListarDocsContratoPrestacionServiciosAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<DocsContratoPrestacionServicioDTO>> listar()
        {
            try
            {
                return await _contexto.TDocsContratoPrestacionServicios.Select(lista => new DocsContratoPrestacionServicioDTO
                {
                    CedulaAbogado = lista.CedulaAbogado,
                    CedulaAbogadoNavigation = lista.CedulaAbogadoNavigation,
                    CedulaCliente = lista.CedulaCliente,
                    CedulaClienteNavigation = lista.CedulaClienteNavigation,
                    CedulaJuridicaEmpresa = lista.CedulaJuridicaEmpresa,
                    CiudadFirma = lista.CiudadFirma,
                    CiudadFirmaNavigation = lista.CiudadFirmaNavigation,
                    FechaFinal = lista.FechaFinal,
                    FechaFirma = lista.FechaFirma,
                    FechaInicio = lista.FechaInicio,
                    HoraFirma = lista.HoraFirma,
                    IdDocumento = lista.IdDocumento,
                    InformacionConfidencial = lista.InformacionConfidencial,
                    MontoHonorarios = lista.MontoHonorarios,
                    Provincia = lista.Provincia,
                    ProvinciaNavigation = lista.ProvinciaNavigation,
                    RazonSocialEmpresa = lista.RazonSocialEmpresa,
                    TipoServicios = lista.TipoServicios

                }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos {ex.Message}");
                return new List<DocsContratoPrestacionServicioDTO>();
            }

        }
    }
}
