using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.LN.CrDireccion1.Listar;
using Preacepta.LN.GeNegocio.BuscarXId;
using Preacepta.LN.GeNegocio.Crear;
using Preacepta.LN.GeNegocio.Editar;
using Preacepta.LN.GeNegocio.Eliminar;
using Preacepta.LN.GeNegocio.Listar;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.UI.Controllers
{
    [Authorize(Roles = "Gestor")]
    public class GeNegocioController : Controller
    {
        private readonly IBuscarNegocioLN _buscar;
        private readonly ICrearNegocioLN _crear;
        private readonly IEditarNegocioLN _editar;
        private readonly IEliminarNegocioLN _eliminar;
        private readonly IListarNegocioLN _listar;
        private readonly IListarCrDireccion1LN _listarDireccion1;



        public GeNegocioController(IBuscarNegocioLN buscar,
            ICrearNegocioLN crear,
            IEditarNegocioLN editar,
            IEliminarNegocioLN eliminar,
            IListarNegocioLN listar,
             IListarCrDireccion1LN listarDireccion1
            )
        {
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;
            _listarDireccion1 = listarDireccion1;
        }

        // GET: GeNegocio
        public async Task<IActionResult> Index()
        {
            return View(await _listar.listar());
        }

        // GET: GeNegocio/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tGeNegocio = await _buscar.buscar(id);
            if (tGeNegocio == null)
            {
                return NotFound();
            }

            return View(tGeNegocio);
        }

        // GET: GeNegocio/Create
        public IActionResult Create()
        {
            ViewData["DireccionCaton"] = new SelectList(_listarDireccion1.listarCantones().Result, "IdCanton", "NombreCanton");
            ViewData["Direccion1"] = new SelectList(_listarDireccion1.listarDistritos().Result, "IdDistrito", "NombreDistrito");
            return View();
        }

        // POST: GeNegocio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CJuridica,Nombre,Telefono,Email,Representante,FechaConsolidacion,Direccion1,Direccion2")] GeNegocioDTO tGeNegocio)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tGeNegocio);
                return RedirectToAction(nameof(Index));
            }
            ViewData["Direccion1"] = new SelectList(_listarDireccion1.listarDistritos().Result, "IdDistrito", "NombreDistrito");
            return View(tGeNegocio);
        }

        // GET: GeNegocio/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tGeNegocio = await _buscar.buscar(id);
            if (tGeNegocio == null)
            {
                return NotFound();
            }
            ViewData["Direccion1"] = new SelectList(_listarDireccion1.listarDistritos().Result, "IdDistrito", "NombreDistrito");
            return View(tGeNegocio);
        }

        // POST: GeNegocio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CJuridica,Nombre,Telefono,Email,Representante,FechaConsolidacion,Direccion1,Direccion2")] GeNegocioDTO tGeNegocio)
        {
            if (id != tGeNegocio.CJuridica)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _crear.Crear(tGeNegocio);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Direccion1"] = new SelectList(_listarDireccion1.listarDistritos().Result, "IdDistrito", "NombreDistrito");
            return View(tGeNegocio);
        }

        // GET: GeNegocio/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tGeNegocio = await _buscar.buscar(id);
            if (tGeNegocio == null)
            {
                return NotFound();
            }

            return View(tGeNegocio);
        }

        // POST: GeNegocio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eliminar.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
