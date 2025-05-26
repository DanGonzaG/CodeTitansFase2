using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CrDireccion1.Crear
{
    public interface ICrearCrDireccion1LN
    {
        Task<int> CrearProvincia(CrProvinciaDTO crear);
        Task<int> CrearCanton(CrCantonDTO crear);
        Task<int> CrearDistrito(CrDistritoDTO crear);
    }
}
