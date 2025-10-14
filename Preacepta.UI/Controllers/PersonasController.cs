using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.LN.CrDireccion1.Listar;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.LN.GePersona.Crear;
using Preacepta.LN.GePersona.Editar;
using Preacepta.LN.GePersona.Eliminar;
using Preacepta.LN.GePersona.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using Preacepta.LN.BitacoraEventos.Crear;

namespace Preacepta.UI.Controllers
{
    [Authorize]

    public class PersonasController : Controller
    {
        private readonly IListarGePersonaLN _listarPersona;
        private readonly IBuscarXidGePersonaLN _buscarPersona;
        private readonly ICrearGePersonaLN _crearPesona;
        private readonly IEditarGePersonaLN _editarPersona;
        private readonly IEliminarPersonaLN _eliminarPersona;
        private readonly IListarCrDireccion1LN _listarDireccion;
        private readonly ICrearEventosLN _bitacoraLN;

        public PersonasController(IListarGePersonaLN listarGePersonaLN,
            IBuscarXidGePersonaLN buscarXidGePersonaLN,
            ICrearGePersonaLN crearGePersonaLN,
            IEditarGePersonaLN editarGePersonaLN,
            IEliminarPersonaLN eliminarPersonaLN,
            IListarCrDireccion1LN listarDireccion,
            ICrearEventosLN bitacora)
        {
            _listarPersona = listarGePersonaLN;
            _buscarPersona = buscarXidGePersonaLN;
            _crearPesona = crearGePersonaLN;
            _editarPersona = editarGePersonaLN;
            _eliminarPersona = eliminarPersonaLN;
            _listarDireccion = listarDireccion;
            _bitacoraLN = bitacora;
        }


        /********************************************************************************************************************************************************************/
        //controller personalizados\\
        /********************************************************************************************************************************************************************/

        #region Crear persona GET
        // GET: TGePersonas/CrearPersona, muestra el formulario de CrearPersona.cshtml
        [Authorize(Roles = "Gestor, Abogado")]
        public IActionResult CrearPersona()
        {
            ViewData["Direccion1"] = new SelectList(_listarDireccion.listarDistritos().Result, "IdDistrito", "NombreDistrito");

            ViewBag.EstadoCivil = new List<SelectListItem>
            {
                new SelectListItem { Text = "Soltero", Value = "Soltero" },
                new SelectListItem { Text = "Casado", Value = "Casado" },
                new SelectListItem { Text = "Divorciado", Value = "Divorciado" },
                new SelectListItem { Text = "Viudo", Value = "Viudo" }
            };
            ViewBag.Genero = new List<SelectListItem>
                        {
                            new SelectListItem { Text = "Femenino", Value = "Femenino" },
                            new SelectListItem { Text = "Masculino", Value = "Masculino" },
                        };
            return View();
        }
        #endregion

        #region Crear persona POST
        // POST: TGePersonas/CrearPersona, controller para la vista de CrearPersona.cshtml
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<IActionResult> CrearPersona([Bind("Cedula,Nombre,Apellido1,Apellido2,FechaNacimiento,Edad,EstadoCivil,Oficio,Direccion1,Direccion2,FechaRegistro,Telefono1,Telefono2,Activo,Email,Password,ConfirmPassword,Genero")] GePersonaDTO tGePersona)

