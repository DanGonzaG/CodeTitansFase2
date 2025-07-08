using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Praecepta.UI.Models;
using Microsoft.AspNetCore.Http;
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
using System.Collections.Generic;
using Azure.Core;

namespace Praecepta.UI.Controllers
{
    [Authorize(Roles = "Abogado")]
    public class CitasController : Controller
    {
        private readonly IListarCitasLN _listarCitasLN;
        private readonly ICrearCitasLN _crearCitasLN;
        private readonly IEliminarCitasLN _eliminarCitasLN;
        private readonly IEditarCitasLN _editarCitasLN;
        private readonly IListarCitasTipoLN _listarCitasTipoLN;
        private readonly IBuscarCitasLN _buscarCitasLN;
        private readonly IObtenerDatosCitasTipoLN _obtenerDatosCitasTipoLN;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Contexto _context;

        public CitasController(
            IListarCitasLN listarCitasLN,
            ICrearCitasLN crearCitasLN,
            IEliminarCitasLN eliminarCitasLN,
            IEditarCitasLN editarCitasLN,
            IBuscarCitasLN buscarCitasLN,
            IListarCitasTipoLN listarCitasTipoLN,
            IObtenerDatosCitasTipoLN obtenerDatosCitasTipoLN,
            UserManager<IdentityUser> userManager,
            Contexto context)
        {
            _userManager = userManager;
            _context = context;
            _listarCitasLN = listarCitasLN;
            _crearCitasLN = crearCitasLN;
            _eliminarCitasLN = eliminarCitasLN;
            _editarCitasLN = editarCitasLN;
            _buscarCitasLN = buscarCitasLN;
            _obtenerDatosCitasTipoLN = obtenerDatosCitasTipoLN;
            _listarCitasTipoLN = listarCitasTipoLN;
        }

        public async Task<IActionResult> Citas()
        {
            var lista = await _listarCitasLN.listar();
            return View("~/Views/CitasPrueba/Index.cshtml", lista);
        }



        //GET: Citas/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var tiposCita = await _listarCitasTipoLN.listar();
            ViewData["CitasTipo"] = tiposCita.Select(n => new SelectListItem
            {
                Value = n.Id.ToString(),
                Text = n.Nombre
            }).ToList();

            // Obtener lista de clientes para mostrar en dropdown
            var clientes = await _context.TGePersonas
                .Select(p => new SelectListItem
                {
                    Value = p.Cedula.ToString(), // o el campo IdCliente que corresponda
                    Text = $"{p.Nombre} {p.Apellido1} {p.Apellido2}"
                })
                .ToListAsync();
            ViewData["Clientes"] = clientes;

            var usuarioActual = await _userManager.GetUserAsync(User);
            var emailUsuario = usuarioActual?.Email;

            var persona = await _context.TGePersonas.FirstOrDefaultAsync(p => p.Email == emailUsuario);
            if (persona == null)
            {
                return Unauthorized();
            }

            var abogado = await _context.TGeAbogados.FirstOrDefaultAsync(a => a.Cedula == persona.Cedula);
            if (abogado == null)
            {
                return NotFound("No se encontró un abogado vinculado con esta cuenta.");
            }

            var citaDTO = new CitasDTO
            {
                Anfitrion = abogado.Cedula,
                NombreAnfitrion = $"{persona.Nombre} {persona.Apellido1} {persona.Apellido2}"
            };

            return PartialView("~/Views/CitasPrueba/Create.cshtml", citaDTO);
        }


