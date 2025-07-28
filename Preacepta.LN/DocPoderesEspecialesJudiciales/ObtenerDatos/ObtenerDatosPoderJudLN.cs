using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocPoderesEspecialesJudiciales.ObtenerDatos
{
    public class ObtenerDatosPoderJudLN : IObtenerDatosPoderJudLN
    {
        public DocsPoderesEspecialesJudicialeDTO ObtenerDeDB(TDocsPoderesEspecialesJudiciale poderJud)
        {
            return new DocsPoderesEspecialesJudicialeDTO
            {
                IdDoc = poderJud.IdDoc,
                IdAbogado = poderJud.IdAbogado,
                IdCliente = poderJud.IdCliente,
                Texto = poderJud.Texto,
                Fecha = poderJud.Fecha.ToString("dd/MM/yyyy"),
                IdAbogadoNavigation = poderJud.IdAbogadoNavigation,
                IdClienteNavigation = poderJud.IdClienteNavigation
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TDocsPoderesEspecialesJudiciale ObtenerDeFront(DocsPoderesEspecialesJudicialeDTO poderJudDTO)
        {
            return new TDocsPoderesEspecialesJudiciale
            {
                IdDoc = poderJudDTO.IdDoc,
                IdAbogado = poderJudDTO.IdAbogado,
                IdCliente = poderJudDTO.IdCliente,
                Texto = poderJudDTO.Texto,
                Fecha = DateTime.Now,
                IdAbogadoNavigation = poderJudDTO.IdAbogadoNavigation,
                IdClienteNavigation = poderJudDTO.IdClienteNavigation
            };
        }
    }
}
