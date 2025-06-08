using Microsoft.AspNetCore.Identity;
using Preacepta.AD.GeAbogado.Crear;
using Preacepta.AD.GePersona.Crear;
using Preacepta.LN.GeAbogado.ObtenerDatos;
using Preacepta.LN.GePersona.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeAbogado.Crear
{
    public class CrearAbogadoLN : ICrearAbogadoLN
    {
        //Personas
        private readonly ICrearGePersonaAD _crearGePersonaLN;
        private readonly IObtenerDatosLN _obtenerDatosPersonaLN;

        //Abogados
        private readonly ICrearAbogadoAD _crearGeAbogado;
        private readonly IObtenerDatosAbogadoLN _obtenerDatosLN;

        //Identity
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;

        public CrearAbogadoLN(
            //inyeccion Personas
            ICrearGePersonaAD crearGePersonaLN,
            IObtenerDatosLN obtenerDatosPersonaLN,

            //inyeccion Abogados
            ICrearAbogadoAD crearGeAbogado,
            IObtenerDatosAbogadoLN obtenerDatosLN,

            //inyeccion Identity
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore)
        {
            //Personas
            _crearGeAbogado = crearGeAbogado;
            _obtenerDatosLN = obtenerDatosLN;

            //Abogados
            _crearGePersonaLN = crearGePersonaLN;
            _obtenerDatosPersonaLN = obtenerDatosPersonaLN;

            //Identity
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
        }

        public async Task<int> Crear(PersonaUnionAbogado crear)
        {
            if (crear == null)
            {
                Console.WriteLine("Error: Objeto nulo.");
                return 0;
            }
            try
            {
                //Creacion de usuario en tabal user
                var user = CreateUser();
                await _userStore.SetUserNameAsync(user, crear.personaDTO.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, crear.personaDTO.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, crear.personaDTO.Password);
                await _userManager.AddToRoleAsync(user, "Abogado");

                //Creacion de persona en tabal TGePersona
                await _crearGePersonaLN.crear(_obtenerDatosPersonaLN.ObtenerDeFrontCrear(crear.personaDTO));

                //Creacion de persona en tabal TGeAbogado
                int bandera = await _crearGeAbogado.crear(_obtenerDatosLN.ObtenerDeFront(crear.geAbogadoDTO));
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

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
}
