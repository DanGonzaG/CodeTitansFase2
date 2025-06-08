using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Testimonios.Crear
{
    public interface ICrearTestAD
    {
        Task<int> crear(TTestimonio test);
    }
}
