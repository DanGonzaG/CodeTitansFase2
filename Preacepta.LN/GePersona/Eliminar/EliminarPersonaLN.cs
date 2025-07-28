using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Preacepta.AD.GePersona.Eliminar;
using Preacepta.LN.GePersona.BuscarXid;
using System.ComponentModel.DataAnnotations;

namespace Preacepta.LN.GePersona.Eliminar
{
    public class EliminarPersonaLN : IEliminarPersonaLN
    {
        private readonly IEliminarPersonaAD _eliminarPersonaAD;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IBuscarXidGePersonaLN _buscarPersona;

        public EliminarPersonaLN(
            IEliminarPersonaAD eliminarPersonaAD,
            UserManager<IdentityUser> userManager,
            IBuscarXidGePersonaLN buscarPersona)
        {
            _eliminarPersonaAD = eliminarPersonaAD;
            _userManager = userManager;
            _buscarPersona = buscarPersona;
        }

        public async Task<int> eliminar(int id)
        {
            if (id < 0)
            {
                Console.WriteLine("el valor de id en menor a 1");
                return 0;
            }
            try
            {
                var persona = await _buscarPersona.buscar(id);
                if (persona == null) 
                {
                    Console.WriteLine("objeto persona no existe es nulo");
                    return 0;
                }
                var user = await _userManager.FindByEmailAsync(persona.Email);
                if (user == null) 
                {
                    Console.WriteLine("userManager no encuatra el usuario");
                    return 0;
                }
                var resultado = await _userManager.DeleteAsync(user);
                int bandera = await _eliminarPersonaAD.eliminar(id);
                if (resultado.Succeeded && bandera == 1) 
                {
                    Console.WriteLine("Usuario eliminado de TGePesona y AspNetUsers");
                    return bandera;
                }
                
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en: EliminarPersonaLN {ex.Message}");
                return -1;
            }


        }
    }
}
