using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CasosTipo.Editar
{
    public interface IEditarCasosTiposLN
    {
        Task<int> Editar(CasosTipoDTO editar);
    }
}
