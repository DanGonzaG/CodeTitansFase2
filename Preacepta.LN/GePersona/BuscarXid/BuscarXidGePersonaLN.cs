using Preacepta.AD.GePersona.BuscarXid;
using Preacepta.LN.GePersona.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GePersona.BuscarXid
{
    public class BuscarXidGePersonaLN : IBuscarXidGePersonaLN
    {
        private readonly IBuscarXidGePersonaAD _buscarXidGePersonaAD;
        private readonly IObtenerDatosLN _obtenerDatosLN;

        public BuscarXidGePersonaLN(IBuscarXidGePersonaAD buscarXidGePersonaAD, IObtenerDatosLN obtnerDatosLN)
        {
            _buscarXidGePersonaAD = buscarXidGePersonaAD;
            _obtenerDatosLN = obtnerDatosLN;
        }

        public async Task <GePersonaDTO?> buscar (int id) 
        {
            try
            {
                TGePersona? gePersona = await _buscarXidGePersonaAD.buscar(id);
                if (gePersona == null)
                {
                    Console.WriteLine("No se encontró la persona.");
                    return null;
                }
                GePersonaDTO gePersonaDTO = _obtenerDatosLN.ObtenerDeDB(gePersona);
                return gePersonaDTO;
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error en BuscarXidGePersonaLN: {ex.Message}");
                return null;
            }
            

        }
    }
}
