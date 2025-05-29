using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CasosEvidencia.Listar
{
    public interface IListarCasosEvidenciaLN
    {
        Task<List<CasosEvidenciaDTO>> listar();
    }
}
