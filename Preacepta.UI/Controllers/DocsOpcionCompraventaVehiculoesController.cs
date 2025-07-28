using DinkToPdf;
using DinkToPdf.Contracts;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Preacepta.UI.Controllers
{
    public class DocsOpcionCompraventaVehiculoesController : Controller
    {
        private readonly IConverter _converter;
        private readonly Contexto _context;
        private readonly IBuscarDocCVLN _buscar;
        private readonly ICrearDocCVLN _crear;
        private readonly IEditarDocCVLN _editar;
        private readonly IEliminarDocCVLN _eliminar;
        private readonly IListarDocCVLN _listar;

        public DocsOpcionCompraventaVehiculoesController(IBuscarDocCVLN buscar,
            ICrearDocCVLN crear,
            IEditarDocCVLN editar,
            IEliminarDocCVLN eliminar,
            IListarDocCVLN listar,
            IConverter converter,
            Contexto context)
        {
            _converter = converter;
            _context = context;
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
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula");
            ViewData["CedulaComprador"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula");
            ViewData["CedulaPropietario"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula");
            ViewData["Combustible"] = new SelectList(_context.TDocsCombustibles, "IdCombustible", "Nombre");
            ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "Nombre");
            ViewData["MarcaMotor"] = new SelectList(_context.TDocsMarcaVehiculos, "IdMarca", "Nombre");
            ViewData["MarcaVehiculo"] = new SelectList(_context.TDocsMarcaVehiculos, "IdMarca", "Nombre");
            ViewData["TipoVehiculo"] = new SelectList(_context.TDocsTipoVehiculos, "IdTipo", "Nombre");
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
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula");
            ViewData["CedulaComprador"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula");
            ViewData["CedulaPropietario"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula");
            ViewData["Combustible"] = new SelectList(_context.TDocsCombustibles, "IdCombustible", "Nombre");
            ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "Nombre");
            ViewData["MarcaMotor"] = new SelectList(_context.TDocsMarcaVehiculos, "IdMarca", "Nombre");
            ViewData["MarcaVehiculo"] = new SelectList(_context.TDocsMarcaVehiculos, "IdMarca", "Nombre");
            ViewData["TipoVehiculo"] = new SelectList(_context.TDocsTipoVehiculos, "IdTipo", "Nombre");
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
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula");
            ViewData["CedulaComprador"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula");
            ViewData["CedulaPropietario"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula");
            ViewData["Combustible"] = new SelectList(_context.TDocsCombustibles, "IdCombustible", "Nombre");
            ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "Nombre");
            ViewData["MarcaMotor"] = new SelectList(_context.TDocsMarcaVehiculos, "IdMarca", "Nombre");
            ViewData["MarcaVehiculo"] = new SelectList(_context.TDocsMarcaVehiculos, "IdMarca", "Nombre");
            ViewData["TipoVehiculo"] = new SelectList(_context.TDocsTipoVehiculos, "IdTipo", "Nombre");
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
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula");
            ViewData["CedulaComprador"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula");
            ViewData["CedulaPropietario"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula");
            ViewData["Combustible"] = new SelectList(_context.TDocsCombustibles, "IdCombustible", "Nombre");
            ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "Nombre");
            ViewData["MarcaMotor"] = new SelectList(_context.TDocsMarcaVehiculos, "IdMarca", "Nombre");
            ViewData["MarcaVehiculo"] = new SelectList(_context.TDocsMarcaVehiculos, "IdMarca", "Nombre");
            ViewData["TipoVehiculo"] = new SelectList(_context.TDocsTipoVehiculos, "IdTipo", "Nombre");
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

        //De aquí en adelante estan mis metodos
        // GET: DocsOpcionCompraventaVehiculoes/Create
        public IActionResult CreateDocsOpcionCompraventaVehiculoes()
        {
            try
            {
                ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula");

                ViewData["CedulaComprador"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula");
                ViewData["CedulaPropietario"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula");

                ViewData["Combustible"] = new SelectList(_context.TDocsCombustibles, "Id", "Nombre");

                ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito");

                ViewData["MarcaMotor"] = new SelectList(_context.TDocsMarcaVehiculos, "Id", "Nombre");
                ViewData["MarcaVehiculo"] = new SelectList(_context.TDocsMarcaVehiculos, "Id", "Nombre");

                ViewData["TipoVehiculo"] = new SelectList(_context.TDocsTipoVehiculos, "Id", "Nombre");

            }
            catch (Exception ex)
            {
                return Content($"Error cargando combos: {ex.Message}");
            }

            return View();
        }

        // POST: DocsOpcionCompraventaVehiculoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDocsOpcionCompraventaVehiculoes(
    [Bind("NumeroEscritura,CedulaAbogado,CedulaPropietario,CedulaComprador,PlacaVehiculo,MarcaVehiculo,TipoVehiculo,ModeloVehiculo,Carroceria,Categoria,Chasis,Serie,Vin,MarcaMotor,NumeroMotor,Color,Combustible,Anio,Capacidad,Cilindraje,Precio,MonedaPrecio,PlazoOpcionAnios,FechaInicio,MontoSenal,MonedaSenal,MontoADevolver,MontoAPerder,MonedaMontoPerdido,GastosTraspasoPagadosPor,LugarFirma,HoraFirma,FechaFirma")] DocsOpcionCompraventaVehiculoDTO tcompraventa,
    [FromForm] int? DocumentoAnteriorId)
        {
            if (ModelState.IsValid)
            {
                // Establecer fecha y hora si no vienen
                tcompraventa.FechaFirma ??= DateTime.Today.ToString("yyyy-MM-dd");
                tcompraventa.HoraFirma ??= DateTime.Now.ToString("HH:mm");

                // Eliminar documento anterior y su historial (si aplica)
                if (DocumentoAnteriorId.HasValue)
                {
                    // Buscar el historial con ese ID
                    var historialAnterior = await _context.HistorialDocumentos
                        .FirstOrDefaultAsync(h => h.Id == DocumentoAnteriorId.Value);

                    if (historialAnterior != null)
                    {
                        // Obtener el ID del documento a eliminar
                        var idDocOriginal = historialAnterior.DocumentoIdOriginal;

                        // Buscar y eliminar el documento original
                        var docAnterior = await _context.TDocsOpcionCompraventaVehiculos
                            .FirstOrDefaultAsync(d => d.IdDocumento == idDocOriginal);

                        if (docAnterior != null)
                            _context.TDocsOpcionCompraventaVehiculos.Remove(docAnterior);

                        // Eliminar historial también
                        _context.HistorialDocumentos.Remove(historialAnterior);

                        await _context.SaveChangesAsync();
                    }
                }

                // Crear nuevo documento
                await _crear.crear(tcompraventa);

                // Obtener nombre del comprador
                var comprador = await _context.TGePersonas
                    .FirstOrDefaultAsync(p => p.Cedula == tcompraventa.CedulaComprador);

                string nombreComprador = comprador != null
                    ? $"{comprador.Nombre} {comprador.Apellido1} {comprador.Apellido2}"
                    : tcompraventa.CedulaComprador.ToString();

                // Obtener nombre del abogado
                var abogado = await _context.TGeAbogados
                    .Where(a => a.Cedula == tcompraventa.CedulaAbogado)
                    .Join(_context.TGePersonas,
                          ab => ab.Cedula,
                          per => per.Cedula,
                          (ab, per) => new
                          {
                              NombreCompleto = per.Nombre + " " + per.Apellido1 + " " + per.Apellido2
                          })
                    .FirstOrDefaultAsync();

                string nombreAbogado = abogado?.NombreCompleto ?? "SIN_ABOGADO";

                // Crear nuevo historial con el ID real del documento recién creado
                var nuevoIdDocumento = _context.TDocsOpcionCompraventaVehiculos
                    .OrderByDescending(x => x.IdDocumento)
                    .Select(x => x.IdDocumento)
                    .FirstOrDefault();

                var historial = new HistorialDocumento
                {
                    Fecha = DateTime.Now,
                    TipoDocumento = "Compra y venta de vehículos",
                    Cliente = nombreComprador,
                    Abogado = nombreAbogado,
                    DocumentoIdOriginal = nuevoIdDocumento
                };

                _context.HistorialDocumentos.Add(historial);
                await _context.SaveChangesAsync();

                return RedirectToAction("DocsHistorial", "HistorialDocumentos");
            }

            // Si el modelo no es válido, retornar vista con datos actuales
            return View(tcompraventa);
        }




        [HttpGet]
        public IActionResult PrevisualizarPDF()
        {
            var htmlPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DocsMachotes", "OpcionCompraVentaVehiculos.html");
            var htmlTemplate = System.IO.File.ReadAllText(htmlPath);

            var q = Request.Query;

            // Mostrar los parámetros que llegan para depurar
            foreach (var key in q.Keys)
            {
                Console.WriteLine($"[DEBUG] Param: '{key}' = '{q[key]}'");
            }

            htmlTemplate = htmlTemplate
                .Replace("{{ID_DOCUMENTO}}", q["idDocumento"])
                .Replace("{{NUMERO_ESCRITURA}}", q["numeroEscritura"])
                .Replace("{{CEDULA_NOTARIO}}", q["cedulaAbogado"])
                .Replace("{{CEDULA_VENDEDOR}}", q["cedulaPropietario"])
                .Replace("{{CEDULA_COMPRADOR}}", q["cedulaComprador"])
                .Replace("{{PLACA_VEHICULO}}", q["placaVehiculo"])
                .Replace("{{MARCA_VEHICULO}}", q["marcaVehiculo"])
                .Replace("{{TIPO_VEHICULO}}", q["tipoVehiculo"])
                .Replace("{{MODELO_VEHICULO}}", q["modeloVehiculo"])
                .Replace("{{CARROCERIA}}", q["carroceria"])
                .Replace("{{CATEGORIA}}", q["categoria"])
                .Replace("{{CHASIS}}", q["chasis"])
                .Replace("{{SERIE}}", q["serie"])
                .Replace("{{VIN}}", q["vin"])
                .Replace("{{MARCA_MOTOR}}", q["marcaMotor"])
                .Replace("{{NUMERO_MOTOR}}", q["numeroMotor"])
                .Replace("{{COLOR}}", q["color"])
                .Replace("{{COMBUSTIBLE}}", q["combustible"])
                .Replace("{{ANIO}}", q["anio"])
                .Replace("{{CAPACIDAD}}", q["capacidad"])
                .Replace("{{CILINDRAJE}}", q["cilindraje"])
                .Replace("{{PRECIO}}", q["precio"])
                .Replace("{{MONEDA_PRECIO}}", q["monedaPrecio"])
                .Replace("{{PLAZO_OPCION_ANIOS}}", q["plazoOpcionAnios"])
                .Replace("{{FECHA_INICIO}}", q["fechaInicio"])
                .Replace("{{MONTO_SENAL}}", q["montoSenal"])
                .Replace("{{MONEDA_SENAL}}", q["monedaSenal"])
                .Replace("{{MONTO_A_DEVOLVER}}", q["montoADevolver"])
                .Replace("{{MONTO_A_PERDER}}", q["montoAPerder"])
                .Replace("{{MONEDA_MONTO_PERDIDO}}", q["monedaMontoPerdido"])
                .Replace("{{GASTOS_TRASPASO_PAGADOS_POR}}", q["gastosTraspasoPagadosPor"])
                .Replace("{{LUGAR_FIRMA}}", q["lugarFirma"])
                .Replace("{{HORA_FIRMA}}", q["horaFirma"])
                .Replace("{{FECHA_FIRMA}}", q["fechaFirma"]);

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait
                },
                Objects = {
            new ObjectSettings
            {
                HtmlContent = htmlTemplate,
                WebSettings = { DefaultEncoding = "utf-8" }
            }
        }
            };

            var pdf = _converter.Convert(doc);
            return File(pdf, "application/pdf");
        }


        [HttpGet]
        public async Task<IActionResult> EditarDesdeHistorial(int id)
        {
            var historial = await _context.HistorialDocumentos.FindAsync(id);
            if (historial == null || historial.TipoDocumento != "Compra y venta de vehículos" || historial.DocumentoIdOriginal == null)
            {
                return NotFound();
            }

            // ✅ Buscar por ID directo (seguro y preciso)
            var docOriginal = await _context.TDocsOpcionCompraventaVehiculos
                .FirstOrDefaultAsync(d => d.IdDocumento == historial.DocumentoIdOriginal);

            if (docOriginal == null)
            {
                return NotFound();
            }

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
                HoraFirma = docOriginal.HoraFirma.ToString("HH:mm"),
                FechaFirma = docOriginal.FechaFirma.ToString("yyyy-MM-dd")
            };

            ViewBag.DocumentoAnteriorId = historial.Id;

            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", model.CedulaAbogado);
            ViewData["CedulaComprador"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula", model.CedulaComprador);
            ViewData["CedulaPropietario"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula", model.CedulaPropietario);
            ViewData["Combustible"] = new SelectList(_context.TDocsCombustibles, "Id", "Nombre", model.Combustible);
            ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", model.LugarFirma);
            ViewData["MarcaMotor"] = new SelectList(_context.TDocsMarcaVehiculos, "Id", "Nombre", model.MarcaMotor);
            ViewData["MarcaVehiculo"] = new SelectList(_context.TDocsMarcaVehiculos, "Id", "Nombre", model.MarcaVehiculo);
            ViewData["TipoVehiculo"] = new SelectList(_context.TDocsTipoVehiculos, "Id", "Nombre", model.TipoVehiculo);

            return View("CreateDocsOpcionCompraventaVehiculoes", model);
        }


    }
}
