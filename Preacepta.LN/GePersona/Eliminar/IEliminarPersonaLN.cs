using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GePersona.Eliminar
{
    public interface IEliminarPersonaLN
    {
        Task<int> eliminar(int id);
    }
}
