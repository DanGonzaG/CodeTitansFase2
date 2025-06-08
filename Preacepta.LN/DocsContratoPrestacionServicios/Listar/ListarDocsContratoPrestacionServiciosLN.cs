using Preacepta.AD.CasosTipo.Listar;
using Preacepta.AD.DocsContratoPrestacionServicios.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsContratoPrestacionServicios.Listar
{
    public class ListarDocsContratoPrestacionServiciosLN : IListarDocsContratoPrestacionServiciosLN
    {
        private readonly IListarDocsContratoPrestacionServiciosAD _listar;

        public ListarDocsContratoPrestacionServiciosLN(IListarDocsContratoPrestacionServiciosAD listar)
        {
            _listar = listar;
        }

        public async Task<List<DocsContratoPrestacionServicioDTO>> listar()
        {
            List<DocsContratoPrestacionServicioDTO> lista = await _listar.listar();
            return lista;
        }
    }
}
