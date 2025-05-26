using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CrDireccion1.Editar
{
    public interface IEditarCrDireccion1LN
    {
        Task<int> EditarProvincia(CrProvinciaDTO editar);

        Task<int> EditarCanton(CrCantonDTO editar);
        Task<int> EditarDistrito(CrDistritoDTO editar);
    }
}
