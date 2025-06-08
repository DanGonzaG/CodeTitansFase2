using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Testimonios.Eliminar
{
    public interface IEliminarTestAD
    {
        Task<int> eliminar(int id);
    }
}
