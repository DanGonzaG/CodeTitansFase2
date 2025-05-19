using Microsoft.AspNetCore.Mvc;
using Praecepta.UI.Models;

namespace Praecepta.UI.Controllers
{
    public class DocsGeneratorController : Controller
    {

        public List<ModelDocsEjemplo> ListaDocEjemplos = new List<ModelDocsEjemplo>()
{
    new ModelDocsEjemplo()
    {
        Fecha = new DateOnly(2025, 03, 05),
        Abogado = "Lic Jose Abarca",
        Cliente = "Daniel González",
        TipoDocumento = "Traspaso Vehículo"
    },

    new ModelDocsEjemplo()
    {
        Fecha = new DateOnly(2025, 03, 10),
        Abogado = "Lic Maria Pérez",
        Cliente = "Carlos Fernández",
        TipoDocumento = "Contrato de Alquiler"
    },

    new ModelDocsEjemplo()
    {
        Fecha = new DateOnly(2025, 03, 15),
        Abogado = "Lic Ana Rojas",
        Cliente = "Elena García",
        TipoDocumento = "Compra Venta Propiedad"
    },

    new ModelDocsEjemplo()
    {
        Fecha = new DateOnly(2025, 03, 20),
        Abogado = "Lic Roberto López",
        Cliente = "Pedro Ramírez",
        TipoDocumento = "Poder General"
    },

    new ModelDocsEjemplo()
    {
        Fecha = new DateOnly(2025, 03, 25),
        Abogado = "Lic Laura Vega",
        Cliente = "Lucía Castro",
        TipoDocumento = "Acta Constitutiva"
    },

    new ModelDocsEjemplo()
    {
        Fecha = new DateOnly(2025, 03, 30),
        Abogado = "Lic Jorge Martínez",
        Cliente = "Andrés Soto",
        TipoDocumento = "Declaración Jurada"
    },

    new ModelDocsEjemplo()
    {
        Fecha = new DateOnly(2025, 04, 05),
        Abogado = "Lic Silvia Mora",
        Cliente = "Ana Morales",
        TipoDocumento = "Testamento"
    },

    new ModelDocsEjemplo()
    {
        Fecha = new DateOnly(2025, 04, 10),
        Abogado = "Lic Luis Porras",
        Cliente = "Diego Vargas",
        TipoDocumento = "Divorcio de Mutuo Acuerdo"
    },

    new ModelDocsEjemplo()
    {
        Fecha = new DateOnly(2025, 04, 15),
        Abogado = "Lic Carolina Herrera",
        Cliente = "María López",
        TipoDocumento = "Rectificación de Escritura"
    },

    new ModelDocsEjemplo()
    {
        Fecha = new DateOnly(2025, 04, 20),
        Abogado = "Lic Andrés Calderón",
        Cliente = "Javier Cruz",
        TipoDocumento = "Reclamación Herencia"
    }
};



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DocsGenerator()
        {
            return View();
        }
        public IActionResult DocsHistorial()
        {
            List<ModelDocsEjemplo> lista = ListaDocEjemplos;
            return View(lista);
        }

        public IActionResult DocsPrevisualizacion()
        {           
            return View();
        }

        public IActionResult Tutorial()
        {
            return View();
        }
    }
}
