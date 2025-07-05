using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Preacepta.LN.GeAbogado.BuscarXid;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.LN.GePersona.Editar;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Praecepta.UI.Areas.Identity.Pages.Account.Manage
{
    public class CambiarUbicacionModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IBuscarXidGePersonaLN _buscarPersona;
        private readonly IBuscarAbogadoLN _buscarAbogado;
        private readonly IEditarGePersonaLN _editarPersona;


        public CambiarUbicacionModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IBuscarXidGePersonaLN buscarPersona,
            IBuscarAbogadoLN buscarAbogado,
            IEditarGePersonaLN editarPersona)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _buscarPersona = buscarPersona;
            _buscarAbogado = buscarAbogado;
            _editarPersona = editarPersona;
        }

        [BindProperty]
        public InputModel? Input { get; set; }

        [DisplayName("Cuenta de usuario")]
        public string? Email { get; set; }

        [DisplayName("Provincia")]        
        public string? ProvinciaPersona { get; set; }

        [DisplayName("Cantón")]        
        public string? CantonPersona { get; set; }

        [DisplayName("Distrito")]        
        public string? DistritoPersona { get; set; }

        [DisplayName("Ubicacion exacta")]        
        public string? Direccion2Persona { get; set; }


        public class InputModel
        {
            [DisplayName("Cuenta de usuario")]
            public string? Email { get; set; }

            [DisplayName("Provincia")]
            [Required(ErrorMessage = "Debe de colocar un número telefónico")]
            public string? Provincia { get; set; }


            [DisplayName("Cantón")]
            [Required(ErrorMessage = "Debe de colocar un número telefónico")]
            public string? Canton { get; set; }

            [DisplayName("Distrito")]
            [Required(ErrorMessage = "Debe de colocar un número telefónico")]
            public string? Distrito { get; set; }

            [DisplayName("Ubicacion exacta")]
            [Required(ErrorMessage = "Debe de colocar un número telefónico")]
            public string? Direccion2 { get; set; }
        }


        public async Task<IActionResult> OnGetAsync()
        {
            var persona = await _buscarPersona.buscarXcorreo(User.Identity.Name);
            if (persona == null)
            {
                return NotFound();
            }

            Email = persona.Email;
            ProvinciaPersona = persona.Direccion1Navigation.IdCatonNavigation.IdProvinciaNavigation.NombreProvincia;
            CantonPersona = persona.Direccion1Navigation.IdCatonNavigation.NombreCanton;
            DistritoPersona = persona.Direccion1Navigation.NombreDistrito;
            Direccion2Persona = persona.Direccion2;          

            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Input == null)
            {
                return NotFound();
            }

            var persona = await _buscarPersona.buscarXcorreo(User.Identity.Name.ToString());
            if (persona != null)
            {

                persona.Direccion1 = int.Parse(Input.Distrito);
                string num2 = Input.Direccion2;
                return Page();
            }

            return NotFound();

        }

    }
}





