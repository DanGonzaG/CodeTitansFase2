using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeAbogado.Eliminar
{
    public interface IEliminarAbogadoAD
    {
        Task<int> Eliminar(int id);
    }
}
