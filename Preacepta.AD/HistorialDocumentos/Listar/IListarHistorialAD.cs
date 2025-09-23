using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.AD.HistorialDocumentos.Listar
{
    public interface IListarHistorialAD
    {
        Task<List<HistorialDocumentoDTO>> listar();

        Task<List<HistorialDocumentoDTO>> listarXabogado(int cedula);

        Task<List<HistorialDocumentoDTO>> listarXcliente(int cedula);

        // Por consistencia con tu firma de Casos: devuelve el último por ABOGADO
        Task<HistorialDocumentoDTO?> listarXultimaFecha(int cedulaAbogado);

        Task<List<HistorialDocumentoDTO>> listarXclienteLos3Docs(int cedulaCliente);

        Task<List<HistorialDocumentoDTO>> listarXabogadoLos3Docs(int cedulaAbogado);
    }
}
