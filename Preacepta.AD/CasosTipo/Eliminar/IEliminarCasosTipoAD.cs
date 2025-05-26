using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CasosTipo.Eliminar
{
    public interface IEliminarCasosTipoAD
    {
        Task<int> Eliminar(int id);
    }
}
