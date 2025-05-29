using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeRedesSociales.Editar
{
    public interface IEditarRedesSocialesAD
    {
        Task<int> Editar(TGeRedesSociale editar);
    }
}
