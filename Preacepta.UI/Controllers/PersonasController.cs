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
using Microsoft.AspNetCore.Identity;
using Preacepta.UI.Areas.Identity.Pages.Account.Manage;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using Preacepta.UI.Services;
using System.Security.Claims;
using Praecepta.UI.Areas.Identity.Pages.Account;
using Preacepta.UI.Extensions;
using Preacepta.Modelos.AbstraccionesBD;

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


        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<PersonasController> _logger;
        private readonly IServicioEmail _emailSender;


        public PersonasController(IListarGePersonaLN listarGePersonaLN,
            IBuscarXidGePersonaLN buscarXidGePersonaLN,
            ICrearGePersonaLN crearGePersonaLN,
            IEditarGePersonaLN editarGePersonaLN,
            IEliminarPersonaLN eliminarPersonaLN,
            IListarCrDireccion1LN listarDireccion,
            ICrearEventosLN bitacora,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<PersonasController> logger,
            IServicioEmail emailSender)
        {
            _listarPersona = listarGePersonaLN;
            _buscarPersona = buscarXidGePersonaLN;
            _crearPesona = crearGePersonaLN;
            _editarPersona = editarGePersonaLN;
            _eliminarPersona = eliminarPersonaLN;
            _listarDireccion = listarDireccion;
            _bitacoraLN = bitacora;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
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

            ViewBag.TipoIdentificacion = new List<SelectListItem>
            {
                new SelectListItem { Text = "Cédula física", Value = "Cedula" },
                new SelectListItem { Text = "DIMEX", Value = "DIMEX" },
                new SelectListItem { Text = "Pasaporte", Value = "Pasaporte" },
                new SelectListItem { Text = "Sin documento de identificación", Value = "SinDocumento" },
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
                    //ViewData["Direccion1"] = new SelectList(_listarDireccion.listarDistritos().Result, "IdDistrito", "NombreDistrito", tGePersona.Direccion1);

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
                        new SelectListItem { Text = "Masculino", Value = "Masculino" }
                    };
                    ViewBag.TipoIdentificacion = new List<SelectListItem>
                    {
                        new SelectListItem { Text = "Cédula física", Value = "Cedula" },
                        new SelectListItem { Text = "DIMEX", Value = "DIMEX" },
                        new SelectListItem { Text = "Pasaporte", Value = "Pasaporte" },
                        new SelectListItem { Text = "Sin documento de identificación", Value = "SinDocumento" },
                    };
                    ViewBag.TempData["ErrorCedula"] = "Cedula ya registrada en el sistema";
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
                    ViewBag.TipoIdentificacion = new List<SelectListItem>
                    {
                        new SelectListItem { Text = "Cédula física", Value = "Cédula física" },
                        new SelectListItem { Text = "DIMEX", Value = "DIMEX" },
                        new SelectListItem { Text = "Pasaporte", Value = "Pasaporte" },
                        new SelectListItem { Text = "Sin documento de identificación", Value = "SinDocumento" },
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
                    ViewBag.TipoIdentificacion = new List<SelectListItem>
                    {
                        new SelectListItem { Text = "Cédula física", Value = "Cédula física" },
                        new SelectListItem { Text = "DIMEX", Value = "DIMEX" },
                        new SelectListItem { Text = "Pasaporte", Value = "Pasaporte" },
                        new SelectListItem { Text = "Sin documento de identificación", Value = "SinDocumento" },
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

                    ViewBag.TipoIdentificacion = new List<SelectListItem>
                    {
                        new SelectListItem { Text = "Cédula física", Value = "Cédula física" },
                        new SelectListItem { Text = "DIMEX", Value = "DIMEX" },
                        new SelectListItem { Text = "Pasaporte", Value = "Pasaporte" },
                        new SelectListItem { Text = "Sin documento de identificación", Value = "SinDocumento" },
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
            ViewBag.TipoIdentificacion = new List<SelectListItem>
            {
                new SelectListItem { Text = "Cédula física", Value = "Cédula física" },
                new SelectListItem { Text = "DIMEX", Value = "DIMEX" },
                new SelectListItem { Text = "Pasaporte", Value = "Pasaporte" },
                new SelectListItem { Text = "Sin documento de identificación", Value = "SinDocumento" },
            };
            return View(tGePersona);

        }
        #endregion

        #region DetallesPersona
        // GET: TGePersonas/Details/5
        [Authorize(Roles = "Abogado")]
        public async Task<IActionResult> DetallesPersona(string id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var tGePersona = await _buscarPersona.buscarXnumCedula(id);

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
                Cedula = tGePersona.NumCedula,
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

        #region Listar Personas
        // GET: TGePersonas
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Index()
        {
            return View(await _listarPersona.listar());
        }
        #endregion

        #region Buscar X Id Persona

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
        #endregion

        #region Crear persona GET
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
            ViewBag.TipoIdentificacion = new List<SelectListItem>
            {
                new SelectListItem { Text = "Cédula física", Value = "Cédula física" },
                new SelectListItem { Text = "DIMEX", Value = "DIMEX" },
                new SelectListItem { Text = "Pasaporte", Value = "Pasaporte" },
                new SelectListItem { Text = "Sin documento de identificación", Value = "SinDocumento" },
            };

            return View();
        }
        #endregion

        #region Crear Persona POST
        // POST: TGePersonas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Create([Bind("NumCedula,TipoIdentificacion,Nombre,Apellido1,Apellido2,FechaNacimiento,Edad,EstadoCivil,Oficio,Direccion1,Direccion2,FechaRegistro,Telefono1,Telefono2,Activo,Email,Password,ConfirmPassword")] GePersonaDTO tGePersona)
        {
            if (ModelState.IsValid)
            {                
                await _crearPesona.crear(tGePersona);

               
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
            ViewBag.TipoIdentificacion = new List<SelectListItem>
            {
                new SelectListItem { Text = "Cédula física", Value = "Cédula física" },
                new SelectListItem { Text = "DIMEX", Value = "DIMEX" },
                new SelectListItem { Text = "Pasaporte", Value = "Pasaporte" },
                new SelectListItem { Text = "Sin documento de identificación", Value = "SinDocumento" },
            };
            return View(tGePersona);
        }
        #endregion

        #region Editar Persona GET
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
            /*ViewData["Direccion1"] = new SelectList(_listarDireccion.listarDistritos().Result, "IdDistrito", "NombreDistrito", tGePersona.Direccion1);

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
            ViewBag.TipoIdentificacion = new List<SelectListItem>
            {
                new SelectListItem { Text = "Cédula física", Value = "Cédula física" },
                new SelectListItem { Text = "DIMEX", Value = "DIMEX" },
                new SelectListItem { Text = "Pasaporte", Value = "Pasaporte" },
                new SelectListItem { Text = "Sin documento de identificación", Value = "SinDocumento" },
            };*/

            ViewBag.EstadoCivil = SelectListPersonas.EstadoCivil;
            ViewBag.Genero = SelectListPersonas.Genero;
            ViewBag.TipoIdentificacion = SelectListPersonas.TipoIdentificacion;

            return View(tGePersona);
        }
        #endregion

        #region Editar Persona POST
        // POST: TGePersonas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int id, [Bind("Cedula,NumCedula,TipoIdentificacion,Nombre,Apellido1,Apellido2,FechaNacimiento,Edad,EstadoCivil,Oficio,Direccion1,Direccion2,Telefono1,Telefono2,FechaRegistro,Activo,Genero,Email,Password,ConfirmPassword")] GePersonaDTO tGePersona, string returnUrl = null)
        {
            if (id != tGePersona.Cedula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    #region Validar Si existe Persona
                    var existe = await _buscarPersona.buscar(tGePersona.Cedula);
                    tGePersona.NumCedula = tGePersona.NumCedula.LimpiarCedula();
                    if (existe.NumCedula != tGePersona.NumCedula) 
                    {
                        bool? cambiarCedula = await _buscarPersona.buscarXnumCedulaBOOLEAN(tGePersona.NumCedula);
                        if (cambiarCedula.Equals(true)) 
                        {
                            ViewBag.EstadoCivil = SelectListPersonas.EstadoCivil;
                            ViewBag.Genero = SelectListPersonas.Genero;
                            ViewBag.TipoIdentificacion = SelectListPersonas.TipoIdentificacion;

                            TempData["ErrorCedula"] = "Cedula ya registrada en el sistema";
                            return View(tGePersona);
                        }
                    }                   
                    #endregion

                    #region Validacion de correo
                    
                    if (existe.Email != tGePersona.Email)
                    {
                        var correo = await _buscarPersona.buscarXcorreo(tGePersona.Email);
                        if(correo != null) 
                        {
                            ViewBag.EstadoCivil = SelectListPersonas.EstadoCivil;
                            ViewBag.Genero = SelectListPersonas.Genero;
                            ViewBag.TipoIdentificacion = SelectListPersonas.TipoIdentificacion;

                            TempData["ErrorEmail"] = "Correo Electronico ya registrado en el sistema";
                            return View(tGePersona);
                        }
                    }
                    /*if (correo != null)//valida si hay un correo igual registrado
                    {
                        //ViewData["Direccion1"] = new SelectList(_listarDireccion.listarDistritos().Result, "IdDistrito", "NombreDistrito", tGePersona.Direccion1);

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
                        new SelectListItem { Text = "Masculino", Value = "Masculino" }
                    };

                        ViewBag.TipoIdentificacion = new List<SelectListItem>
                    {
                        new SelectListItem { Text = "Cédula física", Value = "Cedula" },
                        new SelectListItem { Text = "DIMEX", Value = "DIMEX" },
                        new SelectListItem { Text = "Pasaporte", Value = "Pasaporte" },
                        new SelectListItem { Text = "Sin documento de identificación", Value = "SinDocumento" },
                    };
                        TempData["ErrorEmail"] = "Correo Electronico ya registrado en el sistema";
                        return View(tGePersona);
                    }*/
                    #endregion

                    #region Validacion de teléfonos
                    var telefono1 = await _buscarPersona.buscarXtelefono1(tGePersona.Telefono1);
                    if (telefono1.Cedula == tGePersona.Cedula && telefono1.Telefono1 == tGePersona.Telefono1)
                    {
                        telefono1 = null;
                    }
                    if (telefono1 != null)
                    {
                        //ViewData["Direccion1"] = new SelectList(_listarDireccion.listarDistritos().Result, "IdDistrito", "NombreDistrito", tGePersona.Direccion1);

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
                        new SelectListItem { Text = "Masculino", Value = "Masculino" }
                    };

                        ViewBag.TipoIdentificacion = new List<SelectListItem>
                    {
                        new SelectListItem { Text = "Cédula física", Value = "Cedula" },
                        new SelectListItem { Text = "DIMEX", Value = "DIMEX" },
                        new SelectListItem { Text = "Pasaporte", Value = "Pasaporte" },
                        new SelectListItem { Text = "Sin documento de identificación", Value = "SinDocumento" },
                    };
                        TempData["ErrorTelefono1"] = $"El telefono {tGePersona.Telefono1} ya esta registrado";
                        return View(tGePersona);

                    }
                    #endregion

                    //tGePersona.NumCedula = tGePersona.NumCedula.LimpiarCedula();
                    int bandera = await _editarPersona.editar(tGePersona);
                    if(bandera == 0) 
                    {
                        return BadRequest();
                    }
                    // Registrar evento en bitácora
                    var usuario = User.Identity?.Name ?? "Desconocido";
                    var nombreCompleto = $"{tGePersona.Nombre} {tGePersona.Apellido1} {tGePersona.Apellido2}".Trim();
                    var descripcion = $"El usuario {usuario} actualizó los datos de la persona {nombreCompleto} (Cédula: {tGePersona.Cedula}).";

                    await _bitacoraLN.RegistrarBitacoraAsync(usuario, "Edición de persona", descripcion, tGePersona.Cedula);

                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var user = await _userManager.FindByIdAsync(userId);

                    var userNameResult = await _userManager.SetUserNameAsync(user, tGePersona.Email);
                    var emailResult = await _userManager.SetEmailAsync(user, tGePersona.Email);
                    if (userNameResult.Succeeded && emailResult.Succeeded)
                    {                        
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(tGePersona.Email, "Confirma tu correo electrónico",
                            $"Por favor, confirme su cuenta mediante <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>haciendo clic aquí</a>.");

                        await _signInManager.RefreshSignInAsync(user); // Refresca sesión si es necesario
                        _logger.LogInformation($"Usuario {user.Id} actualizado: UserName y Email sincronizados.");

                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("RegisterConfirmation", new { email = tGePersona.Email, returnUrl = returnUrl });
                            
                        }
                        else
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return LocalRedirect(returnUrl);
                        }
                       
                    }
                    else
                    {
                        var errores = userNameResult.Errors.Concat(emailResult.Errors);
                        foreach (var error in errores)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
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

                        ViewBag.TipoIdentificacion = new List<SelectListItem>
                        {
                            new SelectListItem { Text = "Cédula física", Value = "Cédula física" },
                            new SelectListItem { Text = "DIMEX", Value = "DIMEX" },
                            new SelectListItem { Text = "Pasaporte", Value = "Pasaporte" },
                            new SelectListItem { Text = "Sin documento de identificación", Value = "SinDocumento" },
                        };
                        return View(tGePersona);

                    }


                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                //return RedirectToAction(nameof(Index));
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
            ViewBag.TipoIdentificacion = new List<SelectListItem>
            {
                new SelectListItem { Text = "Cédula física", Value = "Cédula física" },
                new SelectListItem { Text = "DIMEX", Value = "DIMEX" },
                new SelectListItem { Text = "Pasaporte", Value = "Pasaporte" },
                new SelectListItem { Text = "Sin documento de identificación", Value = "SinDocumento" },
            };
            return View(tGePersona);
        }
        #endregion

        #region Eliminar Persona GET
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
        #endregion

        #region Eliminar Persona POST
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

        #endregion

        #region Comprobar Si existe

        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<JsonResult> IdExiste(string id)
        {
            int bandera;
            var ObjetoBuscado = await _buscarPersona.buscarXnumCedula(id);
            bandera = ObjetoBuscado.Cedula;
            if (ObjetoBuscado != null)
            {
                //bandera = true;
                return Json(new { bandera });
            }
            //bandera = false;
            return Json(new { bandera });
        }
        #endregion
        
        
    }
}