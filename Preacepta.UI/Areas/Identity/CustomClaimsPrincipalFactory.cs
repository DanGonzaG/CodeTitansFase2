using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Preacepta.UI.Areas.Identity
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<IdentityUser>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<CustomClaimsPrincipalFactory> _logger;

        public CustomClaimsPrincipalFactory(
            UserManager<IdentityUser> userManager,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<CustomClaimsPrincipalFactory> logger)
            : base(userManager, optionsAccessor)
        {
            _userManager = userManager;
            _logger = logger;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IdentityUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            var roles = await _userManager.GetRolesAsync(user);
            _logger.LogInformation("Generando claims de roles para el usuario {UserId}: {Roles}", user.Id, string.Join(", ", roles));

            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
                _logger.LogDebug("Claim agregado: Role = {Role}", role);
            }

            return identity;
        }
    }
}
