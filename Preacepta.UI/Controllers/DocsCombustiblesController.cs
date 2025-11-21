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
using Preacepta.LN.BitacoraEventos.Crear;
using Microsoft.AspNetCore.Authorization;

namespace Preacepta.UI.Controllers
{
    [Authorize(Roles = "Gestor")]
    public class DocsCombustiblesController : Controller
    {
        
        private readonly IBuscarDocsCombustibleLN _buscar;
        private readonly ICrearDocsCombustibleLN _crear;
        private readonly IEditarDocsCombustibleLN _editar;
        private readonly IEliminarDocsCombustibleLN _eliminar;
        private readonly IListarDocsCombustibleLN _listar;
        private readonly ICrearEventosLN _bitacoraLN;

        public DocsCombustiblesController(
            IBuscarDocsCombustibleLN buscar,
            ICrearDocsCombustibleLN crear,
            IEditarDocsCombustibleLN editar,
            IEliminarDocsCombustibleLN eliminar,
            IListarDocsCombustibleLN listar,
            ICrearEventosLN bitacora)
        {
           
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;
            _bitacoraLN = bitacora;
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
                var nuevoId = await _crear.Crear(tDocsCombustible);

                // Registrar en bitácora
                var usuario = User.Identity?.Name ?? "Desconocido";
                var nombreCorto = tDocsCombustible.Nombre.Length > 50
                    ? tDocsCombustible.Nombre.Substring(0, 47) + "..."
                    : tDocsCombustible.Nombre;
                var accion = $"Se creó documento combustible '{nombreCorto}' con ID {nuevoId}";
                await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_DocsCombustible", accion, nuevoId);

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

                    // Registrar en bitácora
                    var usuario = User.Identity?.Name ?? "Desconocido";
                    var nombreCorto = tDocsCombustible.Nombre.Length > 50
                        ? tDocsCombustible.Nombre.Substring(0, 47) + "..."
                        : tDocsCombustible.Nombre;
                    var accion = $"Se editó documento combustible '{nombreCorto}' con ID {tDocsCombustible.Id}";
                    await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_DocsCombustible", accion, tDocsCombustible.Id);
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
