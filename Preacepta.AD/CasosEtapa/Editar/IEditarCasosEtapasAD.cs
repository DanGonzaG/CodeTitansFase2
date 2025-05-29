using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CasosEtapa.Editar
{
    public interface IEditarCasosEtapasAD
    {
        Task<int> Editar(TCasosEtapa editar);
    }
}
