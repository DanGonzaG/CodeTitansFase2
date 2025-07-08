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
                tDocsPoderesEspecialesJudiciale.Fecha = DateTime.Today.ToString("yyyy-MM-dd");

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
            ViewData["IdAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula");
            ViewData["IdCliente"] = new SelectList(_context.TGePersonas,  "Cedula", "Cedula");

            return View();
        }

        // POST: TDocsPoderesEspecialesJudiciales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDocsPoderesEspecialesJudiciales([Bind("IdDoc,Fecha,IdAbogado,IdCliente,Texto")] DocsPoderesEspecialesJudicialeDTO tDocsPoderesEspecialesJudiciale)
        {
            if (ModelState.IsValid)
            {
                tDocsPoderesEspecialesJudiciale.Fecha = DateTime.Today.ToString("yyyy-MM-dd");

                await _crear.crear(tDocsPoderesEspecialesJudiciale);

                // Obtener cliente desde TGePersonas
                var cliente = await _context.TGePersonas
                    .FirstOrDefaultAsync(p => p.Cedula == tDocsPoderesEspecialesJudiciale.IdCliente);

                string nombreCliente = cliente != null
                    ? $"{cliente.Nombre} {cliente.Apellido1} {cliente.Apellido2}"
                    : tDocsPoderesEspecialesJudiciale.IdCliente.ToString();

                // Obtener abogado desde TGeAbogados (con su persona)
                var abogado = await _context.TGeAbogados
                    .Include(a => a.CedulaNavigation)
                    .FirstOrDefaultAsync(a => a.Cedula == tDocsPoderesEspecialesJudiciale.IdAbogado);

                string nombreAbogado = abogado != null
                    ? $"{abogado.CedulaNavigation.Nombre} {abogado.CedulaNavigation.Apellido1} {abogado.CedulaNavigation.Apellido2}"
                    : tDocsPoderesEspecialesJudiciale.IdAbogado.ToString();

                // Guardar en el historial
                var historial = new HistorialDocumento
                {
                    Fecha = DateTime.Now,
                    TipoDocumento = "Poderes especiales judiciales",
                    Cliente = nombreCliente,
                    Abogado = nombreAbogado
                };

                _context.HistorialDocumentos.Add(historial);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula");
            ViewData["IdCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula");
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


    }

}
