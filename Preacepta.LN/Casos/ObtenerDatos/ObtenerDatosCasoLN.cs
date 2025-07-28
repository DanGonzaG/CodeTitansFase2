using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

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
                Nombre = datos.Nombre,
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
        public TCaso ObtenerDeFrontCrear(CasoDTO datos)
        {
            return new TCaso
            {
                //IdCaso = datos.IdCaso,
                Fecha = DateTime.Now,
                Nombre = datos.Nombre,
                IdTipoCaso = datos.IdTipoCaso,
                Descripcion = datos.Descripcion,
                IdAbogado = datos.IdAbogado,
                IdCliente = datos.IdCliente,
                Activo = true,
                IdAbogadoNavigation = datos.IdAbogadoNavigation,
                IdClienteNavigation = datos.IdClienteNavigation,
                IdTipoCasoNavigation = datos.IdTipoCasoNavigation,
            };
        }

        public TCaso ObtenerDeFrontEditar(CasoDTO datos)
        {
            return new TCaso
            {
                IdCaso = datos.IdCaso,
                Fecha = DateTime.Parse(datos.Fecha),
                Nombre = datos.Nombre,
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
