using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CrDireccion1.Editar
{
    public interface IEditarCrDireccion1AD
    {
        Task<int> EditarProvincia(TCrProvincia editar);
        Task<int> EditarCanton(TCrCantone editar);
        Task<int> EditarDistrito(TCrDistrito editar);
    }
}
