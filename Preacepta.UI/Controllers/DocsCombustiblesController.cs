using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.DocsCombustible.BuscarXid;
using Preacepta.LN.DocsCombustible.Crear;
using Preacepta.LN.DocsCombustible.Editar;
using Preacepta.LN.DocsCombustible.Eliminar;
using Preacepta.LN.DocsCombustible.Listar;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Preacepta.UI.Controllers
{
    public class DocsCombustiblesController : Controller
    {
        private readonly Contexto _context;
        private readonly IBuscarDocsCombustibleLN _buscar;
        private readonly ICrearDocsCombustibleLN _crear;
        private readonly IEditarDocsCombustibleLN _editar;
        private readonly IEliminarDocsCombustibleLN _eliminar;
        private readonly IListarDocsCombustibleLN _listar;

        public DocsCombustiblesController(Contexto context,
            IBuscarDocsCombustibleLN buscar,
            ICrearDocsCombustibleLN crear,
            IEditarDocsCombustibleLN editar,
            IEliminarDocsCombustibleLN eliminar,
            IListarDocsCombustibleLN listar)
        {
            _context = context;
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;
        }

        // GET: DocsCombustibles
        public async Task<IActionResult> Index()
        {
            return View(await _listar.listar());
        }

        // GET: DocsCombustibles/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsCombustible = await _buscar.buscar(id);
            if (tDocsCombustible == null)
            {
                return NotFound();
            }

            return View(tDocsCombustible);
        }

        // GET: DocsCombustibles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DocsCombustibles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] DocsCombustibleDTO tDocsCombustible)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tDocsCombustible);
                return RedirectToAction(nameof(Index));
            }
            return View(tDocsCombustible);
        }

        // GET: DocsCombustibles/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsCombustible = await _buscar.buscar(id);
            if (tDocsCombustible == null)
            {
                return NotFound();
            }
            return View(tDocsCombustible);
        }

        // POST: DocsCombustibles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] DocsCombustibleDTO tDocsCombustible)
        {
            if (id != tDocsCombustible.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.Editar(tDocsCombustible);
                }
                catch (DbUpdateConcurrencyException)
                {
                        return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tDocsCombustible);
        }

        // GET: DocsCombustibles/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsCombustible = await _buscar.buscar(id);
            if (tDocsCombustible == null)
            {
                return NotFound();
            }

            return View(tDocsCombustible);
        }

        // POST: DocsCombustibles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eliminar.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
