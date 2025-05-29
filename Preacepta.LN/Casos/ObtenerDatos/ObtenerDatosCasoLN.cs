using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Casos.ObtenerDatos
{
    public class ObtenerDatosCasoLN : IObtenerDatosCasoLN
    {
        public CasoDTO ObtenerDeDB(TCaso datos)
        {
            return new CasoDTO
            {
                IdCaso = datos.IdCaso,
                Fecha = datos.Fecha.ToString("dd-MM-yyyy"),
                IdTipoCaso = datos.IdTipoCaso,
                Descripcion = datos.Descripcion,
                IdAbogado = datos.IdAbogado,
                IdCliente = datos.IdCliente,
                Activo = datos.Activo,
                IdAbogadoNavigation = datos.IdAbogadoNavigation,
                IdClienteNavigation = datos.IdClienteNavigation,
                IdTipoCasoNavigation = datos.IdTipoCasoNavigation,
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TCaso ObtenerDeFront(CasoDTO datos)
        {
            return new TCaso
            {
                IdCaso = datos.IdCaso,
                Fecha = DateTime.Now,
                IdTipoCaso = datos.IdTipoCaso,
                Descripcion = datos.Descripcion,
                IdAbogado = datos.IdAbogado,
                IdCliente = datos.IdCliente,
                Activo = datos.Activo,
                IdAbogadoNavigation = datos.IdAbogadoNavigation,
                IdClienteNavigation = datos.IdClienteNavigation,
                IdTipoCasoNavigation = datos.IdTipoCasoNavigation,
            };
        }

    }
}
