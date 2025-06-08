using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Testimonios.Eliminar
{
    public interface IEliminarTestLN
    {
        Task<int> eliminar(int id);
    }
}
