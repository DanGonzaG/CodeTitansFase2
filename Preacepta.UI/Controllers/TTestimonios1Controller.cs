using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.UI.Controllers
{
    public class TTestimonios1Controller : Controller
    {
        private readonly Contexto _context;

        public TTestimonios1Controller(Contexto context)
        {
            _context = context;
        }

        // GET: TTestimonios1
        public async Task<IActionResult> Index()
        {
            var contexto = _context.TTestimonios.Include(t => t.IdClienteNavigation);
            return View(await contexto.ToListAsync());
        }

        // GET: TTestimonios1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tTestimonio = await _context.TTestimonios
                .Include(t => t.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdTestimonio == id);
            if (tTestimonio == null)
            {
                return NotFound();
            }

            return View(tTestimonio);
        }

        // GET: TTestimonios1/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1");
            return View();
        }

        // POST: TTestimonios1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTestimonio,Fecha,IdCliente,Comentario,Evaluacion,Activo")] TTestimonio tTestimonio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tTestimonio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tTestimonio.IdCliente);
            return View(tTestimonio);
        }

        // GET: TTestimonios1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tTestimonio = await _context.TTestimonios.FindAsync(id);
            if (tTestimonio == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tTestimonio.IdCliente);
            return View(tTestimonio);
        }

        // POST: TTestimonios1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTestimonio,Fecha,IdCliente,Comentario,Evaluacion,Activo")] TTestimonio tTestimonio)
        {
            if (id != tTestimonio.IdTestimonio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tTestimonio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TTestimonioExists(tTestimonio.IdTestimonio))
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
            ViewData["IdCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tTestimonio.IdCliente);
            return View(tTestimonio);
        }

        // GET: TTestimonios1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tTestimonio = await _context.TTestimonios
                .Include(t => t.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdTestimonio == id);
            if (tTestimonio == null)
            {
                return NotFound();
            }

            return View(tTestimonio);
        }

        // POST: TTestimonios1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tTestimonio = await _context.TTestimonios.FindAsync(id);
            if (tTestimonio != null)
            {
                _context.TTestimonios.Remove(tTestimonio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TTestimonioExists(int id)
        {
            return _context.TTestimonios.Any(e => e.IdTestimonio == id);
        }
    }
}
