using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.Casos.BuscarXid;
using Preacepta.LN.Casos.Crear;
using Preacepta.LN.Casos.Editar;
using Preacepta.LN.Casos.Eliminar;
using Preacepta.LN.Casos.Listar;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.UI.Controllers
{
    public class CasoController : Controller
    {
        private readonly Contexto _context;
        private readonly IBuscarCasosLN _buscar;
        private readonly ICrearCasosLN _crear;
        private readonly IEditarCasosLN _editar;
        private readonly IELiminarCasosLN _eLiminar;
        private readonly IListarCasosLN _listar;

        public CasoController(Contexto context,
            IBuscarCasosLN buscar,
            ICrearCasosLN crear,
            IEditarCasosLN editar,
            IELiminarCasosLN eLiminar,
            IListarCasosLN listar)
        {
            _context = context;
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _listar = listar;
        }

        // GET: Caso
        public async Task<IActionResult> Index()
        {
            //var contexto = _context.TCasos.Include(t => t.IdAbogadoNavigation).Include(t => t.IdClienteNavigation).Include(t => t.IdTipoCasoNavigation);
            return View(await _listar.listar());
        }

        // GET: Caso/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCaso = await _buscar.buscar(id);
            if (tCaso == null)
            {
                return NotFound();
            }

            return View(tCaso);
        }

        // GET: Caso/Create
        public IActionResult Create()
        {
            ViewData["IdAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Nombre");
            ViewData["IdCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Nombre");
            ViewData["IdTipoCaso"] = new SelectList(_context.TCasosTipos, "IdTipoCaso", "Nombre");
            return View();
        }

        // POST: Caso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCaso,Fecha,IdTipoCaso,Descripcion,IdAbogado,IdCliente,Activo")] CasoDTO tCaso)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tCaso);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Nombre", tCaso.IdAbogado);
            ViewData["IdCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Nombre", tCaso.IdCliente);
            ViewData["IdTipoCaso"] = new SelectList(_context.TCasosTipos, "IdTipoCaso", "Nombre", tCaso.IdTipoCaso);
            return View(tCaso);
        }

        // GET: Caso/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCaso = await _buscar.buscar(id);
            if (tCaso == null)
            {
                return NotFound();
            }
            ViewData["IdAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tCaso.IdAbogado);
            ViewData["IdCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tCaso.IdCliente);
            ViewData["IdTipoCaso"] = new SelectList(_context.TCasosTipos, "IdTipoCaso", "Nombre", tCaso.IdTipoCaso);
            return View(tCaso);
        }

        // POST: Caso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCaso,Fecha,IdTipoCaso,Descripcion,IdAbogado,IdCliente,Activo")] CasoDTO tCaso)
        {
            if (id != tCaso.IdCaso)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _editar.Editar(tCaso);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                    
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tCaso.IdAbogado);
            ViewData["IdCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tCaso.IdCliente);
            ViewData["IdTipoCaso"] = new SelectList(_context.TCasosTipos, "IdTipoCaso", "Nombre", tCaso.IdTipoCaso);
            return View(tCaso);
        }

        // GET: Caso/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCaso = await _buscar.buscar(id);
            if (tCaso == null)
            {
                return NotFound();
            }

            return View(tCaso);
        }

        // POST: Caso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           await _eLiminar.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
