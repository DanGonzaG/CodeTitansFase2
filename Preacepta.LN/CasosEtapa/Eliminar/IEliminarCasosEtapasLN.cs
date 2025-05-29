using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CasosEtapa.Eliminar
{
    public interface IEliminarCasosEtapasLN
    {
        Task<int> Eliminar(int id);
    }
}
