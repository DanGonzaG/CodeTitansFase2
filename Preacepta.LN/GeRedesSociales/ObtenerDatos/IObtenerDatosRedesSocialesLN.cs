using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeRedesSociales.ObtenerDatos
{
    public interface IObtenerDatosRedesSocialesLN
    {
        GeRedesSocialeDTO ObtenerDeDB(TGeRedesSociale datos);
        TGeRedesSociale ObtenerDeFront(GeRedesSocialeDTO datos);
    }
}
