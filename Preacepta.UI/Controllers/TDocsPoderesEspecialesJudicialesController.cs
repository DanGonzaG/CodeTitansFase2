using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.DocPoderesEspecialesJudiciales.Buscar;
using Preacepta.LN.DocPoderesEspecialesJudiciales.Crear;
using Preacepta.LN.DocPoderesEspecialesJudiciales.Editar;
using Preacepta.LN.DocPoderesEspecialesJudiciales.Eliminar;
using Preacepta.LN.DocPoderesEspecialesJudiciales.Listar;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.UI.Controllers
{
    public class TDocsPoderesEspecialesJudicialesController : Controller
    {
        private readonly IBuscarPoderJudLN _buscar;
        private readonly ICrearPoderJudLN _crear;
        private readonly IEditarPoderJudLN _editar;
        private readonly IEliminarPoderJudLN _eliminar;
        private readonly IListarPoderJudLN _listar;

        public TDocsPoderesEspecialesJudicialesController(IBuscarPoderJudLN buscar,
            ICrearPoderJudLN crear,
            IEditarPoderJudLN editar,
            IEliminarPoderJudLN eliminar,
            IListarPoderJudLN listar)
        {
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;
        }

        // GET: TDocsPoderesEspecialesJudiciales
        public async Task<IActionResult> Index()
        {
            return View(await _listar.Listar());
        }

        // GET: TDocsPoderesEspecialesJudiciales/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poderJud = await _buscar.buscar(id);
            if (poderJud == null)
            {
                return NotFound();
            }

            return View(poderJud);
        }

        // GET: TDocsPoderesEspecialesJudiciales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TDocsPoderesEspecialesJudiciales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDoc,Fecha,IdAbogado,IdCliente,Texto")] DocsPoderesEspecialesJudicialeDTO tDocsPoderesEspecialesJudiciale)
        {
            if (ModelState.IsValid)
            {
                tDocsPoderesEspecialesJudiciale.Fecha = DateTime.Today.ToString("yyyy-MM-dd");

                await _crear.crear(tDocsPoderesEspecialesJudiciale);
                return RedirectToAction(nameof(Index));
            }
            return View(tDocsPoderesEspecialesJudiciale);
        }

        // GET: TDocsPoderesEspecialesJudiciales/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsPoderesEspecialesJudiciale = await _buscar.buscar(id);
            if (tDocsPoderesEspecialesJudiciale == null)
            {
                return NotFound();
            }
            return View(tDocsPoderesEspecialesJudiciale);
        }

        // POST: TDocsPoderesEspecialesJudiciales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDoc,Fecha,IdAbogado,IdCliente,Texto")] DocsPoderesEspecialesJudicialeDTO tDocsPoderesEspecialesJudiciale)
        {
            if (id != tDocsPoderesEspecialesJudiciale.IdDoc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.editar(tDocsPoderesEspecialesJudiciale);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tDocsPoderesEspecialesJudiciale);
        }

        // GET: TDocsPoderesEspecialesJudiciales/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsPoderesEspecialesJudiciale = await _buscar.buscar(id);
            if (tDocsPoderesEspecialesJudiciale == null)
            {
                return NotFound();
            }

            return View(tDocsPoderesEspecialesJudiciale);
        }

            // POST: TDocsPoderesEspecialesJudiciales/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                await _eliminar.eliminar(id);
                return RedirectToAction(nameof(Index));
            }


        public IActionResult CreateDocsPoderesEspecialesJudiciales()
        {
            return View();
        }

        // POST: TDocsPoderesEspecialesJudiciales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDocsPoderesEspecialesJudiciales([Bind("IdDoc,Fecha,IdAbogado,IdCliente,Texto")] DocsPoderesEspecialesJudicialeDTO tDocsPoderesEspecialesJudiciale)
        {
            if (ModelState.IsValid)
            {
                tDocsPoderesEspecialesJudiciale.Fecha = DateTime.Today.ToString("yyyy-MM-dd");

                await _crear.crear(tDocsPoderesEspecialesJudiciale);
                return RedirectToAction(nameof(Index));
            }
            return View(tDocsPoderesEspecialesJudiciale);
        }
        
    }

    }
