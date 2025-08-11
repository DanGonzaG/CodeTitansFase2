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
using System.Security.Claims;

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
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            _logger.LogInformation("Email recibido: {Email}", Input.Email);
            var usuario = await _userManager.FindByEmailAsync(Input.Email);
            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, $"Usuario no se encuentra registrado");
                return Page();
            }

            int contadorIntentos = await _userManager.GetAccessFailedCountAsync(usuario);
            contadorIntentos++;
            int intentosRestantes = 3 - contadorIntentos;

            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    Input.Email,
                    Input.Password,
                    Input.RememberMe,
                    lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    // 🔄 Obtener ClaimsPrincipal actualizado
                    var principal = await _signInManager.CreateUserPrincipalAsync(usuario);
                    var roles = principal.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value)
                        .ToList();

                    _logger.LogInformation("Roles asignados al usuario tras login: {Roles}", string.Join(", ", roles));

                    if (!roles.Contains("Gestor"))
                    {
                        var persona = await _buscarPersona.buscarXcorreo(Input.Email);
                        if (!persona.Activo)
                        {
                            await _signInManager.SignOutAsync();
                            ModelState.AddModelError(string.Empty, "Su cuenta está desactivada, favor comuníquese con el despacho");
                            return Page();
                        }
                    }

                    if (roles.Any(r => r == "Gestor" || r == "Cliente" || r == "Abogado"))
                    {
                        _logger.LogInformation("Usuario conectado con rol válido.");
                        foreach (var claim in principal.Claims)
                        {
                            _logger.LogInformation("DanielClaim: {Type} = {Value}", claim.Type, claim.Value);
                        }

                        return RedirectToAction("UsuarioAutenticado", "Home", new { correo = Input.Email });
                    }
                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("Cuenta de usuario bloqueada.");
                    TempData["Lockout"] = "Su cuenta está bloqueada por múltiples intentos, favor comuníquese con el despacho";
                    return Page();
                }

                if (intentosRestantes == 1)
                {
                    ModelState.AddModelError(string.Empty, $"Correo o contraseña son inválidos. Cuenta con {intentosRestantes} intento más");
                    return Page();
                }

                ModelState.AddModelError(string.Empty, $"Correo o contraseña son inválidos. Cuenta con {intentosRestantes} intentos más");
                return Page();
            }

            return Page();
        }
    }
}
