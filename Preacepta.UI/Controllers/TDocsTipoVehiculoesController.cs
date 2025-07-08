using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.DocsTipoVehiculo.Buscar;
using Preacepta.LN.DocsTipoVehiculo.Crear;
using Preacepta.LN.DocsTipoVehiculo.Editar;
using Preacepta.LN.DocsTipoVehiculo.Eliminar;
using Preacepta.LN.DocsTipoVehiculo.Listar;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Preacepta.UI.Controllers
{
    public class TDocsTipoVehiculoesController : Controller
    {
        private readonly IConverter _converter;
        private readonly Contexto _context;
        private readonly IBuscarTipoVehiculoLN _buscar;
        private readonly ICrearTipoVehiculoLN _crear;
        private readonly IEditarTipoVehiculoLN _editar;
        private readonly IEliminarTipoVehiculoLN _eliminar;
        private readonly IListarTipoVehiculoLN _listar;

        public TDocsTipoVehiculoesController(IBuscarTipoVehiculoLN buscar,
            ICrearTipoVehiculoLN crear,
            IEditarTipoVehiculoLN editar,
            IEliminarTipoVehiculoLN eliminar,
            IListarTipoVehiculoLN listar,
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

        // GET: TDocsTipoVehiculoes
        public async Task<IActionResult> Index()
        {
            return View(await _listar.Listar());
        }

        // GET: TDocsTipoVehiculoes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsTipoVehiculo = await _buscar.buscar(id);
            if (tDocsTipoVehiculo == null)
            {
                return NotFound();
            }

            return View(tDocsTipoVehiculo);
        }

        // GET: TDocsTipoVehiculoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TDocsTipoVehiculoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] DocsTipoVehiculoDTO tDocsTipoVehiculo)
        {
            if (ModelState.IsValid)
            {
                await _crear.crear(tDocsTipoVehiculo);
                return RedirectToAction(nameof(Index));
            }
            return View(tDocsTipoVehiculo);
        }

        // GET: TDocsTipoVehiculoes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var tDocsTipoVehiculo = await _buscar.buscar(id);
            if (tDocsTipoVehiculo == null)
            {
                return NotFound();
            }
            return View(tDocsTipoVehiculo);
        }

        // POST: TDocsTipoVehiculoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] DocsTipoVehiculoDTO tDocsTipoVehiculo)
        {
            if (id != tDocsTipoVehiculo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.editar(tDocsTipoVehiculo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tDocsTipoVehiculo);
        }

        // GET: TDocsTipoVehiculoes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsTipoVehiculo = await _buscar.buscar(id);
            if (tDocsTipoVehiculo == null)
            {
                return NotFound();
            }

            return View(tDocsTipoVehiculo);
        }

        // POST: TDocsTipoVehiculoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eliminar.eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        //Meto del generation
        public IActionResult CreateGeneratorDocsTipoVehiculoes()
        {
            return View();
        }

        // POST: TDocsTipoVehiculoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGeneratorDocsTipoVehiculoes([Bind("Id,Nombre")] DocsTipoVehiculoDTO tDocsTipoVehiculo)
        {
            if (ModelState.IsValid)
            {
                await _crear.crear(tDocsTipoVehiculo);
                return RedirectToAction(nameof(Index));
            }
            return View(tDocsTipoVehiculo);
        }


        [HttpGet]
        public IActionResult PrevisualizarPDF(
        string Nombre
 )
        {
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DocsMachotes", "AutorizacionExpedienteMachote.html");
            var htmlTemplate = System.IO.File.ReadAllText(templatePath);

            htmlTemplate = htmlTemplate
                .Replace("{{Nombre}}", Nombre);

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
