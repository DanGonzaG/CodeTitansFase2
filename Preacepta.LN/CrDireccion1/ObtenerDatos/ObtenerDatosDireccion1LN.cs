using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CrDireccion1.ObtenerDatos
{
    public class ObtenerDatosDireccion1LN : IObtenerDatosDireccion1LN
    {
        public CrProvinciaDTO ObtenerDeDBProvincias(TCrProvincia datos)
        {
            return new CrProvinciaDTO
            {
                IdProvincia = datos.IdProvincia,
                NombreProvincia = datos.NombreProvincia,                
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TCrProvincia ObtenerDeFrontProvincias(CrProvinciaDTO datos)
        {
            return new TCrProvincia
            {
                IdProvincia = datos.IdProvincia,
                NombreProvincia = datos.NombreProvincia,
            };
        }

        public CrCantonDTO ObtenerDeDBCanton(TCrCantone datos)
        {
            return new CrCantonDTO
            {
                IdCanton = datos.IdCanton,
                NombreCanton = datos.NombreCanton,
                IdProvincia = datos.IdProvincia,
                IdProvinciaNavigation = datos.IdProvinciaNavigation,
               
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TCrCantone ObtenerDeFrontCanton(CrCantonDTO datos)
        {
            return new TCrCantone
            {
                IdCanton = datos.IdCanton,
                NombreCanton = datos.NombreCanton,
                IdProvincia = datos.IdProvincia,
                IdProvinciaNavigation = datos.IdProvinciaNavigation,
            };
        }

        public CrDistritoDTO ObtenerDeDBDistrito(TCrDistrito datos)
        {
            return new CrDistritoDTO
            {
                IdDistrito = datos.IdDistrito,
                NombreDistrito = datos.NombreDistrito,
                IdCaton = datos.IdCaton,
                IdCatonNavigation = datos.IdCatonNavigation,
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TCrDistrito ObtenerDeFrontDistrito(CrDistritoDTO datos)
        {
            return new TCrDistrito
            {
                IdDistrito = datos.IdDistrito,
                NombreDistrito = datos.NombreDistrito,
                IdCaton = datos.IdCaton,
                IdCatonNavigation = datos.IdCatonNavigation,
            };
        }
    }
}
