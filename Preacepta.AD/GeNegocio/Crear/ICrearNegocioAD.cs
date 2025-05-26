using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeNegocio.Crear
{
    public interface ICrearNegocioAD
    {
        Task<int> crear(TGeNegocio crear);
    }
}
