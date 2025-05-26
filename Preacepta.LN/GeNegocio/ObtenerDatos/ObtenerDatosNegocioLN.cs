using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeNegocio.ObtenerDatos
{
    public class ObtenerDatosNegocioLN : IObtenerDatosNegocioLN
    {
        public GeNegocioDTO ObtenerDeDB(TGeNegocio datos)
        {
            return new GeNegocioDTO
            {
                CJuridica = datos.CJuridica,
                Nombre = datos.Nombre,
                Representante = datos.Representante,
                Email = datos.Email,
                FechaConsolidacion = datos.FechaConsolidacion.ToString("dd/MM/yyyy"),
                Telefono = datos.Telefono,
                Direccion1 = datos.Direccion1,
                Direccion2 = datos.Direccion2,
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TGeNegocio ObtenerDeFront(GeNegocioDTO datos)
        {
            return new TGeNegocio
            {
                CJuridica = datos.CJuridica,
                Nombre = datos.Nombre,
                Representante = datos.Representante,
                Email = datos.Email,
                FechaConsolidacion = DateOnly.Parse(datos.FechaConsolidacion),
                Telefono = datos.Telefono,
                Direccion1 = datos.Direccion1,
                Direccion2 = datos.Direccion2,
            };
        }
    }
}
