using Microsoft.AspNetCore.Mvc;
using Preacepta.LN.Casos.Listar;
using Preacepta.LN.GeAbogado.BuscarXid;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.Modelos.AbstraccionesFrond;
using Preacepta.UI.Models;
using System.Diagnostics;

namespace Praecepta.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBuscarAbogadoLN _buscarAbogado;
        private readonly IBuscarXidGePersonaLN _buscarPersona;
        private readonly IListarCasosLN _listarUlimoCaso;

        public HomeController(ILogger<HomeController> logger, IBuscarXidGePersonaLN buscarPersona, IBuscarAbogadoLN buscarAbogado, IListarCasosLN listarUlimoCaso)
        {
            _logger = logger;
            _buscarPersona = buscarPersona;
            _buscarAbogado = buscarAbogado;
            _listarUlimoCaso = listarUlimoCaso;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Attorneys()
        {
            return View();
        }

        public IActionResult Testimonials()
        {
            return View();
        }

        public IActionResult TestimonialForm()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }


        public IActionResult CaseStudyDetails()
        {
            return View("Practice/CaseStudyDetails");
        }

        public IActionResult AttorneyDetails()
        {
            return View("AttorneyDetails/AttorneyDetails");
        }

        public async Task<IActionResult> UsuarioAutenticado(string correo)
        {
            if (correo == "gestor@preacepta.com") 
            {
                return View("Index");
            }
            if (User.IsInRole("Abogado"))
            {
                var persona = await _buscarPersona.buscarXcorreo(correo);
                var abogado = await _buscarAbogado.buscar(persona.Cedula);
                var ultimoCaso = await _listarUlimoCaso.listarXultimaFecha(persona.Cedula);
                PersonaAbogadoCasoDocCita perfilCompleto = new PersonaAbogadoCasoDocCita();
                perfilCompleto.personaDTO = persona;
                perfilCompleto.geAbogadoDTO = abogado;
                perfilCompleto.UltimoCasoCreado = ultimoCaso;

                return View("UsuarioAutenticado",perfilCompleto);
            }
            if (User.IsInRole("Cliente"))
            {
                return View("Index");
            }
            return View("Index");
        }

        /*public async Task<IActionResult> UsuarioAutenticado(string correo)
        {
            if (correo == "gestor@preacepta.com")
            {
                return View("Index");
            }

            return View();
        }*/




    }
}
