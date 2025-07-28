using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GePersona.ObtenerDatos
{
    public interface IObtenerDatosLN
    {
        GePersonaDTO ObtenerDeDB(TGePersona gePersona);
        TGePersona ObtenerDeFrontCrear(GePersonaDTO gePersona);

        TGePersona ObtenerDeFrontEditar(GePersonaDTO gePersona);
    }
}
