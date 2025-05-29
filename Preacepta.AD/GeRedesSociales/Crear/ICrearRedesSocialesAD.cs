using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeRedesSociales.Crear
{
    public interface ICrearRedesSocialesAD
    {
        Task<int> crear(TGeRedesSociale crear);
    }
}
