using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Preacepta.LN.Citas.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using Preacepta.LN.Citas.Crear;
using Preacepta.LN.Citas.Eliminar;
using Preacepta.LN.Citas.Editar;
using Preacepta.LN.Citas.BuscarXid;
using Preacepta.LN.CitasTipo.ObtenerDatos;
using Preacepta.LN.CitasTipo.Listar;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Preacepta.AD;
using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using System.Net.Mail;
using System.Net;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.LN.GePersona.ObtenerDatos;
using Preacepta.LN.GeAbogado.BuscarXid;
using Preacepta.LN.Videollamada;

namespace Praecepta.UI.Controllers
{

    public class CitasController : Controller
    {
        private readonly IListarCitasLN _listarCitasLN;
        private readonly ICrearCitasLN _crearCitasLN;
        private readonly IEliminarCitasLN _eliminarCitasLN;
        private readonly IEditarCitasLN _editarCitasLN;
        private readonly IListarCitasTipoLN _listarCitasTipoLN;
        private readonly IBuscarCitasLN _buscarCitasLN;
        private readonly IObtenerDatosCitasTipoLN _obtenerDatosCitasTipoLN;
        private readonly IBuscarXidGePersonaLN _buscarXidGePersonaLN;
        private readonly IObtenerDatosLN _obtenerDatosLN;
        private readonly IBuscarAbogadoLN _buscarAbogadoLN;
        private readonly UserManager<IdentityUser> _userManager;


        public CitasController(
            IListarCitasLN listarCitasLN,
            ICrearCitasLN crearCitasLN,
            IEliminarCitasLN eliminarCitasLN,
            IEditarCitasLN editarCitasLN,
            IBuscarCitasLN buscarCitasLN,
            IListarCitasTipoLN listarCitasTipoLN,
            IObtenerDatosCitasTipoLN obtenerDatosCitasTipoLN,
            IBuscarXidGePersonaLN buscarXidGePersonaLN,
            IObtenerDatosLN obtenerDatosLN,
            IBuscarAbogadoLN buscarAbogadoLN,
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _listarCitasLN = listarCitasLN;
            _crearCitasLN = crearCitasLN;
            _eliminarCitasLN = eliminarCitasLN;
            _editarCitasLN = editarCitasLN;
            _buscarCitasLN = buscarCitasLN;
            _obtenerDatosCitasTipoLN = obtenerDatosCitasTipoLN;
            _listarCitasTipoLN = listarCitasTipoLN;
            _buscarXidGePersonaLN = buscarXidGePersonaLN;
            _obtenerDatosLN = obtenerDatosLN;
            _buscarAbogadoLN = buscarAbogadoLN;
        }

        //GET: Citas/Create
        [Authorize(Roles = "Abogado,Gestor")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var tiposCita = await _listarCitasTipoLN.listar();
            ViewData["CitasTipo"] = tiposCita.Select(n => new SelectListItem
            {
                Value = n.Id.ToString(),
                Text = n.Nombre
            }).ToList();

            var usuariosCliente = await _userManager.GetUsersInRoleAsync("Cliente");
            var emailsClientes = usuariosCliente.Select(u => u.Email).ToList();

            var personasClientes = new List<SelectListItem>();
            foreach (var email in emailsClientes)
            {
                var personaCliente = await _buscarXidGePersonaLN.buscarXcorreo(email);
                if (personaCliente != null)
                {
                    personasClientes.Add(new SelectListItem
                    {
                        Value = personaCliente.Cedula.ToString(),
                        Text = $"{personaCliente.Nombre} {personaCliente.Apellido1} {personaCliente.Apellido2}"
                    });
                }
            }
            ViewData["Clientes"] = personasClientes;

            var usuarioActual = await _userManager.GetUserAsync(User);
            var emailUsuario = usuarioActual?.Email;

            var persona = await _buscarXidGePersonaLN.buscarXcorreo(emailUsuario);
            if (persona == null)
            {
                return Unauthorized();
            }

            var abogado = await _buscarAbogadoLN.buscar(persona.Cedula);
            if (abogado == null)
            {
                return NotFound("No se encontró un abogado vinculado con esta cuenta.");
            }

            var citaDTO = new CitasDTO
            {
                Anfitrion = abogado.Cedula,
                NombreAnfitrion = $"{persona.Nombre} {persona.Apellido1} {persona.Apellido2}"
            };

            return PartialView("~/Views/Citas/_CreatePartial.cshtml", citaDTO);
        }


