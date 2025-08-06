using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Preacepta.AD;
using Preacepta.LN.Casos.Listar;
using Preacepta.LN.Citas.Listar;
using Preacepta.LN.DocsCompraventaFinca.Listar;
using Preacepta.LN.GeAbogado.BuscarXid;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.Modelos.AbstraccionesFrond;
using Preacepta.UI.Models;
using Preacepta.UI.Services;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Encodings.Web;

namespace Praecepta.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly Contexto _contexto;
        private readonly ILogger<HomeController> _logger;
        private readonly IBuscarAbogadoLN _buscarAbogado;
        private readonly IBuscarXidGePersonaLN _buscarPersona;
        private readonly IListarCasosLN _listarTresUltimosCasos;
        private readonly IListarDocsCompraventaFincaLN _listarTresUltimosDocs;
        private readonly IListarCitasLN _listarTresUltimasCitas;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IServicioEmail _emailSender;

        public HomeController(
            Contexto contexto,
            ILogger<HomeController> logger, 
            IBuscarXidGePersonaLN buscarPersona, 
            IBuscarAbogadoLN buscarAbogado, 
            IListarCasosLN listarTresUltimosCasos,
            IListarDocsCompraventaFincaLN listarTresUltimosDocs,
            IListarCitasLN listarTresUltimasCitas,
            SignInManager<IdentityUser> signInManager,
            IServicioEmail emailSender)
        {
            _contexto = contexto;
            _logger = logger;
            _buscarPersona = buscarPersona;
            _buscarAbogado = buscarAbogado;
            _listarTresUltimosCasos = listarTresUltimosCasos;
            _listarTresUltimosDocs = listarTresUltimosDocs;
            _listarTresUltimasCitas = listarTresUltimasCitas;
            _signInManager = signInManager;
            _emailSender = emailSender;
            
        }

        [BindProperty]
        public ContactoViewModel contactoFormulario { get; set; }


        public class ContactoViewModel
        {
            [Required (ErrorMessage ="Debe agregar su cédula")]
            [DisplayName ("Cédula")]
            public string cedula { get; set; }
            [Required(ErrorMessage = "Debe agregar su nombre completo")]
            [DisplayName("Nombre Completo")]
            public string name { get; set; }
            [Required(ErrorMessage = "Debe agregar un correo electrónico")]
            [EmailAddress]
            [DisplayName("Correo electrónico")]
            public string email { get; set; }
            [Required(ErrorMessage = "Debe agregar un número telefónico")]
            [Phone]
            [DisplayName("Número telefónico")]
            public string phone_number { get; set; }
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

        //vista derecho penal
        public IActionResult CaseStudyDetails()
        {
            return View("Practice/CaseStudyDetails");
        }

        //vista derecho administrativo
        public IActionResult Administrativo()
        {
            return View("Practice/Administrativo");
        }

        //derecho civil
        public IActionResult Civil()
        {
            return View("Practice/Civil");
        }

        //vista derecho laboral
        public IActionResult Laboral()
        {
            return View("Practice/Laboral");
        }

        //vista derecho corporativo
        public IActionResult Corporativo()
        {
            return View("Practice/Corporativo");
        }

        //vista derecho notarial
        public IActionResult Notarial()
        {
            return View("Practice/Notarial");
        }

        public IActionResult AttorneyDetails()
        {
            return View("AttorneyDetails/AttorneyDetails");
        }

        /*Este metodo envia un correo electronico al despacho para contactar con los abogados*/
        public async Task<IActionResult> EnviarSolicitudDeContacto([Bind("cedula,name,email,phone_number")] ContactoViewModel formulario)
        {

            if (ModelState.IsValid) 
            {
                var htmlMensaje = $@"
            <div style='font-family: Arial, sans-serif; padding: 20px; background-color: #f9f9f9;'>
                <h2 style='color: #2a7ae2;'>Solicitud de contacto desde PreaceptaApp</h2>
                <p><strong>Nombre:</strong> {formulario.name}</p>
                <p><strong>Cédula:</strong> {formulario.cedula}</p>
                <p><strong>Teléfono:</strong> {formulario.phone_number}</p>
                <p><strong>Correo:</strong> <a href='mailto:{formulario.email}'>{formulario.email}</a></p>
                <hr />
                <p>Este mensaje fue enviado desde el formulario de contacto web. Por favor comuníquese con la persona cuanto antes.</p>
            </div>";

                await _emailSender.BuzonPreacepta(
                contactoFormulario.email,
                "Sistema de notifcaciónes y correos PreaceptaApp",
               htmlMensaje);

                TempData["MensajeEnviado"] = "Su mensaje fue enviado, pronto le contactaremos";
                return View("Index");

            }
            return View("Index");
        }


        #region Vista para usuarios Autenticados
        [Authorize(Roles = "Gestor,Abogado,Cliente")]
        public async Task<IActionResult> UsuarioAutenticado(string correo)
        {
            if (correo == "gestor@preacepta.com")
            {
                if (User.IsInRole("Gestor"))
                {

                    return View("UsuarioGestor");
                }
                else
                {
                    return View("Index");
                }
            }
            
            

            if (User.IsInRole("Abogado"))
            {

                var persona = await _buscarPersona.buscarXcorreo(correo);
                var abogado = await _buscarAbogado.buscar(persona.Cedula);
                var TresUltimosCasos = await _listarTresUltimosCasos.listarXabogadoLos3Casos(persona.Cedula);
                var TresUltimosDosc = await _listarTresUltimosDocs.ListarTresUltimosDocs(persona.Cedula);
                var citas = await _listarTresUltimasCitas.TresCitasMasProximasXAfitrion(persona.Cedula);
                PersonaAbogadoCasoDocCita perfilCompleto = new PersonaAbogadoCasoDocCita();
                perfilCompleto.personaDTO = persona;
                perfilCompleto.geAbogadoDTO = abogado;
                perfilCompleto.TresUltimosCasosXabogado = TresUltimosCasos;
                perfilCompleto.TresUltimosDocs = TresUltimosDosc;
                perfilCompleto.TresCitasProximasXAfitrion = citas;


                return View("UsuarioAbogado", perfilCompleto);
            }

            if (User.IsInRole("Cliente"))
            {
                var persona = await _buscarPersona.buscarXcorreo(correo);
                var TresUlitmosCasos = await _listarTresUltimosCasos.listarXclienteLos3Casos(persona.Cedula);
                var TresUltimosDosc = await _listarTresUltimosDocs.ListarTresUltimosDocsXCliente(persona.Cedula);
                var citas = await _listarTresUltimasCitas.TresCitasMasProximasXCliente(persona.Cedula);
                var ModeloCompleto = new PersonaAbogadoCasoDocCita
                {
                    personaDTO = persona,
                    TresUltimosCasosXcliente = TresUlitmosCasos,
                    ListarTresUltimosDocsXCliente = TresUltimosDosc,
                    TresCitasMasProximasXCliente = citas
                };
                return View("UsuarioCliente", ModeloCompleto);
            }

            return View("Index");
        }
        #endregion


        [HttpGet]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> VolverPaginaPrincipal()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
