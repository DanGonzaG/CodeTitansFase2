using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.LN.Casos.BuscarXid;
using Preacepta.LN.Casos.Crear;
using Preacepta.LN.Casos.Editar;
using Preacepta.LN.Casos.Eliminar;
using Preacepta.LN.Casos.Listar;
using Preacepta.LN.CasosEtapa.Listar;
using Preacepta.LN.CasosTipo.Listar;
using Preacepta.LN.GeAbogado.Listar;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.LN.GePersona.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System.Security.Claims;
using System.Text;

namespace Preacepta.UI.Controllers
{
    public class CasoController : Controller
    {
        private readonly IBuscarCasosLN _buscar;
        private readonly ICrearCasosLN _crear;
        private readonly IEditarCasosLN _editar;
        private readonly IELiminarCasosLN _eLiminar;
        private readonly IListarCasosLN _listar;

        private readonly IListarAbogadoLN _listarAbogados;
        private readonly IListarGePersonaLN _listarGePersona;
        private readonly IListarCasosTipoLN _listarCasosTipoLN;
        private readonly IBuscarXidGePersonaLN _buscarPersona;

        private readonly IListarCasosEtapasLN _listarCasosEtapas;

        private readonly IConverter _converter;

        public CasoController(
            IBuscarCasosLN buscar,
            ICrearCasosLN crear,
            IEditarCasosLN editar,
            IELiminarCasosLN eLiminar,
            IListarCasosLN listar,
            IListarAbogadoLN listarAbogados,
            IListarGePersonaLN listarGePersona,
            IListarCasosTipoLN listarCasosTipoLN,
            IBuscarXidGePersonaLN buscarPersona,


            IListarCasosEtapasLN listarCasosEtapas,


            IConverter converter)
        {
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _listar = listar;
            _listarAbogados = listarAbogados;
            _listarGePersona = listarGePersona;
            _listarCasosTipoLN = listarCasosTipoLN;
            _buscarPersona = buscarPersona;
            _listarCasosEtapas = listarCasosEtapas;
            _converter = converter;
        }

        /********************************************************************************************************************************************************************/
        //controller de Framework\\
        /********************************************************************************************************************************************************************/

        // GET: Caso
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Index()
        {
            //var contexto = _context.TCasos.Include(t => t.IdAbogadoNavigation).Include(t => t.IdClienteNavigation).Include(t => t.IdTipoCasoNavigation);
            return View(await _listar.listar());
        }

        // GET: Caso/Details/5
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCaso = await _buscar.buscar(id);
            if (tCaso == null)
            {
                return NotFound();
            }

            return View(tCaso);
        }

        // GET: Caso/Create
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Create()
        {
            //ViewData["IdAbogado"] = new SelectList(_listarAbogados.listar().Result, "Cedula", "CedulaNavigation.Nombre");
            //ViewData["IdCliente"] = new SelectList(_listarGePersona.listar().Result, "Cedula", "Nombre");

            ViewData["IdTipoCaso"] = new SelectList(_listarCasosTipoLN.listar().Result, "IdTipoCaso", "Nombre");

            ViewData["IdAbogado"] = (await _listarAbogados.listar())
                .Select(n => new SelectListItem
                {
                    Value = n.Cedula.ToString(),
                    Text = $"{n.Carnet} - Abogado(a) {n.CedulaNavigation.Nombre}"
                })
                .ToList();

            ViewData["IdCliente"] = (await _listarGePersona.listar())
               .Select(n => new SelectListItem
               {
                   Value = n.Cedula.ToString(),
                   Text = $"{n.Cedula} - {n.Nombre} {n.Apellido1} {n.Apellido2}"
               })
               .ToList();
            return View();
        }

        // POST: Caso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Create([Bind("IdCaso,Nombre,Fecha,IdTipoCaso,Descripcion,IdAbogado,IdCliente,Activo")] CasoDTO tCaso)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tCaso);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoCaso"] = new SelectList(_listarCasosTipoLN.listar().Result, "IdTipoCaso", "Nombre");

