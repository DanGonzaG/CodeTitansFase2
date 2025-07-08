using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Preacepta.UI.Controllers
{
    public class HistorialDocumentosController : Controller
    {
        private readonly Contexto _context;

        public HistorialDocumentosController(Contexto context)
        {
            _context = context;
        }

        // GET: HistorialDocumentoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.HistorialDocumentos.ToListAsync());
        }

        // GET: HistorialDocumentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialDocumento = await _context.HistorialDocumentos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historialDocumento == null)
            {
                return NotFound();
            }

            return View(historialDocumento);
        }

        // GET: HistorialDocumentoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HistorialDocumentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,TipoDocumento,Cliente,Abogado")] HistorialDocumento historialDocumento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historialDocumento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(historialDocumento);
        }

        // GET: HistorialDocumentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialDocumento = await _context.HistorialDocumentos.FindAsync(id);
            if (historialDocumento == null)
            {
                return NotFound();
            }
            return View(historialDocumento);
        }

        // POST: HistorialDocumentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,TipoDocumento,Cliente,Abogado")] HistorialDocumento historialDocumento)
        {
            if (id != historialDocumento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historialDocumento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistorialDocumentoExists(historialDocumento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(historialDocumento);
        }

        // GET: HistorialDocumentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialDocumento = await _context.HistorialDocumentos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historialDocumento == null)
            {
                return NotFound();
            }

            return View(historialDocumento);
        }

        // POST: HistorialDocumentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historialDocumento = await _context.HistorialDocumentos.FindAsync(id);
            if (historialDocumento != null)
            {
                _context.HistorialDocumentos.Remove(historialDocumento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistorialDocumentoExists(int id)
        {
            return _context.HistorialDocumentos.Any(e => e.Id == id);
        }

        //Mis metodos
        public async Task<IActionResult> DocsHistorial()
        {
            var historial = await _context.HistorialDocumentos
                .OrderByDescending(h => h.Fecha)
                .ToListAsync();

            return View(historial);
        }

        [HttpGet]
        public async Task<IActionResult> EditarDesdeHistorial(int id)
        {
            var historial = await _context.HistorialDocumentos.FindAsync(id);
            if (historial == null) return NotFound();

            // --- Pagaré ---
            if (historial.TipoDocumento == "Pagaré")
            {
                if (historial.DocumentoIdOriginal == null) return NotFound();

                var docOriginal = await _context.TDocsPagares
                    .FirstOrDefaultAsync(p => p.IdDocumento == historial.DocumentoIdOriginal.Value);
                if (docOriginal == null) return NotFound();

                var model = new DocsPagareDTO
                {
                    MontoNumerico = docOriginal.MontoNumerico,
                    CedulaDeudor = docOriginal.CedulaDeudor,
                    SociedadDeudor = docOriginal.SociedadDeudor,
                    CedulaJuridicaSociedad = docOriginal.CedulaJuridicaSociedad,
                    AcreedorNombre = docOriginal.AcreedorNombre,
                    CedulaJuridicaAcreedor = docOriginal.CedulaJuridicaAcreedor,
                    AcreedorDomicilio = docOriginal.AcreedorDomicilio,
                    FechaVencimiento = docOriginal.FechaVencimiento.ToString("yyyy-MM-dd"),
                    InteresFormula = docOriginal.InteresFormula,
                    InteresTasaActual = docOriginal.InteresTasaActual,
                    InteresBase = docOriginal.InteresBase,
                    LugarPago = docOriginal.LugarPago,
                    CedulaFiador = docOriginal.CedulaFiador,
                    UbicacionFirma = docOriginal.UbicacionFirma
                };

                ViewBag.DocumentoAnteriorId = historial.Id;

                ViewData["CedulaDeudor"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula", model.CedulaDeudor);
                ViewData["CedulaFiador"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula", model.CedulaFiador);
                ViewData["LugarPago"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", model.LugarPago);
                ViewData["UbicacionFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", model.UbicacionFirma);

                return View("~/Views/TDocsPagares/CreateDocsPagares.cshtml", model);
            }

            // --- Compra y venta de vehículos ---
            if (historial.TipoDocumento == "Compra y venta de vehículos")
            {
                if (historial.DocumentoIdOriginal == null) return NotFound();

                var docOriginal = await _context.TDocsOpcionCompraventaVehiculos
                    .FirstOrDefaultAsync(p => p.IdDocumento == historial.DocumentoIdOriginal.Value);
                if (docOriginal == null) return NotFound();

                var model = new DocsOpcionCompraventaVehiculoDTO
                {
                    NumeroEscritura = docOriginal.NumeroEscritura,
                    CedulaAbogado = docOriginal.CedulaAbogado,
                    CedulaPropietario = docOriginal.CedulaPropietario,
                    CedulaComprador = docOriginal.CedulaComprador,
                    PlacaVehiculo = docOriginal.PlacaVehiculo,
                    MarcaVehiculo = docOriginal.MarcaVehiculo,
                    TipoVehiculo = docOriginal.TipoVehiculo,
                    ModeloVehiculo = docOriginal.ModeloVehiculo,
                    Carroceria = docOriginal.Carroceria,
                    Categoria = docOriginal.Categoria,
                    Chasis = docOriginal.Chasis,
                    Serie = docOriginal.Serie,
                    Vin = docOriginal.Vin,
                    MarcaMotor = docOriginal.MarcaMotor,
                    NumeroMotor = docOriginal.NumeroMotor,
                    Color = docOriginal.Color,
                    Combustible = docOriginal.Combustible,
                    Anio = docOriginal.Anio,
                    Capacidad = docOriginal.Capacidad,
                    Cilindraje = docOriginal.Cilindraje,
                    Precio = docOriginal.Precio,
                    MonedaPrecio = docOriginal.MonedaPrecio,
                    PlazoOpcionAnios = docOriginal.PlazoOpcionAnios,
                    FechaInicio = docOriginal.FechaInicio.ToString("yyyy-MM-dd"),
                    MontoSenal = docOriginal.MontoSenal,
                    MonedaSenal = docOriginal.MonedaSenal,
                    MontoADevolver = docOriginal.MontoADevolver,
                    MontoAPerder = docOriginal.MontoAPerder,
                    MonedaMontoPerdido = docOriginal.MonedaMontoPerdido,
                    GastosTraspasoPagadosPor = docOriginal.GastosTraspasoPagadosPor,
                    LugarFirma = docOriginal.LugarFirma,
                    FechaFirma = docOriginal.FechaFirma.ToString("yyyy-MM-dd"),
                    HoraFirma = docOriginal.FechaFirma.ToString("HH:mm")
                };

                ViewBag.DocumentoAnteriorId = docOriginal.IdDocumento;

                ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", model.CedulaAbogado);
                ViewData["CedulaComprador"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", model.CedulaComprador);
                ViewData["CedulaPropietario"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", model.CedulaPropietario);
                ViewData["Combustible"] = new SelectList(_context.TDocsCombustibles, "Id", "Nombre", model.Combustible);
                ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "nombreDistrito", model.LugarFirma);
                ViewData["MarcaMotor"] = new SelectList(_context.TDocsMarcaVehiculos, "Id", "Nombre", model.MarcaMotor);
                ViewData["MarcaVehiculo"] = new SelectList(_context.TDocsMarcaVehiculos, "Id", "Nombre", model.MarcaVehiculo);
                ViewData["TipoVehiculo"] = new SelectList(_context.TDocsTipoVehiculos, "Id", "Nombre", model.TipoVehiculo);

                // Aquí podrías añadir ViewData si tienes selects en la vista
                return View("~/Views/DocsOpcionCompraventaVehiculoes/CreateDocsOpcionCompraventaVehiculoes.cshtml", model);
            }

            return RedirectToAction(nameof(DocsHistorial));
        }

    }
}  
