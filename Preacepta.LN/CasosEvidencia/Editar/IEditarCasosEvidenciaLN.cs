using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CasosEvidencia.Editar
{
    public interface IEditarCasosEvidenciaLN
    {
        Task<int> Editar(CasosEvidenciaDTO editar);
    }
}
