using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.DocsAutorizacionRevisionExpediente.BuscarXid;
using Preacepta.LN.DocsAutorizacionRevisionExpediente.Crear;
using Preacepta.LN.DocsAutorizacionRevisionExpediente.Editar;
using Preacepta.LN.DocsAutorizacionRevisionExpediente.Eliminar;
using Preacepta.LN.DocsAutorizacionRevisionExpediente.Listar;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.LN.GePersona.Listar;
using Preacepta.LN.HistorialDocumentos.BuscarXid;
using Preacepta.LN.HistorialDocumentos.Crear;
using Preacepta.LN.HistorialDocumentos.Eliminar;
using Preacepta.LN.HistorialDocumentos.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using Preacepta.LN.BitacoraEventos.Crear;

namespace Preacepta.UI.Controllers
{
    public class DocsAutorizacionRevisionExpedientesController : Controller
    {
        private readonly IConverter _converter;
        
        private readonly IBuscarDocsAutorizacionRevisionExpedienteLN _buscar;
        private readonly ICrearDocsAutorizacionRevisionExpedienteLN _crear;
        private readonly IEditarDocsAutorizacionRevisionExpedienteLN _editar;
        private readonly IEliminarDocsAutorizacionRevisionExpedienteLN _eliminar;
        private readonly IListarDocsAutorizacionRevisionExpedienteLN _listar;
        private readonly IBuscarXidGePersonaLN _buscarPersona;
        private readonly IListarGePersonaLN _listarPersonas;

        private readonly ICrearHistorialLN _crearHistorialLN;
        private readonly IListarHistorialLN _listarHistorialLN;
        private readonly IBuscarHistorialLN _buscarHistorialLN;
        private readonly IELiminarHistorialLN _eLiminarHistorialLN;
        private readonly ICrearEventosLN _bitacoraLN;

        public DocsAutorizacionRevisionExpedientesController(IConverter converter,
            
            IBuscarDocsAutorizacionRevisionExpedienteLN buscar,
            ICrearDocsAutorizacionRevisionExpedienteLN crear,
            IEditarDocsAutorizacionRevisionExpedienteLN editar,
            IEliminarDocsAutorizacionRevisionExpedienteLN eliminar,
            IListarDocsAutorizacionRevisionExpedienteLN listar,
            IBuscarXidGePersonaLN buscarPersona,
            IListarGePersonaLN listarPersonas,
            ICrearHistorialLN crearHistorialLN,
            IListarHistorialLN listarHistorialLN,
            IBuscarHistorialLN buscarHistorialLN,
            IELiminarHistorialLN eLiminarHistorialLN,
            ICrearEventosLN bitacora)
        {
            _converter = converter;
           
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;
            _buscarPersona = buscarPersona;
            _listarPersonas = listarPersonas;
            _crearHistorialLN = crearHistorialLN;
            _listarHistorialLN = listarHistorialLN;
            _buscarHistorialLN = buscarHistorialLN;
            _eLiminarHistorialLN = eLiminarHistorialLN;
            _bitacoraLN = bitacora;
        }

        /********************************************************/
        //controller de Framework\\
        /********************************************************/


        #region ListarRoot
        // GET: TDocsAutorizacionRevisionDaniel
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Index()
        {
            return View(await _listar.listar());
        }
        #endregion

        #region Detalles Root
        // GET: TDocsAutorizacionRevisionDaniel/Details/5
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsAutorizacionRevisionExpediente = await _buscar.buscar(id);
            if (tDocsAutorizacionRevisionExpediente == null)
            {
                return NotFound();
            }

            return View(tDocsAutorizacionRevisionExpediente);
        }
        #endregion

