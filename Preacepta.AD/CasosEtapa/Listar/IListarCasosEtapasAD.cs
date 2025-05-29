using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CasosEtapa.Listar
{
    public interface IListarCasosEtapasAD
    {
        Task<List<CasosEtapaDTO>> listar();
    }
}
