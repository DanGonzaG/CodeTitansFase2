using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeNegocio.Editar
{
    public interface IEditarNegocioAD
    {
        Task<int> Editar(TGeNegocio editar);
    }
}
