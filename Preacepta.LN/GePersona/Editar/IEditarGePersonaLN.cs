using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GePersona.Editar
{
    public interface IEditarGePersonaLN
    {
        Task<int> editar(GePersonaDTO gePersonaDTO);
    }
}
