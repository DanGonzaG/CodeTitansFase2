using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.DocPoderesEspecialesJudiciales.Buscar;
using Preacepta.LN.DocPoderesEspecialesJudiciales.Crear;
using Preacepta.LN.DocPoderesEspecialesJudiciales.Editar;
using Preacepta.LN.DocPoderesEspecialesJudiciales.Eliminar;
using Preacepta.LN.DocPoderesEspecialesJudiciales.Listar;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Preacepta.UI.Controllers
{
    public class TDocsPoderesEspecialesJudicialesController : Controller
    {
        private readonly IConverter _converter;
        private readonly Contexto _context;
        private readonly IBuscarPoderJudLN _buscar;
        private readonly ICrearPoderJudLN _crear;
        private readonly IEditarPoderJudLN _editar;
        private readonly IEliminarPoderJudLN _eliminar;
        private readonly IListarPoderJudLN _listar;

        public TDocsPoderesEspecialesJudicialesController(IBuscarPoderJudLN buscar,
            ICrearPoderJudLN crear,
            IEditarPoderJudLN editar,
            IEliminarPoderJudLN eliminar,
            IListarPoderJudLN listar,
            IConverter converter,
            Contexto context)
        {
            _converter = converter;
            _context = context;
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;
        }

        // GET: TDocsPoderesEspecialesJudiciales
        public async Task<IActionResult> Index()
        {
            return View(await _listar.Listar());
        }

        // GET: TDocsPoderesEspecialesJudiciales/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poderJud = await _buscar.buscar(id);
            if (poderJud == null)
            {
                return NotFound();
            }

            return View(poderJud);
        }

        // GET: TDocsPoderesEspecialesJudiciales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TDocsPoderesEspecialesJudiciales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDoc,Fecha,IdAbogado,IdCliente,Texto")] DocsPoderesEspecialesJudicialeDTO tDocsPoderesEspecialesJudiciale)
        {
            if (ModelState.IsValid)
            {
                /*tDocsPoderesEspecialesJudiciale.Fecha = DateTime.Today.ToString("yyyy-MM-dd");*/

                await _crear.crear(tDocsPoderesEspecialesJudiciale);
                return RedirectToAction(nameof(Index));
            }
            return View(tDocsPoderesEspecialesJudiciale);
        }

        // GET: TDocsPoderesEspecialesJudiciales/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsPoderesEspecialesJudiciale = await _buscar.buscar(id);
            if (tDocsPoderesEspecialesJudiciale == null)
            {
                return NotFound();
            }
            return View(tDocsPoderesEspecialesJudiciale);
        }

        // POST: TDocsPoderesEspecialesJudiciales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDoc,Fecha,IdAbogado,IdCliente,Texto")] DocsPoderesEspecialesJudicialeDTO tDocsPoderesEspecialesJudiciale)
        {
            if (id != tDocsPoderesEspecialesJudiciale.IdDoc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.editar(tDocsPoderesEspecialesJudiciale);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tDocsPoderesEspecialesJudiciale);
        }

        // GET: TDocsPoderesEspecialesJudiciales/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsPoderesEspecialesJudiciale = await _buscar.buscar(id);
            if (tDocsPoderesEspecialesJudiciale == null)
            {
                return NotFound();
            }

            return View(tDocsPoderesEspecialesJudiciale);
        }

            // POST: TDocsPoderesEspecialesJudiciales/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                await _eliminar.eliminar(id);
                return RedirectToAction(nameof(Index));
            }


        //Mis metodos estan de aquí en adelante
        public IActionResult CreateDocsPoderesEspecialesJudiciales()
        {
            ViewData["IdAbogado"] = new SelectList(_context.TGeAbogados.Include(a => a.CedulaNavigation).Select(a => new
                {
                    Cedula = a.Cedula,
                    Texto = a.CedulaNavigation.Nombre + " " + a.CedulaNavigation.Apellido1 + " - " + a.Cedula
                }),
                "Cedula",
                "Texto");

            ViewData["IdCliente"] = new SelectList(_context.TGePersonas.Select(p => new
            {
                Cedula = p.Cedula,
                apellido = p.Apellido1,
                Texto = p.Nombre + " " + p.Apellido1 + " - " + p.Cedula
            }),
            "Cedula", "Texto");

            return View();
        }

        // POST: TDocsPoderesEspecialesJudiciales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDocsPoderesEspecialesJudiciales(
    [Bind("IdDoc,Fecha,IdAbogado,IdCliente,Texto")] DocsPoderesEspecialesJudicialeDTO tDocsPoderesEspecialesJudiciale,
    [FromForm] int? DocumentoAnteriorId)
        {
            if (ModelState.IsValid)
            {
                /*tDocsPoderesEspecialesJudiciale.Fecha = DateTime.Today.ToString("yyyy-MM-dd");*/

                // Crear el nuevo documento
                await _crear.crear(tDocsPoderesEspecialesJudiciale);

                // 🔍 Obtener el Id del documento recién creado
                var nuevoIdDoc = _context.TDocsPoderesEspecialesJudiciales
                    .OrderByDescending(x => x.IdDoc)
                    .Select(x => x.IdDoc)
                    .FirstOrDefault();

                // Obtener cliente
                var cliente = await _context.TGePersonas
                    .FirstOrDefaultAsync(p => p.Cedula == tDocsPoderesEspecialesJudiciale.IdCliente);
                string nombreCliente = cliente != null
                    ? $"{cliente.Nombre} {cliente.Apellido1} {cliente.Apellido2}"
                    : tDocsPoderesEspecialesJudiciale.IdCliente.ToString();

                // Obtener abogado
                var abogado = await _context.TGeAbogados
                    .Include(a => a.CedulaNavigation)
                    .FirstOrDefaultAsync(a => a.Cedula == tDocsPoderesEspecialesJudiciale.IdAbogado);
                string nombreAbogado = abogado != null
                    ? $"{abogado.CedulaNavigation.Nombre} {abogado.CedulaNavigation.Apellido1} {abogado.CedulaNavigation.Apellido2}"
                    : tDocsPoderesEspecialesJudiciale.IdAbogado.ToString();

                // Guardar en historial usando el ID real
                var historial = new HistorialDocumento
                {
                    Fecha = DateTime.Parse(tDocsPoderesEspecialesJudiciale.Fecha),
                    TipoDocumento = "Poderes especiales judiciales",
                    Cliente = nombreCliente,
                    Abogado = nombreAbogado,
                    DocumentoIdOriginal = nuevoIdDoc
                };

                _context.HistorialDocumentos.Add(historial);

                // Eliminar documento anterior y su historial si aplica
                if (DocumentoAnteriorId.HasValue)
                {
                    var docAnterior = await _context.TDocsPoderesEspecialesJudiciales
                        .FirstOrDefaultAsync(d => d.IdDoc == DocumentoAnteriorId.Value);
                    if (docAnterior != null)
                        _context.TDocsPoderesEspecialesJudiciales.Remove(docAnterior);

                    var historialAnterior = await _context.HistorialDocumentos
                        .FirstOrDefaultAsync(h => h.Id == DocumentoAnteriorId.Value);
                    if (historialAnterior != null)
                        _context.HistorialDocumentos.Remove(historialAnterior);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("DocsHistorial", "HistorialDocumentos");
            }

            ViewData["IdAbogado"] = new SelectList(_context.TGeAbogados.Include(a => a.CedulaNavigation).Select(a => new
                {
                    Cedula = a.Cedula,
                    Texto = a.CedulaNavigation.Nombre + " " + a.CedulaNavigation.Apellido1 + " - " + a.Cedula
                }),
                "Cedula",
                "Texto", tDocsPoderesEspecialesJudiciale?.IdAbogado);

            ViewData["IdCliente"] = new SelectList(_context.TGePersonas.Select(p => new
            {
                Cedula = p.Cedula,
                apellido = p.Apellido1,
                Texto = p.Nombre + " " + p.Apellido1 + " - " + p.Cedula
            }),
            "Cedula", "Texto", tDocsPoderesEspecialesJudiciale?.IdCliente);

            return View(tDocsPoderesEspecialesJudiciale);
        }



        [HttpGet]
        public IActionResult PrevisualizarPDF(
        string idDoc,
        string fecha,
        string idAbogado,
        string idCliente,
        string texto
 )
        {
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DocsMachotes", "PoderesEspecialesJudiciales.html");
            var htmlTemplate = System.IO.File.ReadAllText(templatePath);

            htmlTemplate = htmlTemplate
                .Replace("{{ID_DOC}}", idDoc)
                .Replace("{{FECHA}}", fecha)
                .Replace("{{ID_ABOGADO}}", idAbogado)
                .Replace("{{ID_CLIENTE}}", idCliente)
                .Replace("{{TEXTO}}", texto);

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
            if (historial == null || historial.TipoDocumento != "Poderes especiales judiciales" || historial.DocumentoIdOriginal == null)
            {
                return NotFound();
            }

            var docOriginal = await _context.TDocsPoderesEspecialesJudiciales
                .FirstOrDefaultAsync(d => d.IdDoc == historial.DocumentoIdOriginal);

            if (docOriginal == null)
            {
                return NotFound();
            }

            var model = new DocsPoderesEspecialesJudicialeDTO
            {
                IdDoc = docOriginal.IdDoc,
                Fecha = docOriginal.Fecha.ToString("yyyy-MM-dd"),
                IdAbogado = docOriginal.IdAbogado,
                IdCliente = docOriginal.IdCliente,
                Texto = docOriginal.Texto
            };

            ViewBag.DocumentoAnteriorId = historial.Id;

            ViewData["IdAbogado"] = new SelectList(_context.TGeAbogados.Include(a => a.CedulaNavigation).Select(a => new
            {
                Cedula = a.Cedula,
                Texto = a.CedulaNavigation.Nombre + " " + a.CedulaNavigation.Apellido1 + " - " + a.Cedula
            }),
                "Cedula",
                "Texto", model?.IdAbogado);

            ViewData["IdCliente"] = new SelectList(_context.TGePersonas.Select(p => new
            {
                Cedula = p.Cedula,
                apellido = p.Apellido1,
                Texto = p.Nombre + " " + p.Apellido1 + " - " + p.Cedula
            }),
            "Cedula", "Texto", model?.IdCliente);

            return View("CreateDocsPoderesEspecialesJudiciales", model);
        }



    }

}
