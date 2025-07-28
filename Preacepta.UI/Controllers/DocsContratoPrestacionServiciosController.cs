using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.DocsContratoPrestacionServicios.BuscarXid;
using Preacepta.LN.DocsContratoPrestacionServicios.Crear;
using Preacepta.LN.DocsContratoPrestacionServicios.Editar;
using Preacepta.LN.DocsContratoPrestacionServicios.Eliminar;
using Preacepta.LN.DocsContratoPrestacionServicios.Listar;
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
    public class DocsContratoPrestacionServiciosController : Controller
    {
        private readonly Contexto _context;
        private readonly IConverter _converter;
        private readonly IBuscarDocsContratoPrestacionServiciosLN _buscar;
        private readonly IBuscarAbogadoLN _buscarAbogado;
        private readonly IBuscarXidGePersonaLN _buscarPersona;
        private readonly ICrearDocsContratoPrestacionServiciosLN _crear;
        private readonly IEditarDocsContratoPrestacionServiciosLN _editar;
        private readonly IEliminarDocsContratoPrestacionServiciosLN _eliminar;
        private readonly IListarDocsContratoPrestacionServiciosLN _listar;


        public DocsContratoPrestacionServiciosController(IConverter converter,
            IBuscarAbogadoLN buscarAbogado,
            IBuscarXidGePersonaLN buscarPersona,
            Contexto context,
            IBuscarDocsContratoPrestacionServiciosLN buscar,
            ICrearDocsContratoPrestacionServiciosLN crear,
            IEditarDocsContratoPrestacionServiciosLN editar,
            IEliminarDocsContratoPrestacionServiciosLN eliminar,
            IListarDocsContratoPrestacionServiciosLN listar)
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

        // GET: ContratoPrestacionServicios
        public async Task<IActionResult> Index()
        {
            //var contexto = _context.TDocsContratoPrestacionServicios.Include(t => t.CedulaAbogadoNavigation).Include(t => t.CedulaClienteNavigation).Include(t => t.CiudadFirmaNavigation).Include(t => t.ProvinciaNavigation);
            return View(await _listar.listar());
        }

        // GET: ContratoPrestacionServicios/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsContratoPrestacionServicio = await _buscar.buscar(id);
            if (tDocsContratoPrestacionServicio == null)
            {
                return NotFound();
            }

            return View(tDocsContratoPrestacionServicio);
        }

        // GET: ContratoPrestacionServicios/Create
        public IActionResult Create()
        {
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula");
            ViewData["CedulaCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula");
            ViewData["CiudadFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito");
            ViewData["Provincia"] = new SelectList(_context.TCrProvincias, "IdProvincia", "NombreProvincia");
            return View();
        }

        // POST: ContratoPrestacionServicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDocumento,RazonSocialEmpresa,Provincia,CedulaJuridicaEmpresa,CedulaAbogado,CedulaCliente,TipoServicios,FechaInicio,FechaFinal,MontoHonorarios,InformacionConfidencial,CiudadFirma,HoraFirma,FechaFirma")] DocsContratoPrestacionServicioDTO tDocsContratoPrestacionServicio)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tDocsContratoPrestacionServicio);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tDocsContratoPrestacionServicio.CedulaAbogado);
            ViewData["CedulaCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula", tDocsContratoPrestacionServicio.CedulaCliente);
            ViewData["CiudadFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsContratoPrestacionServicio.CiudadFirma);
            ViewData["Provincia"] = new SelectList(_context.TCrProvincias, "IdProvincia", "NombreProvincia", tDocsContratoPrestacionServicio.Provincia);
            return View(tDocsContratoPrestacionServicio);
        }

        // GET: ContratoPrestacionServicios/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsContratoPrestacionServicio = await _buscar.buscar(id);
            if (tDocsContratoPrestacionServicio == null)
            {
                return NotFound();
            }
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tDocsContratoPrestacionServicio.CedulaAbogado);
            ViewData["CedulaCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsContratoPrestacionServicio.CedulaCliente);
            ViewData["CiudadFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsContratoPrestacionServicio.CiudadFirma);
            ViewData["Provincia"] = new SelectList(_context.TCrProvincias, "IdProvincia", "IdProvincia", tDocsContratoPrestacionServicio.Provincia);
            return View(tDocsContratoPrestacionServicio);
        }

        // POST: ContratoPrestacionServicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDocumento,RazonSocialEmpresa,Provincia,CedulaJuridicaEmpresa,CedulaAbogado,CedulaCliente,TipoServicios,FechaInicio,FechaFinal,MontoHonorarios,InformacionConfidencial,CiudadFirma,HoraFirma,FechaFirma")] DocsContratoPrestacionServicioDTO tDocsContratoPrestacionServicio)
        {
            if (id != tDocsContratoPrestacionServicio.IdDocumento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.Editar(tDocsContratoPrestacionServicio);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tDocsContratoPrestacionServicio.CedulaAbogado);
            ViewData["CedulaCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsContratoPrestacionServicio.CedulaCliente);
            ViewData["CiudadFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsContratoPrestacionServicio.CiudadFirma);
            ViewData["Provincia"] = new SelectList(_context.TCrProvincias, "IdProvincia", "IdProvincia", tDocsContratoPrestacionServicio.Provincia);
            return View(tDocsContratoPrestacionServicio);
        }

        // GET: ContratoPrestacionServicios/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsContratoPrestacionServicio = await _buscar.buscar(id);
            if (tDocsContratoPrestacionServicio == null)
            {
                return NotFound();
            }

            return View(tDocsContratoPrestacionServicio);
        }

        // POST: ContratoPrestacionServicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eliminar.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        //DE AQUI EN ADELNATE ESTAN MIS METODOS
        // GET: ContratoPrestacionServicios/Create
        public IActionResult CreateDocsContratoPrestacionServicios()
        {
            ViewData["CedulaAbogado"] = new SelectList(
                _context.TGeAbogados.Include(a => a.CedulaNavigation).Select(a => new
                {
                    Cedula = a.Cedula,
                    Texto = a.CedulaNavigation.Nombre + " " + a.CedulaNavigation.Apellido1 + " - " + a.Cedula
                }),
                "Cedula",
                "Texto");

            ViewData["CedulaCliente"] = new SelectList(
                _context.TGePersonas.Select(p => new
                {
                    Cedula = p.Cedula,
                    Texto = p.Nombre + " " + p.Apellido1 + " - " + p.Cedula
                }),
                "Cedula",
                "Texto");
            //ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula");
            //ViewData["CedulaCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula");
            ViewData["CiudadFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito");
            ViewData["Provincia"] = new SelectList(_context.TCrProvincias, "IdProvincia", "NombreProvincia");
            return View();
        }

        // POST: ContratoPrestacionServicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDocsContratoPrestacionServicios([Bind("IdDocumento,RazonSocialEmpresa,Provincia,CedulaJuridicaEmpresa,CedulaAbogado,CedulaCliente,TipoServicios,FechaInicio,FechaFinal,MontoHonorarios,InformacionConfidencial,CiudadFirma,HoraFirma,FechaFirma")] DocsContratoPrestacionServicioDTO tDocsContratoPrestacionServicio,
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

                await _crear.Crear(tDocsContratoPrestacionServicio);

                // Obtener comprador desde TGePersonas
                var cliente = await _context.TGePersonas
                     .FirstOrDefaultAsync(p => p.Cedula == tDocsContratoPrestacionServicio.CedulaCliente);

                string nombreCliente = cliente != null
                    ? $"{cliente.Nombre} {cliente.Apellido1} {cliente.Apellido2}"
                    : tDocsContratoPrestacionServicio.CedulaCliente.ToString();

                // Obtener abogado desde TGeAbogados (con su persona)
                var abogado = await _context.TGeAbogados
                    .Include(a => a.CedulaNavigation)
                    .FirstOrDefaultAsync(a => a.Cedula == tDocsContratoPrestacionServicio.CedulaAbogado);

                string nombreAbogado = abogado != null
                    ? $"{abogado.CedulaNavigation.Nombre} {abogado.CedulaNavigation.Apellido1} {abogado.CedulaNavigation.Apellido2}"
                    : tDocsContratoPrestacionServicio.CedulaAbogado.ToString();

                // Crear nuevo historial con el ID real del documento recién creado
                var nuevoIdDocumento = _context.TDocsContratoPrestacionServicios
                .OrderByDescending(x => x.IdDocumento)
                .Select(x => x.IdDocumento)
                .FirstOrDefault();


                // Guardar en el historial
                var historial = new HistorialDocumento
                {
                    Fecha = DateTime.Now,
                    TipoDocumento = "Contrato Prestación de Servicios",
                    Cliente = nombreCliente,
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
                "Texto", tDocsContratoPrestacionServicio?.CedulaAbogado);

            ViewData["CedulaCliente"] = new SelectList(
                _context.TGePersonas.Select(p => new
                {
                    Cedula = p.Cedula,
                    Texto = p.Nombre + " " + p.Apellido1 + " - " + p.Cedula
                }),
                "Cedula",
                "Texto",
                tDocsContratoPrestacionServicio?.CedulaCliente);
            //ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tDocsContratoPrestacionServicio.CedulaAbogado);
            //ViewData["CedulaCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula", tDocsContratoPrestacionServicio.CedulaCliente);
            ViewData["CiudadFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsContratoPrestacionServicio.CiudadFirma);
            ViewData["Provincia"] = new SelectList(_context.TCrProvincias, "IdProvincia", "NombreProvincia", tDocsContratoPrestacionServicio.Provincia);
            return View(tDocsContratoPrestacionServicio);
        }

        [HttpGet]
        public async Task<IActionResult> PrevisualizarPDFPrestacionServicios(
                string razonSocialEmpresa,
                string provincia,
                string cedulaJuridicaEmpresa,
                string cedulaAbogado,
                string cedulaCliente,
                string tipoServicios,
                string fechaInicio,
                string fechaFinal,
                string montoHonorarios,
                string informacionConfidencial,
                string ciudadFirma,
                string horaFirma,
                string fechaFirma)
        {
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DocsMachotes", "PrestacionServiciosMachote.html");
            var htmlTemplate = System.IO.File.ReadAllText(templatePath);
            var abogado = await _buscarAbogado.buscar(int.Parse(cedulaAbogado));
            var cliente = await _buscarPersona.buscar(int.Parse(cedulaCliente));

            htmlTemplate = htmlTemplate
                .Replace("{{RAZON_SOCIAL_EMPRESA}}", razonSocialEmpresa)
                .Replace("{{PROVINCIA}}", provincia)
                .Replace("{{CEDULA_JURIDICA_EMPRESA}}", cedulaJuridicaEmpresa)
                .Replace("{{NOMBRE_ABOGADO}}", abogado.CedulaNavigation.Nombre + " " + abogado.CedulaNavigation.Apellido1 + " " + abogado.CedulaNavigation.Apellido2)
                .Replace("{{ESTADO_CIVIL_ABOGADO}}", abogado.CedulaNavigation.EstadoCivil)
                .Replace("{{OCUPACION_ABOGADO}}", abogado.CedulaNavigation.Oficio)
                .Replace("{{DOMICILIO_ABOGADO}}", abogado.CedulaNavigation.Direccion2)
                .Replace("{{CEDULA_ABOGADO}}", cedulaAbogado)
                .Replace("{{NOMBRE_CLIENTE}}", cliente.Nombre + " " + cliente.Apellido1 + " " + cliente.Apellido2)
                .Replace("{{ESTADO_CIVIL_CLIENTE}}", cliente.EstadoCivil)
                .Replace("{{OCUPACION_CLIENTE}}", cliente.Oficio)
                .Replace("{{DOMICILIO_CLIENTE}}", cliente.Direccion1Navigation.IdCatonNavigation.NombreCanton + ", " + cliente.Direccion1Navigation.NombreDistrito)
                .Replace("{{CEDULA_CLIENTE}}", cedulaCliente)
                .Replace("{{TIPO_SERVICIOS}}", tipoServicios)
                .Replace("{{FECHA_INICIO}}", fechaInicio)
                .Replace("{{FECHA_FINAL}}", fechaFinal)
                .Replace("{{MONTO_HONORARIOS}}", montoHonorarios)
                .Replace("{{INFORMACION_CONFIDENCIAL}}", informacionConfidencial)
                .Replace("{{CIUDAD_FIRMA}}", ciudadFirma)
                .Replace("{{HORA_FIRMA}}", horaFirma)
                .Replace("{{FECHA_FIRMA}}", fechaFirma);

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

            if (historial == null || historial.TipoDocumento != "Contrato Prestación de Servicios" || historial.DocumentoIdOriginal == null)
            {
                return NotFound();
            }


            var docOriginal = await _context.TDocsContratoPrestacionServicios
                .FirstOrDefaultAsync(d => d.IdDocumento == historial.DocumentoIdOriginal.Value);

            if (docOriginal == null)
            {
                return NotFound();
            }

            /*ViewData["ProvinciaFinca"] = new SelectList(_context.TCrProvincias, "IdProvincia", "NombreProvincia", docOriginal.ProvinciaFinca);
            ViewData["DistritoFinca"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", docOriginal.DistritoFinca);
            ViewData["CantonesFinca"] = new SelectList(_context.TCrCantones, "IdCanton", "NombreCanton", docOriginal.CantonFinca);*/


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
            //ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tDocsContratoPrestacionServicio.CedulaAbogado);
            //ViewData["CedulaCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Cedula", tDocsContratoPrestacionServicio.CedulaCliente);
            ViewData["CiudadFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", model.CiudadFirma);
            ViewData["Provincia"] = new SelectList(_context.TCrProvincias, "IdProvincia", "NombreProvincia", model.Provincia);

            return View("CreateDocsContratoPrestacionServicios", model);

        }

    }
}
