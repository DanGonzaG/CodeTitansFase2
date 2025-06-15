using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.LN.CrDireccion1.Listar;
using Preacepta.LN.GeAbogado.BuscarXid;
using Preacepta.LN.GeAbogado.Crear;
using Preacepta.LN.GeAbogado.Editar;
using Preacepta.LN.GeAbogado.Eliminar;
using Preacepta.LN.GeAbogado.Listar;
using Preacepta.LN.GeAbogadoTipo.Listar;
using Preacepta.LN.GeNegocio.Listar;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.LN.GePersona.Listar;
using Preacepta.Modelos.AbstraccionesFrond;


namespace Preacepta.UI.Controllers
{
    [Authorize]
    public class AbogadoController : Controller
    {
        //crud Gestion General Abodados
        private readonly IBuscarAbogadoLN _buscar;
        private readonly ICrearAbogadoLN _crear;
        private readonly IEditarAbogadoLN _editar;
        private readonly IEliminarAbogadoLN _eliminar;
        private readonly IListarAbogadoLN _listar;
        //Listar caracteristicas del bufete
        private readonly IListarNegocioLN _listarNegocio;
        //Buscar personas y listar personas, las personas puede ser abogados.
        private readonly IListarGePersonaLN _listarPersona;
        private readonly IBuscarXidGePersonaLN _buscarPersona;
        //Listar los tipos de aboagos
        private readonly IListarAbogadoTipoLN _listarAbogadoTipo;
        //Listar los distritos
        private readonly IListarCrDireccion1LN _listarDireccion;


        public AbogadoController(
            //crud Gestion General Abodados
            IBuscarAbogadoLN buscar,
            ICrearAbogadoLN crear,
            IEditarAbogadoLN editar,
            IEliminarAbogadoLN eliminar,
            IListarAbogadoLN listar,
            //Listar caracteristicas del bufete
            IListarNegocioLN listarNegocio,
            //Buscar personas y listar personas, las personas puede ser abogados.
            IListarGePersonaLN listarPersona,
            IBuscarXidGePersonaLN buscarPersona,
            //Listar los tipos de aboagos
            IListarAbogadoTipoLN listarAbogadoTipo,
            //Listar los distritos
            IListarCrDireccion1LN listarDireccion
            )
        {
            //crud Gestion General Abodados
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;
            //Listar caracteristicas del bufete
            _listarNegocio = listarNegocio;
            //Buscar personas y listar personas, las personas puede ser abogados.
            _listarPersona = listarPersona;
            _listarAbogadoTipo = listarAbogadoTipo;
            //Listar los tipos de aboagos
            _buscarPersona = buscarPersona;
            //Listar los distritos
            _listarDireccion = listarDireccion;
        }

        // GET: Abogado
        public async Task<IActionResult> Index()
        {
            //var contexto = _context.TGeAbogados.Include(t => t.CJuridicaNavigation).Include(t => t.CedulaNavigation).Include(t => t.IdTipoAbogadoNavigation);
            return View(await _listar.listar());
        }

        // GET: Abogado/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tGeAbogado = await _buscar.buscar(id);
            if (tGeAbogado == null)
            {
                return NotFound();
            }

            return View(tGeAbogado);
        }

        // GET: Abogado/Create
        public async Task<IActionResult> Create()
        {

            ViewData["Direccion1"] = new SelectList(_listarDireccion.listarDistritos().Result, "IdDistrito", "NombreDistrito");

            ViewBag.EstadoCivil = new List<SelectListItem>
        {
            new SelectListItem { Text = "Soltero", Value = "Soltero" },
            new SelectListItem { Text = "Casado", Value = "Casado" },
            new SelectListItem { Text = "Divorciado", Value = "Divorciado" },
            new SelectListItem { Text = "Viudo", Value = "Viudo" }
        };

            ViewData["CJuridica"] = (await _listarNegocio.listar())
                .Select(n => new SelectListItem
                {
                    Value = n.CJuridica.ToString(),
                    Text = $"{n.Nombre} - {n.CJuridica}"
                })
                .ToList();

            ViewData["Cedula"] = (await _listarPersona.listar())
                .Select(n => new SelectListItem
                {
                    Value = n.Cedula.ToString(),
                    Text = $"{n.Cedula} - {n.Nombre}"
                })
                .ToList();

            ViewData["IdTipoAbogado"] = new SelectList(_listarAbogadoTipo.listar().Result, "IdTipoAbogado", "Nombre");
            return View();
        }

