using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.LN.GePersona.Editar;
using Preacepta.Modelos.AbstraccionesFrond;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Preacepta.UI.Areas.Identity.Pages.Account.Manage
{
    public class CambiarContactosModel : PageModel
    {
        private readonly IBuscarXidGePersonaLN _buscarPersona;
        private readonly IEditarGePersonaLN _editarPersona;

        public CambiarContactosModel(IBuscarXidGePersonaLN buscarPersona, IEditarGePersonaLN editarPersona) 
        {
            _buscarPersona = buscarPersona;
            _editarPersona = editarPersona;
        }


        [BindProperty]
        public InputModel? Input { get; set; }       

        /*public string Email { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }*/


        public class InputModel 
        {
            [DisplayName("Cuenta de usuario")]
            public string? Email { get; set; }

            [DisplayName("Primer número telefonónico")]
            [Required(ErrorMessage ="Debe de colocar un número telefónico")]
            public string? Telefono1 { get; set; }
            [DisplayName("Segundo número telefonónico")]
            [Required(ErrorMessage = "Debe de colocar un número telefónico")]
            public string? Telefono2 { get; set; }
        }


        public async Task<IActionResult> OnGetAsync () 
        {
            var persona = await _buscarPersona.buscarXcorreo(User.Identity.Name);
            if (persona == null) 
            {
                return NotFound();
            }

            Input = new InputModel
            {
                Email = persona.Email,
                Telefono1 = persona.Telefono1,
                Telefono2 = persona.Telefono2
            };
           
            return Page();

        }

        public async Task<IActionResult> OnPostAsync() 
        {
            if (Input == null) 
            {
                return NotFound();
            }

            var persona = await _buscarPersona.buscarXcorreo(Input.Email);
            if (persona != null) 
            {
                persona.Telefono1 = Input.Telefono1;
                persona.Telefono2 = Input.Telefono2;
                await _editarPersona.editar(persona);
                OnGetAsync();
                return Page();
            }

            return NotFound();
            
        }
    }
}




