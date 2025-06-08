using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CitasTipo.Eliminar
{
    public interface IEliminarCitasTipoAD
    {
        Task<int> eliminar(int id);
    }
}
