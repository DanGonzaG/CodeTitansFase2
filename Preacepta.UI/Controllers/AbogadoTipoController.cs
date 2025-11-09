using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Preacepta.LN.GeAbogadoTipo.BuscarXid;
using Preacepta.LN.GeAbogadoTipo.Crear;
using Preacepta.LN.GeAbogadoTipo.Editar;
using Preacepta.LN.GeAbogadoTipo.Eliminar;
using Preacepta.LN.GeAbogadoTipo.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using Preacepta.LN.BitacoraEventos.Crear;

namespace Preacepta.UI.Controllers
{
    [Authorize(Roles = "Gestor")]
    public class AbogadoTipoController : Controller
    {
        private readonly IBuscarAbogadoTipoLN _buscar;
        private readonly ICrearAbogadoTipoLN _crear;
        private readonly IEditarAbogadoTipoLN _editar;
        private readonly IEliminarAbogadoTipoLN _eliminar;
        private readonly IListarAbogadoTipoLN _listar;
        private readonly ICrearEventosLN _bitacoraLN;


        public AbogadoTipoController(IBuscarAbogadoTipoLN buscar,
            ICrearAbogadoTipoLN crear,
            IEditarAbogadoTipoLN editar,
            IEliminarAbogadoTipoLN eliminar,
            IListarAbogadoTipoLN listar,
            ICrearEventosLN bitacora)
        {
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;
            _bitacoraLN = bitacora;
        }


        /********************************************************************************************************************************************************************/
        //controller de Framework\\
        /********************************************************************************************************************************************************************/

        #region Listar tipo de abogado Root
        // GET: AbogadoTipo
        public async Task<IActionResult> Index()
        {
            return View(await _listar.listar());
        }
        #endregion

        #region Detalles tipo de abogado Root metodo GET
        // GET: AbogadoTipo/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tGeAbogadoTipo = await _buscar.buscar(id); ;
            if (tGeAbogadoTipo == null)
            {
                return NotFound();
            }

            return View(tGeAbogadoTipo);
        }
        #endregion

        #region Crear tipo de abogado Root metodo GET y POST
        // GET: AbogadoTipo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AbogadoTipo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoAbogado,Nombre")] GeAbogadoTipoDTO tGeAbogadoTipo)
        {
            if (ModelState.IsValid)
            {
                await _crear.crear(tGeAbogadoTipo);

                var usuario = User.Identity?.Name ?? "Desconocido";
                var accion = $"Se creó el tipo de abogado '{tGeAbogadoTipo.Nombre}'.";
                await _bitacoraLN.RegistrarBitacoraAsync(usuario, "GeAbogadoTipo", accion, tGeAbogadoTipo.IdTipoAbogado);


                return RedirectToAction(nameof(Index));
            }
            return View(tGeAbogadoTipo);
        }
        #endregion

        #region Editar tipo de abogado Root metodo GET y POST
        // GET: AbogadoTipo/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tGeAbogadoTipo = await _buscar.buscar(id);
            if (tGeAbogadoTipo == null)
            {
                return NotFound();
            }
            return View(tGeAbogadoTipo);
        }

        // POST: AbogadoTipo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoAbogado,Nombre")] GeAbogadoTipoDTO tGeAbogadoTipo)
        {
            if (id != tGeAbogadoTipo.IdTipoAbogado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.editar(tGeAbogadoTipo);
                    var usuario = User.Identity?.Name ?? "Desconocido";
                    var accion = $"Se editó el tipo de abogado '{tGeAbogadoTipo.Nombre}' (ID: {tGeAbogadoTipo.IdTipoAbogado}).";
                    await _bitacoraLN.RegistrarBitacoraAsync(usuario, "GeAbogadoTipo", accion, tGeAbogadoTipo.IdTipoAbogado);

                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tGeAbogadoTipo);
        }
        #endregion

        #region Eliminar tipo de abogado Root metodo GET y POST
        // GET: AbogadoTipo/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tGeAbogadoTipo = await _buscar.buscar(id);
            if (tGeAbogadoTipo == null)
            {
                return NotFound();
            }

            return View(tGeAbogadoTipo);
        }


        // POST: AbogadoTipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eliminar.eliminar(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
