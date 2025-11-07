using Microsoft.AspNetCore.Mvc;
using Preacepta.LN.CitasTipo.Listar;
using Preacepta.LN.CitasTipo.Crear;
using Preacepta.LN.CitasTipo.Editar;
using Preacepta.LN.CitasTipo.Eliminar;
using Preacepta.Modelos.AbstraccionesFrond;
using Preacepta.Modelos.AbstraccionesBD;
using Microsoft.AspNetCore.Authorization;
using Preacepta.LN.BitacoraEventos.Crear;

namespace Preacepta.UI.Controllers
{
    [Authorize(Roles = "Gestor")]
    public class CitasTipoController : Controller
    {
        private readonly IListarCitasTipoLN _listarLN;
        private readonly ICrearCitasTipoLN _crearLN;
        private readonly IEditarCitasTipoLN _editarLN;
        private readonly IEliminarCitasTipoLN _eliminarLN;
        private readonly ICrearEventosLN _bitacoraLN;

        public CitasTipoController(
            IListarCitasTipoLN listarLN,
            ICrearCitasTipoLN crearLN,
            IEditarCitasTipoLN editarLN,
            IEliminarCitasTipoLN eliminarLN,
            ICrearEventosLN bitacoraLN)
        {
            _listarLN = listarLN;
            _crearLN = crearLN;
            _editarLN = editarLN;
            _eliminarLN = eliminarLN;
            _bitacoraLN = bitacoraLN;
        }

        // Listar
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Index()
        {
            var lista = await _listarLN.listar();
            return View(lista);
        }

        // Detalles
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Details(int id)
        {
            var item = (await _listarLN.listar()).FirstOrDefault(c => c.Id  == id);
            if (item == null) return NotFound();
            return View(item);
        }

        // Crear GET
        [Authorize(Roles = "Gestor")]
        public IActionResult Create()
        {
            return View();
        }

        // Crear POST
        [Authorize(Roles = "Gestor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CitasTipoDTO dto)
        {
            if (ModelState.IsValid)
            {
                var nuevoId = await _crearLN.Crear(dto);

                var usuario = User.Identity?.Name ?? "Desconocido";
                var accion = $"Se creó el tipo de cita '{dto.Nombre}' con ID {nuevoId}";
                await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_CitasTipo", accion, nuevoId);


                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        // Editar GET
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int id)
        {
            var item = (await _listarLN.listar()).FirstOrDefault(c => c.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }

        // Editar POST
        [Authorize(Roles = "Gestor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CitasTipoDTO dto)
        {
            if (id != dto.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var resultado = await _editarLN.editar(dto);
                if (resultado > 0)
                {
                    var usuario = User.Identity?.Name ?? "Desconocido";
                    var accion = $"Se editó el tipo de cita '{dto.Nombre}' con ID {dto.Id}";
                    await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_CitasTipo", accion, dto.Id);
                    
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Error al actualizar");
            }
            return View(dto);
        }

        // Eliminar GET
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = (await _listarLN.listar()).FirstOrDefault(c => c.Id == id);
            if (item == null) return NotFound();


            return View(item); 

        }

        // Eliminar POST
        [Authorize(Roles = "Gestor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eliminarLN.eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
 