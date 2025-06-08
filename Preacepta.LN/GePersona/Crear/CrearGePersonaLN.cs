using Microsoft.AspNetCore.Identity;
using Preacepta.AD.GePersona.Crear;
using Preacepta.LN.GePersona.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GePersona.Crear
{
    public class CrearGePersonaLN : ICrearGePersonaLN
    {
        private readonly ICrearGePersonaAD _crearGePersonaAD;
        private readonly IObtenerDatosLN _obtenerDatosLN;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;

        public CrearGePersonaLN(ICrearGePersonaAD crearGePersonaAD,
            IObtenerDatosLN obtenerDatosLN,
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore)
        {
            _crearGePersonaAD = crearGePersonaAD;
            _obtenerDatosLN = obtenerDatosLN;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
        }

        public async Task<int> crear(GePersonaDTO gePersonaDTO)
        {
            var user = CreateUser();
            await _userStore.SetUserNameAsync(user, gePersonaDTO.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, gePersonaDTO.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, gePersonaDTO.Password);
            await _userManager.AddToRoleAsync(user, "Cliente");
            int bandera = await _crearGePersonaAD.crear(_obtenerDatosLN.ObtenerDeFrontCrear(gePersonaDTO));
            return bandera;
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
