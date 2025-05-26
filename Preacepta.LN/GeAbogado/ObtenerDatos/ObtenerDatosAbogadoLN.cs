using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeAbogado.ObtenerDatos
{
    public class ObtenerDatosAbogadoLN : IObtenerDatosAbogadoLN
    {
        public GeAbogadoDTO ObtenerDeDB(TGeAbogado baseDatos)
        {
            return new GeAbogadoDTO
            {
                Carnet = baseDatos.Carnet,
                Cedula = baseDatos.Cedula,
                CedulaNavigation = baseDatos.CedulaNavigation,
                CJuridica = baseDatos.CJuridica,
                CJuridicaNavigation = baseDatos.CJuridicaNavigation,
                IdTipoAbogado = baseDatos.IdTipoAbogado,
                IdTipoAbogadoNavigation = baseDatos.IdTipoAbogadoNavigation,
                
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TGeAbogado ObtenerDeFront(GeAbogadoDTO Formulario)
        {
            return new TGeAbogado
            {
                Carnet = Formulario.Carnet,
                Cedula = Formulario.Cedula,
                CedulaNavigation = Formulario.CedulaNavigation,
                CJuridica = Formulario.CJuridica,
                CJuridicaNavigation = Formulario.CJuridicaNavigation,
                IdTipoAbogado = Formulario.IdTipoAbogado,
                IdTipoAbogadoNavigation = Formulario.IdTipoAbogadoNavigation,
            };
        }
    }
}
