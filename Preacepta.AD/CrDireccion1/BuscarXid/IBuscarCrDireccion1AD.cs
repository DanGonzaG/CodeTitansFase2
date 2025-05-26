using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CrDireccion1.BuscarXid
{
    public interface IBuscarCrDireccion1AD
    {
        Task<TCrProvincia?> buscarProvincias(int id);
        Task<TCrCantone?> buscarCantones(int id);
        Task<TCrDistrito?> buscarDistritos(int id);
    }
}
