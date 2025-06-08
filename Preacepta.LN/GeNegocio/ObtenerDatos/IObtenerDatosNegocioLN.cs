using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeNegocio.ObtenerDatos
{
    public interface IObtenerDatosNegocioLN
    {
        GeNegocioDTO ObtenerDeDB(TGeNegocio datos);
        TGeNegocio ObtenerDeFront(GeNegocioDTO datos);
    }
}