        {
            if (ModelState.IsValid)//validacion de formulario
            {
                #region Validacion de cédula
                var existe = await _buscarPersona.buscar(tGePersona.Cedula);
                if (existe != null)//valida si hay un cedula igual registrada
                {

                    var correo = await _buscarPersona.buscarXcorreo(tGePersona.Email);
                    if (correo == null)//valida si hay un correo igual registrado
                    {
                        await _crearPesona.crear(tGePersona);//llamado de los LN y AD para crear la persona

                        // Registrar evento en la bitácora
                        var usuario = User.Identity?.Name ?? "Desconocido";
                        var nombreCompleto = $"{tGePersona.Nombre} {tGePersona.Apellido1} {tGePersona.Apellido2}".Trim();
                        var descripcion = $"Se creó una nueva persona: {nombreCompleto} (Cédula: {tGePersona.Cedula}, Email: {tGePersona.Email})";

                        await _bitacoraLN.RegistrarBitacoraAsync(usuario, "Creación de persona", descripcion, tGePersona.Cedula);


                        TempData["PersonaCreada"] = "Se ha creado un nuevo usuario en el sistema";
                        return RedirectToAction("UsuarioAutenticado", "Home", new { correo = User.Identity.Name });
                    }
                    else

                    ViewData["Direccion1"] = new SelectList(_listarDireccion.listarDistritos().Result, "IdDistrito", "NombreDistrito", tGePersona.Direccion1);

                    ViewBag.EstadoCivil = new List<SelectListItem>
                    {
                        new SelectListItem { Text = "Soltero", Value = "Soltero" },
                        new SelectListItem { Text = "Casado", Value = "Casado" },
                        new SelectListItem { Text = "Divorciado", Value = "Divorciado" },
                        new SelectListItem { Text = "Viudo", Value = "Viudo" }
                    };
                    ViewBag.Genero = new List<SelectListItem>
                        {
                            new SelectListItem { Text = "Femenino", Value = "Femenino" },
                            new SelectListItem { Text = "Masculino", Value = "Masculino" },
                        };
                    TempData["ErrorCedula"] = "Cedula ya registrada en el sistema";
                    return View(tGePersona);
                }
                #endregion

                #region Validacion de correo
                var correo = await _buscarPersona.buscarXcorreo(tGePersona.Email);
                if (correo != null)//valida si hay un correo igual registrado
                {
                    ViewData["Direccion1"] = new SelectList(_listarDireccion.listarDistritos().Result, "IdDistrito", "NombreDistrito", tGePersona.Direccion1);

                    ViewBag.EstadoCivil = new List<SelectListItem>
                        {
                            new SelectListItem { Text = "Soltero", Value = "Soltero" },
                            new SelectListItem { Text = "Casado", Value = "Casado" },
                            new SelectListItem { Text = "Divorciado", Value = "Divorciado" },
                            new SelectListItem { Text = "Viudo", Value = "Viudo" }
                        };

                    ViewBag.Genero = new List<SelectListItem>
                        {
                            new SelectListItem { Text = "Femenino", Value = "Femenino" },
                            new SelectListItem { Text = "Masculino", Value = "Masculino" },
                        };

                    TempData["ErrorEmail"] = "Correo Electronico ya registrado en el sistema";
                    return View(tGePersona);
                }
                #endregion

                #region Validacion de teléfonos
                var telefono1 = await _buscarPersona.buscarXtelefono1(tGePersona.Telefono1);
                var telefono2 = await _buscarPersona.buscarXtelefono1(tGePersona.Telefono2);
                if(telefono1 != null) 
                {
                    ViewData["Direccion1"] = new SelectList(_listarDireccion.listarDistritos().Result, "IdDistrito", "NombreDistrito", tGePersona.Direccion1);

                    ViewBag.EstadoCivil = new List<SelectListItem>
                        {
                            new SelectListItem { Text = "Soltero", Value = "Soltero" },
                            new SelectListItem { Text = "Casado", Value = "Casado" },
                            new SelectListItem { Text = "Divorciado", Value = "Divorciado" },
                            new SelectListItem { Text = "Viudo", Value = "Viudo" }
                        };

                    ViewBag.Genero = new List<SelectListItem>
                        {
                            new SelectListItem { Text = "Femenino", Value = "Femenino" },
                            new SelectListItem { Text = "Masculino", Value = "Masculino" },
                        };

                    TempData["ErrorTelefono1"] = $"El telefono {tGePersona.Telefono1} ya esta registrado";
                    return View(tGePersona);

                }
                if (telefono2 != null)
                {
                    ViewData["Direccion1"] = new SelectList(_listarDireccion.listarDistritos().Result, "IdDistrito", "NombreDistrito", tGePersona.Direccion1);

                    ViewBag.EstadoCivil = new List<SelectListItem>
                        {
                            new SelectListItem { Text = "Soltero", Value = "Soltero" },
                            new SelectListItem { Text = "Casado", Value = "Casado" },
                            new SelectListItem { Text = "Divorciado", Value = "Divorciado" },
                            new SelectListItem { Text = "Viudo", Value = "Viudo" }
                        };

                    ViewBag.Genero = new List<SelectListItem>
                        {
                            new SelectListItem { Text = "Femenino", Value = "Femenino" },
                            new SelectListItem { Text = "Masculino", Value = "Masculino" },
                        };

                    TempData["ErrorTelefono2"] = $"El telefono {tGePersona.Telefono2} ya esta registrado";
                    return View(tGePersona);

                }
                #endregion

                await _crearPesona.crear(tGePersona);//llamado de los LN y AD para crear la persona
                TempData["PersonaCreada"] = "Se ha creado un nuevo usuario en el sistema";
                return RedirectToAction("UsuarioAutenticado", "Home", new { correo = User.Identity.Name });                
            }
            ViewData["Direccion1"] = new SelectList(_listarDireccion.listarDistritos().Result, "IdDistrito", "NombreDistrito", tGePersona.Direccion1);

            ViewBag.EstadoCivil = new List<SelectListItem>
            {
                new SelectListItem { Text = "Soltero", Value = "Soltero" },
                new SelectListItem { Text = "Casado", Value = "Casado" },
                new SelectListItem { Text = "Divorciado", Value = "Divorciado" },
                new SelectListItem { Text = "Viudo", Value = "Viudo" }
            };
            ViewBag.Genero = new List<SelectListItem>
                        {
                            new SelectListItem { Text = "Femenino", Value = "Femenino" },
                            new SelectListItem { Text = "Masculino", Value = "Masculino" },
                        };
            return View(tGePersona);

        }
        #endregion

