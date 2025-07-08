// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Preacepta.UI.Services.MensajesPersonalizados;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Praecepta.UI.Areas.Identity.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IValidacionesResetPassword _validacionReset;

        public ResetPasswordModel(UserManager<IdentityUser> userManager, IValidacionesResetPassword validacionReset)
        {
            _userManager = userManager;
            _validacionReset = validacionReset;
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
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "El campo Correo electrónico es obligatorio.")]
            [EmailAddress]
            [DisplayName("Correo Electrónico")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
            [StringLength(100, ErrorMessage = "El {0} debe tener al menos {2} y como máximo {1} caracteres de longitud.", MinimumLength = 6)]
            [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$", ErrorMessage = "La contraseña debe de tener al menos un numero, una mayuscula y un símbolo")]
            [DataType(DataType.Password)]
            [DisplayName("Contraseña")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirmar Contraseña")]
            [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
            [DisplayName("Confirmación de contraseña")]
            public string ConfirmPassword { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            public string Code { get; set; }

        }

        public IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                return BadRequest("Se debe proporcionar un código para restablecer la contraseña.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                ModelState.AddModelError(string.Empty, "Correo no se encuentra registrado");
                return Page();
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                /*ModelState.AddModelError(string.Empty, error.Description);
                if (error.Code == "InvalidToken") 
                {
                    var resultado = _validacionReset.Ejecutar(Input.Email, Input.Password, Input.Code);
                    if (!resultado.Exitoso) 
                    {
                        foreach (var mensaje in resultado.Errores) 
                        {
                            ModelState.AddModelError(string.Empty, mensaje.Descripcion);
                        }
                        return Page();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }*/


                if (error.Code == "InvalidToken")
                {
                    ModelState.AddModelError(string.Empty, "El enlace para restablecer la contraseña no es válido o ha expirado. Por favor solicitá uno nuevo.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
    }
}
