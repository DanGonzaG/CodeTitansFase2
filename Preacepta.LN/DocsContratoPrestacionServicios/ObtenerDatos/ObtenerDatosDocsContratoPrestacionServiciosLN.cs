using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsContratoPrestacionServicios.ObtenerDatos
{
    public class ObtenerDatosDocsContratoPrestacionServiciosLN : IObtenerDatosDocsContratoPrestacionServiciosLN
    {
        public DocsContratoPrestacionServicioDTO ObtenerDeDB(TDocsContratoPrestacionServicio datos)
        {
            return new DocsContratoPrestacionServicioDTO
            {
                CedulaAbogado = datos.CedulaAbogado,
                CedulaAbogadoNavigation = datos.CedulaAbogadoNavigation,
                CedulaCliente = datos.CedulaCliente,
                CedulaClienteNavigation = datos.CedulaClienteNavigation,
                CedulaJuridicaEmpresa = datos.CedulaJuridicaEmpresa,
                CiudadFirma = datos.CiudadFirma,
                CiudadFirmaNavigation = datos.CiudadFirmaNavigation,
                FechaFinal = datos.FechaFinal,
                FechaFirma = datos.FechaFirma,
                FechaInicio = datos.FechaInicio,
                HoraFirma = datos.HoraFirma,
                IdDocumento = datos.IdDocumento,
                InformacionConfidencial = datos.InformacionConfidencial,
                MontoHonorarios = datos.MontoHonorarios,
                Provincia = datos.Provincia,
                ProvinciaNavigation = datos.ProvinciaNavigation,
                RazonSocialEmpresa = datos.RazonSocialEmpresa,
                TipoServicios = datos.TipoServicios
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TDocsContratoPrestacionServicio ObtenerDeFront(DocsContratoPrestacionServicioDTO datos)
        {
            return new TDocsContratoPrestacionServicio
            {
                CedulaAbogado = datos.CedulaAbogado,
                CedulaAbogadoNavigation = datos.CedulaAbogadoNavigation,
                CedulaCliente = datos.CedulaCliente,
                CedulaClienteNavigation = datos.CedulaClienteNavigation,
                CedulaJuridicaEmpresa = datos.CedulaJuridicaEmpresa,
                CiudadFirma = datos.CiudadFirma,
                CiudadFirmaNavigation = datos.CiudadFirmaNavigation,
                FechaFinal = datos.FechaFinal,
                FechaFirma = datos.FechaFirma,
                FechaInicio = datos.FechaInicio,
                HoraFirma = datos.HoraFirma,
                IdDocumento = datos.IdDocumento,
                InformacionConfidencial = datos.InformacionConfidencial,
                MontoHonorarios = datos.MontoHonorarios,
                Provincia = datos.Provincia,
                ProvinciaNavigation = datos.ProvinciaNavigation,
                RazonSocialEmpresa = datos.RazonSocialEmpresa,
                TipoServicios = datos.TipoServicios
            };
        }
    }
}
