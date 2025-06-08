using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.Casos.BuscarXid;
using Preacepta.LN.CasosEtapa.BuscarXid;
using Preacepta.LN.CasosEtapa.Crear;
using Preacepta.LN.CasosEtapa.Editar;
using Preacepta.LN.CasosEtapa.Eliminar;
using Preacepta.LN.CasosEtapa.Listar;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.UI.Controllers
{
    [Authorize(Roles = "Gestor")]
    public class CasosEtapaController : Controller
    {

        private readonly Contexto _context;
        private readonly IBuscarCasosEtapasLN _buscar;
        private readonly ICrearCasosEtapasLN _crear;
        private readonly IEditarCasosEtapasLN _editar;
        private readonly IEliminarCasosEtapasLN _eliminar;
        private readonly IListarCasosEtapasLN _listar;
        private readonly IBuscarCasosLN _buscarCaso;

        public CasosEtapaController(Contexto context,
            IBuscarCasosEtapasLN buscar,
            ICrearCasosEtapasLN crear,
            IEditarCasosEtapasLN editar,
            IEliminarCasosEtapasLN eliminar,
            IListarCasosEtapasLN listar,
            IBuscarCasosLN buscarCaso)
        {
            _context = context;
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;
            _buscarCaso = buscarCaso;
        }

        // GET: CasosEtapa
        public async Task<IActionResult> Index()
        {
            //var contexto = _context.TCasosEtapas.Include(t => t.IdCasoNavigation);
            return View(await _listar.listar());
        }

        // GET: CasosEtapa/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCasosEtapa = await _buscar.buscar(id);
            if (tCasosEtapa == null)
            {
                return NotFound();
            }

            return View(tCasosEtapa);
        }

        // GET: CasosEtapa/Create
        public IActionResult Create()
        {
            ViewData["IdCaso"] = new SelectList(_context.TCasos, "IdCaso", "Nombre");
            return View();
        }

        // POST: CasosEtapa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEtapaPl,Fecha,Nombre,Descripcion,IdCaso,Activo")] CasosEtapaDTO tCasosEtapa)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tCasosEtapa);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCaso"] = new SelectList(_context.TCasos, "IdCaso", "Nombre", tCasosEtapa.IdCaso);
            return View(tCasosEtapa);
        }

        // GET: CasosEtapa/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCasosEtapa = await _buscar.buscar(id);
            if (tCasosEtapa == null)
            {
                return NotFound();
            }
            ViewData["IdCaso"] = new SelectList(_context.TCasos, "IdCaso", "Nombre", tCasosEtapa.IdCaso);
            return View(tCasosEtapa);
        }

        // POST: CasosEtapa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEtapaPl,Fecha,Nombre,Descripcion,IdCaso,Activo")] CasosEtapaDTO tCasosEtapa)
        {
            if (id != tCasosEtapa.IdEtapaPl)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.Editar(tCasosEtapa);
                }
                catch (DbUpdateConcurrencyException)
                {

                    return NotFound();

                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCaso"] = new SelectList(_context.TCasos, "IdCaso", "Nombre", tCasosEtapa.IdCaso);
            return View(tCasosEtapa);
        }

        // GET: CasosEtapa/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCasosEtapa = await _buscar.buscar(id);
            if (tCasosEtapa == null)
            {
                return NotFound();
            }

            return View(tCasosEtapa);
        }

        // POST: CasosEtapa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eliminar.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EtapasPL(int id)
        {
            //var contexto = _context.TCasosEtapas.Include(t => t.IdCasoNavigation);
            //return View(await _listar.listarXcaso(id));

            var Caso = await _buscarCaso.buscar(id);

            var EtapaCaso = await _listar.listarXcaso(id);

            var viewModel = new CasoUnionEtapasPL
            {
                casoDTO = Caso,
                listarCasoEtapas = EtapaCaso
            };
            return View(viewModel);
        }

        // GET: CasosEtapa/Create
        public async Task<IActionResult> FormularioEtapaPL(int IdCaso)
        {
            var caso = await _buscarCaso.buscar(IdCaso);
            ViewBag.IdCaso = caso.IdCaso;
            ViewBag.NombreCaso = caso.Nombre;
            //ViewData["IdCaso"] = new SelectList(_context.TCasos, "IdCaso", "Nombre");
            //ViewData["IdCaso"] = new SelectList(_buscarCaso.buscar(IdCaso).Result, "IdCaso", "Nombre");
            //ViewData["IdCaso"] = IdCaso;
            return View();
        }

        // POST: CasosEtapa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FormularioEtapaPL([Bind("IdEtapaPl,Fecha,Nombre,Descripcion,IdCaso,Activo")] CasosEtapaDTO tCasosEtapa, int IdCaso)
        {

            if (ModelState.IsValid)
            {
                await _crear.Crear(tCasosEtapa);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCaso"] = new SelectList(_context.TCasos, "IdCaso", "Nombre", tCasosEtapa.IdCaso);
            return View(tCasosEtapa);
        }
    }
}
