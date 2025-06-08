using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosEtapa.ObtenerDatos
{
    public interface IObtnerDatosCasoEtapaLN
    {
        CasosEtapaDTO ObtenerDeDB(TCasosEtapa datos);
        TCasosEtapa ObtenerDeFront(CasosEtapaDTO datos);
    }
}
