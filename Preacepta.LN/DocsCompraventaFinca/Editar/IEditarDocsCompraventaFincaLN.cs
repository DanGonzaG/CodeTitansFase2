using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsCompraventaFinca.Editar
{
    public interface IEditarDocsCompraventaFincaLN
    {
        Task<int> Editar(DocsCompraventaFincaDTO editar);
    }
}
