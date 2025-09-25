using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.AD.GePersona.Listar;
using Preacepta.LN.GePersona.BuscarXid;
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
        private readonly IBuscarXidGePersonaLN _buscarPersona;
        private readonly IEmailSender _emailSender;


        public TTestimoniosController(IBuscarTestLN buscar,
            ICrearTestLN crear,
            IEditarTestLN editar,
            IEliminarTestLN eliminar,
            IListarTestLN listar,
            IListarGePersonaLN listarGePersona,
            IBuscarXidGePersonaLN buscarPersona,
            IEmailSender emailSender)
        {
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;
            _listarGePersona = listarGePersona;
            _buscarPersona = buscarPersona;
            _emailSender = emailSender;
        }

        // GET: TTestimonios1
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Index()
        {
            //var contexto = _context.TTestimonios.Include(t => t.IdClienteNavigation);
            return View(await _listar.Listar());
        }

        // GET: TTestimonios1/Details/5
        [Authorize(Roles = "Gestor")]
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
        [Authorize(Roles = "Gestor")]
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
        [Authorize(Roles = "Gestor")]
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
        [Authorize(Roles = "Gestor")]
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
        [Authorize(Roles = "Gestor")]
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
        [Authorize(Roles = "Gestor")]
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
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eliminar.eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        //Mis metodos
        [HttpGet]
        [Authorize(Roles = "Gestor, Abogado, Cliente")]
        public async Task<IActionResult> TestimonialForm()
        {
            // Busca la persona según el correo del usuario logueado
            var persona = await _buscarPersona.buscarXcorreo(User.Identity.Name);

            // Si no la encuentra, muestra error
            if (persona == null)
            {
                TempData["ErrorMessage"] = "No se encontró el usuario actual en el sistema.";
                return RedirectToAction("Index");
            }

            // Puedes pasar el IdCliente y el nombre al ViewBag (o ViewData)
            ViewBag.IdCliente = persona.Cedula;
            ViewBag.NombreCompleto = persona.Nombre + " " + persona.Apellido1 + " " + persona.Apellido2;

            // Ya NO envíes ningún SelectList
            return View();


            /*ViewData["IdCliente"] = new SelectList(_listarGePersona.listar().Result, "Cedula", "Apellido1");
            return View();*/
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TestimonialForm([Bind("IdTestimonio,Fecha,IdCliente,Comentario,Evaluacion,Activo")] TTestimonioDTO tTestimonio)
        {
            if (ModelState.IsValid)
            {
                await _crear.crear(tTestimonio);

                // Si la calificación es menor a 2 (1 estrella)
                if (tTestimonio.Evaluacion < 2)
                {
                    string subject = "Nueva reseña negativa recibida";
                    string body = $@"
                    <h2>Se ha recibido una reseña negativa</h2>
                    <p><b>Comentario:</b> {tTestimonio.Comentario}</p>
                    <p><b>Calificación:</b> {tTestimonio.Evaluacion} estrellas</p>
                    <p>Revisa el sistema para más detalles.</p>
                ";
                    await _emailSender.SendEmailAsync("d.gon.guerrero@gmail.com", subject, body);
                }

                return RedirectToAction("Testimonials");
            }
            ViewData["IdCliente"] = new SelectList(_listarGePersona.listar().Result, "Cedula", "Apellido1");
            return View(tTestimonio);
        }
        */

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor, Abogado, Cliente")]
        public async Task<IActionResult> TestimonialForm([Bind("IdTestimonio,Fecha,IdCliente,Comentario,Evaluacion,Activo")] TTestimonioDTO tTestimonio)
        {
            if (ModelState.IsValid)
            {
                tTestimonio.Activo = true;
                await _crear.crear(tTestimonio);

                // Si la calificación es menor a 2 (1 estrella)
                if (tTestimonio.Evaluacion < 2)
                {
                    string subject = "Nueva reseña negativa recibida";
                    string body = $@"
                <h2>Se ha recibido una reseña negativa</h2>
                <p><b>Comentario:</b> {tTestimonio.Comentario}</p>
                <p><b>Calificación:</b> {tTestimonio.Evaluacion} estrellas</p>
                <p>Revisa el sistema para más detalles.</p>
            ";

                    try
                    {
                        Console.WriteLine("Intentando enviar correo de reseña negativa...");
                        await _emailSender.SendEmailAsync("d.gon.guerrero@gmail.com", subject, body);
                        Console.WriteLine("Correo enviado correctamente.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error enviando correo: " + ex.Message);
                        if (ex.InnerException != null)
                            Console.WriteLine("Inner: " + ex.InnerException.Message);
                    }
                }

                // ENVÍA el correo de confirmación al cliente
                var persona = await _buscarPersona.buscar(tTestimonio.IdCliente);
                if (persona != null && !string.IsNullOrEmpty(persona.Email))
                {
                    string subjectCliente = "¡Gracias por tu testimonio!";
                    string bodyCliente = $@"
                    <h2>¡Testimonio publicado correctamente!</h2>
                    <p>Gracias por compartir tu experiencia.</p>
                    <p><b>Tu comentario:</b> {tTestimonio.Comentario}</p>
                    <p><b>Calificación:</b> {tTestimonio.Evaluacion} estrellas</p>
                    <br>
                    <p>El equipo de Praecepta</p>
                ";
                        await _emailSender.SendEmailAsync(persona.Email, subjectCliente, bodyCliente);
                }

                return RedirectToAction("Testimonials"); // O el nombre real de tu acción/lista
            }

            ViewData["IdCliente"] = new SelectList(_listarGePersona.listar().Result, "Cedula", "Apellido1");
            return View(tTestimonio);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor, Abogado, Cliente")]
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
        [Authorize(Roles = "Gestor, Abogado, Cliente")]
        public async Task<IActionResult> Testimonials()
        {
            var testimonios = await _listar.Listar();
            return View(testimonios);
        }

        //// GET: TTestimonios/TestimonialsLista
        [Authorize(Roles = "Gestor, Abogado, Cliente")]
        public async Task<IActionResult> TestimonialsLista()
        {
            var testimonios = await _listar.ListarTodosSinFiltro();
            var activos = testimonios.Where(t => t.Activo).ToList();
            return View(activos);
        }

        [Authorize(Roles = "Gestor, Abogado, Cliente")]
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

        // GET: TTestimonios/Reportados
        [Authorize(Roles = "Gestor, Abogado, Cliente")]
        public async Task<IActionResult> Reportados()
        {
            // Solo testimonios con Activo == false
            var testimoniosReportados = await _listar.ListarInactivos();

            return View(testimoniosReportados); // Apunta a una vista llamada Reportados.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor, Abogado, Cliente")]
        public async Task<IActionResult> Reportar(int id)
        {
            Console.WriteLine("ID recibido: " + id);

            try
            {
                var testimonioExistente = await _buscar.buscar(id);
                if (testimonioExistente == null)
                    return Json(new { success = false, message = "Testimonio no encontrado" });

                // Actualiza solo el campo Activo
                testimonioExistente.Activo = false;
                var resultado = await _editar.editar(testimonioExistente);

                if (resultado > 0)
                    return Json(new { success = true, message = "Testimonio reportado correctamente" });
                else
                    return Json(new { success = false, message = "No se pudo reportar el testimonio" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error interno al reportar", error = ex.Message });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor, Abogado, Cliente")]
        public async Task<IActionResult> Delete2(int id)
        {
            await _eliminar.eliminar(id);
            return Json(new { success = true, message = "Eliminado correctamente" });
        }




    }
}
