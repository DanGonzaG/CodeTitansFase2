using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GePersona.Crear
{
    public interface ICrearGePersonaAD
    {
        Task<int> crear(TGePersona gePersona);
    }
}
