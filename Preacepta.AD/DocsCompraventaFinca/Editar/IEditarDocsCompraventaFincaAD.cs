using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsCompraventaFinca.Editar
{
    public interface IEditarDocsCompraventaFincaAD
    {
        Task<int> Editar(TDocsCompraventaFinca editar);
    }
}
