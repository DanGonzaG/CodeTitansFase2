using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.DocsAutorizacionRevisionExpediente.BuscarXid;
using Preacepta.LN.DocsAutorizacionRevisionExpediente.Crear;
using Preacepta.LN.DocsAutorizacionRevisionExpediente.Editar;
using Preacepta.LN.DocsAutorizacionRevisionExpediente.Eliminar;
using Preacepta.LN.DocsAutorizacionRevisionExpediente.Listar;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Preacepta.UI.Controllers
{
    public class DocsAutorizacionRevisionExpedientesController : Controller
    {
        private readonly IConverter _converter;
        private readonly Contexto _context;
        private readonly IBuscarDocsAutorizacionRevisionExpedienteLN _buscar;
        private readonly ICrearDocsAutorizacionRevisionExpedienteLN _crear;
        private readonly IEditarDocsAutorizacionRevisionExpedienteLN _editar;
        private readonly IEliminarDocsAutorizacionRevisionExpedienteLN _eliminar;
        private readonly IListarDocsAutorizacionRevisionExpedienteLN _listar;

        public DocsAutorizacionRevisionExpedientesController(IConverter converter,
            Contexto context,
            IBuscarDocsAutorizacionRevisionExpedienteLN buscar,
            ICrearDocsAutorizacionRevisionExpedienteLN crear,
            IEditarDocsAutorizacionRevisionExpedienteLN editar,
            IEliminarDocsAutorizacionRevisionExpedienteLN eliminar,
            IListarDocsAutorizacionRevisionExpedienteLN listar)
        {
            _converter = converter;
            _context = context;
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;
        }

        // GET: AutorizacionRevisionExpedientes
        public async Task<IActionResult> Index()
        {
            //var contexto = _context.TDocsAutorizacionRevisionExpedientes.Include(t => t.CedulaAbogadoNavigation).Include(t => t.CedulaAsistenteNavigation).Include(t => t.CedulaImputadoNavigation);
            return View(await _listar.listar());
        }

        // GET: AutorizacionRevisionExpedientes/Details/5
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

        // GET: AutorizacionRevisionExpedientes/Create
        public IActionResult Create()
        {
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula");
            ViewData["CedulaAsistente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1");
            ViewData["CedulaImputado"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1");
            return View();
        }

        // POST: AutorizacionRevisionExpedientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDocumento,Expediente,Delito,CedulaImputado,Ofendido,CedulaAbogado,CedulaAsistente")] DocsAutorizacionRevisionExpedienteDTO tDocsAutorizacionRevisionExpediente,
            [FromForm] int? DocumentoAnteriorId
            )
        {
            if (ModelState.IsValid)
            {

                // Eliminar documento anterior y su historial
                if (DocumentoAnteriorId.HasValue)
                {
                    // Buscar historial por ID
                    var historialAnterior = await _context.HistorialDocumentos
                        .FirstOrDefaultAsync(h => h.Id == DocumentoAnteriorId.Value);

                    if (historialAnterior != null)
                    {
                        // Obtener el ID del documento original desde el historial
                        var idDocOriginal = historialAnterior.DocumentoIdOriginal;

                        // Buscar y eliminar el documento original
                        var docAnterior = await _context.TDocsAutorizacionRevisionExpedientes
                            .FirstOrDefaultAsync(p => p.IdDocumento == idDocOriginal);

                        if (docAnterior != null)
                            _context.TDocsAutorizacionRevisionExpedientes.Remove(docAnterior);

                        // Eliminar también el historial
                        _context.HistorialDocumentos.Remove(historialAnterior);

                        await _context.SaveChangesAsync();
                    }
                }

                await _crear.Crear(tDocsAutorizacionRevisionExpediente);

                // Obtener cliente desde TGePersonas
                var cliente = await _context.TGePersonas
                     .FirstOrDefaultAsync(p => p.Cedula == tDocsAutorizacionRevisionExpediente.CedulaImputado);

                string nombreCliente = cliente != null
                    ? $"{cliente.Nombre} {cliente.Apellido1} {cliente.Apellido2}"
                    : tDocsAutorizacionRevisionExpediente.CedulaImputado.ToString();

                // Obtener abogado desde TGeAbogados (con su persona)
                var abogado = await _context.TGeAbogados
                    .Include(a => a.CedulaNavigation)
                    .FirstOrDefaultAsync(a => a.Cedula == tDocsAutorizacionRevisionExpediente.CedulaAbogado);

                string nombreAbogado = abogado != null
                    ? $"{abogado.CedulaNavigation.Nombre} {abogado.CedulaNavigation.Apellido1} {abogado.CedulaNavigation.Apellido2}"
                    : tDocsAutorizacionRevisionExpediente.CedulaAbogado.ToString();

                // Crear nuevo historial con ID real del documento creado
                var nuevoIdDocumento = _context.TDocsAutorizacionRevisionExpedientes
                    .OrderByDescending(p => p.IdDocumento)
                    .Select(p => p.IdDocumento)
                    .FirstOrDefault();

                // Guardar en el historial
                var historial = new HistorialDocumento
                {
                    Fecha = DateTime.Now,
                    TipoDocumento = "Autorización de revisión de expedientes",
                    Cliente = nombreCliente,
                    Abogado = nombreAbogado,
                    DocumentoIdOriginal = nuevoIdDocumento
                };

                _context.HistorialDocumentos.Add(historial);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tDocsAutorizacionRevisionExpediente.CedulaAbogado);
            ViewData["CedulaAsistente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsAutorizacionRevisionExpediente.CedulaAsistente);
            ViewData["CedulaImputado"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsAutorizacionRevisionExpediente.CedulaImputado);
            return View(tDocsAutorizacionRevisionExpediente);
        }

        // GET: AutorizacionRevisionExpedientes/Edit/5
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
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tDocsAutorizacionRevisionExpediente.CedulaAbogado);
            ViewData["CedulaAsistente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsAutorizacionRevisionExpediente.CedulaAsistente);
            ViewData["CedulaImputado"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsAutorizacionRevisionExpediente.CedulaImputado);
            return View(tDocsAutorizacionRevisionExpediente);
        }

        // POST: AutorizacionRevisionExpedientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tDocsAutorizacionRevisionExpediente.CedulaAbogado);
            ViewData["CedulaAsistente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsAutorizacionRevisionExpediente.CedulaAsistente);
            ViewData["CedulaImputado"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsAutorizacionRevisionExpediente.CedulaImputado);
            return View(tDocsAutorizacionRevisionExpediente);
        }

        // GET: AutorizacionRevisionExpedientes/Delete/5
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

        // POST: AutorizacionRevisionExpedientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eliminar.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        // DE ACA EN ADELNATE ESTAN MIS METODOS
        // GET: AutorizacionRevisionExpedientes/Create
        public IActionResult CreateDocsAutorizacionRevisionExpedientes()
        {
            ViewData["CedulaAbogado"] = new SelectList(
                _context.TGeAbogados.Include(a => a.CedulaNavigation).Select(a => new
                {
                    Cedula = a.Cedula,
                    Texto = a.CedulaNavigation.Nombre + " " + a.CedulaNavigation.Apellido1 + " - " + a.Cedula
                }),
                "Cedula",
                "Texto");

            ViewData["CedulaAsistente"] = new SelectList(
                _context.TGePersonas.Select(p => new
                {
                    Cedula = p.Cedula,
                    Texto = p.Nombre + " " + p.Apellido1 + " - " + p.Cedula
                }),
                "Cedula",
                "Texto");

            ViewData["CedulaImputado"] = new SelectList(
                _context.TGePersonas.Select(p => new {
                    Cedula = p.Cedula,
                    Texto = p.Nombre + " " + p.Apellido1 + " - " + p.Cedula
                }),
                "Cedula",
                "Texto");

            return View();
        }

        // POST: AutorizacionRevisionExpedientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDocsAutorizacionRevisionExpedientes([Bind("IdDocumento,Expediente,Delito,CedulaImputado,Ofendido,CedulaAbogado,CedulaAsistente")] DocsAutorizacionRevisionExpedienteDTO tDocsAutorizacionRevisionExpediente,
            [FromForm] int? DocumentoAnteriorId
            )
        {
            if (ModelState.IsValid)
            {

                // Eliminar documento anterior y su historial
                if (DocumentoAnteriorId.HasValue)
                {
                    // Buscar historial por ID
                    var historialAnterior = await _context.HistorialDocumentos
                        .FirstOrDefaultAsync(h => h.Id == DocumentoAnteriorId.Value);

                    if (historialAnterior != null)
                    {
                        // Obtener el ID del documento original desde el historial
                        var idDocOriginal = historialAnterior.DocumentoIdOriginal;

                        // Buscar y eliminar el documento original
                        var docAnterior = await _context.TDocsAutorizacionRevisionExpedientes
                            .FirstOrDefaultAsync(p => p.IdDocumento == idDocOriginal);

                        if (docAnterior != null)
                            _context.TDocsAutorizacionRevisionExpedientes.Remove(docAnterior);

                        // Eliminar también el historial
                        _context.HistorialDocumentos.Remove(historialAnterior);

                        await _context.SaveChangesAsync();
                    }
                }

                await _crear.Crear(tDocsAutorizacionRevisionExpediente);

                // Obtener cliente desde TGePersonas
                var cliente = await _context.TGePersonas
                     .FirstOrDefaultAsync(p => p.Cedula == tDocsAutorizacionRevisionExpediente.CedulaImputado);

                string nombreCliente = cliente != null
                    ? $"{cliente.Nombre} {cliente.Apellido1} {cliente.Apellido2}"
                    : tDocsAutorizacionRevisionExpediente.CedulaImputado.ToString();

                // Obtener abogado desde TGeAbogados (con su persona)
                var abogado = await _context.TGeAbogados
                    .Include(a => a.CedulaNavigation)
                    .FirstOrDefaultAsync(a => a.Cedula == tDocsAutorizacionRevisionExpediente.CedulaAbogado);

                string nombreAbogado = abogado != null
                    ? $"{abogado.CedulaNavigation.Nombre} {abogado.CedulaNavigation.Apellido1} {abogado.CedulaNavigation.Apellido2}"
                    : tDocsAutorizacionRevisionExpediente.CedulaAbogado.ToString();

                // Crear nuevo historial con ID real del documento creado
                var nuevoIdDocumento = _context.TDocsAutorizacionRevisionExpedientes
                    .OrderByDescending(p => p.IdDocumento)
                    .Select(p => p.IdDocumento)
                    .FirstOrDefault();

                // Guardar en el historial
                var historial = new HistorialDocumento
                {
                    Fecha = DateTime.Now,
                    TipoDocumento = "Autorización de revisión de expedientes",
                    Cliente = nombreCliente,
                    Abogado = nombreAbogado,
                    DocumentoIdOriginal = nuevoIdDocumento
                };

                _context.HistorialDocumentos.Add(historial);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                var historialDocs = await _context.HistorialDocumentos
                .OrderByDescending(h => h.Fecha)
                .ToListAsync();

                return View("~/Views/HistorialDocumentos/DocsHistorial.cshtml", historialDocs);
            }

            ViewData["CedulaAbogado"] = new SelectList(
                _context.TGeAbogados.Include(a => a.CedulaNavigation).Select(a => new
                {
                    Cedula = a.Cedula,
                    Texto = a.CedulaNavigation.Nombre + " " + a.CedulaNavigation.Apellido1 + " - " + a.Cedula
                }),
                "Cedula",
                "Texto", tDocsAutorizacionRevisionExpediente?.CedulaAbogado);

            ViewData["CedulaAsistente"] = new SelectList(
                _context.TGePersonas.Select(p => new
                {
                    Cedula = p.Cedula,
                    Texto = p.Nombre + " " + p.Apellido1 + " - " + p.Cedula
                }),
                "Cedula",
                "Texto",
                tDocsAutorizacionRevisionExpediente?.CedulaAsistente);

            ViewData["CedulaImputado"] = new SelectList(
                _context.TGePersonas.Select(p => new {
                    Cedula = p.Cedula,
                    Texto = p.Nombre + " " + p.Apellido1 + " - " + p.Cedula
                }),
                "Cedula",
                "Texto",
                tDocsAutorizacionRevisionExpediente?.CedulaImputado);

            return View(tDocsAutorizacionRevisionExpediente);
        }

        [HttpGet]
        public IActionResult PrevisualizarPDF(string expediente, string delito, string cedulaImputado, string ofendido, string cedulaAbogado, string cedulaAsistente)
        {
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DocsMachotes", "AutorizacionExpedienteMachote.html");
            var htmlTemplate = System.IO.File.ReadAllText(templatePath);

            // Reemplazar marcadores con los datos del formulario
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

        [HttpGet]
        public async Task<IActionResult> EditarDesdeHistorial(int id)
        {
            var historial = await _context.HistorialDocumentos.FindAsync(id);
            if (historial == null || historial.TipoDocumento != "Autorización de revisión de expedientes" || historial.DocumentoIdOriginal == null)
            {
                return NotFound();
            }

            // ✅ Buscar por ID directo (seguro y preciso)
            var docOriginal = await _context.TDocsAutorizacionRevisionExpedientes
                .FirstOrDefaultAsync(d => d.IdDocumento == historial.DocumentoIdOriginal);

            if (docOriginal == null)
            {
                return NotFound();
            }

            var model = new DocsAutorizacionRevisionExpedienteDTO
            {
                Expediente = docOriginal.Expediente,
                Delito = docOriginal.Delito,
                CedulaImputado = docOriginal.CedulaImputado,
                Ofendido = docOriginal.Ofendido,
                CedulaAbogado = docOriginal.CedulaAbogado,
                CedulaAsistente = docOriginal.CedulaAsistente
            };

            ViewBag.DocumentoAnteriorId = historial.Id;

            ViewData["CedulaAbogado"] = new SelectList(
                _context.TGeAbogados.Include(a => a.CedulaNavigation).Select(a => new
                {
                    Cedula = a.Cedula,
                    Texto = a.CedulaNavigation.Nombre + " " + a.CedulaNavigation.Apellido1 + " - " + a.Cedula
                }),
                "Cedula",
                "Texto", model.CedulaAbogado);

            ViewData["CedulaAsistente"] = new SelectList(
                _context.TGePersonas.Select(p => new
                {
                    Cedula = p.Cedula,
                    Texto = p.Nombre + " " + p.Apellido1 + " - " + p.Cedula
                }),
                "Cedula",
                "Texto", model.CedulaAsistente);

            ViewData["CedulaImputado"] = new SelectList(
                _context.TGePersonas.Select(p => new {
                    Cedula = p.Cedula,
                    Texto = p.Nombre + " " + p.Apellido1 + " - " + p.Cedula
                }),
                "Cedula",
                "Texto",
                model.CedulaImputado);

            return View("CreateDocsAutorizacionRevisionExpedientes", model);
        }

    }
}
