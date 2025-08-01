﻿using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosTipo.ObtenerDatos
{
    public class ObtenerDatosCasosTipoLN : IObtenerDatosCasosTipoLN
    {
        public CasosTipoDTO ObtenerDeDB(TCasosTipo baseDatos)
        {
            return new CasosTipoDTO
            {
                IdTipoCaso = baseDatos.IdTipoCaso,
                Nombre = baseDatos.Nombre,
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TCasosTipo ObtenerDeFront(CasosTipoDTO Formulario)
        {
            return new TCasosTipo
            {
                IdTipoCaso = Formulario.IdTipoCaso,
                Nombre = Formulario.Nombre,
            };
        }
    }
}
