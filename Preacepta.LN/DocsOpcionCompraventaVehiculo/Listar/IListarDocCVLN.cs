using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsOpcionCompraventaVehiculo.Listar
{
    public interface IListarDocCVLN
    {
        Task<List<DocsOpcionCompraventaVehiculoDTO>> Listar();
    }
}