        #region DetallesPersona
        // GET: TGePersonas/Details/5
        [Authorize(Roles = "Abogado")]
        public async Task<IActionResult> DetallesPersona(int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var tGePersona = await _buscarPersona.buscar(id);

            if (tGePersona == null)
            {
                var noEncontrado = new
                {
                    mensaje = "El usuario no se encuentra en nuestros registros",
                    Bandera = false
                };
                return Json(noEncontrado);
            }

            var datos = new
            {
                Bandera = true,
                Cedula = tGePersona.Cedula,
                Nombre = tGePersona.Nombre,
                Apellido1 = tGePersona.Apellido1,
                Apellido2 = tGePersona.Apellido2,
                Ocupacion = tGePersona.Oficio,
                Telefono = tGePersona.Telefono1,
                Correo = tGePersona.Email
            };

            return Json(datos);
        }
        #endregion

        /********************************************************************************************************************************************************************/
        //controller de Framework\\
        /********************************************************************************************************************************************************************/


        // GET: TGePersonas
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Index()
        {
            return View(await _listarPersona.listar());
        }

        // GET: TGePersonas/Details/5
        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tGePersona = await _buscarPersona.buscar(id);
            if (tGePersona == null)
            {
                return NotFound();
            }

            return View(tGePersona);
        }

        // GET: TGePersonas/Create
        [Authorize(Roles = "Gestor")]
        public IActionResult Create()
        {

            ViewData["Direccion1"] = new SelectList(_listarDireccion.listarDistritos().Result, "IdDistrito", "NombreDistrito");

            ViewBag.EstadoCivil = new List<SelectListItem>
        {
            new SelectListItem { Text = "Soltero", Value = "Soltero" },
            new SelectListItem { Text = "Casado", Value = "Casado" },
            new SelectListItem { Text = "Divorciado", Value = "Divorciado" },
            new SelectListItem { Text = "Viudo", Value = "Viudo" }
        };
            ViewBag.Genero = new List<SelectListItem>
                        {
                            new SelectListItem { Text = "Femenino", Value = "Femenino" },
                            new SelectListItem { Text = "Masculino", Value = "Masculino" },
                        };

            return View();
        }

