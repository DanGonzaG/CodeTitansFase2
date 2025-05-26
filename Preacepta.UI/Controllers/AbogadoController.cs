using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.AD.GeAbogado.Eliminar;
using Preacepta.LN.GeAbogado.BuscarXid;
using Preacepta.LN.GeAbogado.Crear;
using Preacepta.LN.GeAbogado.Editar;
using Preacepta.LN.GeAbogado.Eliminar;
using Preacepta.LN.GeAbogado.Listar;
using Preacepta.LN.GeAbogadoTipo.Listar;
using Preacepta.LN.GeNegocio.Listar;
using Preacepta.LN.GePersona.Listar;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.UI.Controllers
{
    public class AbogadoController : Controller
    {
        private readonly IBuscarAbogadoLN _buscar;
        private readonly ICrearAbogadoLN _crear;
        private readonly IEditarAbogadoLN _editar;
        private readonly IEliminarAbogadoLN _eliminar;
        private readonly IListarAbogadoLN _listar;
        private readonly IListarNegocioLN _listarNegocio;
        private readonly IListarGePersonaLN _listarPersona;
        private readonly IListarAbogadoTipoLN _listarAbogadoTipo;

        public AbogadoController(IBuscarAbogadoLN buscar,
            ICrearAbogadoLN crear,
            IEditarAbogadoLN editar,
            IEliminarAbogadoLN eliminar,
            IListarAbogadoLN listar,
            IListarNegocioLN listarNegocio,
            IListarGePersonaLN listarPersona,
            IListarAbogadoTipoLN listarAbogadoTipo)
        {
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;
            _listarNegocio = listarNegocio;
            _listarPersona = listarPersona;
            _listarAbogadoTipo = listarAbogadoTipo;
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
        public IActionResult Create()
        {
            ViewData["CJuridica"] = new SelectList(_listarNegocio.listar().Result, "CJuridica", "Nombre");
            ViewData["Cedula"] = new SelectList(_listarPersona.listar().Result, "Cedula", "Cedula");
            ViewData["IdTipoAbogado"] = new SelectList(_listarAbogadoTipo.listar().Result, "IdTipoAbogado", "Nombre");
            return View();
        }

        // POST: Abogado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Carnet,Cedula,IdTipoAbogado,CJuridica")] GeAbogadoDTO tGeAbogado)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tGeAbogado);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CJuridica"] = new SelectList(_listarNegocio.listar().Result, "CJuridica", "Nombre", tGeAbogado.CJuridica);
            ViewData["Cedula"] = new SelectList(_listarPersona.listar().Result, "Cedula", "Apellido1", tGeAbogado.Cedula);
            ViewData["IdTipoAbogado"] = new SelectList(_listarAbogadoTipo.listar().Result, "IdTipoAbogado", "Nombre", tGeAbogado.IdTipoAbogado);
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
            ViewData["CJuridica"] = new SelectList(_listarNegocio.listar().Result, "CJuridica", "Nombre", tGeAbogado.CJuridica);
            ViewData["Cedula"] = new SelectList(_listarPersona.listar().Result, "Cedula", "Apellido1", tGeAbogado.Cedula);
            ViewData["IdTipoAbogado"] = new SelectList(_listarAbogadoTipo.listar().Result, "IdTipoAbogado", "Nombre", tGeAbogado.IdTipoAbogado);
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
            ViewData["CJuridica"] = new SelectList(_listarNegocio.listar().Result, "CJuridica", "Nombre", tGeAbogado.CJuridica);
            ViewData["Cedula"] = new SelectList(_listarPersona.listar().Result, "Cedula", "Apellido1", tGeAbogado.Cedula);
            ViewData["IdTipoAbogado"] = new SelectList(_listarAbogadoTipo.listar().Result, "IdTipoAbogado", "Nombre", tGeAbogado.IdTipoAbogado);
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
    }
}
