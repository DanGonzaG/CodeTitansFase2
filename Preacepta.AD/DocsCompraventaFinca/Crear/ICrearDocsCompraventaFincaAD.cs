using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsCompraventaFinca.Crear
{
    public interface ICrearDocsCompraventaFincaAD
    {
        Task<int> crear(TDocsCompraventaFinca crear);
    }
}
