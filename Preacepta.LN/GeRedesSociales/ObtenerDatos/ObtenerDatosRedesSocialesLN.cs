using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeRedesSociales.ObtenerDatos
{
    public class ObtenerDatosRedesSocialesLN : IObtenerDatosRedesSocialesLN
    {

        public GeRedesSocialeDTO ObtenerDeDB(TGeRedesSociale datos)
        {
            return new GeRedesSocialeDTO
            {
                Cedula = datos.Cedula,
                CedulaNavigation = datos.CedulaNavigation,
                IdRs = datos.IdRs,
                LinkRedSocila = datos.LinkRedSocila,
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TGeRedesSociale ObtenerDeFront(GeRedesSocialeDTO datos)
        {
            return new TGeRedesSociale
            {
                Cedula = datos.Cedula,
                CedulaNavigation = datos.CedulaNavigation,
                IdRs = datos.IdRs,
                LinkRedSocila = datos.LinkRedSocila,
            };
        }
    }
}
