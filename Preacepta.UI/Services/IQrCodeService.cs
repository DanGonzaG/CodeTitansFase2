using Microsoft.AspNetCore.Identity;

namespace Preacepta.UI.Services
{
    public interface IQrCodeService
    {
        string GenerarQr(string uri);
    }
}
