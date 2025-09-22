using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO;

namespace Preacepta.UI.Services
{
    public class PdfConverterService
    {
        public static SynchronizedConverter GetConverter()
        {
            //DinkToPdfAll.LibraryLoader.Load(); // Carga automática
            return new SynchronizedConverter(new PdfTools());
        }
    }
}
