using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.DocsOpcionCompraventaVehiculo.Buscar;
using Preacepta.LN.DocsOpcionCompraventaVehiculo.Crear;
using Preacepta.LN.DocsOpcionCompraventaVehiculo.Editar;
using Preacepta.LN.DocsOpcionCompraventaVehiculo.Eliminar;
using Preacepta.LN.DocsOpcionCompraventaVehiculo.Listar;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.UI.Controllers
{
    public class DocsOpcionCompraventaVehiculoesController : Controller
    {
        private readonly IBuscarDocCVLN _buscar;
        private readonly ICrearDocCVLN _crear;
        private readonly IEditarDocCVLN _editar;
        private readonly IEliminarDocCVLN _eliminar;
        private readonly IListarDocCVLN _listar;

        public DocsOpcionCompraventaVehiculoesController(IBuscarDocCVLN buscar,
            ICrearDocCVLN crear,
            IEditarDocCVLN editar,
            IEliminarDocCVLN eliminar,
            IListarDocCVLN listar)
        {
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;
        }

        // GET: DocsOpcionCompraventaVehiculoes
        public async Task<IActionResult> Index()
        {
            return View(await _listar.Listar());
        }

        // GET: DocsOpcionCompraventaVehiculoes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comV = await _buscar.buscar(id);
            if (comV == null)
            {
                return NotFound();
            }

            return View(comV);
        }

        // GET: DocsOpcionCompraventaVehiculoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DocsOpcionCompraventaVehiculoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDocumento,NumeroEscritura,CedulaAbogado,CedulaPropietario,CedulaComprador,PlacaVehiculo,MarcaVehiculo,TipoVehiculo,ModeloVehiculo,Carroceria,Categoria,Chasis,Serie,Vin,MarcaMotor,NumeroMotor,Color,Combustible,Anio,Capacidad,Cilindraje,Precio,MonedaPrecio,PlazoOpcionAnios,FechaInicio,MontoSenal,MonedaSenal,MontoADevolver,MontoAPerder,MonedaMontoPerdido,GastosTraspasoPagadosPor,LugarFirma,HoraFirma,FechaFirma")] DocsOpcionCompraventaVehiculoDTO tDocsOpcionCompraventaVehiculo)
        {
            if (ModelState.IsValid)
            {
                tDocsOpcionCompraventaVehiculo.FechaInicio = DateTime.Today.ToString("yyyy-MM-dd");
                tDocsOpcionCompraventaVehiculo.HoraFirma = DateTime.Now.ToString("HH:mm");

                await _crear.crear(tDocsOpcionCompraventaVehiculo);
                return RedirectToAction(nameof(Index));
            }
            return View(tDocsOpcionCompraventaVehiculo);
        }

        // GET: DocsOpcionCompraventaVehiculoes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsOpcionCompraventaVehiculo = await _buscar.buscar(id);
            if (tDocsOpcionCompraventaVehiculo == null)
            {
                return NotFound();
            }
            return View(tDocsOpcionCompraventaVehiculo);
        }

        // POST: DocsOpcionCompraventaVehiculoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDocumento,NumeroEscritura,CedulaAbogado,CedulaPropietario,CedulaComprador,PlacaVehiculo,MarcaVehiculo,TipoVehiculo,ModeloVehiculo,Carroceria,Categoria,Chasis,Serie,Vin,MarcaMotor,NumeroMotor,Color,Combustible,Anio,Capacidad,Cilindraje,Precio,MonedaPrecio,PlazoOpcionAnios,FechaInicio,MontoSenal,MonedaSenal,MontoADevolver,MontoAPerder,MonedaMontoPerdido,GastosTraspasoPagadosPor,LugarFirma,HoraFirma,FechaFirma")] DocsOpcionCompraventaVehiculoDTO tDocsOpcionCompraventaVehiculo)
        {
            if (id != tDocsOpcionCompraventaVehiculo.IdDocumento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.editar(tDocsOpcionCompraventaVehiculo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tDocsOpcionCompraventaVehiculo);
        }

        // GET: DocsOpcionCompraventaVehiculoes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsOpcionCompraventaVehiculo = await _buscar.buscar(id);
            if (tDocsOpcionCompraventaVehiculo == null)
            {
                return NotFound();
            }

            return View(tDocsOpcionCompraventaVehiculo);
        }

        // POST: DocsOpcionCompraventaVehiculoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eliminar.eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: DocsOpcionCompraventaVehiculoes/Create
        public IActionResult CreateDocsOpcionCompraventaVehiculoes()
        {
            return View();
        }

        // POST: DocsOpcionCompraventaVehiculoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDocsOpcionCompraventaVehiculoes([Bind("IdDocumento,NumeroEscritura,CedulaAbogado,CedulaPropietario,CedulaComprador,PlacaVehiculo,MarcaVehiculo,TipoVehiculo,ModeloVehiculo,Carroceria,Categoria,Chasis,Serie,Vin,MarcaMotor,NumeroMotor,Color,Combustible,Anio,Capacidad,Cilindraje,Precio,MonedaPrecio,PlazoOpcionAnios,FechaInicio,MontoSenal,MonedaSenal,MontoADevolver,MontoAPerder,MonedaMontoPerdido,GastosTraspasoPagadosPor,LugarFirma,HoraFirma,FechaFirma")] DocsOpcionCompraventaVehiculoDTO tDocsOpcionCompraventaVehiculo)
        {
            if (ModelState.IsValid)
            {
                tDocsOpcionCompraventaVehiculo.FechaInicio = DateTime.Today.ToString("yyyy-MM-dd");
                tDocsOpcionCompraventaVehiculo.HoraFirma = DateTime.Now.ToString("HH:mm");

                await _crear.crear(tDocsOpcionCompraventaVehiculo);
                return RedirectToAction(nameof(Index));
            }
            return View(tDocsOpcionCompraventaVehiculo);
        }
    }
}
