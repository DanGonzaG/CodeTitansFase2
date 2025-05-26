using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CrDireccion1.Crear
{
    public interface ICrearCrDireccion1AD
    {
        Task<int> crearProvincia(TCrProvincia crear);
        Task<int> crearCanton(TCrCantone crear);
        Task<int> crearDistrito(TCrDistrito crear);
    }
}
