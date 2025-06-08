using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.GeRedesSociales.BuscarXid;
using Preacepta.LN.GeRedesSociales.Crear;
using Preacepta.LN.GeRedesSociales.Editar;
using Preacepta.LN.GeRedesSociales.Eliminar;
using Preacepta.LN.GeRedesSociales.Listar;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.UI.Controllers
{
    [Authorize(Roles = "Gestor")]
    public class GeRedesSocialesController : Controller
    {
        private readonly Contexto _context;
        private readonly IBuscarRedesSocialesLN _buscar;
        private readonly ICrearRedesSocialesLN _crear;
        private readonly IEditarRedesSocialesLN _editar;
        private readonly IEliminarRedesSocialesLN _elminar;
        private readonly IListarRedesSocialesLN _listar;

        public GeRedesSocialesController(Contexto context,
            IBuscarRedesSocialesLN buscar,
            ICrearRedesSocialesLN crear,
            IEditarRedesSocialesLN editar,
            IEliminarRedesSocialesLN eliminar,
            IListarRedesSocialesLN listar)
        {
            _context = context;
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _elminar = eliminar;
            _listar = listar;
        }

        // GET: GeRedesSociales
        public async Task<IActionResult> Index()
        {
            //var contexto = _context.TGeRedesSociales.Include(t => t.CedulaNavigation);
            return View(await _listar.listar());
        }

        // GET: GeRedesSociales/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tGeRedesSociale = await _buscar.buscar(id);
            if (tGeRedesSociale == null)
            {
                return NotFound();
            }

            return View(tGeRedesSociale);
        }

        // GET: GeRedesSociales/Create
        public IActionResult Create()
        {
            ViewData["Cedula"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula");
            return View();
        }

        // POST: GeRedesSociales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRs,Cedula,LinkRedSocila")] GeRedesSocialeDTO tGeRedesSociale)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tGeRedesSociale);
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cedula"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tGeRedesSociale.Cedula);
            return View(tGeRedesSociale);
        }

        // GET: GeRedesSociales/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tGeRedesSociale = await _buscar.buscar(id);
            if (tGeRedesSociale == null)
            {
                return NotFound();
            }
            ViewData["Cedula"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tGeRedesSociale.Cedula);
            return View(tGeRedesSociale);
        }

        // POST: GeRedesSociales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRs,Cedula,LinkRedSocila")] GeRedesSocialeDTO tGeRedesSociale)
        {
            if (id != tGeRedesSociale.IdRs)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.Editar(tGeRedesSociale);
                }
                catch (DbUpdateConcurrencyException)
                {

                    return NotFound();

                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cedula"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tGeRedesSociale.Cedula);
            return View(tGeRedesSociale);
        }

        // GET: GeRedesSociales/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tGeRedesSociale = await _buscar.buscar(id);
            if (tGeRedesSociale == null)
            {
                return NotFound();
            }

            return View(tGeRedesSociale);
        }

        // POST: GeRedesSociales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _elminar.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
