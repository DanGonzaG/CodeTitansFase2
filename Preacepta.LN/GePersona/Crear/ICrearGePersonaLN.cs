using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GePersona.Crear
{
    public interface ICrearGePersonaLN
    {
        Task<int> crear(GePersonaDTO gePersonaDTO);
    }
}
