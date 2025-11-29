// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Preacepta.LN.GeAbogado.BuscarXid;
using Preacepta.LN.GeAbogado.Crear;
using Preacepta.LN.GeAbogadoTipo.Listar;
using Preacepta.LN.GeNegocio.Listar;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.LN.GePersona.Crear;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using Preacepta.UI.Extensions;
using Preacepta.UI.Services;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;

namespace Praecepta.UI.Areas.Identity.Pages.Account
{
    public class RegisterAbogadoModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<RegisterAbogadoModel> _logger;
        private readonly IServicioEmail _emailSender;

        private readonly ICrearGePersonaLN _crearPersonaLN;
        private readonly IBuscarXidGePersonaLN _buscarPersona;

        private readonly IListarAbogadoTipoLN _listarAbogadoTipo;
        private readonly IBuscarAbogadoLN _buscarAbogado;
        private readonly ICrearAbogadoLN _crearAbogado;

        //Listar caracteristicas del bufete
        private readonly IListarNegocioLN _listarNegocio;




        public RegisterAbogadoModel(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterAbogadoModel> logger,
            IServicioEmail emailSender,

            IBuscarXidGePersonaLN buscarXidGePersonaLN,
            ICrearGePersonaLN crearPersonaLN,

            IListarAbogadoTipoLN listarAbogadoTipo,
            IBuscarAbogadoLN buscarAbogado,
            ICrearAbogadoLN crearAbogado,

            IListarNegocioLN listarNegocio
)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _buscarPersona = buscarXidGePersonaLN;
            _crearPersonaLN = crearPersonaLN;
            _buscarAbogado = buscarAbogado;
            _crearAbogado = crearAbogado;
            _listarNegocio = listarNegocio;
            _listarAbogadoTipo = listarAbogadoTipo;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        /*[BindProperty]
        public InputModel Input { get; set; }*/

        //[BindProperty]
        //public GePersonaDTO tGePersona { get; set; }

        //[BindProperty]
        //public GeAbogadoDTO geAbogado { get; set; }

        [BindProperty]
        public PersonaUnionAbogado geAbogado { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
       
        public List<SelectListItem> EstadoCivil { get; set; }
        public List<SelectListItem> Genero { get; set; }
        public List<SelectListItem> Negocio { get; set; }
        public List<SelectListItem> TipoAbogado { get; set; }
        public List<SelectListItem> TipoIdentificacion { get; set; }



        [Authorize(Roles = "Abogado")]
        public async Task OnGetAsync(string returnUrl = null)
        {

            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();         

            EstadoCivil = SelectListPersonas.EstadoCivil;
            Genero = SelectListPersonas.Genero;
            TipoIdentificacion = SelectListPersonas.TipoIdentificacion;
            Negocio = await SelectListPersonas.Negocios(_listarNegocio);
            TipoAbogado = await SelectListPersonas.TipoAbogado(_listarAbogadoTipo);
        }


        [Authorize(Roles = "Abogado")]
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                #region creación de persona

                #region Validacion de cédula
                var existe = await _buscarPersona.buscarXnumCedula(geAbogado.personaDTO.NumCedula);
                if (existe != null)//valida si hay un cedula igual registrada
                {
                    EstadoCivil = SelectListPersonas.EstadoCivil;
                    Genero = SelectListPersonas.Genero;
                    TipoIdentificacion = SelectListPersonas.TipoIdentificacion;
                    Negocio = await SelectListPersonas.Negocios(_listarNegocio);
                    TipoAbogado = await SelectListPersonas.TipoAbogado(_listarAbogadoTipo);
                    TempData["ErrorCedula"] = "Cedula ya registrada en el sistema";
                    return Page();
                }
                #endregion

                #region Validacion de carnet
                var carnet = await _buscarAbogado.buscarXcarnet(geAbogado.geAbogadoDTO.Carnet);
                if (carnet != null)//valida si hay un cedula igual registrada
                {
                    EstadoCivil = SelectListPersonas.EstadoCivil;
                    Genero = SelectListPersonas.Genero;
                    TipoIdentificacion = SelectListPersonas.TipoIdentificacion;
                    Negocio = await SelectListPersonas.Negocios(_listarNegocio);
                    TipoAbogado = await SelectListPersonas.TipoAbogado(_listarAbogadoTipo);
                    TempData["ErrorCarnet"] = "El carnet ya se encuentra registrado en el sistema";
                    return Page();
                }
                #endregion

                #region Validacion de correo
                var correo = await _buscarPersona.buscarXcorreo(geAbogado.personaDTO.Email);
                if (correo != null)//valida si hay un correo igual registrado
                {
                    EstadoCivil = SelectListPersonas.EstadoCivil;
                    Genero = SelectListPersonas.Genero;
                    TipoIdentificacion = SelectListPersonas.TipoIdentificacion;
                    Negocio = await SelectListPersonas.Negocios(_listarNegocio);
                    TipoAbogado = await SelectListPersonas.TipoAbogado(_listarAbogadoTipo);
                    TempData["ErrorEmail"] = "Correo Electronico ya registrado en el sistema";
                    return Page();
                }
                #endregion

                #region Validacion de teléfonos
                var telefono1 = await _buscarPersona.buscarXtelefono1(geAbogado.personaDTO.Telefono1);
                
                if (telefono1 != null)
                {
                    EstadoCivil = SelectListPersonas.EstadoCivil;
                    Genero = SelectListPersonas.Genero;
                    TipoIdentificacion = SelectListPersonas.TipoIdentificacion;
                    Negocio = await SelectListPersonas.Negocios(_listarNegocio);
                    TipoAbogado = await SelectListPersonas.TipoAbogado(_listarAbogadoTipo);
                    TempData["ErrorTelefono1"] = $"El telefono {geAbogado.personaDTO.Telefono1} ya esta registrado";
                    return Page();

                }
                #endregion
                geAbogado.personaDTO.NumCedula = geAbogado.personaDTO.NumCedula.LimpiarCedula();
                //int bandera =  await _crearPersonaLN.crear(geAbogado.personaDTO);//llamado de los LN y AD para crear la persona
                int bandera = await _crearAbogado.Crear(geAbogado);
                #endregion

                if (bandera < 0)
                {
                    return BadRequest();
                }

                

                #region Registro de Persona en Servicio de Autenticacion
                var user = CreateUser();

                
                

                await _userStore.SetUserNameAsync(user, geAbogado.personaDTO.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, geAbogado.personaDTO.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, geAbogado.personaDTO.Password);
                await _userManager.AddToRoleAsync(user, "Abogado");

                if (result.Succeeded)
                {
                    
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(geAbogado.personaDTO.Email, "Confirma tu correo electrónico",
                        $"Por favor, confirme su cuenta mediante <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>haciendo clic aquí</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        //return RedirectToPage("RegisterConfirmation", new { email = tGePersona.Email, returnUrl = returnUrl });
                        TempData["PersonaCreada"] = "Se ha creado un nuevo usuario pendiente confirmar correo";
                        return RedirectToAction("UsuarioAutenticado", "Home", new { correo = User.Identity.Name });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                #endregion
            }
            EstadoCivil = SelectListPersonas.EstadoCivil;
            Genero = SelectListPersonas.Genero;
            TipoIdentificacion = SelectListPersonas.TipoIdentificacion;
            Negocio = await SelectListPersonas.Negocios(_listarNegocio);
            TipoAbogado = await SelectListPersonas.TipoAbogado(_listarAbogadoTipo);
            return Page();
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

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
