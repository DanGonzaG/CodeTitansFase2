using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GePersona.Eliminar
{
    public interface IEliminarPersonaAD
    {
        Task<int> eliminar(int id);
    }
}
