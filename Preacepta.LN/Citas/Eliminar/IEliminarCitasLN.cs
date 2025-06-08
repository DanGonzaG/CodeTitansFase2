using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Citas.Eliminar
{
    public interface IEliminarCitasLN
    {
        Task<int> Eliminar(int id);
    }
}
