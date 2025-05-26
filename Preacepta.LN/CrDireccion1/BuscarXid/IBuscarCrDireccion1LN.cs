using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CrDireccion1.BuscarXid
{
    public interface IBuscarCrDireccion1LN
    {
        Task<CrProvinciaDTO?> buscarProvincia(int id);
        Task<CrCantonDTO?> buscarCanton(int id);
        Task<CrDistritoDTO?> buscarDistrito(int id);
    }
}