        // POST: Citas/Create
        [Authorize(Roles = "Abogado,Gestor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CitasDTO citaDTO)
        {
            if (citaDTO.Fecha < DateOnly.FromDateTime(DateTime.Now) ||
                (citaDTO.Fecha == DateOnly.FromDateTime(DateTime.Now) && citaDTO.Hora.ToTimeSpan() < DateTime.Now.TimeOfDay))
            {
                ModelState.AddModelError("Fecha", "La fecha y hora deben ser futuras.");
                return BadRequest(ModelState);
            }

            if (citaDTO.Anfitrion == 0)
            {
                var usuarioActual = await _userManager.GetUserAsync(User);
                var persona = await _buscarXidGePersonaLN.buscarXcorreo(usuarioActual.Email);
                var abogado = await _buscarAbogadoLN.buscar(persona.Cedula);

                if (persona != null && abogado != null)
                {
                    citaDTO.Anfitrion = abogado.Cedula;
                    citaDTO.NombreAnfitrion = $"{persona.Nombre} {persona.Apellido1} {persona.Apellido2}";
                }
            }

            if (!citaDTO.IdCliente.HasValue)
            {
                return BadRequest("Debe seleccionar un cliente válido.");
            }

            try
            {
                var idCita = await _crearCitasLN.crear(citaDTO);

                if (idCita <= 0)
                {
                    return Json(new
                    {
                        success = false,
                        errors = new List<string> { "No se pudo crear la cita. Verifique los datos." }
                    });
                }
               
                // Enviar correo al cliente
                var cliente = await _buscarXidGePersonaLN.buscar(citaDTO.IdCliente.Value);
                var abogadoPersona = await _buscarXidGePersonaLN.buscar(citaDTO.Anfitrion);

                if (cliente != null && !string.IsNullOrWhiteSpace(cliente.Email) && abogadoPersona != null)
                {
                    var correos = new List<string> { cliente.Email };
                    var nombreCliente = $"{cliente.Nombre} {cliente.Apellido1} {cliente.Apellido2}";
                    var nombreAbogado = $"{abogadoPersona.Nombre} {abogadoPersona.Apellido1} {abogadoPersona.Apellido2}";
                    DateTime fecha = citaDTO.Fecha.ToDateTime(citaDTO.Hora);

                    await EnviarCorreoNotificacionCita(correos, fecha, nombreCliente, nombreAbogado);
                }

                return Json(new
                {
                    success = true,
                    nuevaFecha = citaDTO.Fecha.ToString("yyyy-MM-dd"),
                    nuevaHora = citaDTO.Hora.ToString(@"hh\:mm"),
                    idCita
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear cita: {ex.Message}");
                return Json(new { success = false, errors = new List<string> { ex.Message } });
            }
        }


        [Authorize(Roles = "Abogado,Gestor")]
        private async Task EnviarCorreoNotificacionCita(List<string> correos, DateTime fecha, string nombreCliente, string nombreAbogado)
        {
            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,

                Credentials = new NetworkCredential("d.gon.guerrero@gmail.com", "oiup tfoc roio sbei"), 

                EnableSsl = true
            };
            foreach (var correo in correos)
            {
                try
                {
                    var cuerpo = $"Hola {nombreCliente},\n\n" +
        $"Su cita ha sido agendada exitosamente.\n\n" +
        $"Detalles de la cita:\n" +
        $"Fecha: {fecha.ToString("dd/MM/yyyy")}\n" +
        $"Hora: {fecha.ToString("HH:mm")}\n" +
        $"Abogado: {nombreAbogado}\n\n" +
        $"Por favor, asegúrese de estar disponible a la hora programada.\n\n" +
        $"Gracias por confiar en nuestros servicios.\n\n" +
        $"Atentamente,\n" +
        $"Equipo Praecepta";

                    var mail = new MailMessage("d.gon.guerrero@gmail.com", correo)
                {

                    Subject = "Confirmación de cita agendada",
                    Body = cuerpo,
                    IsBodyHtml = false
                };

                await smtp.SendMailAsync(mail);
            }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error enviando correo a {correo}: {ex.Message}");
                }
            }
        }
        [Authorize(Roles = "Cliente,Abogado,Gestor")]
        [HttpGet]
        public async Task<IActionResult> ObtenerCitas()
        {
            var usuarioActual = await _userManager.GetUserAsync(User);
            if (usuarioActual == null) return Unauthorized();

            bool esCliente = await _userManager.IsInRoleAsync(usuarioActual, "Cliente");
            List<CitasDTO> citas;

            if (esCliente)
            {
                var persona = await _buscarXidGePersonaLN.buscarXcorreo(usuarioActual.Email);
                if (persona == null) return NotFound("No se encontró persona asociada al usuario.");

                citas = await _listarCitasLN.ListarPorIdCliente(persona.Cedula);
            }
            else
            {
                citas = await _listarCitasLN.listar(); 
            }

            var resultado = citas.Select(c => new
            {
                idCita = c.IdCita,
                fecha = c.Fecha.ToString("yyyy-MM-dd"),
                hora = c.Hora.ToString(@"hh\:mm"),
                nombreTipoCita = c.NombreTipoCita ?? "Sin título"
            });

            return Json(resultado);
        }

        [Authorize(Roles = "Cliente,Abogado,Gestor")]
        [HttpGet]
        public async Task<IActionResult> ObtenerCitasPorFecha(DateTime fecha)
        {
            var usuarioActual = await _userManager.GetUserAsync(User);
            if (usuarioActual == null) return Unauthorized();

            bool esCliente = await _userManager.IsInRoleAsync(usuarioActual, "Cliente");
            List<CitasDTO> citas;

            if (esCliente)
            {
                var persona = await _buscarXidGePersonaLN.buscarXcorreo(usuarioActual.Email);
                if (persona == null) return NotFound();

                citas = (await _listarCitasLN.ListarPorIdCliente(persona.Cedula))
                         .Where(c => c.Fecha == DateOnly.FromDateTime(fecha)).ToList();
            }
            else
            {
                citas = (await _listarCitasLN.listar())
                         .Where(c => c.Fecha == DateOnly.FromDateTime(fecha)).ToList();
            }


            var resultado = citas.Select(c => new

            {
                idCita = c.IdCita,
                hora = c.Hora.ToString(@"hh\:mm"),
                nombreTipoCita = c.NombreTipoCita ?? "Sin título"
            });

            return Json(resultado);
        }


        // GET: Citas/Edit
        [Authorize(Roles = "Abogado")]
        public async Task<IActionResult> Edit(int id)
        {
            var cita = await _buscarCitasLN.ObtenerCitaConClientes(id);
            if (cita == null)
                return NotFound();
            var estados = new List<SelectListItem>
{
    new SelectListItem { Value = "0", Text = "En proceso" },
    new SelectListItem { Value = "1", Text = "Terminada" },
    new SelectListItem { Value = "2", Text = "Cancelada por cliente" },
    new SelectListItem { Value = "3", Text = "Cancelada por Abogado" },
    new SelectListItem { Value = "4", Text = "Reprogramada" }
};
            ViewBag.EstadoList = new SelectList(estados, "Value", "Text", cita.Estado);

            var tiposCita = await _listarCitasLN.ListarTiposCita();
            ViewBag.TipoCitaList = tiposCita.Select(n => new SelectListItem
            {
                Value = n.Id.ToString(),
                Text = n.Nombre
            }).ToList();

            var usuariosCliente = await _userManager.GetUsersInRoleAsync("Cliente");
            var emailsClientes = usuariosCliente.Select(u => u.Email).ToList();

            var personasClientes = new List<SelectListItem>();
            foreach (var email in emailsClientes)
            {
                var personaCliente = await _buscarXidGePersonaLN.buscarXcorreo(email);
                if (personaCliente != null)
                {
                    personasClientes.Add(new SelectListItem
                    {
                        Value = personaCliente.Cedula.ToString(),
                        Text = $"{personaCliente.Nombre} {personaCliente.Apellido1} {personaCliente.Apellido2}"
                    });
                }
            }
            ViewData["Clientes"] = personasClientes;

            var clienteAsignado = cita.TCitasClientes?.FirstOrDefault()?.IdCliente;
            if (clienteAsignado.HasValue)
            {
                cita.IdCliente = clienteAsignado.Value;
                var cliente = await _buscarXidGePersonaLN.buscar(clienteAsignado.Value);
                if (cliente != null)
                {
                    cita.NombresClientes = new List<string> { $"{cliente.Nombre} {cliente.Apellido1} {cliente.Apellido2}" };
                }
            }

            if (string.IsNullOrEmpty(cita.NombreAnfitrion))
            {
                var persona = await _listarCitasLN.ObtenerPersonaPorCedula(cita.Anfitrion.ToString());
                if (persona != null)
                {
                    cita.NombreAnfitrion = $"{persona.Nombre} {persona.Apellido1} {persona.Apellido2}";
                }
            }
            return PartialView("~/Views/Citas/_EditPartial.cshtml", cita);
        }

        [Authorize(Roles = "Abogado")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CitasDTO cita)
        {
            if (id != cita.IdCita)
                return BadRequest();

            var citaOriginal = await _listarCitasLN.ObtenerPorId(id);
            if (citaOriginal == null)
                return NotFound();

            var fechaAnterior = citaOriginal.Fecha.ToDateTime(citaOriginal.Hora);

            cita.Anfitrion = citaOriginal.Anfitrion;
            cita.IdCliente = citaOriginal.IdCliente; 
            cita.NombresClientes = citaOriginal.NombresClientes;
            cita.TCitasClientes = citaOriginal.TCitasClientes;

            var persona = await _listarCitasLN.ObtenerPersonaPorCedula(cita.Anfitrion.ToString());
            if (persona != null)
            {
                cita.NombreAnfitrion = $"{persona.Nombre} {persona.Apellido1} {persona.Apellido2}";
            }
            var fechaSeleccionada = cita.Fecha.ToDateTime(cita.Hora);
            bool fechaCambiada = fechaSeleccionada != citaOriginal.Fecha.ToDateTime(citaOriginal.Hora);
            if (fechaCambiada && fechaSeleccionada < DateTime.Now)
            {
                ModelState.AddModelError("Fecha", "La fecha debe ser posterior a la actual.");
                ModelState.AddModelError("Hora", "La hora debe ser posterior a la actual.");
            }
            if (ModelState.IsValid)
            {
                bool enviarCorreo = citaOriginal.Estado != 1 && cita.Estado == 1;

                var tipoCita = await _listarCitasLN.ListarTiposCita();
                var nombreTipoCita = tipoCita.FirstOrDefault(t => t.Id == cita.IdTipoCita)?.Nombre;
                bool linkEliminado = false;
                if (nombreTipoCita == "Virtual" && string.IsNullOrEmpty(cita.LinkVideo))
                {
                    var auth = new ZoomAuthService();
                    var token = await auth.ObtenerAccessTokenAsync();

                    var servicio = new ZoomMeetingService();
                    var zoomResult = await servicio.CrearReunionProgramadaAsync(token, cita.FechaHora, 60, "Cita con cliente");
                    cita.LinkVideo = zoomResult.JoinUrl;

                } 
                else if (nombreTipoCita != "Virtual" && !string.IsNullOrEmpty(citaOriginal.LinkVideo))
                {
                    cita.LinkVideo = null;
                    linkEliminado = true;
                }

                var resultado = await _editarCitasLN.editar(cita);
                if (linkEliminado && Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new
                    {
                        success = true,
                        linkEliminado = true,
                        mensaje = "Se eliminó el link de la reunión virtual porque la cita ya no es virtual"
                    });
                }

                if (ModelState.IsValid)
                {
                    var estadoAnterior = citaOriginal.Estado;
                    await _editarCitasLN.editar(cita); // Guardar cambios

                    // Enviar correo si cambió a "Terminada"
                    if (estadoAnterior != 1 && cita.Estado == 1)
                    {
                        await EnviarCorreoAlTerminarCita(cita);
                    }
                }
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new
                    {
                        success = true,
                        fechaAnterior,
                        nuevaFecha = cita.Fecha.ToString("yyyy-MM-dd"),
                        idCita = cita.IdCita
                    });
                }

                return RedirectToAction(nameof(Calendar));
            }


            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var estados = new List<SelectListItem>
    {
        new SelectListItem { Value = "0", Text = "En proceso" },
        new SelectListItem { Value = "1", Text = "Terminada" },
        new SelectListItem { Value = "2", Text = "Cancelada por cliente" },
        new SelectListItem { Value = "3", Text = "Cancelada por Abogado" },
        new SelectListItem { Value = "4", Text = "Reprogramada" }
    };
                ViewBag.EstadoList = new SelectList(estados, "Value", "Text", cita.Estado.ToString());


