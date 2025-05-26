using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeAbogado.Crear
{
    public interface ICrearAbogadoLN
    {
        Task<int> Crear(GeAbogadoDTO crear);
    }
}
