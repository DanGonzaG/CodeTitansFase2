using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsOpcionCompraventaVehiculo.Crear
{
    public interface ICrearDocCVAD
    {
        Task<int> crear(TDocsOpcionCompraventaVehiculo docCV);
    }
}
