using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsOpcionCompraventaVehiculo.Eliminar
{
    public interface IEliminarDocCVLN
    {
        Task<int> eliminar(int id);
    }
}
