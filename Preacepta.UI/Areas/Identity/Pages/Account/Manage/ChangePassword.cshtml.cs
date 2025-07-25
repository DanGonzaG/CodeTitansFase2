// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Praecepta.UI.Areas.Identity.Pages.Account.Manage
{
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;

        public ChangePasswordModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<ChangePasswordModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
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
        [TempData]
        public string StatusMessage { get; set; }

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
            [Required (ErrorMessage = "El campo Contraseña es obligatorio.")]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña actual")]
            public string OldPassword { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required (ErrorMessage = "El campo Contraseña nueva es obligatorio.")]
            [StringLength(100, ErrorMessage = "El {0} debe tener al menos {2} y como máximo {1} caracteres de longitud.", MinimumLength = 6)]
            [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$", ErrorMessage = "La contraseña debe de tener al menos un numero, una mayuscula y un símbolo")]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña nueva")]
            public string NewPassword { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirmación de nueva contraseña")]
            [Compare("NewPassword", ErrorMessage = "La nueva contraseña y la contraseña de confirmación no coinciden.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"No se puede cargar el usuario con ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"No se puede cargar el usuario con ID '{_userManager.GetUserId(User)}'.");
            }

            var ComparacionDePass = await _userManager.CheckPasswordAsync(user, Input.NewPassword);
            if (ComparacionDePass)
            {
                ModelState.AddModelError(string.Empty, "La contraseña no puede ser igual a la anterior");
                return Page();

            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {

                    if (error.Code == "PasswordMismatch")
                    {
                        ModelState.AddModelError(string.Empty, "Contraseña incorrecta, intente de nuevo");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }




                    //ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("El usuario cambió su contraseña exitosamente.");
            StatusMessage = "Su contraseña ha sido cambiada";

            return RedirectToPage();
        }
    }
}
