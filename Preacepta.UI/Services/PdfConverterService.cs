using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO;

namespace Preacepta.UI.Services
{
    public class PdfConverterService
    {
        public static SynchronizedConverter GetConverter()
        {
            var context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "DinkToPdf", "lib64", "libwkhtmltox.dll"));

            return new SynchronizedConverter(new PdfTools());
        }
    }
}
