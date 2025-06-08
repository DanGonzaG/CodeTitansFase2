using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsCombustible.Editar
{
    public interface IEditarDocsCombustibleLN
    {
        Task<int> Editar(DocsCombustibleDTO editar);
    }
}
