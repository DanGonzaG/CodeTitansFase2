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
    public class CrCantonesController : Controller
    {
        
        private readonly IBuscarCrDireccion1LN _buscar;
        private readonly ICrearCrDireccion1LN _crear;
        private readonly IEditarCrDireccion1LN _editar;
        private readonly IEliminarCrDireccion1LN _eliminar;
        private readonly IListarCrDireccion1LN _listar;

        public CrCantonesController(IBuscarCrDireccion1LN buscar,
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

        // GET: CrCantones
        public async Task<IActionResult> Index()
        {
            return View(await _listar.listarCantones());
        }

        // GET: CrCantones/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCrCantone = await _buscar.buscarCanton(id);
            if (tCrCantone == null)
            {
                return NotFound();
            }

            return View(tCrCantone);
        }

        // GET: CrCantones/Create
        public IActionResult Create()
        {
            ViewData["IdProvincia"] = new SelectList(_listar.listarProvincias().Result, "IdProvincia", "NombreProvincia");
            return View();
        }

        // POST: CrCantones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCanton,IdProvincia,NombreCanton,IdProvinciaNavigation")] CrCantonDTO tCrCantone)
        {
            if (ModelState.IsValid)
            {
                await _crear.CrearCanton(tCrCantone);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProvincia"] = new SelectList(_listar.listarProvincias().Result, "IdProvincia", "NombreProvincia", tCrCantone.IdProvincia);
            return View(tCrCantone);
        }

        // GET: CrCantones/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCrCantone = await _buscar.buscarCanton(id);
            if (tCrCantone == null)
            {
                return NotFound();
            }
            ViewData["IdProvincia"] = new SelectList(_listar.listarProvincias().Result, "IdProvincia", "NombreProvincia", tCrCantone.IdProvincia);
            return View(tCrCantone);
        }

        // POST: CrCantones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCanton,IdProvincia,NombreCanton")] CrCantonDTO tCrCantone)
        {
            if (id != tCrCantone.IdCanton)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.EditarCanton(tCrCantone);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();                    
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProvincia"] = new SelectList(_listar.listarProvincias().Result, "IdProvincia", "NombreProvincia", tCrCantone.IdProvincia);
            return View(tCrCantone);
        }

        // GET: CrCantones/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCrCantone = await /*_context.TCrCantones
                .Include(t => t.IdProvinciaNavigation)
                .FirstOrDefaultAsync(m => m.IdCanton == id)*/
                _buscar.buscarCanton(id);
            if (tCrCantone == null)
            {
                return NotFound();
            }

            return View(tCrCantone);
        }

        // POST: CrCantones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {          
            await _eliminar.EliminarCanton(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
