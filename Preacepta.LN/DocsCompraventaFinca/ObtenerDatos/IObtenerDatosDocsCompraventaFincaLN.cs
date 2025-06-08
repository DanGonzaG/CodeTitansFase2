using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsCompraventaFinca.ObtenerDatos
{
    public interface IObtenerDatosDocsCompraventaFincaLN
    {
        DocsCompraventaFincaDTO ObtenerDeDB(TDocsCompraventaFinca datos);
        TDocsCompraventaFinca ObtenerDeFront(DocsCompraventaFincaDTO datos);
    }
}
