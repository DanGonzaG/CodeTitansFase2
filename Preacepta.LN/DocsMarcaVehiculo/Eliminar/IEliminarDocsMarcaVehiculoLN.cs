using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsMarcaVehiculo.Eliminar
{
    public interface IEliminarDocsMarcaVehiculoLN
    {
        Task<int> Eliminar(int id);
    }
}
