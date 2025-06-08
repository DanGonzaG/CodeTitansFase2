using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsContratoPrestacionServicios.ObtenerDatos
{
    public interface IObtenerDatosDocsContratoPrestacionServiciosLN
    {
        DocsContratoPrestacionServicioDTO ObtenerDeDB(TDocsContratoPrestacionServicio datos);
        TDocsContratoPrestacionServicio ObtenerDeFront(DocsContratoPrestacionServicioDTO datos);
    }
}
