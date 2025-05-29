using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Casos.Crear
{
    public interface ICrearCasosLN
    {
        Task<int> Crear(CasoDTO crear);
    }
}
