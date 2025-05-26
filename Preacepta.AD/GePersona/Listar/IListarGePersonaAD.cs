using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GePersona.Listar
{
    public interface IListarGePersonaAD
    {
        Task<List<GePersonaDTO>> listar();
    }
}
