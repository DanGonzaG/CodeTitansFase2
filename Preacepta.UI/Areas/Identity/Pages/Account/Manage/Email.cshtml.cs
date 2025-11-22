
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.LN.GePersona.Editar;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.UI.Services;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;

namespace Praecepta.UI.Areas.Identity.Pages.Account.Manage
{
    public class EmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;        
        private readonly IBuscarXidGePersonaLN _buscarPersona;
        private readonly IEditarGePersonaLN _editarPersona;
        private readonly IServicioEmail _emailSender;
        private readonly ILogger<EmailModel> _logger;

        public EmailModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IBuscarXidGePersonaLN buscarPersona,
            IEditarGePersonaLN editarPersona,
            IServicioEmail emailSender,
             ILogger<EmailModel> logger
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _buscarPersona = buscarPersona;
            _editarPersona = editarPersona;
            _logger = logger;
            _logger.LogInformation("EmailSender inyectado: {Type}", _emailSender.GetType().FullName);

        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        /// 
        [Display(Name = "Correo Electrónico Actual")]
        public string Email { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

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
            [Required(ErrorMessage = "El campo Correo Electrónico nuevo es obligatorio")]
            [EmailAddress]
            [Display(Name = "Correo Electrónico nuevo")]
            public string NewEmail { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var email = await _userManager.GetEmailAsync(user);
            Email = email;

            Input = new InputModel
            {
                NewEmail = email,
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
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
        
        
        public async Task<IActionResult> OnPostChangeEmailAsync(string returnUrl = null)
        {
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

            var existente = await _userManager.FindByEmailAsync(Input.NewEmail);
            if (existente != null && existente.Id != user.Id)
            {
                ModelState.AddModelError(string.Empty, "Ya existe una cuenta con ese correo electrónico.");
                TempData["ErrorCorreoExistente"] = $"El correo ya se encuentra registrado";
                await LoadAsync(user);
                return Page();
            }


            var email = await _userManager.GetEmailAsync(user);
            if (Input.NewEmail != email)
            {
                //actualiza la tabla persona
                var persona = await _buscarPersona.buscarXcorreo(user.UserName);
                persona.Email = Input.NewEmail;
                int cont = await _editarPersona.editar(persona);

                //actualiza datos de identity
                // Actualiza datos del usuario en Identity
                user.Email = Input.NewEmail;
                user.NormalizedEmail = Input.NewEmail.ToUpperInvariant();

                // Si usás el correo como UserName, actualizalo también
                user.UserName = Input.NewEmail;
                user.NormalizedUserName = Input.NewEmail.ToUpperInvariant();
                user.EmailConfirmed = false;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);                    
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                             "/Account/ConfirmEmail",
                             pageHandler: null,
                             values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                             protocol: Request.Scheme);
                    try 
                    {
                        _logger.LogWarning("→ Entrando a SendEmailAsync con destino: {Email}", user.Email);
                        await _emailSender.SendEmailAsync(await _userManager.GetEmailAsync(user), "Confirma tu correo electrónico",
                            $"Por favor, confirme su cuenta mediante <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>haciendo clic aquí</a>.");
                        _logger.LogWarning("← Salida de SendEmailAsync sin excepción");

                        Console.WriteLine($"URL de confirmación: {callbackUrl}");
                    }
                    catch (Exception ex) 
                    {
                        Console.WriteLine($"error, {ex.Message}");
                    }
                   
                    StatusMessage = "Se ha enviado un correo electrónico de verificación. Por favor, revise su correo electrónico.";
                    TempData["CorreoModificado"] = $"Su correo fue modificado";
                    await _signInManager.RefreshSignInAsync(user);
                    return Page();                   
                }
                else 
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();

                }
            }
            StatusMessage = "El correo no ha cambiado.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
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

            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Se ha enviado un correo electrónico de verificación. Por favor, revise su correo electrónico.";
            return RedirectToPage();
        }
    }
}
