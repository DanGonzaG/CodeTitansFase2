using Preacepta.AD.GePersona.Crear;
using Preacepta.LN.GePersona.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GePersona.Crear
{
    public class CrearGePersonaLN : ICrearGePersonaLN
    {
        private readonly ICrearGePersonaAD _crearGePersonaAD;
        private readonly IObtenerDatosLN _obtenerDatosLN;

        public CrearGePersonaLN(ICrearGePersonaAD crearGePersonaAD, IObtenerDatosLN obtenerDatosLN) 
        {
            _crearGePersonaAD = crearGePersonaAD;
            _obtenerDatosLN = obtenerDatosLN;
        }

        public async Task <int> crear(GePersonaDTO gePersonaDTO) 
        {
            int bandera = await _crearGePersonaAD.crear(_obtenerDatosLN.ObtenerDeFront(gePersonaDTO));
            return bandera;
        }
    }
}
