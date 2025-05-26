using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.CrDireccion1.Listar;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.LN.GePersona.Crear;
using Preacepta.LN.GePersona.Editar;
using Preacepta.LN.GePersona.Eliminar;
using Preacepta.LN.GePersona.Listar;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.UI.Controllers;

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

    // GET: TGePersonas
    public async Task<IActionResult> Index()
    {
        //var contexto = _context.TGePersonas.Include(t => t.Direccion1Navigation);
        return View(await _listarPersona.listar());
    }

    // GET: TGePersonas/Details/5
    public async Task<IActionResult> Details(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        /*var tGePersona = await _context.TGePersonas
            .Include(t => t.Direccion1Navigation)
            .FirstOrDefaultAsync(m => m.Cedula == id);*/
        var tGePersona = await _buscarPersona.buscar(id);
        if (tGePersona == null)
        {
            return NotFound();
        }

        return View(tGePersona);
    }

    // GET: TGePersonas/Create
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

        return View();
    }

    // POST: TGePersonas/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Cedula,Nombre,Apellido1,Apellido2,FechaNacimiento,Edad,EstadoCivil,Oficio,Direccion1,Direccion2,FechaRegistro,Telefono1,Telefono2,Activo,Email")] GePersonaDTO tGePersona)
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
        return View(tGePersona);
    }

    // GET: TGePersonas/Edit/5
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

        return View(tGePersona);
    }

    // POST: TGePersonas/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Cedula,Nombre,Apellido1,Apellido2,FechaNacimiento,Edad,EstadoCivil,Oficio,Direccion1,Direccion2,Telefono1,Telefono2,FechaRegistro,Activo,Email")] GePersonaDTO tGePersona)
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

        return View(tGePersona);
    }

    // GET: TGePersonas/Delete/5
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
    public async Task<IActionResult> DeleteConfirmed(int id)
    {

        await _eliminarPersona.eliminar(id);
        return RedirectToAction(nameof(Index));
    }
}

