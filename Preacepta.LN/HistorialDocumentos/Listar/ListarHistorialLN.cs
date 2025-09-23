using Preacepta.AD.HistorialDocumentos.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.HistorialDocumentos.Listar
{
    public class ListarHistorialLN : IListarHistorialLN
    {
        private readonly IListarHistorialAD _listar;

        public ListarHistorialLN(IListarHistorialAD listar)
        {
            _listar = listar;
        }

        public async Task<List<HistorialDocumentoDTO>> listar()
        {
            var lista = await _listar.listar();
            return lista;
        }

        public async Task<List<HistorialDocumentoDTO>> listarXabogado(int cedula)
        {
            var lista = await _listar.listarXabogado(cedula);
            return lista;
        }

        public async Task<List<HistorialDocumentoDTO>> listarXcliente(int cedula)
        {
            var lista = await _listar.listarXcliente(cedula);
            return lista;
        }

        public async Task<HistorialDocumentoDTO?> listarXultimaFecha(int cedulaAbogado)
        {
            var dato = await _listar.listarXultimaFecha(cedulaAbogado);
            return dato;
        }

        public async Task<List<HistorialDocumentoDTO>> listarXclienteLos3Docs(int cedulaCliente)
        {
            var lista = await _listar.listarXclienteLos3Docs(cedulaCliente);
            return lista;
        }

        public async Task<List<HistorialDocumentoDTO>> listarXabogadoLos3Docs(int cedulaAbogado)
        {
            var lista = await _listar.listarXabogadoLos3Docs(cedulaAbogado);
            return lista;
        }
    }
}
