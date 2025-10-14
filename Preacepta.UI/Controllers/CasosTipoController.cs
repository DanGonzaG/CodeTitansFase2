using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.CasosTipo.BuscarXid;
using Preacepta.LN.CasosTipo.Crear;
using Preacepta.LN.CasosTipo.Editar;
using Preacepta.LN.CasosTipo.Eliminar;
using Preacepta.LN.CasosTipo.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using Preacepta.LN.BitacoraEventos.Crear;
using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.UI.Controllers
{
    [Authorize(Roles = "Gestor")]
    public class CasosTipoController : Controller
    {
        private readonly Contexto _context;
        private readonly IBuscarCasosTiposLN _buscar;
        private readonly ICrearCasosTiposLN _crear;
        private readonly IEditarCasosTiposLN _editar;
        private readonly IEliminarCasosTipoLN _eliminar;
        private readonly IListarCasosTipoLN _listar;
        private readonly ICrearEventosLN _bitacoraLN;

        public CasosTipoController(Contexto context,
            IBuscarCasosTiposLN buscar,
            ICrearCasosTiposLN crear,
            IEditarCasosTiposLN editar,
            IEliminarCasosTipoLN eliminar,
            IListarCasosTipoLN listar,
            ICrearEventosLN bitacoraLN)
        {
            _context = context;
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;
            _bitacoraLN = bitacoraLN;
        }

        /********************************************************************************************************************************************************************/
        /*-----------------------------------------------------------------//CONTROLLER METODOS DE FRAMEWORK\\--------------------------------------------------------------*/
        /********************************************************************************************************************************************************************/

        #region Listar Root
        // GET: CasosTipo
        public async Task<IActionResult> Index()
        {
            return View(await _listar.listar());
        }
        #endregion

        #region Detalles Root
        // GET: CasosTipo/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCasosTipo = await _buscar.buscar(id);
            if (tCasosTipo == null)
            {
                return NotFound();
            }

            return View(tCasosTipo);
        }
        #endregion

        #region Crear Root metodo POST y GET
        // GET: CasosTipo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CasosTipo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoCaso,Nombre")] CasosTipoDTO tCasosTipo)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tCasosTipo);

                var usuario = User.Identity?.Name ?? "Desconocido";
                var tituloCorto = tCasosTipo.Nombre.Length > 50
                    ? tCasosTipo.Nombre.Substring(0, 47) + "..."
                    : tCasosTipo.Nombre;
                var accion = $"Se creó '{tituloCorto}' del caso {tCasosTipo.IdTipoCaso}";
                await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_CasosEvidencia", accion, tCasosTipo.IdTipoCaso);


                return RedirectToAction(nameof(Index));
            }
            return View(tCasosTipo);
        }
        #endregion

        #region Editar Root metodo POST y GET
        // GET: CasosTipo/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCasosTipo = await _buscar.buscar(id);
            if (tCasosTipo == null)
            {
                return NotFound();
            }
            return View(tCasosTipo);
        }

        // POST: CasosTipo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoCaso,Nombre")] CasosTipoDTO tCasosTipo)
        {
            if (id != tCasosTipo.IdTipoCaso)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.Editar(tCasosTipo);

                    var usuario = User.Identity?.Name ?? "Desconocido";
                    var titulo = tCasosTipo.Nombre.Length > 50
                        ? tCasosTipo.Nombre.Substring(0, 47) + "..."
                        : tCasosTipo.Nombre;
                    var accion = $"Se editó '{titulo}' del caso {tCasosTipo.IdTipoCaso}";
                    await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_CasosEvidencia", accion, tCasosTipo.IdTipoCaso);

                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tCasosTipo);
        }
        #endregion

        #region Eliminar Root metodo POST y GET
        // GET: CasosTipo/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCasosTipo = await _buscar.buscar(id);
            if (tCasosTipo == null)
            {
                return NotFound();
            }

            return View(tCasosTipo);
        }

        // POST: CasosTipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eliminar.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
