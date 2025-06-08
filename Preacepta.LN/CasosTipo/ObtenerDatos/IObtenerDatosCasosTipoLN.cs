using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosTipo.ObtenerDatos
{
    public interface IObtenerDatosCasosTipoLN
    {
        CasosTipoDTO ObtenerDeDB(TCasosTipo baseDatos);
        TCasosTipo ObtenerDeFront(CasosTipoDTO Formulario);
    }
}
