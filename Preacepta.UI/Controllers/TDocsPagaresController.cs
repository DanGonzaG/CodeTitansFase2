using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.DocsPagare.Buscar;
using Preacepta.LN.DocsPagare.Crear;
using Preacepta.LN.DocsPagare.Editar;
using Preacepta.LN.DocsPagare.Eliminar;
using Preacepta.LN.DocsPagare.Listar;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.UI.Controllers
{
    public class TDocsPagaresController : Controller
    {
        private readonly IBuscarPagareLN _buscar;
        private readonly ICrearPagareLN _crear;
        private readonly IEditarPagareLN _editar;
        private readonly IEliminarPagareLN _eliminar;
        private readonly IListarPagareLN _listar;


        public TDocsPagaresController(IBuscarPagareLN buscar,
            ICrearPagareLN crear,
            IEditarPagareLN editar,
            IEliminarPagareLN eliminar,
            IListarPagareLN listar)
        {
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;
        }

        // GET: TDocsPagares
        public async Task<IActionResult> Index()
        {
            return View(await _listar.Listar());
        }

        // GET: TDocsPagares/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsPagare = await _buscar.buscar(id);
            if (tDocsPagare == null)
            {
                return NotFound();
            }

            return View(tDocsPagare);
        }

        // GET: TDocsPagares/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TDocsPagares/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDocumento,MontoNumerico,CedulaDeudor,SociedadDeudor,CedulaJuridicaSociedad,AcreedorNombre,CedulaJuridicaAcreedor,AcreedorDomicilio,FechaFirma,HoraFirma,FechaVencimiento,InteresFormula,InteresTasaActual,InteresBase,LugarPago,CedulaFiador,UbicacionFirma")] DocsPagareDTO tDocsPagare)
        {
            if (ModelState.IsValid)
            {
                tDocsPagare.FechaFirma = DateTime.Today.ToString("yyyy-MM-dd");
                tDocsPagare.HoraFirma = DateTime.Now.ToString("HH:mm"); ;

                await _crear.crear(tDocsPagare);
                return RedirectToAction(nameof(Index));
            }
            return View(tDocsPagare);
        }

        // GET: TDocsPagares/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsPagare = await _buscar.buscar(id);
            if (tDocsPagare == null)
            {
                return NotFound();
            }
            return View(tDocsPagare);
        }

        // POST: TDocsPagares/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDocumento,MontoNumerico,CedulaDeudor,SociedadDeudor,CedulaJuridicaSociedad,AcreedorNombre,CedulaJuridicaAcreedor,AcreedorDomicilio,FechaFirma,HoraFirma,FechaVencimiento,InteresFormula,InteresTasaActual,InteresBase,LugarPago,CedulaFiador,UbicacionFirma")] DocsPagareDTO tDocsPagare)
        {
            if (id != tDocsPagare.IdDocumento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.editar(tDocsPagare);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tDocsPagare);
        }

        // GET: TDocsPagares/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsPagare = await _buscar.buscar(id);
            if (tDocsPagare == null)
            {
                return NotFound();
            }

            return View(tDocsPagare);
        }

        // POST: TDocsPagares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eliminar.eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
