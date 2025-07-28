using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsTipoVehiculo.Eliminar
{
    public interface IEliminarTipoVehiculoLN
    {
        Task<int> eliminar(int id);
    }
}
