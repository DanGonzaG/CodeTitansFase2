using Microsoft.AspNetCore.Identity;
using QRCoder;

namespace Preacepta.UI.Services
{
    public class QrCodeService : IQrCodeService
    {
        public string GenerarQr(string uri)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(uri, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);
            var qrCodeBytes = qrCode.GetGraphic(20);

            return $"data:image/png;base64,{Convert.ToBase64String(qrCodeBytes)}";
        }

    }

}
