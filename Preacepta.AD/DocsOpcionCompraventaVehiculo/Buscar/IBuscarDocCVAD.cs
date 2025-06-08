using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsOpcionCompraventaVehiculo.Buscar
{
    public interface IBuscarDocCVAD
    {
        Task<TDocsOpcionCompraventaVehiculo?> buscar(int id);
    }
}
