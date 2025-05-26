using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeNegocio.Editar
{
    public interface IEditarNegocioLN
    {
        Task<int> Editar(GeNegocioDTO editar);
    }
}
