using DinkToPdf;
using DinkToPdf.Contracts;
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
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Preacepta.UI.Controllers
{
    public class TDocsPagaresController : Controller
    {
        private readonly IConverter _converter;
        private readonly Contexto _context;
        private readonly IBuscarPagareLN _buscar;
        private readonly ICrearPagareLN _crear;
        private readonly IEditarPagareLN _editar;
        private readonly IEliminarPagareLN _eliminar;
        private readonly IListarPagareLN _listar;


        public TDocsPagaresController(IBuscarPagareLN buscar,
            ICrearPagareLN crear,
            IEditarPagareLN editar,
            IEliminarPagareLN eliminar,
            IListarPagareLN listar,
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
                /*tDocsPagare.FechaFirma = DateTime.Today.ToString("yyyy-MM-dd");
                tDocsPagare.HoraFirma = DateTime.Now.ToString("HH:mm"); ;*/

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

        //De aquí en adelante estan mis metodos
        //Creacion de paagare
        public IActionResult CreateDocsPagares()
        {
            ViewData["CedulaDeudor"] = new SelectList(
            _context.TGePersonas.Select(p => new
            {
                Cedula = p.Cedula,
                apellido = p.Apellido1,
                Texto = p.Nombre + " " + p.Apellido1 + " - " + p.Cedula
            }),
            "Cedula",
            "Texto");

            /*ViewData["CedulaDeudor"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula");*/
            ViewData["CedulaFiador"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula");

            ViewData["LugarPago"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito");
            ViewData["UbicacionFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito");

            return View();

        }

        // POST: TDocsPagares/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDocsPagares(
        [Bind("IdDocumento,MontoNumerico,CedulaDeudor,SociedadDeudor,CedulaJuridicaSociedad,AcreedorNombre,CedulaJuridicaAcreedor,AcreedorDomicilio,FechaVencimiento,InteresFormula,InteresTasaActual,InteresBase,LugarPago,CedulaFiador,UbicacionFirma")] DocsPagareDTO tDocsPagare,
        [FromForm] int? DocumentoAnteriorId
)
        {
            if (ModelState.IsValid)
            {
                // Establecer fecha y hora actuales si no vienen
               /* tDocsPagare.FechaFirma = DateTime.Today.ToString("yyyy-MM-dd");
                tDocsPagare.HoraFirma = DateTime.Now.ToString("HH:mm");*/

                // Eliminar documento anterior y su historial
                /*if (DocumentoAnteriorId.HasValue)
                {
                    // Buscar historial por ID
                    var historialAnterior = await _context.HistorialDocumentos
                        .FirstOrDefaultAsync(h => h.Id == DocumentoAnteriorId.Value);

                    if (historialAnterior != null)
                    {
                        // Obtener el ID del documento original desde el historial
                        var idDocOriginal = historialAnterior.DocumentoIdOriginal;

                        // Buscar y eliminar el documento original
                        var docAnterior = await _context.TDocsPagares
                            .FirstOrDefaultAsync(p => p.IdDocumento == idDocOriginal);

                        if (docAnterior != null)
                            _context.TDocsPagares.Remove(docAnterior);

                        // Eliminar también el historial
                        _context.HistorialDocumentos.Remove(historialAnterior);

                        await _context.SaveChangesAsync();
                    }
                }*/

                // Crear nuevo documento
                await _crear.crear(tDocsPagare);

                // Obtener nombre del deudor
                var deudor = await _context.TGePersonas
                    .FirstOrDefaultAsync(p => p.Cedula == tDocsPagare.CedulaDeudor);

                string nombreDeudor = deudor != null
                    ? $"{deudor.Nombre} {deudor.Apellido1} {deudor.Apellido2}"
                    : tDocsPagare.CedulaDeudor.ToString();

                // Crear nuevo historial con ID real del documento creado
                var nuevoIdDocumento = _context.TDocsPagares
                    .OrderByDescending(p => p.IdDocumento)
                    .Select(p => p.IdDocumento)
                    .FirstOrDefault();

                var nuevoHistorial = new HistorialDocumento
                {
                    Fecha = DateTime.Now,
                    TipoDocumento = "Pagaré",
                    Cliente = nombreDeudor,
                    Abogado = tDocsPagare.AcreedorNombre,
                    DocumentoIdOriginal = nuevoIdDocumento
                };

                _context.HistorialDocumentos.Add(nuevoHistorial);
                await _context.SaveChangesAsync();

                return RedirectToAction("DocsHistorial", "HistorialDocumentos");
            }

            // Si falla la validación, recargar listas
            ViewData["LugarPago"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsPagare.LugarPago);
            ViewData["CedulaDeudor"] = new SelectList(_context.TGePersonas.Select(p => new
            {
                Cedula = p.Cedula,
                apellido = p.Apellido1,
                Texto = p.Nombre + " " + p.Apellido1 + " - " + p.Cedula
            }),
            "Cedula", "Texto", tDocsPagare?.CedulaDeudor);
            ViewData["CedulaDeudor"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula", tDocsPagare.CedulaDeudor);
            ViewData["CedulaFiador"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula", tDocsPagare.CedulaFiador);
            ViewData["UbicacionFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsPagare.UbicacionFirma);

            return View(tDocsPagare);
        }





        [HttpGet]
        public IActionResult PrevisualizarPDF(
        string idDocumento,
        string montoNumerico,
        string cedulaDeudor,
        string sociedadDeudor,
        string cedulaJuridicaSociedad,
        string acreedorNombre,
        string cedulaJuridicaAcreedor,
        string acreedorDomicilio,
        string? fechaFirmaNuevo,
        string? horaFirmaNuevo,
        string fechaVencimiento,
        string interesFormula,
        string interesTasaActual,
        string interesBase,
        string lugarPago,
        string cedulaFiador,
        string ubicacionFirma
 )
        {
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DocsMachotes", "DocsPagare.html");
            var htmlTemplate = System.IO.File.ReadAllText(templatePath);

            htmlTemplate = htmlTemplate
                .Replace("{{ID_DOCUMENTO}}", idDocumento)
                .Replace("{{MONTO_NUMERICO}}", montoNumerico)
                .Replace("{{CEDULA_DEUDOR}}", cedulaDeudor)
                .Replace("{{SOCIEDAD_DEUDOR}}", sociedadDeudor)
                .Replace("{{CEDULA_JURIDICA_SOCIEDAD}}", cedulaJuridicaSociedad)
                .Replace("{{ACREEDOR_NOMBRE}}", acreedorNombre)
                .Replace("{{CEDULA_JURIDICA_ACREEDOR}}", cedulaJuridicaAcreedor)
                .Replace("{{ACREEDOR_DOMICILIO}}", acreedorDomicilio)

                /*.Replace("{{FECHA_FIRMA_NUEVO}}", fechaFirmaNuevo)

                .Replace("{{HORA_FIRMA_NUEVO}}",
                DateTime.TryParse(horaFirmaNuevo, out var hora) ? hora.ToString("hh:mm tt") : "[Hora inválida]")*/

                .Replace("{{FECHA_VENCIMIENTO}}", fechaVencimiento)
                .Replace("{{INTERES_FORMULA}}", interesFormula)
                .Replace("{{INTERES_TASA_ACTUAL}}", interesTasaActual)
                .Replace("{{INTERES_BASE}}", interesBase)
                .Replace("{{LUGAR_PAGO}}", lugarPago)
                .Replace("{{CEDULA_FIADOR}}", cedulaFiador)
                .Replace("{{UBICACION_FIRMA}}", ubicacionFirma);

                var ahora = DateTime.Now;

                htmlTemplate = htmlTemplate
                    .Replace("{{HORA_FIRMA_NUEVO}}", ahora.ToString("hh:mm tt"))
                    .Replace("{{FECHA_FIRMA_NUEVO}}", ahora.ToString("yyyy-MM-dd"));

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


        //Prueba para editar en historialDocumentos
        [HttpGet]
        public async Task<IActionResult> EditarDesdeHistorial(int id)
        {
            var historial = await _context.HistorialDocumentos.FindAsync(id);
            if (historial == null || historial.TipoDocumento != "Pagaré" || historial.DocumentoIdOriginal == null)
            {
                return NotFound();
            }

            var docOriginal = await _context.TDocsPagares
                .FirstOrDefaultAsync(d => d.IdDocumento == historial.DocumentoIdOriginal.Value);

            if (docOriginal == null)
            {
                return NotFound();
            }

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

            ViewBag.DocumentoAnteriorId = historial.Id; // ✅ Deja esto

            ViewData["CedulaDeudor"] = new SelectList(
            _context.TGePersonas.Select(p => new
            {
                Cedula = p.Cedula,
                apellido = p.Apellido1,
                Texto = p.Nombre + " " + p.Apellido1 + " - " + p.Cedula
            }),
            "Cedula",
            "Texto", model.CedulaDeudor);
            ViewData["CedulaFiador"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula", model.CedulaFiador);
            ViewData["LugarPago"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", model.LugarPago);
            ViewData["UbicacionFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", model.UbicacionFirma);

            return View("CreateDocsPagares", model);
        }


    }
}