        #region Crear Root metodo POST y GET
        // GET: TDocsAutorizacionRevisionDaniel/Create
        [Authorize(Roles = "Gestor")]
        public IActionResult Create()
        {
            ViewData["CedulaAbogado"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula");
            ViewData["CedulaAsistente"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula");
            ViewData["CedulaImputado"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula");
            return View();
        }

        // POST: TDocsAutorizacionRevisionDaniel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Create([Bind("IdDocumento,Expediente,Delito,CedulaImputado,Ofendido,CedulaAbogado,CedulaAsistente")] DocsAutorizacionRevisionExpedienteDTO tDocsAutorizacionRevisionExpediente)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tDocsAutorizacionRevisionExpediente);
                var Registros = await _listar.listar();
                var idDocumento = Registros.LastOrDefault();
                HistorialDocumentoDTO historialDocumentoDTO = new HistorialDocumentoDTO
                {
                    Cliente = tDocsAutorizacionRevisionExpediente.CedulaImputado,
                    Abogado = tDocsAutorizacionRevisionExpediente.CedulaAbogado,
                    Fecha = DateTime.Now.ToString(),
                    TipoDocumento = "Atorización RE.",
                    IdDocumento = idDocumento.IdDocumento,
                    Titulo = $"Doc.no.{idDocumento.IdDocumento} Atorización"

                };
                await _crearHistorialLN.Crear(historialDocumentoDTO);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CedulaAbogado"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula", tDocsAutorizacionRevisionExpediente.CedulaAbogado);
            ViewData["CedulaAsistente"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula", tDocsAutorizacionRevisionExpediente.CedulaAsistente);
            ViewData["CedulaImputado"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula", tDocsAutorizacionRevisionExpediente.CedulaImputado);
            return View(tDocsAutorizacionRevisionExpediente);
        }
        #endregion

        #region Editar Root metodo POST y GET
        // GET: TDocsAutorizacionRevisionDaniel/Edit/5
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsAutorizacionRevisionExpediente = await _buscar.buscar(id);
            if (tDocsAutorizacionRevisionExpediente == null)
            {
                return NotFound();
            }
            ViewData["CedulaAbogado"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula", tDocsAutorizacionRevisionExpediente.CedulaAbogado);
            ViewData["CedulaAsistente"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula", tDocsAutorizacionRevisionExpediente.CedulaAsistente);
            ViewData["CedulaImputado"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula", tDocsAutorizacionRevisionExpediente.CedulaImputado);
            return View(tDocsAutorizacionRevisionExpediente);
        }

        // POST: TDocsAutorizacionRevisionDaniel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int id, [Bind("IdDocumento,Expediente,Delito,CedulaImputado,Ofendido,CedulaAbogado,CedulaAsistente")] DocsAutorizacionRevisionExpedienteDTO tDocsAutorizacionRevisionExpediente)
        {
            if (id != tDocsAutorizacionRevisionExpediente.IdDocumento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.Editar(tDocsAutorizacionRevisionExpediente);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CedulaAbogado"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula", tDocsAutorizacionRevisionExpediente.CedulaAbogado);
            ViewData["CedulaAsistente"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula", tDocsAutorizacionRevisionExpediente.CedulaAsistente);
            ViewData["CedulaImputado"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula", tDocsAutorizacionRevisionExpediente.CedulaImputado);
            return View(tDocsAutorizacionRevisionExpediente);
        }
        #endregion

        #region Eliminar Root metodo POST y GET
        // GET: TDocsAutorizacionRevisionDaniel/Delete/5
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsAutorizacionRevisionExpediente = await _buscar.buscar(id);
            if (tDocsAutorizacionRevisionExpediente == null)
            {
                return NotFound();
            }

            return View(tDocsAutorizacionRevisionExpediente);
        }

        // POST: TDocsAutorizacionRevisionDaniel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tDocsAutorizacionRevisionExpediente = await _buscar.buscar(id);
            if (tDocsAutorizacionRevisionExpediente != null)
            {
                await _eliminar.Eliminar(id);
                int buscarHistorial = await _buscarHistorialLN.BuscarXidDocumento(id, "Atorización RE.");
                await _eLiminarHistorialLN.Eliminar(buscarHistorial);
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        /********************************************************/
        //controller de personalizados\\
        /********************************************************/


        #region Crear Documento Abogado
        [HttpGet]
        [Authorize(Roles = "Abogado")]
        public async Task<IActionResult> CreateDocsAutorizacionRevisionExpedientes(int cedulaImputado, int cedulaAsistente)
        {
            var cliente = await _buscarPersona.buscar(cedulaImputado);
            var asistente = await _buscarPersona.buscar(cedulaAsistente);
            
            var abogado = await _buscarPersona.buscarXcorreo(User.Identity.Name);

            ViewBag.ClienteCedula = cliente.Cedula;
            ViewBag.ClienteNombre = cliente.Nombre;
            ViewBag.ClienteApellido1 = cliente.Apellido1;
            ViewBag.ClienteApellido2 = cliente.Apellido2;
            ViewBag.Dash = " - ";

            ViewBag.AbogadoCedula = abogado.Cedula;
            ViewBag.AbogadoNombre = abogado.Nombre;
            ViewBag.AbogadoApellido1 = abogado.Apellido1;
            ViewBag.AbogadoApellido2 = abogado.Apellido2;

            ViewBag.CedulaAsistente = asistente.Cedula;
            ViewBag.NombreAsistente = asistente.Nombre;
            ViewBag.Apellido1Asistente = asistente.Apellido1;
            ViewBag.Apellido2Asistente = asistente.Apellido2;

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Abogado")]
        public async Task<IActionResult> CreateDocsAutorizacionRevisionExpedientes([Bind("IdDocumento,Expediente,Delito,CedulaImputado,Ofendido,CedulaAbogado,CedulaAsistente")] DocsAutorizacionRevisionExpedienteDTO tDocsAutorizacionRevisionExpediente)
        {
            var cliente = await _buscarPersona.buscar(tDocsAutorizacionRevisionExpediente.CedulaImputado);
            var asistente = await _buscarPersona.buscar(tDocsAutorizacionRevisionExpediente.CedulaAsistente);
            var abogado = await _buscarPersona.buscarXcorreo(User.Identity.Name);
            if (ModelState.IsValid)
            {
                await _crear.Crear(tDocsAutorizacionRevisionExpediente);
                var Registros = await _listar.listar();
                var idDocumento = Registros.LastOrDefault();

                // Registrar en bitácora
                var usuario = User.Identity?.Name ?? "Desconocido";
                var accion = $"Se creó autorización de revisión de expediente Doc.no.{idDocumento.IdDocumento} para el imputado {tDocsAutorizacionRevisionExpediente.CedulaImputado}";
                await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_DocsAutorizacionRevisionExpediente", accion, idDocumento.IdDocumento);


                HistorialDocumentoDTO historialDocumentoDTO = new HistorialDocumentoDTO
                {
                    Cliente = tDocsAutorizacionRevisionExpediente.CedulaImputado,
                    Abogado = tDocsAutorizacionRevisionExpediente.CedulaAbogado,
                    Fecha = DateTime.Now.ToString(),
                    TipoDocumento = "Atorización RE.",
                    IdDocumento = idDocumento.IdDocumento,
                    Titulo = $"Doc.no.{idDocumento.IdDocumento} Atorización"

                };
                await _crearHistorialLN.Crear(historialDocumentoDTO);



                return RedirectToAction("DocsHistorial", "THistorialDocumento1");
            }
            ViewBag.ClienteCedula = cliente.Cedula;
            ViewBag.ClienteNombre = cliente.Nombre;
            ViewBag.ClienteApellido1 = cliente.Apellido1;
            ViewBag.ClienteApellido2 = cliente.Apellido2;
            ViewBag.Dash = " - ";

            ViewBag.AbogadoCedula = abogado.Cedula;
            ViewBag.AbogadoNombre = abogado.Nombre;
            ViewBag.AbogadoApellido1 = abogado.Apellido1;
            ViewBag.AbogadoApellido2 = abogado.Apellido2;

            ViewBag.CedulaAsistente = asistente.Cedula;
            ViewBag.NombreAsistente = asistente.Nombre;
            ViewBag.Apellido1Cliente = asistente.Apellido1;
            ViewBag.Apellido2Cliente = asistente.Apellido2;

            return View(tDocsAutorizacionRevisionExpediente);
        }
        #endregion

        #region Previzualizar PDF Abogado
        [HttpGet]
        [Authorize(Roles = "Abogado")]
        public IActionResult PrevisualizarPDF(string expediente, string delito, string cedulaImputado, string ofendido, string cedulaAbogado, string cedulaAsistente)
        {
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DocsMachotes", "AutorizacionExpedienteMachote.html");
            var htmlTemplate = System.IO.File.ReadAllText(templatePath);

            htmlTemplate = htmlTemplate
                .Replace("{{EXPEDIENTE}}", expediente)
                .Replace("{{DELITO}}", delito)
                .Replace("{{CEDULA_IMPUTADO}}", cedulaImputado)
                .Replace("{{OFENDIDO}}", ofendido)
                .Replace("{{CEDULA_ABOGADO}}", cedulaAbogado)
                .Replace("{{CEDULA_ASISTENTE}}", cedulaAsistente);

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
        #endregion
    }
}