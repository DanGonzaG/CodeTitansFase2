using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.Casos.ObtenerDatos
{
    public interface IObtenerDatosCasoLN
    {
        CasoDTO ObtenerDeDB(TCaso datos);
        TCaso ObtenerDeFrontCrear(CasoDTO datos);
        TCaso ObtenerDeFrontEditar(CasoDTO datos);
    }
}
