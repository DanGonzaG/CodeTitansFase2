using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CasosEtapa.Eliminar
{
    public interface IEliminarCasosEtapasAD
    {
        Task<int> Eliminar(int id);
    }
}