            ViewData["IdAbogado"] = (await _listarAbogados.listar())
                .Select(n => new SelectListItem
                {
                    Value = n.Cedula.ToString(),
                    Text = $"{n.Carnet} - Abogado(a) {n.CedulaNavigation.Nombre}"
                })
                .ToList();

            ViewData["IdCliente"] = (await _listarGePersona.listar())
               .Select(n => new SelectListItem
               {
                   Value = n.Cedula.ToString(),
                   Text = $"{n.Cedula} - {n.Nombre} {n.Apellido1} {n.Apellido2}"
               })
               .ToList();
            return View(tCaso);
        }

        // GET: Caso/Edit/5
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCaso = await _buscar.buscar(id);
            if (tCaso == null)
            {
                return NotFound();
            }
            ViewData["IdTipoCaso"] = new SelectList(_listarCasosTipoLN.listar().Result, "IdTipoCaso", "Nombre");

            ViewData["IdAbogado"] = (await _listarAbogados.listar())
               .Select(n => new SelectListItem
               {
                   Value = n.Cedula.ToString(),
                   Text = $"{n.Carnet} - Abogado(a) {n.CedulaNavigation.Nombre}"
               })
               .ToList();

            ViewData["IdCliente"] = (await _listarGePersona.listar())
               .Select(n => new SelectListItem
               {
                   Value = n.Cedula.ToString(),
                   Text = $"{n.Cedula} - {n.Nombre} {n.Apellido1} {n.Apellido2}"
               })
               .ToList();
            return View(tCaso);
        }

        // POST: Caso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int id, [Bind("IdCaso,Nombre,Fecha,IdTipoCaso,Descripcion,IdAbogado,IdCliente,Activo")] CasoDTO tCaso)
        {
            if (id != tCaso.IdCaso)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.Editar(tCaso);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();

                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoCaso"] = new SelectList(_listarCasosTipoLN.listar().Result, "IdTipoCaso", "Nombre");

            ViewData["IdAbogado"] = (await _listarAbogados.listar())
               .Select(n => new SelectListItem
               {
                   Value = n.Cedula.ToString(),
                   Text = $"{n.Carnet} - Abogado(a) {n.CedulaNavigation.Nombre}"
               })
               .ToList();

            ViewData["IdCliente"] = (await _listarGePersona.listar())
               .Select(n => new SelectListItem
               {
                   Value = n.Cedula.ToString(),
                   Text = $"{n.Cedula} - {n.Nombre} {n.Apellido1} {n.Apellido2}"
               })
               .ToList();
            return View(tCaso);
        }

        // GET: Caso/Delete/5
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCaso = await _buscar.buscar(id);
            if (tCaso == null)
            {
                return NotFound();
            }

            return View(tCaso);
        }

        // POST: Caso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID no válido.");
            }
            try
            {
                await _eLiminar.Eliminar(id);
                return RedirectToAction(nameof(Index));
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Error de referencia nula: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Otro error: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }


        }


        /********************************************************************************************************************************************************************/
        //controller de personalizados\\
        /********************************************************************************************************************************************************************/

        #region Creación de caso GET Y POST
        // GET: Caso/FormularioCaso
        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<IActionResult> FormularioCaso()
        {
            ViewData["IdTipoCaso"] = new SelectList(_listarCasosTipoLN.listar().Result, "IdTipoCaso", "Nombre");

            ViewData["IdAbogado"] = (await _listarAbogados.listar())
               .Select(n => new SelectListItem
               {
                   Value = n.Cedula.ToString(),
                   Text = $"{n.Carnet} - Abogado(a) {n.CedulaNavigation.Nombre}"
               })
               .ToList();

            ViewData["IdCliente"] = (await _listarGePersona.listar())
               .Select(n => new SelectListItem
               {
                   Value = n.Cedula.ToString(),
                   Text = $"{n.Cedula} - {n.Nombre} {n.Apellido1} {n.Apellido2}"
               })
               .ToList();
            return View();
        }

        // POST: Caso/FormularioCaso
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<IActionResult> FormularioCaso([Bind("Nombre,IdTipoCaso,Descripcion,IdAbogado,IdCliente")] CasoDTO tCaso)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tCaso);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoCaso"] = new SelectList(_listarCasosTipoLN.listar().Result, "IdTipoCaso", "Nombre");

            ViewData["IdAbogado"] = (await _listarAbogados.listar())
               .Select(n => new SelectListItem
               {
                   Value = n.Cedula.ToString(),
                   Text = $"{n.Carnet} - Abogado(a) {n.CedulaNavigation.Nombre}"
               })
               .ToList();

            ViewData["IdCliente"] = (await _listarGePersona.listar())
               .Select(n => new SelectListItem
               {
                   Value = n.Cedula.ToString(),
                   Text = $"{n.Cedula} - {n.Nombre} {n.Apellido1} {n.Apellido2}"
               })
               .ToList();
            return View(tCaso);
        }
        #endregion

        #region Listar los caso Según el usuario y el rol
        // GET: ListarCasos
        [Authorize(Roles = "Gestor, Abogado, Cliente")]
        public async Task<IActionResult> CasosListado()
        {
            //var abogado = await _buscarPersona.buscarXcorreo(User.Identity.Name);
            //var listaCasos = _listar.listarXabogado(abogado.Cedula);

            //var contexto = _context.TCasos.Include(t => t.IdAbogadoNavigation).Include(t => t.IdClienteNavigation).Include(t => t.IdTipoCasoNavigation);

            var usuario = (ClaimsIdentity)User.Identity;
            var rol = usuario.FindFirst(ClaimTypes.Role)?.Value;
            var persona = await _buscarPersona.buscarXcorreo(User.Identity.Name);

            return rol switch
            {
                "Gestor" => View(await _listar.listar()),
                "Abogado" => View(await _listar.listarXabogado(persona.Cedula)),
                "Cliente" => View(await _listar.listarXcliente(persona.Cedula)),
                _ => View(new List<CasoDTO>())
            };
        }
        #endregion

        #region Listar los casos CERRADOS segun usuario y rol
        // GET: CasosListadoHistorial
        [Authorize(Roles = "Gestor, Abogado, Cliente")]
        public async Task<IActionResult> CasosListadoHistorial()
        {

            var usuario = (ClaimsIdentity)User.Identity;
            var rol = usuario.FindFirst(ClaimTypes.Role)?.Value;
            var persona = await _buscarPersona.buscarXcorreo(User.Identity.Name);

            return rol switch
            {
                "Gestor" => View(await _listar.listar()),
                "Abogado" => View(await _listar.listarXabogado(persona.Cedula)),
                "Cliente" => View(await _listar.listarXcliente(persona.Cedula)),
                _ => View(new List<CasoDTO>())
            };
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> DescargarCasoCompleto(int IdCaso)
        {           
            var imagenPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "img", "PreaceptaLogoColorNegro.png");
            var imagenlogo = System.IO.File.ReadAllBytes(imagenPath);
            var base64 = Convert.ToBase64String(imagenlogo);
            var casoEncontrado = await _buscar.buscar(IdCaso);
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DescargaCasos", "CasoPlantillaPrincipalFem.html");
            if (casoEncontrado == null)
            {
                throw new Exception("Caso no encontrado");
            }
            var etapasEncontradas = await _listarCasosEtapas.listarXcaso(IdCaso);
            if (etapasEncontradas == null)
            {
                throw new Exception("Lista de etapas no encontradas");
            }

            if(casoEncontrado.IdClienteNavigation.Genero == "Femenino") 
            {
                templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DescargaCasos", "CasoPlantillaPrincipalFem.html");
            }
            else 
            {
                templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DescargaCasos", "CasoPlantillaPrincipalMasc.html");
            }
            var templatePathEtapas = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DescargaCasos", "CasoPlantilla2.html");
            var htmlTemplate = System.IO.File.ReadAllText(templatePath);
            var htmlEtapas = System.IO.File.ReadAllText(templatePathEtapas);
            var etapasEntradasHtml = new StringBuilder(); // esta variable va a contener el arreglo de etapasPL

            htmlTemplate = htmlTemplate
                //encabezado
                .Replace("{{LOGO}}", base64)
                .Replace("{{CedulaJuridica}}", casoEncontrado.IdAbogadoNavigation.CJuridica.ToString())
                .Replace("{{NombreBufete}}", casoEncontrado.IdAbogadoNavigation.CJuridicaNavigation.Nombre)
                .Replace("{{TelefonoDespacho}}", casoEncontrado.IdAbogadoNavigation.CJuridicaNavigation.Telefono)
                .Replace("{{EmailDespacho}}", casoEncontrado.IdAbogadoNavigation.CJuridicaNavigation.Email)
                //Datos del cliente y del abogado
                .Replace("{{NombreCliente}}", casoEncontrado.IdClienteNavigation.Nombre)
                .Replace("{{Apellido1Cliente}}", casoEncontrado.IdClienteNavigation.Apellido1)
                .Replace("{{Apellido2Cliente}}", casoEncontrado.IdClienteNavigation.Apellido2)
                .Replace("{{IdCliente}}", casoEncontrado.IdClienteNavigation.Cedula.ToString())
                .Replace("{{NombreAbogado}}", casoEncontrado.IdAbogadoNavigation.CedulaNavigation.Nombre)
                .Replace("{{Apellido1Abogado}}", casoEncontrado.IdAbogadoNavigation.CedulaNavigation.Apellido1)
                .Replace("{{Apellido2Abogado}}", casoEncontrado.IdAbogadoNavigation.CedulaNavigation.Apellido2)
                .Replace("{{Carnet}}", casoEncontrado.IdAbogadoNavigation.Carnet.ToString())
                //Datos del caso
                .Replace("{{idCaso}}", casoEncontrado.IdCaso.ToString())
                .Replace("{{FechaIncio}}", casoEncontrado.Fecha)
                .Replace("{{TipoDeCaso}}", casoEncontrado.IdTipoCasoNavigation.Nombre)
                .Replace("{{NombreCaso}}", casoEncontrado.Nombre)
                .Replace("{{Descripcion}}", casoEncontrado.Descripcion)
                .Replace("{{contadorEtapas}}", etapasEncontradas.Count().ToString())
            //Feha y hora
                .Replace("{{nombreDIA}}", DateTime.Now.ToString("dddd"))
                .Replace("{{DIA}}", DateTime.Now.ToString("dd"))
                .Replace("{{NombreMes}}", DateTime.Now.ToString("MMMM"))
                .Replace("{{ANYO}}", DateTime.Now.ToString("yyyy"))
                .Replace("{{HoraActual}}", DateTime.Now.ToString("HH:mm"));
            //Etapas del caso

            foreach (var item in etapasEncontradas)
            {
                etapasEntradasHtml.Append
                    (htmlEtapas
                    .Replace("{{FechaEtapa}}", item.Fecha)
                    .Replace("{{NumeroEtapa}}", item.IdEtapaPl.ToString())
                    .Replace("{{NombreEtapa}}", item.Nombre)
                    .Replace("{{EtapaDescripcon}}", item.Descripcion)                    
                    );
            }
            htmlTemplate = htmlTemplate.Replace("{{ETAPAS}}", etapasEntradasHtml.ToString());
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait
                },
                Objects = {
            new ObjectSettings
            {
                HtmlContent = htmlTemplate,
                WebSettings = { DefaultEncoding = "utf-8" }
            }
        }
            };

            var pdf = _converter.Convert(doc);

            return File(pdf, "application/pdf");
        }
    }
}
