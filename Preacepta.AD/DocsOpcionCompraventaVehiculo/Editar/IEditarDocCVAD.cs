using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsOpcionCompraventaVehiculo.Editar
{
    public interface IEditarDocCVAD
    {
        Task<int> editar(TDocsOpcionCompraventaVehiculo docCV);
    }
}
