using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.AD.GePersona.Listar;
using Preacepta.LN.GePersona.Listar;
using Preacepta.LN.Testimonios.Buscar;
using Preacepta.LN.Testimonios.Crear;
using Preacepta.LN.Testimonios.Editar;
using Preacepta.LN.Testimonios.Eliminar;
using Preacepta.LN.Testimonios.Listar;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Preacepta.UI.Controllers
{
    public class TTestimoniosController : Controller
    {
        private readonly IBuscarTestLN _buscar;
        private readonly ICrearTestLN _crear;
        private readonly IEditarTestLN _editar;
        private readonly IEliminarTestLN _eliminar;
        private readonly IListarTestLN _listar;
        private readonly IListarGePersonaLN _listarGePersona;

        public TTestimoniosController(IBuscarTestLN buscar,
            ICrearTestLN crear,
            IEditarTestLN editar,
            IEliminarTestLN eliminar,
            IListarTestLN listar,
            IListarGePersonaLN listarGePersona)
        {
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;
            _listarGePersona = listarGePersona;
        }

        // GET: TTestimonios1
        public async Task<IActionResult> Index()
        {
            //var contexto = _context.TTestimonios.Include(t => t.IdClienteNavigation);
            return View(await _listar.Listar());
        }

        // GET: TTestimonios1/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

                    var Test = await _buscar.buscar(id);
                    if (Test == null)
                    {
                        return NotFound();
                    }

                    return View(Test);
        }

        // GET: TTestimonios1/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_listarGePersona.listar().Result, "Cedula", "Apellido1");
            return View();
        }

        // POST: TTestimonios1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTestimonio,Fecha,IdCliente,Comentario,Evaluacion,Activo")] TTestimonioDTO tTestimonio)
        {
            if (ModelState.IsValid)
            {
                      await _crear.crear(tTestimonio);
                      return RedirectToAction(nameof(Index));
                  }
            ViewData["IdCliente"] = new SelectList(_listarGePersona.listar().Result, "Cedula", "Apellido1");
            return View(tTestimonio);;
        }

        // GET: TTestimonios1/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

                    var tTestimonio = await _buscar.buscar(id);
                    if (tTestimonio == null)
                    {
                        return NotFound();
                    }
            ViewData["IdCliente"] = new SelectList(_listarGePersona.listar().Result, "Cedula", "Apellido1");
            return View(tTestimonio);
        }

        // POST: TTestimonios1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTestimonio,Fecha,IdCliente,Comentario,Evaluacion,Activo")] TTestimonioDTO tTestimonio)
        {
            if (id != tTestimonio.IdTestimonio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.editar(tTestimonio);
                }
                catch (DbUpdateConcurrencyException)
                {
                   
                  return NotFound();
                                    
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdCliente"] = new SelectList(_listarGePersona.listar().Result, "Cedula", "Apellido1");
            return View(tTestimonio);
        }

        // GET: TTestimonios1/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tTestimonio = await _buscar.buscar(id);
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
            await _eliminar.eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> TestimonialForm()
        {
            ViewData["IdCliente"] = new SelectList(_listarGePersona.listar().Result, "Cedula", "Apellido1");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TestimonialForm([Bind("IdTestimonio,Fecha,IdCliente,Comentario,Evaluacion,Activo")] TTestimonioDTO tTestimonio)
        {
            if (ModelState.IsValid)
            {
                await _crear.crear(tTestimonio);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_listarGePersona.listar().Result, "Cedula", "Apellido1");
            return View(tTestimonio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Activar(int id)
        {
            try
            {
                var testimonioExistente = await _buscar.buscar(id);
                if (testimonioExistente == null)
                {
                    return Json(new { success = false, message = "Testimonio no encontrado" });
                }

                // Solo actualiza el campo Activo
                var testimonioActualizado = new TTestimonioDTO
                {
                    IdTestimonio = id,
                    Activo = true,
                    // Mantener los mismos valores para los demás campos
                    Fecha = testimonioExistente.Fecha,
                    IdCliente = testimonioExistente.IdCliente,
                    Comentario = testimonioExistente.Comentario,
                    Evaluacion = testimonioExistente.Evaluacion
                };

                var resultado = await _editar.editar(testimonioActualizado);

                if (resultado > 0)
                {
                    return Json(new { success = true, message = "Testimonio Actiavado correctamente" });
                }
                return Json(new { success = false, message = "No se pudo actualizar el testimonio" });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Error interno al reportar",
                    error = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }

        //// GET: TTestimonios
        public async Task<IActionResult> Testimonials()
        {
            var testimonios = await _listar.Listar();
            return View(testimonios);
        }

        //// GET: TTestimonios/TestimonialsLista
        public async Task<IActionResult> TestimonialsLista()
        {
            var testimonios = await _listar.ListarTodosSinFiltro();
            return View(testimonios);
        }

        public async Task<JsonResult> IdExiste(int id)
        {
            bool bandera;
            var ObjetoBuscado = await _buscar.buscar(id);
            if (ObjetoBuscado != null)
            {
                bandera = true;
                return Json(new { bandera });
            }
            bandera = false;
            return Json(new { bandera });
        }

    }
}
