using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CasosEvidencia.ObtenerDatos
{
    public class ObtnerDatosCasoEvidenciaLN : IObtnerDatosCasoEvidenciaLN
    {
        public CasosEvidenciaDTO ObtenerDeDB(TCasosEvidencia datos)
        {
            return new CasosEvidenciaDTO
            {
                IdEvidencia = datos.IdEvidencia,
                IdCaso = datos.IdCaso,
                Archivo = datos.Archivo,
                Titulo = datos.Titulo,
                IdCasoNavigation = datos.IdCasoNavigation,
                IdCaso1 = datos.IdCaso1
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TCasosEvidencia ObtenerDeFront(CasosEvidenciaDTO datos)
        {
            return new TCasosEvidencia
            {
                IdEvidencia = datos.IdEvidencia,
                IdCaso = datos.IdCaso,
                Archivo = datos.Archivo,
                Titulo = datos.Titulo,
                IdCasoNavigation = datos.IdCasoNavigation,
                IdCaso1 = datos.IdCaso1
            };
        }
    }
}
