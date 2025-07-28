using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsCompraventaFinca.Listar
{
    public interface IListarDocsCompraventaFincaAD
    {
        Task<List<DocsCompraventaFincaDTO>> listar();
        Task<List<DocsCompraventaFincaDTO>> ListarTresUltimosDocs(int cedula);
        Task<List<DocsCompraventaFincaDTO>> ListarTresUltimosDocsXCliente(int cedula);
    }
}
