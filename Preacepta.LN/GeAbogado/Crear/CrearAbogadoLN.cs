using Microsoft.AspNetCore.Identity;
using Preacepta.AD.GeAbogado.Crear;
using Preacepta.AD.GePersona.Crear;
using Preacepta.LN.GeAbogado.ObtenerDatos;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.LN.GePersona.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeAbogado.Crear
{
    public class CrearAbogadoLN : ICrearAbogadoLN
    {
        //Personas
        private readonly ICrearGePersonaAD _crearGePersonaLN;
        private readonly IObtenerDatosLN _obtenerDatosPersonaLN;
        private readonly IBuscarXidGePersonaLN _buscarXidGePersonaLN;

        //Abogados
        private readonly ICrearAbogadoAD _crearGeAbogado;
        private readonly IObtenerDatosAbogadoLN _obtenerDatosLN;

        public CrearAbogadoLN(
            //inyeccion Personas
            ICrearGePersonaAD crearGePersonaLN,
            IObtenerDatosLN obtenerDatosPersonaLN,
            IBuscarXidGePersonaLN buscarXidGePersonaLN,

            //inyeccion Abogados
            ICrearAbogadoAD crearGeAbogado,
            IObtenerDatosAbogadoLN obtenerDatosLN)                      
        {
            //Personas
            _crearGeAbogado = crearGeAbogado;
            _obtenerDatosLN = obtenerDatosLN;
            _buscarXidGePersonaLN = buscarXidGePersonaLN;

            //Abogados
            _crearGePersonaLN = crearGePersonaLN;

            _obtenerDatosPersonaLN = obtenerDatosPersonaLN;
        }

        public async Task<int> Crear(PersonaUnionAbogado crear)
        {
            
            //crear.geAbogadoDTO.Cedula = crear.personaDTO.Cedula;
            if (crear == null)
            {
                Console.WriteLine("Error: Objeto nulo.");
                return 0;
            }
            try
            {
                int bandera;                
                bandera = await _crearGePersonaLN.crear(_obtenerDatosPersonaLN.ObtenerDeFrontCrear(crear.personaDTO));
                if (bandera == 0) 
                {
                    Console.WriteLine("Crear persona LN fallo");
                    return 0;

                }
                GePersonaDTO personaDTO = await _buscarXidGePersonaLN.buscarXnumCedula(crear.personaDTO.NumCedula);
                crear.geAbogadoDTO.Cedula = personaDTO.Cedula;

                //Creacion de la persona en la tabla TGeAbogado
                bandera = await _crearGeAbogado.crear(_obtenerDatosLN.ObtenerDeFront(crear.geAbogadoDTO));
                if (bandera == null)
                {
                    Console.WriteLine("Conversion de GeAbogadoDTO fallido");
                    return 0;
                }
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearAbogadoLN{ex.Message}");
                return -1;
            }
        }        
    }
}
