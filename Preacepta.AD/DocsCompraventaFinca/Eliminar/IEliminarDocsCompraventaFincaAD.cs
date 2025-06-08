using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsCompraventaFinca.Eliminar
{
    public interface IEliminarDocsCompraventaFincaAD
    {
        Task<int> Eliminar(int id);
    }
}
