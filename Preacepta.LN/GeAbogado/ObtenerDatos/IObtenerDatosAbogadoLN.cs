using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeAbogado.ObtenerDatos
{
    public interface IObtenerDatosAbogadoLN
    {
        GeAbogadoDTO ObtenerDeDB(TGeAbogado baseDatos);
        TGeAbogado ObtenerDeFront(GeAbogadoDTO Formulario);
    }
}
