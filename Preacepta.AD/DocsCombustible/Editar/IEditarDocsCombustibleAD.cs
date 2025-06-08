using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsCombustible.Editar
{
    public interface IEditarDocsCombustibleAD
    {
        Task<int> Editar(TDocsCombustible editar);
    }
}
