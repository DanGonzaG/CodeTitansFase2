using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Citas.ObtenerDatos
{
    public interface IObtenerDatosCitasLN
    {
        CitasDTO ObtenerDeDB(TCita baseDatos);
        TCita ObtenerDeFront(CitasDTO Formulario);
        Task<List<CitasDTO>> ListarCitasAsync();
    }
}
