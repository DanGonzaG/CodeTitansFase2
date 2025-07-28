using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsOpcionCompraventaVehiculo.Eliminar
{
    public interface IEliminarDocCVAD
    {
        Task<int> eliminar(int id);
    }
}
