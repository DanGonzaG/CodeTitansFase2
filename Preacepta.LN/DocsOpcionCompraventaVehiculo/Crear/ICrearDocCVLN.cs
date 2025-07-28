using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsOpcionCompraventaVehiculo.Crear
{
    public interface ICrearDocCVLN
    {
        Task<int> crear(DocsOpcionCompraventaVehiculoDTO docCVDTO);
    }
}
