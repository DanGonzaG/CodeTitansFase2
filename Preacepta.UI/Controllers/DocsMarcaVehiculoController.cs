using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.DocsMarcaVehiculo.BuscarXid;
using Preacepta.LN.DocsMarcaVehiculo.Crear;
using Preacepta.LN.DocsMarcaVehiculo.Editar;
using Preacepta.LN.DocsMarcaVehiculo.Eliminar;
using Preacepta.LN.DocsMarcaVehiculo.Listar;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.UI.Controllers
{
    public class DocsMarcaVehiculoController : Controller
    {
        private readonly Contexto _context;
        private readonly IBuscarDocsMarcaVehiculoLN _buscar;
        private readonly ICrearDocsMarcaVehiculoLN _crear;
        private readonly IEditarDocsMarcaVehiculoLN _editar;
        private readonly IEliminarDocsMarcaVehiculoLN _eliminar;
        private readonly IListarDocsMarcaVehiculoLN _listar;

        public DocsMarcaVehiculoController(Contexto context,
            IBuscarDocsMarcaVehiculoLN buscar,
            ICrearDocsMarcaVehiculoLN crear,
            IEditarDocsMarcaVehiculoLN editar,
            IEliminarDocsMarcaVehiculoLN eliminar,
            IListarDocsMarcaVehiculoLN listar)
        {
            _context = context;
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;
        }

        // GET: DocsMarcaVehiculo
        public async Task<IActionResult> Index()
        {
            return View(await _listar.listar());
        }

        // GET: DocsMarcaVehiculo/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsMarcaVehiculo = await _buscar.buscar(id);
            if (tDocsMarcaVehiculo == null)
            {
                return NotFound();
            }

            return View(tDocsMarcaVehiculo);
        }

        // GET: DocsMarcaVehiculo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DocsMarcaVehiculo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] DocsMarcaVehiculoDTO tDocsMarcaVehiculo)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tDocsMarcaVehiculo);
                return RedirectToAction(nameof(Index));
            }
            return View(tDocsMarcaVehiculo);
        }

        // GET: DocsMarcaVehiculo/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsMarcaVehiculo = await _buscar.buscar(id);
            if (tDocsMarcaVehiculo == null)
            {
                return NotFound();
            }
            return View(tDocsMarcaVehiculo);
        }

        // POST: DocsMarcaVehiculo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] DocsMarcaVehiculoDTO tDocsMarcaVehiculo)
        {
            if (id != tDocsMarcaVehiculo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.Editar(tDocsMarcaVehiculo);
                }
                catch (DbUpdateConcurrencyException)
                {
                        return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tDocsMarcaVehiculo);
        }

        // GET: DocsMarcaVehiculo/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsMarcaVehiculo = await _buscar.buscar(id);
            if (tDocsMarcaVehiculo == null)
            {
                return NotFound();
            }

            return View(tDocsMarcaVehiculo);
        }

        // POST: DocsMarcaVehiculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eliminar.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
