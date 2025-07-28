using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CitasTipo.ObtenerDatos
{
    public interface IObtenerDatosCitasTipoLN
    {
        CitasTipoDTO ObtenerDeDB(TCitasTipo baseDatos);
        TCitasTipo ObtenerDeFront(CitasTipoDTO Formulario);
        Task<List<CitasTipoDTO>> ListarTiposAsync();
    }
}
