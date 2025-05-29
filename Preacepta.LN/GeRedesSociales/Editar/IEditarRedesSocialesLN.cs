using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeRedesSociales.Editar
{
    public interface IEditarRedesSocialesLN
    {
        Task<int> Editar(GeRedesSocialeDTO editar);
    }
}
