using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsCompraventaFinca.Eliminar
{
    public interface IEliminarDocsCompraventaFincaLN
    {
        Task<int> Eliminar(int id);
    }
}
