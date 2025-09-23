using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.HistorialDocumentos.Listar
{
    public interface IListarHistorialLN
    {
        Task<List<HistorialDocumentoDTO>> listar();
        Task<List<HistorialDocumentoDTO>> listarXabogado(int cedula);
        Task<List<HistorialDocumentoDTO>> listarXcliente(int cedula);
        Task<HistorialDocumentoDTO?> listarXultimaFecha(int cedulaAbogado);
        Task<List<HistorialDocumentoDTO>> listarXclienteLos3Docs(int cedulaCliente);
        Task<List<HistorialDocumentoDTO>> listarXabogadoLos3Docs(int cedulaAbogado);
    }
}
