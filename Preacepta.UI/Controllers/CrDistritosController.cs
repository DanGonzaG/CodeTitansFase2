using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.CrDireccion1.BuscarXid;
using Preacepta.LN.CrDireccion1.Crear;
using Preacepta.LN.CrDireccion1.Editar;
using Preacepta.LN.CrDireccion1.Eliminar;
using Preacepta.LN.CrDireccion1.Listar;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.UI.Controllers
{
    public class CrDistritosController : Controller
    {
        private readonly IBuscarCrDireccion1LN _buscar;
        private readonly ICrearCrDireccion1LN _crear;
        private readonly IEditarCrDireccion1LN _editar;
        private readonly IEliminarCrDireccion1LN _eliminar;
        private readonly IListarCrDireccion1LN _listar;

        public CrDistritosController(IBuscarCrDireccion1LN buscar,
            ICrearCrDireccion1LN crear,
            IEditarCrDireccion1LN editar,
            IEliminarCrDireccion1LN eliminar,
            IListarCrDireccion1LN listar)
        {
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;
        }

        // GET: CrDistritos
        public async Task<IActionResult> Index()
        {
            //var contexto = _context.TCrDistritos.Include(t => t.IdCatonNavigation);
            return View(await _listar.listarDistritos());
        }

        // GET: CrDistritos/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCrDistrito = await /*_context.TCrDistritos
                .Include(t => t.IdCatonNavigation)
                .FirstOrDefaultAsync(m => m.IdDistrito == id)*/
                _buscar.buscarDistrito(id);
            if (tCrDistrito == null)
            {
                return NotFound();
            }

            return View(tCrDistrito);
        }

        // GET: CrDistritos/Create
        public IActionResult Create()
        {
            ViewData["IdCaton"] = new SelectList(_listar.listarCantones().Result, "IdCanton", "NombreCanton");
            return View();
        }

        // POST: CrDistritos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDistrito,IdCaton,NombreDistrito")] CrDistritoDTO tCrDistrito)
        {
            if (ModelState.IsValid)
            {
                await _crear.CrearDistrito(tCrDistrito);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCaton"] = new SelectList(_listar.listarCantones().Result, "IdCanton", "NombreCanton", tCrDistrito.IdCaton);
            return View(tCrDistrito);
        }

        // GET: CrDistritos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCrDistrito = await _buscar.buscarDistrito(id);
            if (tCrDistrito == null)
            {
                return NotFound();
            }
            ViewData["IdCaton"] = new SelectList(_listar.listarCantones().Result, "IdCanton", "NombreCanton", tCrDistrito.IdCaton);
            return View(tCrDistrito);
        }

        // POST: CrDistritos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDistrito,IdCaton,NombreDistrito")] CrDistritoDTO tCrDistrito)
        {
            if (id != tCrDistrito.IdDistrito)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.EditarDistrito(tCrDistrito);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCaton"] = new SelectList(_listar.listarCantones().Result, "IdCanton", "NombreCanton", tCrDistrito.IdCaton);
            return View(tCrDistrito);
        }

        // GET: CrDistritos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCrDistrito = await _buscar.buscarDistrito(id);
            if (tCrDistrito == null)
            {
                return NotFound();
            }

            return View(tCrDistrito);
        }

        // POST: CrDistritos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eliminar.EliminarDistrito(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
