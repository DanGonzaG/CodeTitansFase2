using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CrDireccion1.ObtenerDatos
{
    public interface IObtenerDatosDireccion1LN
    {
        CrProvinciaDTO ObtenerDeDBProvincias(TCrProvincia datos);
        TCrProvincia ObtenerDeFrontProvincias(CrProvinciaDTO datos);
        CrCantonDTO ObtenerDeDBCanton(TCrCantone datos);
        TCrCantone ObtenerDeFrontCanton(CrCantonDTO datos);
        CrDistritoDTO ObtenerDeDBDistrito(TCrDistrito datos);
        TCrDistrito ObtenerDeFrontDistrito(CrDistritoDTO datos);

    }
}
