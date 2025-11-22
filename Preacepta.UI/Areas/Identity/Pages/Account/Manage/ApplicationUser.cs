using Microsoft.AspNetCore.Identity;

namespace Preacepta.UI.Areas.Identity.Pages.Account.Manage
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime FechaPassword {  get; set; } = DateTime.UtcNow;
    }
}
