using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.AD.GeAbogado.BuscarXid;
using Preacepta.LN.DocsCompraventaFinca.BuscarXid;
using Preacepta.LN.DocsCompraventaFinca.Crear;
using Preacepta.LN.DocsCompraventaFinca.Editar;
using Preacepta.LN.DocsCompraventaFinca.Eliminar;
using Preacepta.LN.DocsCompraventaFinca.Listar;
using Preacepta.LN.GeAbogado.BuscarXid;
using Preacepta.LN.GePersona.BuscarXid;
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
        private readonly IConverter _converter;
        private readonly IBuscarDocsCompraventaFincaLN _buscar;
        private readonly IBuscarAbogadoLN _buscarAbogado;
        private readonly IBuscarXidGePersonaLN _buscarPersona;
        private readonly ICrearDocsCompraventaFincaLN _crear;
        private readonly IEditarDocsCompraventaFincaLN _editar;
        private readonly IEliminarDocsCompraventaFincaLN _eliminar;
        private readonly IListarDocsCompraventaFincaLN _listar;

        public DocsCompraventaFincasController(IConverter converter,
            IBuscarAbogadoLN buscarAbogado,
            IBuscarXidGePersonaLN buscarPersona,
            Contexto context,
            IBuscarDocsCompraventaFincaLN buscar,
            ICrearDocsCompraventaFincaLN crear,
            IEditarDocsCompraventaFincaLN editar,
            IEliminarDocsCompraventaFincaLN eliminar,
            IListarDocsCompraventaFincaLN listar)
        {
            _converter = converter;
            _buscarAbogado = buscarAbogado;
            _buscarPersona = buscarPersona;
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
            ViewData["CedulaComprador"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula");
            ViewData["CedulaVendedor"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula");
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
            ViewData["CedulaComprador"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula", tDocsCompraventaFinca.CedulaComprador);
            ViewData["CedulaVendedor"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula", tDocsCompraventaFinca.CedulaVendedor);
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
            ViewData["CedulaAbogado"] = new SelectList(
                _context.TGeAbogados.Include(a => a.CedulaNavigation).Select(a => new
                {
                    Cedula = a.Cedula,
                    Texto = a.CedulaNavigation.Nombre + " " + a.CedulaNavigation.Apellido1 + " - " + a.Cedula
                }),
                "Cedula",
                "Texto");

            ViewData["CedulaComprador"] = new SelectList(
                _context.TGePersonas.Select(p => new
                {
                    Cedula = p.Cedula,
                    Texto = p.Nombre + " " + p.Apellido1 + " - " + p.Cedula
                }),
                "Cedula",
                "Texto");

            ViewData["CedulaVendedor"] = new SelectList(
                _context.TGePersonas.Select(p => new {
                    Cedula = p.Cedula,
                    Texto = p.Nombre + " " + p.Apellido1 + " - " + p.Cedula
                }),
                "Cedula",
                "Texto");
            //ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula");
            //ViewData["CedulaComprador"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula");
            //ViewData["CedulaVendedor"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula"); //llamar a listar provincias, cantones provincias
            ViewData["ProvinciaFinca"] = new SelectList(_context.TCrProvincias, "IdProvincia", "NombreProvincia");
            ViewData["DistritoFinca"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito");
            ViewData["CantonesFinca"] = new SelectList(_context.TCrCantones, "IdCanton", "NombreCanton");
            ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito");
            return View();
        }

        // POST: DocsCompraventaFincas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDocsCompraventaFincas([Bind("IdDocumento,NumeroEscritura,CedulaAbogado,CedulaVendedor,CedulaComprador,MontoVenta,PartidoFinca,MatriculaFinca,NaturalezaFinca,DistritoFinca,CantonFinca,ProvinciaFinca,AreaFincaM2,PlanoCatastrado,ColindaNorte,ColindaSur,ColindaEste,ColindaOeste,FormaPago,MedioPago,OrigenFondos,LugarFirma,HoraFirma,FechaFirma")] DocsCompraventaFincaDTO tDocsCompraventaFinca,
            [FromForm] int? DocumentoAnteriorId
            )
        {
            if (ModelState.IsValid)
            {

                // Eliminar documento anterior y su historial
                if (DocumentoAnteriorId.HasValue)
                {
                    // Buscar historial por ID
                    var historialAnterior = await _context.HistorialDocumentos
                        .FirstOrDefaultAsync(h => h.Id == DocumentoAnteriorId.Value);

                    if (historialAnterior != null)
                    {
                        // Obtener el ID del documento original desde el historial
                        var idDocOriginal = historialAnterior.DocumentoIdOriginal;

                        // Buscar y eliminar el documento original
                        var docAnterior = await _context.TDocsCompraventaFincas
                            .FirstOrDefaultAsync(p => p.IdDocumento == idDocOriginal);

                        if (docAnterior != null)
                            _context.TDocsCompraventaFincas.Remove(docAnterior);

                        // Eliminar también el historial
                        _context.HistorialDocumentos.Remove(historialAnterior);

                        await _context.SaveChangesAsync();
                    }
                }

                await _crear.Crear(tDocsCompraventaFinca);

                // Obtener comprador desde TGePersonas
                var comprador = await _context.TGePersonas
                     .FirstOrDefaultAsync(p => p.Cedula == tDocsCompraventaFinca.CedulaComprador);

                string nombreComprador = comprador != null
                    ? $"{comprador.Nombre} {comprador.Apellido1} {comprador.Apellido2}"
                    : tDocsCompraventaFinca.CedulaComprador.ToString();

                // Obtener abogado desde TGeAbogados (con su persona)
                var abogado = await _context.TGeAbogados
                    .Include(a => a.CedulaNavigation)
                    .FirstOrDefaultAsync(a => a.Cedula == tDocsCompraventaFinca.CedulaAbogado);

                string nombreAbogado = abogado != null
                    ? $"{abogado.CedulaNavigation.Nombre} {abogado.CedulaNavigation.Apellido1} {abogado.CedulaNavigation.Apellido2}"
                    : tDocsCompraventaFinca.CedulaAbogado.ToString();

                // Crear nuevo historial con el ID real del documento recién creado
                var nuevoIdDocumento = _context.TDocsCompraventaFincas
                .OrderByDescending(x => x.IdDocumento)
                .Select(x => x.IdDocumento)
                .FirstOrDefault();


                // Guardar en el historial
                var historial = new HistorialDocumento
                {
                    Fecha = DateTime.Now,
                    TipoDocumento = "Compra y Venta de Fincas",
                    Cliente = nombreComprador,
                    Abogado = nombreAbogado,
                    DocumentoIdOriginal = nuevoIdDocumento
                };

                _context.HistorialDocumentos.Add(historial);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                var historialDocs = await _context.HistorialDocumentos
                .OrderByDescending(h => h.Fecha)
                .ToListAsync();

                return View("~/Views/HistorialDocumentos/DocsHistorial.cshtml", historialDocs);
            }
            ViewData["CedulaAbogado"] = new SelectList(
                _context.TGeAbogados.Include(a => a.CedulaNavigation).Select(a => new
                {
                    Cedula = a.Cedula,
                    Texto = a.CedulaNavigation.Nombre + " " + a.CedulaNavigation.Apellido1 + " - " + a.Cedula
                }),
                "Cedula",
                "Texto", tDocsCompraventaFinca?.CedulaAbogado);

            ViewData["CedulaComprador"] = new SelectList(
                _context.TGePersonas.Select(p => new
                {
                    Cedula = p.Cedula,
                    Texto = p.Nombre + " " + p.Apellido1 + " - " + p.Cedula
                }),
                "Cedula",
                "Texto",
                tDocsCompraventaFinca?.CedulaComprador);

            ViewData["CedulaVendedor"] = new SelectList(
                _context.TGePersonas.Select(p => new {
                    Cedula = p.Cedula,
                    Texto = p.Nombre + " " + p.Apellido1 + " - " + p.Cedula
                }),
                "Cedula",
                "Texto",
                tDocsCompraventaFinca?.CedulaVendedor);
            //ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tDocsCompraventaFinca.CedulaAbogado);
            //ViewData["CedulaComprador"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsCompraventaFinca.CedulaComprador);
            //ViewData["CedulaVendedor"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsCompraventaFinca.CedulaVendedor);
            ViewData["ProvinciaFinca"] = new SelectList(_context.TCrProvincias, "IdProvincia", "NombreProvincia", tDocsCompraventaFinca.ProvinciaFinca);
            ViewData["DistritoFinca"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsCompraventaFinca.DistritoFinca);
            ViewData["CantonesFinca"] = new SelectList(_context.TCrCantones, "IdCanton", "NombreCanton", tDocsCompraventaFinca.CantonFinca);
            ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsCompraventaFinca.LugarFirma);
            return View(tDocsCompraventaFinca);
        }


        [HttpGet]
        public async Task<IActionResult> PrevisualizarPDFCompraventaFinca(
    string numeroEscritura,
    string cedulaAbogado,
    string cedulaVendedor,
    string cedulaComprador,
    string montoVenta,
    string partidoFinca,
    string matriculaFinca,
    string naturalezaFinca,
    string distritoFinca,
    string cantonFinca,
    string provinciaFinca,
    string areaFincaM2,
    string planoCatastrado,
    string colindaNorte,
    string colindaSur,
    string colindaEste,
    string colindaOeste,
    string formaPago,
    string medioPago,
    string origenFondos,
    string lugarFirma,
    string horaFirma,   
    string fechaFirma)
        {
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DocsMachotes", "CompraVentaFincasMachote.html");
            var htmlTemplate = System.IO.File.ReadAllText(templatePath);
            var abogado = await _buscarAbogado.buscar(int.Parse(cedulaAbogado));
            var vendedor = await _buscarPersona.buscar(int.Parse(cedulaVendedor));
            var comprador = await _buscarPersona.buscar(int.Parse(cedulaComprador));

            htmlTemplate = htmlTemplate
    .Replace("{{NUMERO}}", numeroEscritura)
    .Replace("{{NOMBRE_NOTARIO}}", abogado.CedulaNavigation.Nombre + " " + abogado.CedulaNavigation.Apellido1 + " " + abogado.CedulaNavigation.Apellido2)
    .Replace("{{DIRECCION_NOTARIO}}", abogado.CedulaNavigation.Direccion2)
    .Replace("{{NOMBRE_VENDEDOR}}", vendedor.Nombre + " " + vendedor.Apellido1 + " " + vendedor.Apellido2)
    .Replace("{{CEDULA_VENDEDOR}}", cedulaVendedor)
    .Replace("{{ESTADO_CIVIL_VENDEDOR}}", vendedor.EstadoCivil)
    .Replace("{{PROFESION_VENDEDOR}}", vendedor.Oficio)
    .Replace("{{DIRECCION_VENDEDOR}}", vendedor.Direccion1Navigation.IdCatonNavigation.NombreCanton + ", " + vendedor.Direccion1Navigation.NombreDistrito)
    .Replace("{{NOMBRE_COMPRADOR}}", comprador.Nombre + " " + comprador.Apellido1 + " " + comprador.Apellido2)
    .Replace("{{CEDULA_COMPRADOR}}", cedulaComprador)
    .Replace("{{ESTADO_CIVIL_COMPRADOR}}", comprador.EstadoCivil)
    .Replace("{{PROFESION_COMPRADOR}}", comprador.Oficio)
    .Replace("{{DIRECCION_COMPRADOR}}", comprador.Direccion1Navigation.IdCatonNavigation.NombreCanton + ", " + comprador.Direccion1Navigation.NombreDistrito)
    .Replace("{{MONTO_VENTA}}", montoVenta)
    .Replace("{{PARTIDO_FINCA}}", partidoFinca)
    .Replace("{{MATRICULA_FINCA}}", matriculaFinca)
    .Replace("{{NATURALEZA_FINCA}}", naturalezaFinca)
    .Replace("{{DISTRITO_FINCA}}", distritoFinca)
    .Replace("{{CANTON_FINCA}}", cantonFinca)
    .Replace("{{PROVINCIA_FINCA}}", provinciaFinca)
    .Replace("{{AREA_FINCA}}", areaFincaM2)
    .Replace("{{PLANO_FINCA}}", planoCatastrado)
    .Replace("{{NORTE_FINCA}}", colindaNorte)
    .Replace("{{SUR_FINCA}}", colindaSur)
    .Replace("{{ESTE_FINCA}}", colindaEste)
    .Replace("{{OESTE_FINCA}}", colindaOeste)
    .Replace("{{FORMA_PAGO}}", formaPago)
    .Replace("{{MEDIO_PAGO}}", medioPago)
    .Replace("{{ORIGEN_FONDOS}}", origenFondos)
    .Replace("{{LUGAR}}", lugarFirma)
    .Replace("{{HORAS}}", horaFirma)
    .Replace("{{FECHA}}", fechaFirma);


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

            if (historial == null || historial.TipoDocumento != "Compra y Venta de Fincas" || historial.DocumentoIdOriginal == null)
            {
                return NotFound();
            }

           
            var docOriginal = await _context.TDocsCompraventaFincas
                .FirstOrDefaultAsync(d => d.IdDocumento == historial.DocumentoIdOriginal.Value);

            if (docOriginal == null)
            {
                return NotFound();
            }

            /*ViewData["ProvinciaFinca"] = new SelectList(_context.TCrProvincias, "IdProvincia", "NombreProvincia", docOriginal.ProvinciaFinca);
            ViewData["DistritoFinca"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", docOriginal.DistritoFinca);
            ViewData["CantonesFinca"] = new SelectList(_context.TCrCantones, "IdCanton", "NombreCanton", docOriginal.CantonFinca);*/


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

            ViewBag.DocumentoAnteriorId = historial.Id; // ✅ Deja esto

            ViewData["CedulaAbogado"] = new SelectList(
                _context.TGeAbogados.Include(a => a.CedulaNavigation).Select(a => new
                {
                    Cedula = a.Cedula,
                    Texto = a.CedulaNavigation.Nombre + " " + a.CedulaNavigation.Apellido1 + " - " + a.Cedula
                }),
                "Cedula",
                "Texto", model.CedulaAbogado);

            ViewData["CedulaComprador"] = new SelectList(
                _context.TGePersonas.Select(p => new
                {
                    Cedula = p.Cedula,
                    Texto = p.Nombre + " " + p.Apellido1 + " - " + p.Cedula
                }),
                "Cedula",
                "Texto",
                model.CedulaComprador);

            ViewData["CedulaVendedor"] = new SelectList(
                _context.TGePersonas.Select(p => new {
                    Cedula = p.Cedula,
                    Texto = p.Nombre + " " + p.Apellido1 + " - " + p.Cedula
                }),
                "Cedula",
                "Texto",
                model.CedulaVendedor);
            //ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", model.CedulaAbogado);
            //ViewData["CedulaComprador"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", model.CedulaComprador);
            //ViewData["CedulaVendedor"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", model.CedulaVendedor);
            ViewData["ProvinciaFinca"] = new SelectList(_context.TCrProvincias, "IdProvincia", "NombreProvincia", model.ProvinciaFinca);
            ViewData["DistritoFinca"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", model.DistritoFinca);
            ViewData["CantonesFinca"] = new SelectList(_context.TCrCantones, "IdCanton", "NombreCanton", model.CantonFinca);
            ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", model.LugarFirma);

            return View("CreateDocsCompraventaFincas", model);

        }

        //agregar metodo post

    }

}
