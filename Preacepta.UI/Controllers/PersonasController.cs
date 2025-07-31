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


        public PersonasController(IListarGePersonaLN listarGePersonaLN,
            IBuscarXidGePersonaLN buscarXidGePersonaLN,
            ICrearGePersonaLN crearGePersonaLN,
            IEditarGePersonaLN editarGePersonaLN,
            IEliminarPersonaLN eliminarPersonaLN,
            IListarCrDireccion1LN listarDireccion)
        {
            _listarPersona = listarGePersonaLN;
            _buscarPersona = buscarXidGePersonaLN;
            _crearPesona = crearGePersonaLN;
            _editarPersona = editarGePersonaLN;
            _eliminarPersona = eliminarPersonaLN;
            _listarDireccion = listarDireccion;
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
                var existe = await _buscarPersona.buscar(tGePersona.Cedula);
                if (existe == null)//valida si hay un cedula igual registrada
                {
                    var correo = await _buscarPersona.buscarXcorreo(tGePersona.Email);
                    if (correo == null)//valida si hay un correo igual registrado
                    {
                        await _crearPesona.crear(tGePersona);//llamado de los LN y AD para crear la persona
                        TempData["PersonaCreada"] = "Se ha creado un nuevo usuario en el sistema";
                        return RedirectToAction("UsuarioAutenticado", "Home", new { correo = User.Identity.Name });
                    }
                    else
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
                }
                else
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
                    TempData["ErrorCedula"] = "Cedula ya registrada en el sistema";
                    return View(tGePersona);
                }
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
        [Authorize(Roles = "Gestor")]        
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

            await _eliminarPersona.eliminar(id);
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



