using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.UI.Areas
{
    [Area("DocsPagaresController.cs")]
    public class DocsPagaresController : Controller
    {
        private readonly Contexto _context;

        public DocsPagaresController(Contexto context)
        {
            _context = context;
        }

        // GET: DocsPagaresController.cs/DocsPagares
        public async Task<IActionResult> Index()
        {
            var contexto = _context.TDocsPagares.Include(t => t.CedulaDeudorNavigation).Include(t => t.CedulaFiadorNavigation).Include(t => t.LugarPagoNavigation);
            return View(await contexto.ToListAsync());
        }

        // GET: DocsPagaresController.cs/DocsPagares/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsPagare = await _context.TDocsPagares
                .Include(t => t.CedulaDeudorNavigation)
                .Include(t => t.CedulaFiadorNavigation)
                .Include(t => t.LugarPagoNavigation)
                .FirstOrDefaultAsync(m => m.IdDocumento == id);
            if (tDocsPagare == null)
            {
                return NotFound();
            }

            return View(tDocsPagare);
        }

        // GET: DocsPagaresController.cs/DocsPagares/Create
        public IActionResult Create()
        {
            ViewData["CedulaDeudor"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1");
            ViewData["CedulaFiador"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1");
            ViewData["LugarPago"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito");
            return View();
        }

        // POST: DocsPagaresController.cs/DocsPagares/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDocumento,MontoNumerico,CedulaDeudor,SociedadDeudor,CedulaJuridicaSociedad,AcreedorNombre,CedulaJuridicaAcreedor,AcreedorDomicilio,FechaFirma,HoraFirma,FechaVencimiento,InteresFormula,InteresTasaActual,InteresBase,LugarPago,CedulaFiador,UbicacionFirma")] TDocsPagare tDocsPagare)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tDocsPagare);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CedulaDeudor"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsPagare.CedulaDeudor);
            ViewData["CedulaFiador"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsPagare.CedulaFiador);
            ViewData["LugarPago"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsPagare.LugarPago);
            return View(tDocsPagare);
        }

        // GET: DocsPagaresController.cs/DocsPagares/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsPagare = await _context.TDocsPagares.FindAsync(id);
            if (tDocsPagare == null)
            {
                return NotFound();
            }
            ViewData["CedulaDeudor"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsPagare.CedulaDeudor);
            ViewData["CedulaFiador"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsPagare.CedulaFiador);
            ViewData["LugarPago"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsPagare.LugarPago);
            return View(tDocsPagare);
        }

        // POST: DocsPagaresController.cs/DocsPagares/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDocumento,MontoNumerico,CedulaDeudor,SociedadDeudor,CedulaJuridicaSociedad,AcreedorNombre,CedulaJuridicaAcreedor,AcreedorDomicilio,FechaFirma,HoraFirma,FechaVencimiento,InteresFormula,InteresTasaActual,InteresBase,LugarPago,CedulaFiador,UbicacionFirma")] TDocsPagare tDocsPagare)
        {
            if (id != tDocsPagare.IdDocumento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tDocsPagare);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TDocsPagareExists(tDocsPagare.IdDocumento))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CedulaDeudor"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsPagare.CedulaDeudor);
            ViewData["CedulaFiador"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsPagare.CedulaFiador);
            ViewData["LugarPago"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsPagare.LugarPago);
            return View(tDocsPagare);
        }

        // GET: DocsPagaresController.cs/DocsPagares/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsPagare = await _context.TDocsPagares
                .Include(t => t.CedulaDeudorNavigation)
                .Include(t => t.CedulaFiadorNavigation)
                .Include(t => t.LugarPagoNavigation)
                .FirstOrDefaultAsync(m => m.IdDocumento == id);
            if (tDocsPagare == null)
            {
                return NotFound();
            }

            return View(tDocsPagare);
        }

        // POST: DocsPagaresController.cs/DocsPagares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tDocsPagare = await _context.TDocsPagares.FindAsync(id);
            if (tDocsPagare != null)
            {
                _context.TDocsPagares.Remove(tDocsPagare);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TDocsPagareExists(int id)
        {
            return _context.TDocsPagares.Any(e => e.IdDocumento == id);
        }
    }
}
