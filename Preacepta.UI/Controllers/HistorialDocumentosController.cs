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
        [HttpGet]
        public async Task<IActionResult> DocsHistorial(DateTime? fecha, string tipo)
        {
            var query = _context.HistorialDocumentos.AsQueryable();

            // Filtros
            if (fecha.HasValue)
            {
                query = query.Where(h => h.Fecha.Date == fecha.Value.Date);
            }

            if (!string.IsNullOrWhiteSpace(tipo))
            {
                query = query.Where(h => h.TipoDocumento == tipo);
            }

            // Para el dropdown
            var tiposDocumento = await _context.HistorialDocumentos
                .Select(h => h.TipoDocumento)
                .Distinct()
                .OrderBy(t => t)
                .ToListAsync();

            ViewBag.TipoDocumento = new SelectList(tiposDocumento, tipo); // 'tipo' es el valor seleccionado

            var historial = await query.OrderByDescending(h => h.Fecha).ToListAsync();
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
                    UbicacionFirma = docOriginal.UbicacionFirma,
                    FechaFirma = docOriginal.FechaFirma.ToString("yyyy-MM-dd"),
                    HoraFirma = docOriginal.HoraFirma.ToString("HH:mm")
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

                ViewBag.DocumentoAnteriorId = historial.Id;

                ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", model.CedulaAbogado);
                ViewData["CedulaComprador"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", model.CedulaComprador);
                ViewData["CedulaPropietario"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", model.CedulaPropietario);
                ViewData["Combustible"] = new SelectList(_context.TDocsCombustibles, "Id", "Nombre", model.Combustible);
                ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "nombreDistrito", model.LugarFirma);
                ViewData["MarcaMotor"] = new SelectList(_context.TDocsMarcaVehiculos, "Id", "Nombre", model.MarcaMotor);
                ViewData["MarcaVehiculo"] = new SelectList(_context.TDocsMarcaVehiculos, "Id", "Nombre", model.MarcaVehiculo);
                ViewData["TipoVehiculo"] = new SelectList(_context.TDocsTipoVehiculos, "Id", "Nombre", model.TipoVehiculo);

                return View("~/Views/DocsOpcionCompraventaVehiculoes/CreateDocsOpcionCompraventaVehiculoes.cshtml", model);
            }


            // --- Poderes especiales judiciales ---
            if (historial.TipoDocumento == "Poderes especiales judiciales")
            {
                if (historial.DocumentoIdOriginal == null) return NotFound();

                var docOriginal = await _context.TDocsPoderesEspecialesJudiciales
                    .FirstOrDefaultAsync(p => p.IdDoc == historial.DocumentoIdOriginal.Value);
                if (docOriginal == null) return NotFound();

                var model = new DocsPoderesEspecialesJudicialeDTO
                {
                    IdDoc = docOriginal.IdDoc,
                    Fecha = docOriginal.Fecha.ToString("yyyy-MM-dd"),
                    IdAbogado = docOriginal.IdAbogado,
                    IdCliente = docOriginal.IdCliente,
                    Texto = docOriginal.Texto
                };

                ViewBag.DocumentoAnteriorId = historial.Id;

                ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", model.CedulaAbogado);
                ViewData["CedulaAsistente"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula", model.CedulaAsistente);
                ViewData["CedulaImputado"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula", model.CedulaImputado);

                return View("~/Views/DocsAutorizacionRevisionExpedientes/CreateDocsAutorizacionRevisionExpedientes.cshtml", model);
            }

            // --- compra y venta de fincas ---
            if (historial.TipoDocumento == "Compra y Venta de Fincas")
            {
                if (historial.DocumentoIdOriginal == null) return NotFound();

                var docOriginal = await _context.TDocsCompraventaFincas
                    .FirstOrDefaultAsync(p => p.IdDocumento == historial.DocumentoIdOriginal.Value);
                if (docOriginal == null) return NotFound();

                var model = new DocsCompraventaFincaDTO
                {
                    NumeroEscritura = docOriginal.NumeroEscritura,
                    CedulaAbogado = docOriginal.CedulaAbogado,
                    CedulaVendedor = docOriginal.CedulaVendedor,
                    CedulaComprador = docOriginal.CedulaComprador,
                    MontoVenta = docOriginal.MontoVenta,
                    PartidoFinca = docOriginal.PartidoFinca,
                    MatriculaFinca = docOriginal.MatriculaFinca,
                    NaturalezaFinca = docOriginal.NaturalezaFinca,
                    DistritoFinca = docOriginal.DistritoFinca,
                    CantonFinca = docOriginal.CantonFinca,
                    ProvinciaFinca = docOriginal.ProvinciaFinca,
                    AreaFincaM2 = docOriginal.AreaFincaM2,
                    PlanoCatastrado = docOriginal.PlanoCatastrado,
                    ColindaNorte = docOriginal.ColindaNorte,
                    ColindaSur = docOriginal.ColindaSur,
                    ColindaEste = docOriginal.ColindaEste,
                    ColindaOeste = docOriginal.ColindaOeste,
                    FormaPago = docOriginal.FormaPago,
                    MedioPago = docOriginal.MedioPago,
                    OrigenFondos = docOriginal.OrigenFondos,
                    LugarFirma = docOriginal.LugarFirma,
                    HoraFirma = docOriginal.HoraFirma,
                    FechaFirma = docOriginal.FechaFirma
                };

                ViewBag.DocumentoAnteriorId = historial.Id;

                ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", model.CedulaAbogado);
                ViewData["CedulaComprador"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", model.CedulaComprador);
                ViewData["CedulaVendedor"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", model.CedulaVendedor);
                ViewData["ProvinciaFinca"] = new SelectList(_context.TCrProvincias, "IdProvincia", "NombreProvincia", model.ProvinciaFinca);
                ViewData["DistritoFinca"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", model.DistritoFinca);
                ViewData["CantonesFinca"] = new SelectList(_context.TCrCantones, "IdCanton", "NombreCanton", model.CantonFinca);
                ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", model.LugarFirma);

                return View("~/Views/DocsCompraventaFincas/CreateDocsCompraventaFincas.cshtml", model);
            }

            // --- contrato prestacion de servcios ---
            if (historial.TipoDocumento == "Contrato Prestación de Servicios")
            {
                if (historial.DocumentoIdOriginal == null) return NotFound();

                var docOriginal = await _context.TDocsContratoPrestacionServicios
                    .FirstOrDefaultAsync(p => p.IdDocumento == historial.DocumentoIdOriginal.Value);
                if (docOriginal == null) return NotFound();

                var model = new DocsContratoPrestacionServicioDTO
                {
                    RazonSocialEmpresa = docOriginal.RazonSocialEmpresa,
                    Provincia = docOriginal.Provincia,
                    CedulaJuridicaEmpresa = docOriginal.CedulaJuridicaEmpresa,
                    CedulaAbogado = docOriginal.CedulaAbogado,
                    CedulaCliente = docOriginal.CedulaCliente,
                    TipoServicios = docOriginal.TipoServicios,
                    FechaInicio = docOriginal.FechaInicio,
                    FechaFinal = docOriginal.FechaFinal,
                    MontoHonorarios = docOriginal.MontoHonorarios,
                    InformacionConfidencial = docOriginal.InformacionConfidencial,
                    CiudadFirma = docOriginal.CiudadFirma,
                    HoraFirma = docOriginal.HoraFirma,
                    FechaFirma = docOriginal.FechaFirma,
                };

                ViewBag.DocumentoAnteriorId = historial.Id;

                ViewData["CedulaAbogado"] = new SelectList(
                _context.TGeAbogados.Include(a => a.CedulaNavigation).Select(a => new
                {
                    Cedula = a.Cedula,
                    Texto = a.CedulaNavigation.Nombre + " " + a.CedulaNavigation.Apellido1 + " - " + a.Cedula
                }),
                "Cedula",
                "Texto", model?.CedulaAbogado);

                ViewData["CedulaCliente"] = new SelectList(
                    _context.TGePersonas.Select(p => new
                    {
                        Cedula = p.Cedula,
                        Texto = p.Nombre + " " + p.Apellido1 + " - " + p.Cedula
                    }),
                    "Cedula",
                    "Texto",
                    model?.CedulaCliente);
                //ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", model.CedulaAbogado);
                //ViewData["CedulaCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula", model.CedulaCliente);
                ViewData["CiudadFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", model.CiudadFirma);
                ViewData["Provincia"] = new SelectList(_context.TCrProvincias, "IdProvincia", "NombreProvincia", model.Provincia);

                return View("~/Views/DocsContratoPrestacionServicios/CreateDocsContratoPrestacionServicios.cshtml", model);
            }

            // --- contrato inscripcion de vehiculo ---
            if (historial.TipoDocumento == "Inscripción de Vehículos")
            {
                if (historial.DocumentoIdOriginal == null) return NotFound();

                var docOriginal = await _context.TDocsInscripcionVehiculos
                    .FirstOrDefaultAsync(p => p.IdDocumento == historial.DocumentoIdOriginal.Value);
                if (docOriginal == null) return NotFound();

                var model = new DocsInscripcionVehiculoDTO
                {
                    CedulaCliente = docOriginal.CedulaCliente,
                    CedulaAbogado = docOriginal.CedulaAbogado,
                    MarcaVehiculo = docOriginal.MarcaVehiculo,
                    EstiloVehiculo = docOriginal.EstiloVehiculo,
                    ModeloVehiculo = docOriginal.ModeloVehiculo,
                    Categoria = docOriginal.Categoria,
                    MarcaMotor = docOriginal.MarcaMotor,
                    NumeroMotor = docOriginal.NumeroMotor,
                    NumeroSerieChasis = docOriginal.NumeroSerieChasis,
                    Vin = docOriginal.Vin,
                    Anio = docOriginal.Anio,
                    Carroceria = docOriginal.Carroceria,
                    PesoNeto = docOriginal.PesoNeto,
                    PesoBruto = docOriginal.PesoBruto,
                    Potencia = docOriginal.Potencia,
                    Color = docOriginal.Color,
                    Capacidad = docOriginal.Capacidad,
                    Combustible = docOriginal.Combustible,
                    Cilindraje = docOriginal.Cilindraje,
                    LugarFirma = docOriginal.LugarFirma,
                    FechaFirma = docOriginal.FechaFirma
                };

                ViewBag.DocumentoAnteriorId = historial.Id;

                ViewData["CedulaAbogado"] = new SelectList(
                _context.TGeAbogados.Include(a => a.CedulaNavigation).Select(a => new
                {
                    Cedula = a.Cedula,
                    Texto = a.CedulaNavigation.Nombre + " " + a.CedulaNavigation.Apellido1 + " - " + a.Cedula
                }),
                "Cedula",
                "Texto", model?.CedulaAbogado);

                ViewData["CedulaCliente"] = new SelectList(
                    _context.TGePersonas.Select(p => new
                    {
                        Cedula = p.Cedula,
                        Texto = p.Nombre + " " + p.Apellido1 + " - " + p.Cedula
                    }),
                    "Cedula",
                    "Texto",
                    model?.CedulaCliente);
                ViewData["EstiloVehiculo"] = new SelectList(_context.TDocsTipoVehiculos, "Id", "Nombre", model.EstiloVehiculo);
                ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", model.LugarFirma);
                ViewData["MarcaVehiculo"] = new SelectList(_context.TDocsMarcaVehiculos, "Id", "Nombre", model.MarcaVehiculo);

                return View("~/Views/DocsInscripcionVehiculo/CreateDocsInscripcionVehiculo.cshtml", model);

            }

            return RedirectToAction(nameof(DocsHistorial));
        }



        public async Task<JsonResult> IdExiste(int id)
        {
            bool bandera;
            var ObjetoBuscado = await _context.HistorialDocumentos.FindAsync(id);
            if (ObjetoBuscado != null)
            {
                bandera = true;
                return Json(new { bandera });
            }
            bandera = false;
            return Json(new { bandera });
        }

    }
}
