using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosEvidencia.ObtenerDatos
{
    public interface IObtnerDatosCasoEvidenciaLN
    {
        CasosEvidenciaDTO ObtenerDeDB(TCasosEvidencia datos);
        TCasosEvidencia ObtenerDeFront(CasosEvidenciaDTO datos);
    }
}
