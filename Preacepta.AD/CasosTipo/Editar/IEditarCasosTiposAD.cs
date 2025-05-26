using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CasosTipo.Editar
{
    public interface IEditarCasosTiposAD
    {
        Task<int> Editar(TCasosTipo editar);
    }
}
