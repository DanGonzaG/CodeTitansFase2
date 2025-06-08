using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.CasosEvidencia.BuscarXid;
using Preacepta.LN.CasosEvidencia.Crear;
using Preacepta.LN.CasosEvidencia.Editar;
using Preacepta.LN.CasosEvidencia.Eliminar;
using Preacepta.LN.CasosEvidencia.Listar;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.UI.Controllers
{
    [Authorize(Roles = "Gestor")]
    public class CasosEvidenciaController : Controller
    {
        private readonly Contexto _context;
        private readonly IBuscarCasosEvidenciaLN _buscar;
        private readonly ICrearCasosEvidenciaLN _crear;
        private readonly IEditarCasosEvidenciaLN _editar;
        private readonly IEliminarCasosEvidenciaLN _eliminar;
        private readonly IListarCasosEvidenciaLN _listar;

        public CasosEvidenciaController(Contexto context,
            IBuscarCasosEvidenciaLN buscar,
            ICrearCasosEvidenciaLN crear,
            IEditarCasosEvidenciaLN editar,
            IEliminarCasosEvidenciaLN eliminar,
            IListarCasosEvidenciaLN listar)
        {
            _context = context;
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;
        }

        // GET: CasosEvidencia
        public async Task<IActionResult> Index()
        {
            //var contexto = _context.TCasosEvidencias.Include(t => t.IdCaso1).Include(t => t.IdCasoNavigation);
            return View(await _listar.listar());
        }

        // GET: CasosEvidencia/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCasosEvidencia = await _buscar.buscar(id);
            if (tCasosEvidencia == null)
            {
                return NotFound();
            }

            return View(tCasosEvidencia);
        }

        // GET: CasosEvidencia/Create
        public IActionResult Create()
        {
            ViewData["IdCaso"] = new SelectList(_context.TCasosEtapas, "IdEtapaPl", "Descripcion");
            ViewData["IdCaso"] = new SelectList(_context.TCasos, "IdCaso", "Descripcion");
            return View();
        }

        // POST: CasosEvidencia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEvidencia,Titulo,IdCaso,Archivo")] CasosEvidenciaDTO tCasosEvidencia)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tCasosEvidencia);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCaso"] = new SelectList(_context.TCasosEtapas, "IdEtapaPl", "Descripcion", tCasosEvidencia.IdCaso);
            ViewData["IdCaso"] = new SelectList(_context.TCasos, "IdCaso", "Descripcion", tCasosEvidencia.IdCaso);
            return View(tCasosEvidencia);
        }

        // GET: CasosEvidencia/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCasosEvidencia = await _buscar.buscar(id);
            if (tCasosEvidencia == null)
            {
                return NotFound();
            }
            ViewData["IdCaso"] = new SelectList(_context.TCasosEtapas, "IdEtapaPl", "Descripcion", tCasosEvidencia.IdCaso);
            ViewData["IdCaso"] = new SelectList(_context.TCasos, "IdCaso", "Descripcion", tCasosEvidencia.IdCaso);
            return View(tCasosEvidencia);
        }

        // POST: CasosEvidencia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEvidencia,Titulo,IdCaso,Archivo")] CasosEvidenciaDTO tCasosEvidencia)
        {
            if (id != tCasosEvidencia.IdEvidencia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.Editar(tCasosEvidencia);
                }
                catch (DbUpdateConcurrencyException)
                {

                    return NotFound();

                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCaso"] = new SelectList(_context.TCasosEtapas, "IdEtapaPl", "Descripcion", tCasosEvidencia.IdCaso);
            ViewData["IdCaso"] = new SelectList(_context.TCasos, "IdCaso", "Descripcion", tCasosEvidencia.IdCaso);
            return View(tCasosEvidencia);
        }

        // GET: CasosEvidencia/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCasosEvidencia = await _buscar.buscar(id);
            if (tCasosEvidencia == null)
            {
                return NotFound();
            }

            return View(tCasosEvidencia);
        }

        // POST: CasosEvidencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eliminar.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
