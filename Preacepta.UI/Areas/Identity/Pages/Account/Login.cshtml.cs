// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.LN.GePersona.Editar;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Praecepta.UI.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IBuscarXidGePersonaLN _buscarPersona;
        private readonly IEditarGePersonaLN _editarPersona;

        public LoginModel(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, 
            ILogger<LoginModel> logger,
            IBuscarXidGePersonaLN buscarPersona,
            IEditarGePersonaLN editarPersona)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _buscarPersona = buscarPersona;
            _editarPersona = editarPersona;
        }


        
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
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

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
            [Required (ErrorMessage = "El campo Correo electrónico es obligatorio.")]
            [EmailAddress]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required (ErrorMessage = "El campo Contraseña es obligatorio.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            //[Display(Name = "Remember me?")]
            [Display(Name = "Recordar")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }


        //Método POST para validar credenciales
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            var usuario = await _userManager.FindByEmailAsync(Input.Email); // Obtiene al objeto User de la BD
            if (usuario == null) //Valida si el usuario existe
            {
                ModelState.AddModelError(string.Empty, $"Usuario no se encuentra registrado");
                return Page();
            }
            
            int contadorIntentos = await _userManager.GetAccessFailedCountAsync(usuario); //Obtiene los intentos fallidos, siempre empieza en 0
            contadorIntentos = contadorIntentos + 1; // se asigna un 1 para que muestre el dato correcto en la vista
            int intestosRestantes = 3 - contadorIntentos; // el sistema solo permite 3 intentos

            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(
                    Input.Email, 
                    Input.Password, 
                    Input.RememberMe, 
                    lockoutOnFailure: true); //se activo el bloqueo automatico

                if (result.Succeeded)
                {

                    if (!User.IsInRole("Gestor")) // Valida que el usuario no es gestor
                    {
                        var persona = await _buscarPersona.buscarXcorreo(Input.Email); // obtiene le objeto persona
                        if (!persona.Activo) //Valida si el usuario esta activo en la tabla TGePesona en BD
                        {
                            ModelState.AddModelError(string.Empty, "Su cuenta esta desactivada, favor comuniquese con el despacho"); //HU PP-MA - 1 criterio 2
                            return Page();
                        }
                    }
                 
                    _logger.LogInformation("Usuario conectado.");
                    //return LocalRedirect(returnUrl);
                    return RedirectToAction("UsuarioAutenticado", "Home", new { correo = Input.Email }); // Ingreso exitóso HU PP-MA-1 criterio 1
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut) //Valida si el usuario esta bloqueado
                {

                    _logger.LogWarning("Cuenta de usuario bloqueada.");
                    //ModelState.AddModelError(string.Empty, "Su cuenta esta bloqueda por multiples intentos, favor comuniquese con el despacho");
                    TempData["Lockout"] = "Su cuenta esta bloqueda por multiples intentos, favor comuniquese con el despacho"; //PP-MA-2 Criterio 3
                    //return RedirectToPage("./Lockout");
                    return Page();
                }
                else
                {   
                    //His
                    if(intestosRestantes == 1) 
                    {
                        ModelState.AddModelError(string.Empty, $"Correo o contraseña son inválidos. Cuenta con {intestosRestantes} intento más"); //PP-MA-2 Criterio 1 y 2 
                        return Page();
                    }

                    ModelState.AddModelError(string.Empty, $"Correo o contraseña son inválidos. Cuenta con {intestosRestantes} intentos más"); //PP-MA-2 Criterio 1 y 2
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
