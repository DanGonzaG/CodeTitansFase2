using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CasosEvidencia.Eliminar
{
    public interface IEliminarCasosEvidenciaAD
    {
        Task<int> Eliminar(int id);
    }
}
