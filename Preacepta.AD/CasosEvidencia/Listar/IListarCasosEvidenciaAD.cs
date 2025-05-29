using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CasosEvidencia.Listar
{
    public interface IListarCasosEvidenciaAD
    {
        Task<List<CasosEvidenciaDTO>> listar();
    }
}
