using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Casos.Editar
{
    public interface IEditarCasosLN
    {
        Task<int> Editar(CasoDTO editar);
    }
}
