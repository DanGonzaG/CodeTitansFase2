using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeAbogado.ObtenerDatos
{
    public interface IObtenerDatosAbogadoLN
    {
        GeAbogadoDTO ObtenerDeDB(TGeAbogado baseDatos);
        TGeAbogado ObtenerDeFront(GeAbogadoDTO Formulario);
    }
}
