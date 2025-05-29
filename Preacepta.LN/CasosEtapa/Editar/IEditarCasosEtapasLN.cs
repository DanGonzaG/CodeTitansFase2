using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CasosEtapa.Editar
{
    public interface IEditarCasosEtapasLN
    {
        Task<int> Editar(CasosEtapaDTO editar);
    }
}