                var tiposCita = await _listarCitasLN.ListarTiposCita();
                ViewBag.TipoCitaList = tiposCita.Select(n => new SelectListItem
                {
                    Value = n.Id.ToString(),
                    Text = n.Nombre
                }).ToList();

                var usuariosCliente = await _userManager.GetUsersInRoleAsync("Cliente");
                var emailsClientes = usuariosCliente.Select(u => u.Email).ToList();
                var personasClientes = new List<SelectListItem>();
                foreach (var email in emailsClientes)
                {
                    var personaCliente = await _buscarXidGePersonaLN.buscarXcorreo(email);
                    if (personaCliente != null)
                    {
                        personasClientes.Add(new SelectListItem
                        {
                            Value = personaCliente.Cedula.ToString(),
                            Text = $"{personaCliente.Nombre} {personaCliente.Apellido1} {personaCliente.Apellido2}"
                        });
                    }
                }
                ViewData["Clientes"] = new SelectList(personasClientes, "Value", "Text", cita.IdCliente?.ToString());

                return PartialView("~/Views/Citas/_EditPartial.cshtml", cita);
            }

            return View("~/Views/CitasPrueba/Edit.cshtml", cita);
        }


        // GET: Citas/Details
        [Authorize(Roles = "Cliente,Abogado,Gestor")]
        public async Task<IActionResult> Details(int id)
        {
            var cita = await _buscarCitasLN.ObtenerCitaConDocumentosAsync(id);

            if (cita == null)
                return NotFound();
            if (User.IsInRole("Cliente"))
            {
                var usuarioActual = await _userManager.GetUserAsync(User);
                if (usuarioActual == null)
                    return Unauthorized();

                var persona = await _buscarXidGePersonaLN.buscarXcorreo(usuarioActual.Email);
                if (persona == null)
                    return NotFound("No se encontró la persona asociada al usuario.");

                bool pertenece = cita.NombresClientes.Any(n => n.Contains(persona.Nombre)
                                                           || n.Contains(persona.Apellido1)
                                                           || n.Contains(persona.Apellido2));
                if (!pertenece)
                    return Forbid(); 
            }


            if (string.IsNullOrEmpty(cita.NombreTipoCita) && cita.IdTipoCita > 0)

            {
                var tipos = await _listarCitasLN.ListarTiposCita();
                var tipo = tipos.FirstOrDefault(t => t.Id == cita.IdTipoCita);
                if (tipo != null)
                    cita.NombreTipoCita = tipo.Nombre;
            }

            return PartialView("~/Views/Citas/_DetailsPartial.cshtml", cita);
        }


        [Authorize(Roles = "Cliente,Abogado,Gestor")]
        public async Task<IActionResult> Calendar()
        {
            var usuarioActual = await _userManager.GetUserAsync(User);

            if (usuarioActual == null)
                return Unauthorized();

            var roles = await _userManager.GetRolesAsync(usuarioActual);
            List<CitasDTO> citas;

            if (roles.Contains("Cliente"))
            {
                var emailUsuario = usuarioActual.Email;
                var persona = await _buscarXidGePersonaLN.buscarXcorreo(emailUsuario);

                if (persona == null)
                    return NotFound("No se encontró una persona asociada a este email.");

                citas = await _listarCitasLN.ListarPorIdCliente(persona.Cedula);
            }
            else if (roles.Contains("Abogado") || roles.Contains("Gestor"))
            {
                citas = await _listarCitasLN.listar(); 
            }
            else
            {
                return Forbid();
            }

            return View("~/Views/Citas/Calendar.cshtml", citas);
        }

        [Authorize(Roles = "Cliente,Abogado,Gestor")]
        private async Task<List<CitasDTO>> ObtenerCitasClienteActual()
        {
            var usuarioActual = await _userManager.GetUserAsync(User);
            if (usuarioActual == null)
                return new List<CitasDTO>();

            var emailUsuario = usuarioActual.Email;


            var persona = await _buscarXidGePersonaLN.buscarXcorreo(emailUsuario);
            if (persona == null)
        return new List<CitasDTO>();


            return await _listarCitasLN.ListarPorIdCliente(persona.Cedula);
        }


        [Authorize(Roles = "Cliente,Abogado,Gestor")]
        public async Task<IActionResult> CalendarPasado()
{
    var lista = await ObtenerCitasClienteActual();
    var citasPasadas = lista.Where(c => c.FechaHora < DateTime.Now).ToList();
    return View(citasPasadas);
}

        [Authorize(Roles = "Cliente,Abogado,Gestor")]
        public async Task<IActionResult> _CitaFuturo()
{
    var lista = await ObtenerCitasPorRol();
    var citasFuturas = lista.Where(c => c.FechaHora > DateTime.Now).ToList();
    return View(citasFuturas);
}


        [HttpGet]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> CalendarCliente()
        {
            var usuarioActual = await _userManager.GetUserAsync(User);
            var emailUsuario = usuarioActual?.Email;

            if (string.IsNullOrEmpty(emailUsuario))
                return Unauthorized();

            var persona = await _buscarXidGePersonaLN.buscarXcorreo(emailUsuario);

            if (persona == null)
                return NotFound("No se encontró una persona asociada a este email.");

            var citas = await _listarCitasLN.ListarPorIdCliente(persona.Cedula);

            return View("~/Views/Citas/Calendar.cshtml", citas);
        }

        [Authorize(Roles = "Cliente,Abogado,Gestor")]
        private async Task<List<CitasDTO>> ObtenerCitasPorRol()
        {
            var usuarioActual = await _userManager.GetUserAsync(User);
            var emailUsuario = usuarioActual?.Email;

            if (string.IsNullOrEmpty(emailUsuario))
                return new List<CitasDTO>();

            var persona = await _buscarXidGePersonaLN.buscarXcorreo(emailUsuario);
            if (persona == null)
                return new List<CitasDTO>();

            var esAbogado = await _userManager.IsInRoleAsync(usuarioActual, "Abogado");
            var esGestor = await _userManager.IsInRoleAsync(usuarioActual, "Gestor");
            var esCliente = await _userManager.IsInRoleAsync(usuarioActual, "Cliente");

            if (!esCliente && (esAbogado || esGestor))
            {
                return await _listarCitasLN.listar();
            }


            return await _listarCitasLN.ListarPorIdCliente(persona.Cedula);

        }
           

        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<JsonResult> IdExiste(int id)
        {
            bool bandera;
            var ObjetoBuscado = await _buscarCitasLN.buscar(id);
            if (ObjetoBuscado != null)
            {
                bandera = true;
                return Json(new { bandera });
            }
            bandera = false;
            return Json(new { bandera });
        }

        [Authorize(Roles = "Abogado,Gestor")]
        [HttpPost]
        public async Task<IActionResult> MarcarComoTerminada(int idCita)
        {
           
            var cita = await _listarCitasLN.ObtenerPorId(idCita);
            if (cita == null)
                return NotFound("Cita no encontrada.");

         
            cita.Estado = 1;

           
            await _editarCitasLN.editar(cita);

           
            await EnviarCorreoAlTerminarCita(cita);

            return Ok(new { success = true, message = "Cita marcada como terminada y correo enviado." });
        }



        public class CambiarEstadoRequest
        {
            public int IdCita { get; set; }
            public int NuevoEstado { get; set; }
        }

        [Authorize(Roles = "Abogado,Gestor")]
        private async Task<bool> EnviarCorreoAlTerminarCita(CitasDTO cita)
        {
            if (cita.TCitasClientes != null)
            {
                foreach (var cc in cita.TCitasClientes)
                {
                }
            }

            try
            {
                
                var clienteRelacion = cita.TCitasClientes?.FirstOrDefault();
                if (clienteRelacion == null)
                {
                    return false;
                }

                var cliente = clienteRelacion.IdClienteNavigation;
                    if (cliente == null || string.IsNullOrWhiteSpace(cliente.Email))
                {
                    return false;
                }

                var abogado = await _buscarXidGePersonaLN.buscar(cita.Anfitrion);
                var nombreAbogado = abogado != null
                    ? $"{abogado.Nombre} {abogado.Apellido1} {abogado.Apellido2}"
                    : "Su abogado";

                DateTime fechaCita;
                try
                {
                    fechaCita = cita.Fecha.ToDateTime(cita.Hora);
                }
                catch
                {
                    fechaCita = DateTime.Now;
                }

                var correos = new List<string> { cliente.Email };
                var nombreCliente = $"{cliente.Nombre} {cliente.Apellido1} {cliente.Apellido2}";

                string innerUrl = $"/Home/UsuarioAutenticado?correo={Uri.EscapeDataString(cliente.Email)}&redirectTo={Uri.EscapeDataString("/TTestimonios/TestimonialForm")}";
                string loginUrl = $"https://localhost:7065/Identity/Account/Login?ReturnUrl={Uri.EscapeDataString(innerUrl)}";

                string cuerpo = $"Saludos {nombreCliente},\n\n" +
                                $"Gracias por haber asistido a su cita el {fechaCita:dd/MM/yyyy HH:mm}.\n\n" +
                                $"Nos gustaría conocer su opinión sobre el servicio brindado.\n" +
                                $"Después de su cita, puede dejar su testimonio aquí:\n{loginUrl}\n\n" +
                                $"(Debe iniciar sesión para dejar su testimonio.)";

                using (var smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("d.gon.guerrero@gmail.com", "oiup tfoc roio sbei"),
                    EnableSsl = true
                })
                {
                    foreach (var correo in correos)
                    {
                        var mail = new MailMessage("d.gon.guerrero@gmail.com", correo)
                        {
                            Subject = "Notificación de cita finalizada",
                            Body = cuerpo,
                            IsBodyHtml = false
                        };

                        try
                        {
                            await smtp.SendMailAsync(mail);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
public async Task<IActionResult> CambiarEstadoEnviarCorreo([FromBody] dynamic payload)
{
    try
    {
        int idCita = (int)payload.idCita;
        int nuevoEstado = (int)payload.nuevoEstado;

       
        var cita = await _listarCitasLN.ObtenerPorId(idCita);
        if (cita == null)
            return Json(new { success = false, message = "Cita no encontrada" });

        cita.Estado = nuevoEstado;
        await _editarCitasLN.editar(cita);

        if (nuevoEstado == 1) 
            await EnviarCorreoAlTerminarCita(cita);

        return Json(new { success = true });
    }
    catch (Exception ex)
    {
        return Json(new { success = false, message = ex.Message });
    }
}


    }
}
