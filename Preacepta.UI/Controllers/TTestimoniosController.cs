using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.Testimonios.Buscar;
using Preacepta.LN.Testimonios.Crear;
using Preacepta.LN.Testimonios.Editar;
using Preacepta.LN.Testimonios.Eliminar;
using Preacepta.LN.Testimonios.Listar;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.UI.Controllers
{
    public class TTestimoniosController : Controller
    {
        private readonly IBuscarTestLN _buscar;
        private readonly ICrearTestLN _crear;
        private readonly IEditarTestLN _editar;
        private readonly IEliminarTestLN _eliminar;
        private readonly IListarTestLN _listar;

        public TTestimoniosController(IBuscarTestLN buscar,
            ICrearTestLN crear,
            IEditarTestLN editar,
            IEliminarTestLN eliminar,
            IListarTestLN listar)
        {
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;
        }

        // GET: TTestimonios
        public async Task<IActionResult> Testimonials()
        {
            var testimonios = await _listar.Listar();
            return View(testimonios);
        }

        // GET: TTestimonios/TestimonialsLista
        public async Task<IActionResult> TestimonialsLista()
        {
            var testimonios = await _listar.ListarTodosSinFiltro();
            return View(testimonios);
        }

        // GET: TTestimonios/Details/5
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

        // GET: TTestimonios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TTestimonios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTestimonio,Fecha,IdCliente,Comentario,Evaluacion,Activo")] TTestimonioDTO tTestimonio)
        {
            if (ModelState.IsValid)
            {
                tTestimonio.Fecha = DateTime.Today.ToString("yyyy-MM-dd");
                tTestimonio.Activo = true;
                tTestimonio.IdCliente = 123456789;

                await _crear.crear(tTestimonio);
                return RedirectToAction(nameof(Index));
            }
            return View(tTestimonio);
        }

        // GET: TTestimonios/Edit/5
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
            return View(tTestimonio);
        }

        // POST: TTestimonios/Edit/5
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
                    tTestimonio.IdCliente = 123456789;
                    await _editar.editar(tTestimonio);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tTestimonio);
        }

        // GET: TTestimonios/Delete/5
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

        // POST: TTestimonios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eliminar.eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        //Prueba

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reportar(int id)
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
                    Activo = false,
                    // Mantener los mismos valores para los demás campos
                    Fecha = testimonioExistente.Fecha,
                    IdCliente = testimonioExistente.IdCliente,
                    Comentario = testimonioExistente.Comentario,
                    Evaluacion = testimonioExistente.Evaluacion
                };

                var resultado = await _editar.editar(testimonioActualizado);

                if (resultado > 0)
                {
                    return Json(new { success = true, message = "Testimonio reportado correctamente" });
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

        // POST: TTestimonios/Delete2
        [HttpPost]
        public async Task<IActionResult> Delete2(int id)
        {
            try
            {
                await _eliminar.eliminar(id);
                TempData["SuccessMessage"] = "Testimonio eliminado correctamente";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al eliminar el testimonio";
                // Log the exception
            }
            return RedirectToAction(nameof(TestimonialsLista));
        }

        //TestimonialForm

        [HttpGet]
        public async Task<IActionResult> TestimonialForm()
        {
            int idCliente = 101010101;
            var cliente = await _buscar.BuscarClientePorId(idCliente);

            var model = new TTestimonioDTO
            {
                IdCliente = idCliente,
                Fecha = DateTime.Today.ToString("yyyy-MM-dd"),
                Activo = true,
                IdClienteNavigation = cliente != null ? new TGePersona
                {
                    Cedula = cliente.Cedula,
                    Nombre = cliente.Nombre
                } : null
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TestimonialForm([Bind("IdTestimonio,Fecha,IdCliente,Comentario,Evaluacion,Activo")] TTestimonioDTO tTestimonio)
        {
            // Forzar valores importantes
            tTestimonio.Activo = true; // Esto ya lo tienes
            tTestimonio.Fecha = DateTime.Today.ToString("yyyy-MM-dd");
            tTestimonio.IdCliente = 101010101;

            // DEBUG: Agrega este log
            Console.WriteLine($"Valor Activo en Controller (antes de conversión): {tTestimonio.Activo}");

            if (!ModelState.IsValid)
            {
                // Recargar datos de navegación si es necesario
                return View(tTestimonio);
            }

            try
            {
                // DEBUG: Verifica los valores antes de enviar
                Console.WriteLine($"Valores a insertar - Activo: {tTestimonio.Activo}, Fecha: {tTestimonio.Fecha}");

                await _crear.crear(tTestimonio);
                return RedirectToAction("Testimonials");
            }
            catch (Exception ex)
            {
                // Log detallado del error
                Console.WriteLine($"Error al crear testimonio: {ex.ToString()}");
                ModelState.AddModelError("", "Error al guardar el testimonio. Por favor intente nuevamente.");
                return View(tTestimonio);
            }
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
    }
}
