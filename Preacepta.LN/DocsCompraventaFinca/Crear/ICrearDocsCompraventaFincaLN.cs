using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsCompraventaFinca.Crear
{
    public interface ICrearDocsCompraventaFincaLN
    {
        Task<int> Crear(DocsCompraventaFincaDTO crear);
    }
}
