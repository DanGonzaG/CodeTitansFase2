using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.DocsCompraventaFinca.BuscarXid;
using Preacepta.LN.DocsCompraventaFinca.Crear;
using Preacepta.LN.DocsCompraventaFinca.Editar;
using Preacepta.LN.DocsCompraventaFinca.Eliminar;
using Preacepta.LN.DocsCompraventaFinca.Listar;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Preacepta.UI.Controllers
{
    public class DocsCompraventaFincasController : Controller
    {
        private readonly Contexto _context;
        private readonly IBuscarDocsCompraventaFincaLN _buscar;
        private readonly ICrearDocsCompraventaFincaLN _crear;
        private readonly IEditarDocsCompraventaFincaLN _editar;
        private readonly IEliminarDocsCompraventaFincaLN _eliminar;
        private readonly IListarDocsCompraventaFincaLN _listar;

        public DocsCompraventaFincasController(Contexto context,
            IBuscarDocsCompraventaFincaLN buscar,
            ICrearDocsCompraventaFincaLN crear,
            IEditarDocsCompraventaFincaLN editar,
            IEliminarDocsCompraventaFincaLN eliminar,
            IListarDocsCompraventaFincaLN listar)
        {
            _context = context;
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;
        }

        // GET: DocsCompraventaFincas
        public async Task<IActionResult> Index()
        {
            //var contexto = _context.TDocsCompraventaFincas.Include(t => t.CedulaAbogadoNavigation).Include(t => t.CedulaCompradorNavigation).Include(t => t.CedulaVendedorNavigation).Include(t => t.DistritoFincaNavigation).Include(t => t.LugarFirmaNavigation);
            return View(await _listar.listar());
        }

        // GET: DocsCompraventaFincas/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsCompraventaFinca = await _buscar.buscar(id);
            if (tDocsCompraventaFinca == null)
            {
                return NotFound();
            }

            return View(tDocsCompraventaFinca);
        }

        // GET: DocsCompraventaFincas/Create
        public IActionResult Create()
        {
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula");
            ViewData["CedulaComprador"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1");
            ViewData["CedulaVendedor"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1");
            ViewData["DistritoFinca"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito");
            ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito");
            return View();
        }

        // POST: DocsCompraventaFincas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDocumento,NumeroEscritura,CedulaAbogado,CedulaVendedor,CedulaComprador,MontoVenta,PartidoFinca,MatriculaFinca,NaturalezaFinca,DistritoFinca,CantonFinca,ProvinciaFinca,AreaFincaM2,PlanoCatastrado,ColindaNorte,ColindaSur,ColindaEste,ColindaOeste,FormaPago,MedioPago,OrigenFondos,LugarFirma,HoraFirma,FechaFirma")] DocsCompraventaFincaDTO tDocsCompraventaFinca)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tDocsCompraventaFinca);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tDocsCompraventaFinca.CedulaAbogado);
            ViewData["CedulaComprador"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsCompraventaFinca.CedulaComprador);
            ViewData["CedulaVendedor"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsCompraventaFinca.CedulaVendedor);
            ViewData["DistritoFinca"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsCompraventaFinca.DistritoFinca);
            ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsCompraventaFinca.LugarFirma);
            return View(tDocsCompraventaFinca);
        }

        // GET: DocsCompraventaFincas/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsCompraventaFinca = await _buscar.buscar(id);
            if (tDocsCompraventaFinca == null)
            {
                return NotFound();
            }
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tDocsCompraventaFinca.CedulaAbogado);
            ViewData["CedulaComprador"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsCompraventaFinca.CedulaComprador);
            ViewData["CedulaVendedor"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsCompraventaFinca.CedulaVendedor);
            ViewData["DistritoFinca"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsCompraventaFinca.DistritoFinca);
            ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsCompraventaFinca.LugarFirma);
            return View(tDocsCompraventaFinca);
        }

        // POST: DocsCompraventaFincas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDocumento,NumeroEscritura,CedulaAbogado,CedulaVendedor,CedulaComprador,MontoVenta,PartidoFinca,MatriculaFinca,NaturalezaFinca,DistritoFinca,CantonFinca,ProvinciaFinca,AreaFincaM2,PlanoCatastrado,ColindaNorte,ColindaSur,ColindaEste,ColindaOeste,FormaPago,MedioPago,OrigenFondos,LugarFirma,HoraFirma,FechaFirma")] DocsCompraventaFincaDTO tDocsCompraventaFinca)
        {
            if (id != tDocsCompraventaFinca.IdDocumento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.Editar(tDocsCompraventaFinca);
                }
                catch (DbUpdateConcurrencyException)
                {
                        return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tDocsCompraventaFinca.CedulaAbogado);
            ViewData["CedulaComprador"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsCompraventaFinca.CedulaComprador);
            ViewData["CedulaVendedor"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsCompraventaFinca.CedulaVendedor);
            ViewData["DistritoFinca"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsCompraventaFinca.DistritoFinca);
            ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsCompraventaFinca.LugarFirma);
            return View(tDocsCompraventaFinca);
        }

        // GET: DocsCompraventaFincas/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsCompraventaFinca = await _buscar.buscar(id);
            if (tDocsCompraventaFinca == null)
            {
                return NotFound();
            }

            return View(tDocsCompraventaFinca);
        }

        // POST: DocsCompraventaFincas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eliminar.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        // DE AQUI EN ADELNATE VAN MIS METODOS

        // GET: DocsCompraventaFincas/Create
        public IActionResult CreateDocsCompraventaFincas()
        {
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula");
            ViewData["CedulaComprador"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1");
            ViewData["CedulaVendedor"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1");
            ViewData["DistritoFinca"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito");
            ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito");
            return View();
        }

        // POST: DocsCompraventaFincas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDocsCompraventaFincas([Bind("IdDocumento,NumeroEscritura,CedulaAbogado,CedulaVendedor,CedulaComprador,MontoVenta,PartidoFinca,MatriculaFinca,NaturalezaFinca,DistritoFinca,CantonFinca,ProvinciaFinca,AreaFincaM2,PlanoCatastrado,ColindaNorte,ColindaSur,ColindaEste,ColindaOeste,FormaPago,MedioPago,OrigenFondos,LugarFirma,HoraFirma,FechaFirma")] DocsCompraventaFincaDTO tDocsCompraventaFinca)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tDocsCompraventaFinca);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tDocsCompraventaFinca.CedulaAbogado);
            ViewData["CedulaComprador"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsCompraventaFinca.CedulaComprador);
            ViewData["CedulaVendedor"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsCompraventaFinca.CedulaVendedor);
            ViewData["DistritoFinca"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsCompraventaFinca.DistritoFinca);
            ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsCompraventaFinca.LugarFirma);
            return View(tDocsCompraventaFinca);
        }

    }
}