        // POST: Abogado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Carnet,Cedula,IdTipoAbogado,CJuridica")] PersonaUnionAbogado tGeAbogado)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tGeAbogado);
                return RedirectToAction(nameof(Index));
            }

            ViewData["Direccion1"] = new SelectList(_listarDireccion.listarDistritos().Result, "IdDistrito", "NombreDistrito", tGeAbogado.personaDTO.Direccion1);

            ViewBag.EstadoCivil = new List<SelectListItem>
        {
            new SelectListItem { Text = "Soltero", Value = "Soltero" },
            new SelectListItem { Text = "Casado", Value = "Casado" },
            new SelectListItem { Text = "Divorciado", Value = "Divorciado" },
            new SelectListItem { Text = "Viudo", Value = "Viudo" }
        };

            ViewData["CJuridica"] = (await _listarNegocio.listar())
                 .Select(n => new SelectListItem
                 {
                     Value = n.CJuridica.ToString(),
                     Text = $"{n.Nombre} - {n.CJuridica}"
                 })
                 .ToList();

            ViewData["Cedula"] = (await _listarPersona.listar())
                .Select(n => new SelectListItem
                {
                    Value = n.Cedula.ToString(),
                    Text = $"{n.Cedula} - {n.Nombre}"
                })
                .ToList();

            ViewData["IdTipoAbogado"] = new SelectList(_listarAbogadoTipo.listar().Result, "IdTipoAbogado", "Nombre");
            return View(tGeAbogado);
        }

        // GET: Abogado/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }



            var tGeAbogado = await _buscar.buscar(id);
            if (tGeAbogado == null)
            {
                return NotFound();
            }

            ViewData["Direccion1"] = new SelectList(_listarDireccion.listarDistritos().Result, "IdDistrito", "NombreDistrito", tGeAbogado.CedulaNavigation.Direccion1);

            ViewBag.EstadoCivil = new List<SelectListItem>
            {
                new SelectListItem { Text = "Casado", Value = "Casado" },
                new SelectListItem { Text = "Divorciado", Value = "Divorciado" },
                new SelectListItem { Text = "Viudo", Value = "Viudo" }
            };

            ViewData["CJuridica"] = (await _listarNegocio.listar())
                .Select(n => new SelectListItem
                {
                    Value = n.CJuridica.ToString(),
                    Text = $"{n.Nombre} - {n.CJuridica}"
                })
                .ToList();

            ViewData["Cedula"] = (await _listarPersona.listar())
                .Select(n => new SelectListItem
                {
                    Value = n.Cedula.ToString(),
                    Text = $"{n.Cedula} - {n.Nombre}"
                })
                .ToList();

            ViewData["IdTipoAbogado"] = new SelectList(_listarAbogadoTipo.listar().Result, "IdTipoAbogado", "Nombre");
            return View(tGeAbogado);
        }

        // POST: Abogado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Carnet,Cedula,IdTipoAbogado,CJuridica")] GeAbogadoDTO tGeAbogado)
        {
            if (id != tGeAbogado.Cedula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.Editar(tGeAbogado);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["Direccion1"] = new SelectList(_listarDireccion.listarDistritos().Result, "IdDistrito", "NombreDistrito", tGeAbogado.CedulaNavigation.Direccion1);

            ViewBag.EstadoCivil = new List<SelectListItem>
        {
            new SelectListItem { Text = "Soltero", Value = "Soltero" },
            new SelectListItem { Text = "Casado", Value = "Casado" },
            new SelectListItem { Text = "Divorciado", Value = "Divorciado" },
            new SelectListItem { Text = "Viudo", Value = "Viudo" }
        };

            ViewData["CJuridica"] = (await _listarNegocio.listar())
                .Select(n => new SelectListItem
                {
                    Value = n.CJuridica.ToString(),
                    Text = $"{n.Nombre} - {n.CJuridica}"
                })
                .ToList();

            ViewData["Cedula"] = (await _listarPersona.listar())
                .Select(n => new SelectListItem
                {
                    Value = n.Cedula.ToString(),
                    Text = $"{n.Cedula} - {n.Nombre}"
                })
                .ToList();

            ViewData["IdTipoAbogado"] = new SelectList(_listarAbogadoTipo.listar().Result, "IdTipoAbogado", "Nombre");
            return View(tGeAbogado);
        }

        // GET: Abogado/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tGeAbogado = await _buscar.buscar(id);
            if (tGeAbogado == null)
            {
                return NotFound();
            }

            return View(tGeAbogado);
        }

        // POST: Abogado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eliminar.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }


        // GET: Abogado/Create
        public async Task<IActionResult> CrearAbogado()
        {

            ViewData["Direccion1"] = new SelectList(_listarDireccion.listarDistritos().Result, "IdDistrito", "NombreDistrito");

            ViewBag.EstadoCivil = new List<SelectListItem>
        {
            new SelectListItem { Text = "Soltero", Value = "Soltero" },
            new SelectListItem { Text = "Casado", Value = "Casado" },
            new SelectListItem { Text = "Divorciado", Value = "Divorciado" },
            new SelectListItem { Text = "Viudo", Value = "Viudo" }
        };

            ViewData["CJuridica"] = (await _listarNegocio.listar())
                .Select(n => new SelectListItem
                {
                    Value = n.CJuridica.ToString(),
                    Text = $"{n.Nombre} - {n.CJuridica}"
                })
                .ToList();

            ViewData["Cedula"] = (await _listarPersona.listar())
                .Select(n => new SelectListItem
                {
                    Value = n.Cedula.ToString(),
                    Text = $"{n.Cedula} - {n.Nombre}"
                })
                .ToList();

            ViewData["IdTipoAbogado"] = new SelectList(_listarAbogadoTipo.listar().Result, "IdTipoAbogado", "Nombre");
            return View();
        }

        // POST: Abogado/CrearAbogado
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearAbogado([Bind("personaDTO,geAbogadoDTO")] PersonaUnionAbogado tGeAbogado)
        {
            #region validacion de persona
            //Verifica que la persona no este en el sistema verificanco que la cédula no exista
            int cedula = tGeAbogado.personaDTO.Cedula;
            var existe = await _buscar.buscar(cedula);
            if (existe != null)
            {
                ViewData["Direccion1"] = new SelectList(_listarDireccion.listarDistritos().Result, "IdDistrito", "NombreDistrito", tGeAbogado.personaDTO.Direccion1);

                ViewBag.EstadoCivil = new List<SelectListItem>
        {
            new SelectListItem { Text = "Soltero", Value = "Soltero" },
            new SelectListItem { Text = "Casado", Value = "Casado" },
            new SelectListItem { Text = "Divorciado", Value = "Divorciado" },
            new SelectListItem { Text = "Viudo", Value = "Viudo" }
        };

                ViewData["CJuridica"] = (await _listarNegocio.listar())
                 .Select(n => new SelectListItem
                 {
                     Value = n.CJuridica.ToString(),
                     Text = $"{n.Nombre} - {n.CJuridica}"
                 })
                 .ToList();

                ViewData["Cedula"] = (await _listarPersona.listar())
                    .Select(n => new SelectListItem
                    {
                        Value = n.Cedula.ToString(),
                        Text = $"{n.Cedula} - {n.Nombre}"
                    })
                    .ToList();

                ViewData["IdTipoAbogado"] = new SelectList(_listarAbogadoTipo.listar().Result, "IdTipoAbogado", "Nombre");

                TempData["ErrorCedula"] = "Cedula ya registrada en el sistema";
                return View(tGeAbogado);
            }
            #endregion

            #region Verificacion de carnet de abogado
            //Evita que se repitan los carnets los cuales son únicos por cada abado en el país
            var carnet = await _buscar.buscarXcarnet(tGeAbogado.geAbogadoDTO.Carnet);
            if (carnet != null)
            {
                ViewData["Direccion1"] = new SelectList(_listarDireccion.listarDistritos().Result, "IdDistrito", "NombreDistrito", tGeAbogado.personaDTO.Direccion1);

                ViewBag.EstadoCivil = new List<SelectListItem>
        {
            new SelectListItem { Text = "Soltero", Value = "Soltero" },
            new SelectListItem { Text = "Casado", Value = "Casado" },
            new SelectListItem { Text = "Divorciado", Value = "Divorciado" },
            new SelectListItem { Text = "Viudo", Value = "Viudo" }
        };

                ViewData["CJuridica"] = (await _listarNegocio.listar())
                .Select(n => new SelectListItem
                {
                    Value = n.CJuridica.ToString(),
                    Text = $"{n.Nombre} - {n.CJuridica}"
                })
                .ToList();

                ViewData["Cedula"] = (await _listarPersona.listar())
                    .Select(n => new SelectListItem
                    {
                        Value = n.Cedula.ToString(),
                        Text = $"{n.Cedula} - {n.Nombre}"
                    })
                    .ToList();

                ViewData["IdTipoAbogado"] = new SelectList(_listarAbogadoTipo.listar().Result, "IdTipoAbogado", "Nombre");
                TempData["ErrorCarnet"] = "El carnet ya se encuentra registrado en el sistema";
                return View(tGeAbogado);
            }
            #endregion

            #region Verificacion de correo

            //Evita que se repitan los correos los cuales deben ser unicos para la autenticación
            var correo = await _buscarPersona.buscarXcorreo(tGeAbogado.personaDTO.Email);
            if (correo != null)
            {
                ViewData["Direccion1"] = new SelectList(_listarDireccion.listarDistritos().Result, "IdDistrito", "NombreDistrito", tGeAbogado.personaDTO.Direccion1);

                ViewBag.EstadoCivil = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Soltero", Value = "Soltero" },
                    new SelectListItem { Text = "Casado", Value = "Casado" },
                    new SelectListItem { Text = "Divorciado", Value = "Divorciado" },
                    new SelectListItem { Text = "Viudo", Value = "Viudo" }
                };

                ViewData["CJuridica"] = (await _listarNegocio.listar())
                .Select(n => new SelectListItem
                {
                    Value = n.CJuridica.ToString(),
                    Text = $"{n.Nombre} - {n.CJuridica}"
                })
                .ToList();

                ViewData["Cedula"] = (await _listarPersona.listar())
                    .Select(n => new SelectListItem
                    {
                        Value = n.Cedula.ToString(),
                        Text = $"{n.Cedula} - {n.Nombre}"
                    })
                    .ToList();

                ViewData["IdTipoAbogado"] = new SelectList(_listarAbogadoTipo.listar().Result, "IdTipoAbogado", "Nombre");
                TempData["ErrorEmail"] = "Correo Electronico ya registrado en el sistema";
                return View(tGeAbogado);
            }
            #endregion

            #region validacion de formulario
            if (ModelState.IsValid)
            {
                tGeAbogado.geAbogadoDTO.Cedula = tGeAbogado.personaDTO.Cedula;
                await _crear.Crear(tGeAbogado);
                return RedirectToAction(nameof(Index));
            }

            ViewData["Direccion1"] = new SelectList(_listarDireccion.listarDistritos().Result, "IdDistrito", "NombreDistrito", tGeAbogado.personaDTO.Direccion1);

            ViewBag.EstadoCivil = new List<SelectListItem>
        {
            new SelectListItem { Text = "Soltero", Value = "Soltero" },
            new SelectListItem { Text = "Casado", Value = "Casado" },
            new SelectListItem { Text = "Divorciado", Value = "Divorciado" },
            new SelectListItem { Text = "Viudo", Value = "Viudo" }
        };

            ViewData["CJuridica"] = (await _listarNegocio.listar())
                 .Select(n => new SelectListItem
                 {
                     Value = n.CJuridica.ToString(),
                     Text = $"{n.Nombre} - {n.CJuridica}"
                 })
                 .ToList();

            ViewData["Cedula"] = (await _listarPersona.listar())
                .Select(n => new SelectListItem
                {
                    Value = n.Cedula.ToString(),
                    Text = $"{n.Cedula} - {n.Nombre}"
                })
                .ToList();

            ViewData["IdTipoAbogado"] = new SelectList(_listarAbogadoTipo.listar().Result, "IdTipoAbogado", "Nombre");
            return View(tGeAbogado);
            #endregion

        }



    }
}

