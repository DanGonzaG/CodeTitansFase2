// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Preacepta.LN.GeAbogado.BuscarXid;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.LN.GePersona.Editar;
using Preacepta.Modelos.AbstraccionesFrond;
using Preacepta.Modelos.AbstraccionesFrond.ViewModelsFront;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Praecepta.UI.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IBuscarXidGePersonaLN _buscarPersona;
        private readonly IBuscarAbogadoLN _buscarAbogado;
        private readonly IEditarGePersonaLN _editarPersona;



        public IndexModel(
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

        /*[BindProperty]
        public PersonaUnionAbogado personaUnionIdentity { get; set; }

        public GePersonaDTO personaDTO { get; set; }

        public GeAbogadoDTO abogadoDTO { get; set; }*/

        public List<SelectListItem> EstadoCivilLista { get; set; }

        public List<SelectListItem> GeneroLista { get; set; }

        public List<SelectListItem> TipoIdentificacionLista { get; set; }

        //Modelo parcial de persona      


        public int Cedula { get; set; }

        [Required(ErrorMessage = "Debe de seleccionar el tipo de Identificación")]
        [MaxLength(100, ErrorMessage = "El tipo de identificación no debe exceder los 100 caracteres")]
        [DisplayName("Tipo de identificación")]
        public string TipoIdentificacion { get; set; } = null!;


        [Required(ErrorMessage = "Debe de ingresar un número de Identificación")]
        [MaxLength(100, ErrorMessage = "El tipo de identificación no debe exceder los 100 caracteres")]
        [DisplayName("No. Idenficación")]
        public string NumCedula { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese el nombre de la persona")]
        [MaxLength(50, ErrorMessage = "El nombre no debe exceder los 50 caracteres")]
        [DisplayName("Nombre")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese el primer apellido de la persona")]
        [MaxLength(50, ErrorMessage = "El apellido no debe exceder los 50 caracteres")]
        [DisplayName("Primer apellido")]
        public string Apellido1 { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese el segundo apellido de la persona")]
        [MaxLength(50, ErrorMessage = "El segundo no debe exceder los 50 caracteres")]
        [DisplayName("Segundo Apellido")]
        public string Apellido2 { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese la fecha de nacimiento de la persona")]
        [MaxLength(50, ErrorMessage = "La fecha no debe exceder los 50 caracteres")]
        [DisplayName("Fecha de nacimiento")]
        public string FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Debe ingresar una edad")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "Seleccione el estado civil de la persona")]
        [MaxLength(50, ErrorMessage = "El estado civil no debe exceder los 50 caracteres")]
        [DisplayName("Estado Civil")]
        public string EstadoCivil { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese la ocupación de la persona")]
        [MaxLength(50, ErrorMessage = "La ocupación no debe exceder los 50 caracteres")]
        [DisplayName("Ocupación")]
        public string Oficio { get; set; } = null!;

        [Required(ErrorMessage = "Seleccione un género")]
        [MaxLength(10, ErrorMessage = "El género no debe exceder los 10 caracteres")]
        public string Genero { get; set; } = null!;


        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "Ingrese al cédula de la persona")]
            [DisplayName("Cédula")]
            public int Cedula { get; set; }

            [Required(ErrorMessage = "Ingrese el nombre de la persona")]
            [MaxLength(50, ErrorMessage = "El nombre no debe exceder los 50 caracteres")]
            [DisplayName("Nombre")]
            public string Nombre { get; set; } = null!;

            [Required(ErrorMessage = "Ingrese el primer apellido de la persona")]
            [MaxLength(50, ErrorMessage = "El apellido no debe exceder los 50 caracteres")]
            [DisplayName("Primer apellido")]
            public string Apellido1 { get; set; } = null!;

            [Required(ErrorMessage = "Ingrese el segundo apellido de la persona")]
            [MaxLength(50, ErrorMessage = "El segundo no debe exceder los 50 caracteres")]
            [DisplayName("Segundo Apellido")]
            public string Apellido2 { get; set; } = null!;

            [Required(ErrorMessage = "Ingrese la fecha de nacimiento de la persona")]
            [MaxLength(50, ErrorMessage = "La fecha no debe exceder los 50 caracteres")]
            [DisplayName("Fecha de nacimiento")]
            public string FechaNacimiento { get; set; }

            [Required(ErrorMessage = "Debe ingresar una edad")]
            public int Edad { get; set; }

            [Required(ErrorMessage = "Seleccione el estado civil de la persona")]
            [MaxLength(50, ErrorMessage = "El estado civil no debe exceder los 50 caracteres")]
            [DisplayName("Estado Civil")]
            public string EstadoCivil { get; set; } = null!;

            [Required(ErrorMessage = "Ingrese la ocupación de la persona")]
            [MaxLength(50, ErrorMessage = "La ocupación no debe exceder los 50 caracteres")]
            [DisplayName("Ocupación")]
            public string Oficio { get; set; } = null!;

            [Required(ErrorMessage = "Seleccione un género")]
            [MaxLength(10, ErrorMessage = "El género no debe exceder los 10 caracteres")]
            public string Genero { get; set; } = null!;

            [Required(ErrorMessage = "Debe de seleccionar el tipo de Identificación")]
            [MaxLength(100, ErrorMessage = "El tipo de identificación no debe exceder los 100 caracteres")]
            [DisplayName("Tipo de identificación")]
            public string TipoIdentificacion { get; set; }

            [Required(ErrorMessage = "Debe de ingresar un número de Identificación")]
            [MaxLength(100, ErrorMessage = "El tipo de identificación no debe exceder los 100 caracteres")]
            [DisplayName("No. Idenficación")]
            public string NumCedula { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            if (!User.IsInRole("Gestor")) 
            {
                var persona = await _buscarPersona.buscarXcorreo(user.ToString());
                

                Username = userName;
                Cedula = persona.Cedula;
                Nombre = persona.Nombre;
                Apellido1 = persona.Apellido1;
                Apellido2 = persona.Apellido2;
                //FechaNacimiento = DateTime.Parse(persona.FechaNacimiento).ToString("yyyy-MM-dd");
                FechaNacimiento = DateTime.ParseExact(persona.FechaNacimiento,"dd/MM/yyyy",CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                Edad = (int)persona.Edad;
                EstadoCivil = persona.EstadoCivil;
                Oficio = persona.Oficio;
                Genero = persona.Genero;
                TipoIdentificacion = persona.TipoIdentificacion;
                NumCedula = persona.NumCedula;



                Input = new InputModel
                {
                    PhoneNumber = phoneNumber
                    /*Cedula = persona.Cedula,
                    Nombre = persona.Nombre,
                    Apellido1 = persona.Apellido1,
                    Apellido2 = persona.Apellido2,
                    FechaNacimiento = persona.FechaNacimiento,
                    Edad = persona.Edad,
                    EstadoCivil = persona.EstadoCivil,
                    Oficio = persona.Oficio,
                    Genero = persona.Genero,*/

                };

                EstadoCivilLista = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Soltero", Value = "Soltero" },
                    new SelectListItem { Text = "Casado", Value = "Casado" },
                    new SelectListItem { Text = "Divorciado", Value = "Divorciado" },
                    new SelectListItem { Text = "Viudo", Value = "Viudo" }
                };

                GeneroLista = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Femenino", Value = "Femenino" },
                    new SelectListItem { Text = "Masculino", Value = "Masculino" }
                };

                TipoIdentificacionLista = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Cédula física", Value = "Cedula" },
                    new SelectListItem { Text = "DIMEX", Value = "DIMEX" },
                    new SelectListItem { Text = "Pasaporte", Value = "Pasaporte" },
                    new SelectListItem { Text = "Sin documento de identificación", Value = "SinDocumento" }
                };
            }

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber      

            };



        }

        public async Task<IActionResult> OnGetAsync()
        {
            
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(InputModel modeloFormulario)
        {
            var usuario = await _userManager.GetUserAsync(User);
            if (!ModelState.IsValid) 
            {
                await LoadAsync(usuario);
                return Page();
            }



            var datosNoCambiados = await _buscarPersona.buscar(modeloFormulario.Cedula);
            GePersonaDTO gePersonaDTO = new GePersonaDTO();
            gePersonaDTO.Cedula = modeloFormulario.Cedula;
            gePersonaDTO.Nombre = modeloFormulario.Nombre;
            gePersonaDTO.Apellido1 = modeloFormulario.Apellido1;
            gePersonaDTO.Apellido2 = modeloFormulario.Apellido2;
            gePersonaDTO.FechaNacimiento = modeloFormulario.FechaNacimiento;
            gePersonaDTO.Edad = modeloFormulario.Edad;
            gePersonaDTO.EstadoCivil = modeloFormulario.EstadoCivil;
            gePersonaDTO.Oficio = modeloFormulario.Oficio;
            gePersonaDTO.Genero = modeloFormulario.Genero;
            gePersonaDTO.Direccion1 = datosNoCambiados.Direccion1;
            gePersonaDTO.Direccion2 = datosNoCambiados.Direccion2;
            gePersonaDTO.Email = datosNoCambiados.Email;
            gePersonaDTO.FechaRegistro = datosNoCambiados.FechaRegistro;
            gePersonaDTO.Activo = datosNoCambiados.Activo;
            gePersonaDTO.Telefono1 = datosNoCambiados.Telefono1;
            gePersonaDTO.Telefono2 = datosNoCambiados.Telefono2;
            gePersonaDTO.TipoIdentificacion = datosNoCambiados.TipoIdentificacion;
            gePersonaDTO.NumCedula = datosNoCambiados.NumCedula;


            var user = await _userManager.GetUserAsync(User);
            

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {

                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            await _editarPersona.editar(gePersonaDTO);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Su información personal ha sido actualizada";
            return RedirectToPage();
        }
    }
}
