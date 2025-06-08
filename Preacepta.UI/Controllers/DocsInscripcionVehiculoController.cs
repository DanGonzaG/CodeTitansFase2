using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.DocsInscripcionVehiculo.BuscarXid;
using Preacepta.LN.DocsInscripcionVehiculo.Crear;
using Preacepta.LN.DocsInscripcionVehiculo.Editar;
using Preacepta.LN.DocsInscripcionVehiculo.Eliminar;
using Preacepta.LN.DocsInscripcionVehiculo.Listar;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Preacepta.UI.Controllers
{
    public class DocsInscripcionVehiculoController : Controller
    {
        private readonly Contexto _context;
        private readonly IBuscarDocsInscripcionVehiculoLN _buscar;
        private readonly ICrearDocsInscripcionVehiculoLN _crear;
        private readonly IEditarDocsInscripcionVehiculoLN _editar;
        private readonly IEliminarDocsInscripcionVehiculoLN _eliminar;
        private readonly IListarDocsInscripcionVehiculoLN _listar;

        public DocsInscripcionVehiculoController(Contexto context,
            IBuscarDocsInscripcionVehiculoLN buscar,
            ICrearDocsInscripcionVehiculoLN crear,
            IEditarDocsInscripcionVehiculoLN editar,
            IEliminarDocsInscripcionVehiculoLN eliminar,
            IListarDocsInscripcionVehiculoLN listar)
        {
            _context = context;
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;
        }

        // GET: TDocsInscripcionVehiculo
        public async Task<IActionResult> Index()
        {
            //var contexto = _context.TDocsInscripcionVehiculos.Include(t => t.CedulaAbogadoNavigation).Include(t => t.CedulaClienteNavigation).Include(t => t.EstiloVehiculoNavigation).Include(t => t.LugarFirmaNavigation).Include(t => t.MarcaVehiculoNavigation);
            return View(await _listar.listar());
        }

        // GET: TDocsInscripcionVehiculo/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsInscripcionVehiculo = await _buscar.buscar(id);
            if (tDocsInscripcionVehiculo == null)
            {
                return NotFound();
            }

            return View(tDocsInscripcionVehiculo);
        }

        // GET: TDocsInscripcionVehiculo/Create
        public IActionResult Create()
        {
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula");
            ViewData["CedulaCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1");
            ViewData["EstiloVehiculo"] = new SelectList(_context.TDocsTipoVehiculos, "Id", "Nombre");
            ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito");
            ViewData["MarcaVehiculo"] = new SelectList(_context.TDocsMarcaVehiculos, "Id", "Nombre");
            return View();
        }

        // POST: TDocsInscripcionVehiculo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDocumento,CedulaCliente,CedulaAbogado,MarcaVehiculo,EstiloVehiculo,ModeloVehiculo,Categoria,MarcaMotor,NumeroMotor,NumeroSerieChasis,Vin,Anio,Carroceria,PesoNeto,PesoBruto,Potencia,Color,Capacidad,Combustible,Cilindraje,LugarFirma,FechaFirma")] DocsInscripcionVehiculoDTO tDocsInscripcionVehiculo)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tDocsInscripcionVehiculo);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tDocsInscripcionVehiculo.CedulaAbogado);
            ViewData["CedulaCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsInscripcionVehiculo.CedulaCliente);
            ViewData["EstiloVehiculo"] = new SelectList(_context.TDocsTipoVehiculos, "Id", "Nombre", tDocsInscripcionVehiculo.EstiloVehiculo);
            ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsInscripcionVehiculo.LugarFirma);
            ViewData["MarcaVehiculo"] = new SelectList(_context.TDocsMarcaVehiculos, "Id", "Nombre", tDocsInscripcionVehiculo.MarcaVehiculo);
            return View(tDocsInscripcionVehiculo);
        }

        // GET: TDocsInscripcionVehiculo/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsInscripcionVehiculo = await _buscar.buscar(id);
            if (tDocsInscripcionVehiculo == null)
            {
                return NotFound();
            }
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tDocsInscripcionVehiculo.CedulaAbogado);
            ViewData["CedulaCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsInscripcionVehiculo.CedulaCliente);
            ViewData["EstiloVehiculo"] = new SelectList(_context.TDocsTipoVehiculos, "Id", "Nombre", tDocsInscripcionVehiculo.EstiloVehiculo);
            ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsInscripcionVehiculo.LugarFirma);
            ViewData["MarcaVehiculo"] = new SelectList(_context.TDocsMarcaVehiculos, "Id", "Nombre", tDocsInscripcionVehiculo.MarcaVehiculo);
            return View(tDocsInscripcionVehiculo);
        }

        // POST: TDocsInscripcionVehiculo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDocumento,CedulaCliente,CedulaAbogado,MarcaVehiculo,EstiloVehiculo,ModeloVehiculo,Categoria,MarcaMotor,NumeroMotor,NumeroSerieChasis,Vin,Anio,Carroceria,PesoNeto,PesoBruto,Potencia,Color,Capacidad,Combustible,Cilindraje,LugarFirma,FechaFirma")] DocsInscripcionVehiculoDTO tDocsInscripcionVehiculo)
        {
            if (id != tDocsInscripcionVehiculo.IdDocumento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.Editar(tDocsInscripcionVehiculo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tDocsInscripcionVehiculo.CedulaAbogado);
            ViewData["CedulaCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsInscripcionVehiculo.CedulaCliente);
            ViewData["EstiloVehiculo"] = new SelectList(_context.TDocsTipoVehiculos, "Id", "Nombre", tDocsInscripcionVehiculo.EstiloVehiculo);
            ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsInscripcionVehiculo.LugarFirma);
            ViewData["MarcaVehiculo"] = new SelectList(_context.TDocsMarcaVehiculos, "Id", "Nombre", tDocsInscripcionVehiculo.MarcaVehiculo);
            return View(tDocsInscripcionVehiculo);
        }

        // GET: TDocsInscripcionVehiculo/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsInscripcionVehiculo = await _buscar.buscar(id);
            if (tDocsInscripcionVehiculo == null)
            {
                return NotFound();
            }

            return View(tDocsInscripcionVehiculo);
        }

        // POST: TDocsInscripcionVehiculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eliminar.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: TDocsInscripcionVehiculo/Create
        public IActionResult CreateDocsInscripcionVehiculo()
        {
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula");
            ViewData["CedulaCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1");
            ViewData["EstiloVehiculo"] = new SelectList(_context.TDocsTipoVehiculos, "Id", "Nombre");
            ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito");
            ViewData["MarcaVehiculo"] = new SelectList(_context.TDocsMarcaVehiculos, "Id", "Nombre");
            return View();
        }

        // POST: TDocsInscripcionVehiculo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDocsInscripcionVehiculo([Bind("IdDocumento,CedulaCliente,CedulaAbogado,MarcaVehiculo,EstiloVehiculo,ModeloVehiculo,Categoria,MarcaMotor,NumeroMotor,NumeroSerieChasis,Vin,Anio,Carroceria,PesoNeto,PesoBruto,Potencia,Color,Capacidad,Combustible,Cilindraje,LugarFirma,FechaFirma")] DocsInscripcionVehiculoDTO tDocsInscripcionVehiculo)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tDocsInscripcionVehiculo);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tDocsInscripcionVehiculo.CedulaAbogado);
            ViewData["CedulaCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsInscripcionVehiculo.CedulaCliente);
            ViewData["EstiloVehiculo"] = new SelectList(_context.TDocsTipoVehiculos, "Id", "Nombre", tDocsInscripcionVehiculo.EstiloVehiculo);
            ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsInscripcionVehiculo.LugarFirma);
            ViewData["MarcaVehiculo"] = new SelectList(_context.TDocsMarcaVehiculos, "Id", "Nombre", tDocsInscripcionVehiculo.MarcaVehiculo);
            return View(tDocsInscripcionVehiculo);
        }

    }
}