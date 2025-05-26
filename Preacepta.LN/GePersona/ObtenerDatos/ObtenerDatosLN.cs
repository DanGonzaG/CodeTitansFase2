using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GePersona.ObtenerDatos
{
    public class ObtenerDatosLN : IObtenerDatosLN
    {
        /*metodo para usarlo en los vistas de detalles o deatails o buscar por id*/
        public GePersonaDTO ObtenerDeDB(TGePersona gePersona)
        {
            return new GePersonaDTO
            {
                Cedula = gePersona.Cedula,
                Nombre = gePersona.Nombre,
                Apellido1 = gePersona.Apellido1,
                Apellido2 = gePersona.Apellido2,
                FechaNacimiento = gePersona.FechaNacimiento.ToString("dd/MM/yyyy"),
                Edad = gePersona.Edad,
                EstadoCivil = gePersona.EstadoCivil,
                Oficio = gePersona.Oficio,                
                Direccion1 = gePersona.Direccion1,
                Direccion1Navigation = gePersona.Direccion1Navigation,
                Direccion2 = gePersona.Direccion2,
                Email = gePersona.Email,
                FechaRegistro = gePersona.FechaRegistro.ToString("dd/MM/yyyy"),
                Activo = gePersona.Activo,
                Telefono1 = gePersona.Telefono1,
                Telefono2 = gePersona.Telefono2,
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TGePersona ObtenerDeFront(GePersonaDTO gePersona)
        {
            return new TGePersona
            {
                Cedula = gePersona.Cedula,
                Nombre = gePersona.Nombre,
                Apellido1 = gePersona.Apellido1,
                Apellido2 = gePersona.Apellido2,
                FechaNacimiento = DateOnly.Parse(gePersona.FechaNacimiento),
                Edad = gePersona.Edad,
                EstadoCivil = gePersona.EstadoCivil,
                Oficio = gePersona.Oficio,
                Direccion1 = gePersona.Direccion1,
                Direccion1Navigation = gePersona.Direccion1Navigation,
                Direccion2 = gePersona.Direccion2,
                Email = gePersona.Email,
                FechaRegistro = DateTime.Now,
                Activo = gePersona.Activo,
                Telefono1 = gePersona.Telefono1,
                Telefono2 = gePersona.Telefono2,                
            };
        }
    }
}