        // POST: Citas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CitasDTO citaDTO)
        {
            // Validar fecha y hora futura
            if (citaDTO.Fecha < DateOnly.FromDateTime(DateTime.Now) ||
                (citaDTO.Fecha == DateOnly.FromDateTime(DateTime.Now) && citaDTO.Hora.ToTimeSpan() < DateTime.Now.TimeOfDay))
            {
                ModelState.AddModelError("Fecha", "La fecha y hora deben ser futuras.");
            }

            // Reasignar anfitrión si es 0
            if (citaDTO.Anfitrion == 0)
            {
                var usuarioActual = await _userManager.GetUserAsync(User);
                var emailUsuario = usuarioActual?.Email;

                var persona = await _context.TGePersonas.FirstOrDefaultAsync(p => p.Email == emailUsuario);

                if (persona != null)
                {
                    var abogado = await _context.TGeAbogados.FirstOrDefaultAsync(a => a.Cedula == persona.Cedula);

                    if (abogado != null)
                    {
                        citaDTO.Anfitrion = abogado.Cedula;
                        citaDTO.NombreAnfitrion = $"{persona.Nombre} {persona.Apellido1} {persona.Apellido2}";
                    }
                }
            }

            // Validar tipo de cita
            if (!await _context.TCitasTipos.AnyAsync(t => t.Id == citaDTO.IdTipoCita))
            {
                ModelState.AddModelError("IdTipoCita", "El tipo de cita seleccionado no es válido.");
            }

            // Validar abogado anfitrión
            if (!await _context.TGeAbogados.AnyAsync(a => a.Cedula == citaDTO.Anfitrion))
            {
                ModelState.AddModelError("Anfitrion", "El abogado indicado no existe en la base de datos.");
            }

            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors = errores });
            }
            if (citaDTO.IdCliente == null || !await _context.TGePersonas.AnyAsync(p => p.Cedula == citaDTO.IdCliente))
            {
                ModelState.AddModelError("IdCliente", "Debe seleccionar un cliente válido.");
            }

            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors = errores });
            }

            try
            {
                var entidad = new TCita
                {
                    Fecha = citaDTO.Fecha,
                    Hora = citaDTO.Hora,
                    IdTipoCita = citaDTO.IdTipoCita,
                    Anfitrion = citaDTO.Anfitrion,
                    LinkVideo = citaDTO.LinkVideo,
                };

                _context.Add(entidad);
                await _context.SaveChangesAsync();

                var relacionClienteCita = new TCitasCliente
                {
                    IdCita = entidad.IdCita,
                    IdCliente = citaDTO.IdCliente.Value
                };
                _context.Add(relacionClienteCita);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    nuevaFecha = citaDTO.Fecha.ToString("yyyy-MM-dd"),
                    nuevaHora = citaDTO.Hora.ToString(@"hh\:mm"),
                });
            }
            catch (Exception ex)
            {
                // Manejo de error
                return PartialView("~/Views/CitasPrueba/Create.cshtml", citaDTO);
            }
        }



        [HttpGet]
        public async Task<IActionResult> ObtenerCitasPorFecha(DateTime fecha)
        {
            DateOnly fechaOnly = DateOnly.FromDateTime(fecha);

            var citas = await (
        from c in _context.TCitas
        join t in _context.TCitasTipos on c.IdTipoCita equals t.Id
        where c.Fecha == fechaOnly
        select new
        {
            idCita = c.IdCita,
            hora = c.Hora.ToString("HH:mm"),
            nombreTipoCita = t.Nombre
        }).ToListAsync();

            return Json(citas);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCitas()
        {
            var citas = await _buscarCitasLN.obtenerTodas();
            Console.WriteLine($"Total citas encontradas: {citas.Count()}");  

            var resultado = citas.Select(c => new {
                idCita = c.IdCita,
                fecha = c.Fecha,
                hora = c.Hora,
                nombreTipoCita = c.NombreTipoCita
            });

            return Json(resultado);
        }




        // GET: Citas/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var cita = (await _listarCitasLN.listar()).FirstOrDefault(c => c.IdCita == id);
            if (cita == null)
                return NotFound();

            var tiposCita = await _listarCitasTipoLN.listar();
            ViewBag.TipoCitaList = tiposCita.Select(n => new SelectListItem
            {
                Value = n.Id.ToString(),
                Text = n.Nombre
            }).ToList();
            if (string.IsNullOrEmpty(cita.NombreAnfitrion))
            {
                var persona = await (
                    from abogado in _context.TGeAbogados
                    join p in _context.TGePersonas on abogado.Cedula equals p.Cedula
                    where abogado.Cedula == cita.Anfitrion
                    select new { p.Nombre, p.Apellido1, p.Apellido2 }
                ).FirstOrDefaultAsync();

                if (persona != null)
                {
                    cita.NombreAnfitrion = $"{persona.Nombre} {persona.Apellido1} {persona.Apellido2}";
                }
            }

            return PartialView("~/Views/CitasPrueba/_EditPartial.cshtml", cita);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CitasDTO cita)
        {
            if (id != cita.IdCita)
                return BadRequest();

            var citaOriginal = await _context.TCitas.FirstOrDefaultAsync(c => c.IdCita == id);
            if (citaOriginal == null)
                return NotFound();

            var fechaAnterior = citaOriginal.Fecha.ToDateTime(citaOriginal.Hora);

            cita.Anfitrion = citaOriginal.Anfitrion;

            var persona = await (
                from abogado in _context.TGeAbogados
                join p in _context.TGePersonas on abogado.Cedula equals p.Cedula
                where abogado.Cedula == cita.Anfitrion
                select new { p.Nombre, p.Apellido1, p.Apellido2 }
            ).FirstOrDefaultAsync();

            if (persona != null)
            {
                cita.NombreAnfitrion = $"{persona.Nombre} {persona.Apellido1} {persona.Apellido2}";
            }

            if (ModelState.IsValid)
            {
                var resultado = await _editarCitasLN.editar(cita);

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
                var tiposCita = await _listarCitasTipoLN.listar();
                ViewBag.TipoCitaList = tiposCita.Select(n => new SelectListItem
                {
                    Value = n.Id.ToString(),
                    Text = n.Nombre
                }).ToList();

                return PartialView("~/Views/CitasPrueba/_EditPartial.cshtml", cita);
            }

        
            return View("~/Views/CitasPrueba/Edit.cshtml", cita);
        }


        // GET: Citas/Details
        public async Task<IActionResult> Details(int id)
        {
            var cita = await _buscarCitasLN.buscar(id);

            if (cita == null)
            {
                Console.WriteLine($"No se encontró cita con ID: {id}");
                return NotFound();
            }
            // Buscar nombres de los clientes relacionados
            var nombresClientes = await _context.TCitasClientes
                .Where(cc => cc.IdCita == id)
                .Include(cc => cc.IdClienteNavigation)
                .Select(cc => cc.IdClienteNavigation.Nombre + " " +
                               cc.IdClienteNavigation.Apellido1 + " " +
                               cc.IdClienteNavigation.Apellido2)
                .ToListAsync();

            cita.NombresClientes = nombresClientes;
            var documentos = await _context.TDocumentosCita
        .Where(d => d.IdCita == id)
        .ToListAsync();

            cita.Documentos = documentos.Select(d => new DocumentosCitaDTO
            {
                Id = d.Id,
                IdCita = d.IdCita,
                NombreArchivo = d.NombreArchivo,
                RutaArchivo = d.RutaArchivo,
                FechaSubida = d.FechaSubida
            }).ToList();

            return PartialView("~/Views/CitasPrueba/_DetailsPartial.cshtml", cita);
        }
        // GET: Citas/Delete
        public async Task<IActionResult> Delete(int id)
        {
            var cita = (await _listarCitasLN.listar()).FirstOrDefault(c => c.IdCita == id);
            if (cita == null)
                return NotFound();

            return PartialView("~/Views/CitasPrueba/_DeletePartial.cshtml", cita);

        }

        // POST: Citas/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int IdCita)
        {
            var cita = (await _listarCitasLN.listar()).FirstOrDefault(c => c.IdCita == IdCita);
            if (cita == null)
            {
                return Json(new { success = false });
            }

            await _eliminarCitasLN.Eliminar(IdCita);

            return Json(new
            {
                success = true,
                fechaAnterior = cita.Fecha.ToString("yyyy-MM-dd"),
                idCita = cita.IdCita
            });

        }

        public async Task<IActionResult> Calendar()
        {
            var lista = await _listarCitasLN.listar();
            return View("~/Views/Citas/Calendar.cshtml", lista); // Asegúrate que sea esta vista, no Json ni Partial
        }


        public async Task<IActionResult> CalendarPasado()
        {
            var lista = await _listarCitasLN.listar();
            var citasPasadas = lista.Where(c => c.FechaHora < DateTime.Now).ToList();
            return View(citasPasadas);
        }

        public async Task<IActionResult> _CitaFuturo()
        {
            var lista = await _listarCitasLN.listar();
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

            // Buscar persona por email
            var persona = await _context.TGePersonas
                .FirstOrDefaultAsync(p => p.Email == emailUsuario);

            if (persona == null)
                return NotFound("No se encontró una persona asociada a este email.");

            // Pasamos el ID de la persona, no la cédula
            var citas = await _listarCitasLN.ListarPorIdCliente(persona.Cedula);

            return View("~/Views/Citas/Calendar.cshtml", citas);
        }


        public async Task<List<CitasDTO>> ListarPorIdCliente(int idCliente)
        {
            var citas = await (
                from cc in _context.TCitasClientes
                join c in _context.TCitas on cc.IdCita equals c.IdCita
                join t in _context.TCitasTipos on c.IdTipoCita equals t.Id
                where cc.IdCliente == idCliente
                select new CitasDTO
                {
                    IdCita = c.IdCita,
                    Fecha = c.Fecha,
                    Hora = c.Hora,
                    IdTipoCita = c.IdTipoCita,
                    NombreTipoCita = t.Nombre,
                    Anfitrion = c.Anfitrion,
                    LinkVideo = c.LinkVideo,
                }
            ).ToListAsync();

            return citas;
        }



        /*List<ModuloCitasVideo> listaCitas = new List<ModuloCitasVideo>()
{
    new ModuloCitasVideo()
    {
        NumCita = 1,
        Evento = "Revisión de contrato laboral",
        FechaHora = new DateTime(2025, 03, 31, 09, 00, 00),
        tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 2,
        Evento = "Consulta con cliente sobre propiedad",
        FechaHora = new DateTime(2025, 03, 31, 10, 30, 00),
         tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 3,
        Evento = "Audiencia judicial civil",
        FechaHora = new DateTime(2025, 04, 03, 13, 00, 00),
        tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 4,
        Evento = "Redacción de escritura pública",
        FechaHora = new DateTime(2025, 04, 05, 15, 45, 00),
         tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 5,
        Evento = "Revisión de contrato de alquiler",
        FechaHora = new DateTime(2025, 04, 07, 08, 15, 00),
        tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 6,
        Evento = "Consulta sobre herencias",
        FechaHora = new DateTime(2025, 04, 10, 11, 00, 00),
         tipo = "Virtual",
    },
    new ModuloCitasVideo()
    {
        NumCita = 7,
        Evento = "Firma de acuerdo judicial",
        FechaHora = new DateTime(2025, 04, 12, 14, 30, 00),
         tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 8,
        Evento = "Elaboración de apelación",
        FechaHora = new DateTime(2025, 04, 15, 16, 00, 00),
         tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 9,
        Evento = "Audiencia de conciliación",
        FechaHora = new DateTime(2025, 04, 18, 09, 45, 00),
         tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 10,
        Evento = "Negociación de contrato comercial",
        FechaHora = new DateTime(2025, 04, 20, 10, 15, 00),
         tipo = "Virtual",
    },
    new ModuloCitasVideo()
    {
        NumCita = 11,
        Evento = "Preparación de documentos notariales",
        FechaHora = new DateTime(2025, 04, 23, 15, 00, 00),
         tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 12,
        Evento = "Revisión de demandas",
        FechaHora = new DateTime(2025, 04, 25, 13, 15, 00),
         tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 13,
        Evento = "Firma de documentos de compraventa",
        FechaHora = new DateTime(2025, 04, 28, 09, 30, 00),
         tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 14,
        Evento = "Consulta con cliente corporativo",
        FechaHora = new DateTime(2025, 05, 01, 11, 45, 00),
         tipo = "Virtual",
    },
    new ModuloCitasVideo()
    {
        NumCita = 15,
        Evento = "Audiencia penal",
        FechaHora = new DateTime(2025, 05, 05, 10, 00, 00),
         tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 16,
        Evento = "Revisión de contrato de servicio",
        FechaHora = new DateTime(2025, 05, 08, 14, 30, 00),
        tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 17,
        Evento = "Asesoría legal sobre bienes raíces",
        FechaHora = new DateTime(2025, 05, 12, 08, 45, 00),
        tipo = "Virtual",
    },
    new ModuloCitasVideo()
    {
        NumCita = 18,
        Evento = "Firma de documentos judiciales",
        FechaHora = new DateTime(2025, 05, 15, 13, 00, 00),
        tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 19,
        Evento = "Estrategia para un caso civil",
        FechaHora = new DateTime(2025, 05, 20, 15, 15, 00),
        tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 20,
        Evento = "Consulta sobre derecho fiscal",
        FechaHora = new DateTime(2025, 05, 31, 10, 30, 00),
        tipo = "Virtual",
    }
};*/


    }
}