        // POST: TGePersonas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Create([Bind("Cedula,Nombre,Apellido1,Apellido2,FechaNacimiento,Edad,EstadoCivil,Oficio,Direccion1,Direccion2,FechaRegistro,Telefono1,Telefono2,Activo,Email,Password,ConfirmPassword")] GePersonaDTO tGePersona)
        {
            if (ModelState.IsValid)
            {
                /*_context.Add(tGePersona);
                await _context.SaveChangesAsync();*/
                await _crearPesona.crear(tGePersona);

                // Registrar evento en la bitácora
                var usuario = User.Identity?.Name ?? "Desconocido";
                var nombreCompleto = $"{tGePersona.Nombre} {tGePersona.Apellido1} {tGePersona.Apellido2}".Trim();
                var descripcion = $"Se creó una nueva persona: {nombreCompleto} (Cédula: {tGePersona.Cedula}, Email: {tGePersona.Email})";

                await _bitacoraLN.RegistrarBitacoraAsync(usuario, "Creación de persona", descripcion, tGePersona.Cedula);

                return RedirectToAction(nameof(Index));
            }
            ViewData["Direccion1"] = new SelectList(_listarDireccion.listarDistritos().Result, "IdDistrito", "NombreDistrito", tGePersona.Direccion1);

            ViewBag.EstadoCivil = new List<SelectListItem>
        {
            new SelectListItem { Text = "Soltero", Value = "Soltero" },
            new SelectListItem { Text = "Casado", Value = "Casado" },
            new SelectListItem { Text = "Divorciado", Value = "Divorciado" },
            new SelectListItem { Text = "Viudo", Value = "Viudo" }
        };
            ViewBag.Genero = new List<SelectListItem>
                        {
                            new SelectListItem { Text = "Femenino", Value = "Femenino" },
                            new SelectListItem { Text = "Masculino", Value = "Masculino" },
                        };
            return View(tGePersona);
        }

        // GET: TGePersonas/Edit/5
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tGePersona = await _buscarPersona.buscar(id);
            if (tGePersona == null)
            {
                return NotFound();
            }
            ViewData["Direccion1"] = new SelectList(_listarDireccion.listarDistritos().Result, "IdDistrito", "NombreDistrito", tGePersona.Direccion1);

            ViewBag.EstadoCivil = new List<SelectListItem>
        {
            new SelectListItem { Text = "Soltero", Value = "Soltero" },
            new SelectListItem { Text = "Casado", Value = "Casado" },
            new SelectListItem { Text = "Divorciado", Value = "Divorciado" },
            new SelectListItem { Text = "Viudo", Value = "Viudo" }
        };
            ViewBag.Genero = new List<SelectListItem>
                        {
                            new SelectListItem { Text = "Femenino", Value = "Femenino" },
                            new SelectListItem { Text = "Masculino", Value = "Masculino" },
                        };

            return View(tGePersona);
        }

        // POST: TGePersonas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int id, [Bind("Cedula,Nombre,Apellido1,Apellido2,FechaNacimiento,Edad,EstadoCivil,Oficio,Direccion1,Direccion2,Telefono1,Telefono2,FechaRegistro,Activo,Genero,Email,Password,ConfirmPassword")] GePersonaDTO tGePersona)
        {
            if (id != tGePersona.Cedula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editarPersona.editar(tGePersona);

                    // Registrar evento en bitácora
                    var usuario = User.Identity?.Name ?? "Desconocido";
                    var nombreCompleto = $"{tGePersona.Nombre} {tGePersona.Apellido1} {tGePersona.Apellido2}".Trim();
                    var descripcion = $"El usuario {usuario} actualizó los datos de la persona {nombreCompleto} (Cédula: {tGePersona.Cedula}).";

                    await _bitacoraLN.RegistrarBitacoraAsync(usuario, "Edición de persona", descripcion, tGePersona.Cedula);

                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Direccion1"] = new SelectList(_listarDireccion.listarDistritos().Result, "IdDistrito", "NombreDistrito", tGePersona.Direccion1);

            ViewBag.EstadoCivil = new List<SelectListItem>
        {
            new SelectListItem { Text = "Soltero", Value = "Soltero" },
            new SelectListItem { Text = "Casado", Value = "Casado" },
            new SelectListItem { Text = "Divorciado", Value = "Divorciado" },
            new SelectListItem { Text = "Viudo", Value = "Viudo" }
        };
            ViewBag.Genero = new List<SelectListItem>
                        {
                            new SelectListItem { Text = "Femenino", Value = "Femenino" },
                            new SelectListItem { Text = "Masculino", Value = "Masculino" },
                        };
            return View(tGePersona);
        }

        // GET: TGePersonas/Delete/5
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tGePersona = await _buscarPersona.buscar(id);
            if (tGePersona == null)
            {
                return NotFound();
            }

            return View(tGePersona);
        }

        // POST: TGePersonas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            int resultado = await _eliminarPersona.eliminar(id);
            if (resultado == 0) 
            {
                TempData["ErrorElimanacion"] = "El usuario no pudo ser eliminado";
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<JsonResult> IdExiste(int id)
        {
            bool bandera;
            var ObjetoBuscado = await _buscarPersona.buscar(id);
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