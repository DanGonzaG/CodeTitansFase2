using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeAbogadoTipo.ObtenerDatos
{
    public interface IObtenerDatosAbogadoTipoLN
    {
        GeAbogadoTipoDTO ObtenerDeDB(TGeAbogadoTipo geAbogadoTipo);
        TGeAbogadoTipo ObtenerDeFront(GeAbogadoTipoDTO geAbogadoTipoDTO);
    }
}
