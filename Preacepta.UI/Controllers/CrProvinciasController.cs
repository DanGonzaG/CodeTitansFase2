using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.CrDireccion1.BuscarXid;
using Preacepta.LN.CrDireccion1.Crear;
using Preacepta.LN.CrDireccion1.Editar;
using Preacepta.LN.CrDireccion1.Eliminar;
using Preacepta.LN.CrDireccion1.Listar;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.UI.Controllers
{
    [Authorize(Roles = "Gestor")]
    public class CrProvinciasController : Controller
    {
        private readonly IBuscarCrDireccion1LN _buscar;
        private readonly ICrearCrDireccion1LN _crear;
        private readonly IEditarCrDireccion1LN _editar;
        private readonly IEliminarCrDireccion1LN _eliminar;
        private readonly IListarCrDireccion1LN _listar;


        public CrProvinciasController(Contexto context,
            IBuscarCrDireccion1LN buscar,
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

        // GET: CrProvincias
        public async Task<IActionResult> Index()
        {
            return View(await _listar.listarProvincias());
        }

        // GET: CrProvincias/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCrProvincia = await _buscar.buscarProvincia(id);
            if (tCrProvincia == null)
            {
                return NotFound();
            }

            return View(tCrProvincia);
        }

        // GET: CrProvincias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CrProvincias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProvincia,NombreProvincia")] CrProvinciaDTO tCrProvincia)
        {
            if (ModelState.IsValid)
            {
                await _crear.CrearProvincia(tCrProvincia);
                return RedirectToAction(nameof(Index));
            }
            return View(tCrProvincia);
        }

        // GET: CrProvincias/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCrProvincia = await _buscar.buscarProvincia(id);
            if (tCrProvincia == null)
            {
                return NotFound();
            }
            return View(tCrProvincia);
        }

        // POST: CrProvincias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProvincia,NombreProvincia")] CrProvinciaDTO tCrProvincia)
        {
            if (id != tCrProvincia.IdProvincia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.EditarProvincia(tCrProvincia);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tCrProvincia);
        }

        // GET: CrProvincias/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCrProvincia = await _buscar.buscarProvincia(id);
            if (tCrProvincia == null)
            {
                return NotFound();
            }

            return View(tCrProvincia);
        }

        // POST: CrProvincias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eliminar.EliminarProvincia(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
