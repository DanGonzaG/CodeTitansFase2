using Microsoft.AspNetCore.Mvc;
using Preacepta.LN.CitasTipo.Listar;
using Preacepta.LN.CitasTipo.Crear;
using Preacepta.LN.CitasTipo.Editar;
using Preacepta.LN.CitasTipo.Eliminar;
using Preacepta.Modelos.AbstraccionesFrond;
using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.UI.Controllers
{
    public class CitasTipoController : Controller
    {
        private readonly IListarCitasTipoLN _listarLN;
        private readonly ICrearCitasTipoLN _crearLN;
        private readonly IEditarCitasTipoLN _editarLN;
        private readonly IEliminarCitasTipoLN _eliminarLN;

        public CitasTipoController(
            IListarCitasTipoLN listarLN,
            ICrearCitasTipoLN crearLN,
            IEditarCitasTipoLN editarLN,
            IEliminarCitasTipoLN eliminarLN)
        {
            _listarLN = listarLN;
            _crearLN = crearLN;
            _editarLN = editarLN;
            _eliminarLN = eliminarLN;
        }

        // Listar
        public async Task<IActionResult> Index()
        {
            var lista = await _listarLN.listar();
            return View(lista);
        }

        // Detalles
        public async Task<IActionResult> Details(int id)
        {
            var item = (await _listarLN.listar()).FirstOrDefault(c => c.Id  == id);
            if (item == null) return NotFound();
            return View(item);
        }

        // Crear GET
        public IActionResult Create()
        {
            return View();
        }

        // Crear POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CitasTipoDTO dto)
        {
            if (ModelState.IsValid)
            {
                await _crearLN.Crear(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        // Editar GET
        public async Task<IActionResult> Edit(int id)
        {
            var item = (await _listarLN.listar()).FirstOrDefault(c => c.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }

        // Editar POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CitasTipoDTO dto)
        {
            if (id != dto.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var resultado = await _editarLN.editar(dto);
                if (resultado > 0) return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", "Error al actualizar");
            }
            return View(dto);
        }

        // Eliminar GET
        public async Task<IActionResult> Delete(int id)
        {
            var item = (await _listarLN.listar()).FirstOrDefault(c => c.Id == id);
            if (item == null) return NotFound();


            return View(item); 

        }

        // Eliminar POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eliminarLN.eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
 