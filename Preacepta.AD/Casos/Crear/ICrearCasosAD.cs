using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Casos.Crear
{
    public interface ICrearCasosAD
    {
        Task<int> crear(TCaso crear);
    }
}
