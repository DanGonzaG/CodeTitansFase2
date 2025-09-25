using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Preacepta.LN.Citas.BuscarXid;
using Preacepta.LN.Citas.Editar;
using Preacepta.LN.Citas.Eliminar;
using Preacepta.LN.Citas.Listar;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.LN.Videollamada;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System.Linq;
using System.Threading.Tasks;

[Authorize(Roles = "Gestor")]
public class CitasGestorController : Controller
{
    private readonly IListarCitasLN _listarCitasLN;
    private readonly IEditarCitasLN _editarCitasLN;
    private readonly IEliminarCitasLN _eliminarCitasLN;
    private readonly IBuscarCitasLN _buscarCitasLN;
    private readonly IBuscarXidGePersonaLN _buscarXidGePersonaLN;
    private readonly UserManager<IdentityUser> _userManager;

    public CitasGestorController(
        IListarCitasLN listarCitasLN,
        IEditarCitasLN editarCitasLN,
        IEliminarCitasLN eliminarCitasLN,
        IBuscarCitasLN buscarCitasLN,
        IBuscarXidGePersonaLN buscarXidGePersonaLN,
        UserManager<IdentityUser> userManager)
    {
        _listarCitasLN = listarCitasLN;
        _editarCitasLN = editarCitasLN;
        _eliminarCitasLN = eliminarCitasLN;
        _buscarCitasLN = buscarCitasLN;
        _buscarXidGePersonaLN = buscarXidGePersonaLN;
        _userManager = userManager;
    }

    // Listado para Gestor
    [Authorize(Roles = "Gestor")]
    public async Task<IActionResult> Index()
    {
        var lista = await _listarCitasLN.listar();

        var listaOrdenada = lista
        .OrderByDescending(c => c.FechaHora) 
        .ToList();

        return View("~/Views/CitasPrueba/Index.cshtml", listaOrdenada);
    }

    // GET: CitasGestor/Details
    public async Task<IActionResult> Details(int id)
    {
        var cita = await _buscarCitasLN.ObtenerCitaConClientes(id);

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

        return PartialView("~/Views/CitasPrueba/Details.cshtml", cita);
    }

    // GET: CitasGestor/Edit
    public async Task<IActionResult> Edit(int id)
    {
        var cita = await _buscarCitasLN.ObtenerCitaConClientes(id);
        if (cita == null)
            return NotFound();

        if (string.IsNullOrEmpty(cita.NombreAnfitrion))
        {
            var persona = await _listarCitasLN.ObtenerPersonaPorCedula(cita.Anfitrion.ToString());
            if (persona != null)
                cita.NombreAnfitrion = $"{persona.Nombre} {persona.Apellido1} {persona.Apellido2}";
        }

        if (cita.NombresClientes == null || !cita.NombresClientes.Any())
            cita.NombresClientes = new List<string> { "No asignado" };

        var tiposCita = await _listarCitasLN.ListarTiposCita();
        ViewBag.TipoCitaList = tiposCita.Select(t => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
        {
            Value = t.Id.ToString(),
            Text = t.Nombre
        }).ToList();

        return View("~/Views/CitasPrueba/Edit.cshtml", cita);
    }

    // POST: CitasGestor/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit( CitasDTO model)
    {

        var citaOriginal = await _buscarCitasLN.ObtenerCitaConClientes(model.IdCita);
        if (citaOriginal == null)
            return NotFound();

     
        model.Anfitrion = citaOriginal.Anfitrion;
        model.IdCliente = citaOriginal.IdCliente;
        model.LinkVideo = citaOriginal.LinkVideo;

      
        var persona = await _listarCitasLN.ObtenerPersonaPorCedula(model.Anfitrion.ToString());
        if (persona != null)
            model.NombreAnfitrion = $"{persona.Nombre} {persona.Apellido1} {persona.Apellido2}";

        if (ModelState.IsValid)
        {
      
            if (model.IdTipoCita == 1005 && string.IsNullOrEmpty(model.LinkVideo))
            {
                var auth = new ZoomAuthService();
                var token = await auth.ObtenerAccessTokenAsync();

                var servicio = new ZoomMeetingService();
                var zoomResult = await servicio.CrearReunionProgramadaAsync(token, model.FechaHora, 60, "Cita con cliente");
                model.LinkVideo = zoomResult.JoinUrl;
            }

            await _editarCitasLN.editar(model);
            return RedirectToAction(nameof(Index));
        }

  
        var tiposCita = await _listarCitasLN.ListarTiposCita();
        ViewBag.TipoCitaList = tiposCita.Select(t => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
        {
            Value = t.Id.ToString(),
            Text = t.Nombre
        }).ToList();

        return View(model);
    }


    // GET: CitasGestor/Delete
    public async Task<IActionResult> Delete(int id)
    {
        var cita = await _listarCitasLN.ObtenerPorId(id);
        if (cita == null)
            return NotFound();

        return View("~/Views/CitasPrueba/Delete.cshtml", cita);
    }

    // POST: CitasGestor/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _eliminarCitasLN.Eliminar(id);
        return RedirectToAction(nameof(Index));
    }
}
