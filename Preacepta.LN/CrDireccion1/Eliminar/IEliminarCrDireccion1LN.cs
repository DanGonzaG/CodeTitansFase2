using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CrDireccion1.Eliminar
{
    public interface IEliminarCrDireccion1LN
    {
        Task<int> EliminarProvincia(int id);
        Task<int> EliminarCanton(int id);
        Task<int> EliminarDistrito(int id);
    }
}
