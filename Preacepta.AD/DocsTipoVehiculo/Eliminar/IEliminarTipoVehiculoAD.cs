using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsTipoVehiculo.Eliminar
{
    public interface IEliminarTipoVehiculoAD
    {
        Task<int> eliminar(int id);
    }
}
