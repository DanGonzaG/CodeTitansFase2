using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CasosTipo.Eliminar
{
    public interface IEliminarCasosTipoLN
    {
        Task<int> Eliminar(int id);
    }
}
