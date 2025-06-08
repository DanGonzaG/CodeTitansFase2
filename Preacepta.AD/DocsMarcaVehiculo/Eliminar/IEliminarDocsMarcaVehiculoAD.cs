using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsMarcaVehiculo.Eliminar
{
    public interface IEliminarDocsMarcaVehiculoAD
    {
        Task<int> Eliminar(int id);
    }
}
