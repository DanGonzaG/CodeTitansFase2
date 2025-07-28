using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.DocsInscripcionVehiculo.BuscarXid;
using Preacepta.LN.DocsInscripcionVehiculo.Crear;
using Preacepta.LN.DocsInscripcionVehiculo.Editar;
using Preacepta.LN.DocsInscripcionVehiculo.Eliminar;
using Preacepta.LN.DocsInscripcionVehiculo.Listar;
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
    public class DocsInscripcionVehiculoController : Controller
    {
        private readonly IConverter _converter;
        private readonly Contexto _context;
        private readonly IBuscarDocsInscripcionVehiculoLN _buscar;
        private readonly IBuscarAbogadoLN _buscarAbogado;
        private readonly IBuscarXidGePersonaLN _buscarPersona;
        private readonly ICrearDocsInscripcionVehiculoLN _crear;
        private readonly IEditarDocsInscripcionVehiculoLN _editar;
        private readonly IEliminarDocsInscripcionVehiculoLN _eliminar;
        private readonly IListarDocsInscripcionVehiculoLN _listar;

        public DocsInscripcionVehiculoController(IConverter converter,
            IBuscarAbogadoLN buscarAbogado,
            IBuscarXidGePersonaLN buscarPersona,
            Contexto context,
            IBuscarDocsInscripcionVehiculoLN buscar,
            ICrearDocsInscripcionVehiculoLN crear,
            IEditarDocsInscripcionVehiculoLN editar,
            IEliminarDocsInscripcionVehiculoLN eliminar,
            IListarDocsInscripcionVehiculoLN listar)
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


        // DE AQUI EN ADELANTE VAN MIS METODOS
        // GET: TDocsInscripcionVehiculo/Create
        public IActionResult CreateDocsInscripcionVehiculo()
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
            //ViewData["CedulaCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1");
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
        public async Task<IActionResult> CreateDocsInscripcionVehiculo([Bind("IdDocumento,CedulaCliente,CedulaAbogado,MarcaVehiculo,EstiloVehiculo,ModeloVehiculo,Categoria,MarcaMotor,NumeroMotor,NumeroSerieChasis,Vin,Anio,Carroceria,PesoNeto,PesoBruto,Potencia,Color,Capacidad,Combustible,Cilindraje,LugarFirma,FechaFirma")] DocsInscripcionVehiculoDTO tDocsInscripcionVehiculo,
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
                        var docAnterior = await _context.TDocsInscripcionVehiculos
                            .FirstOrDefaultAsync(p => p.IdDocumento == idDocOriginal);

                        if (docAnterior != null)
                            _context.TDocsInscripcionVehiculos.Remove(docAnterior);

                        // Eliminar también el historial
                        _context.HistorialDocumentos.Remove(historialAnterior);

                        await _context.SaveChangesAsync();
                    }
                }

                await _crear.Crear(tDocsInscripcionVehiculo);
                //return RedirectToAction(nameof(Index));

                // Obtener comprador desde TGePersonas
                var cliente = await _context.TGePersonas
                     .FirstOrDefaultAsync(p => p.Cedula == tDocsInscripcionVehiculo.CedulaCliente);

                string nombreCliente = cliente != null
                    ? $"{cliente.Nombre} {cliente.Apellido1} {cliente.Apellido2}"
                    : tDocsInscripcionVehiculo.CedulaCliente.ToString();

                // Obtener abogado desde TGeAbogados (con su persona)
                var abogado = await _context.TGeAbogados
                    .Include(a => a.CedulaNavigation)
                    .FirstOrDefaultAsync(a => a.Cedula == tDocsInscripcionVehiculo.CedulaAbogado);

                string nombreAbogado = abogado != null
                    ? $"{abogado.CedulaNavigation.Nombre} {abogado.CedulaNavigation.Apellido1} {abogado.CedulaNavigation.Apellido2}"
                    : tDocsInscripcionVehiculo.CedulaAbogado.ToString();

                // Crear nuevo historial con el ID real del documento recién creado
                var nuevoIdDocumento = _context.TDocsInscripcionVehiculos
                .OrderByDescending(x => x.IdDocumento)
                .Select(x => x.IdDocumento)
                .FirstOrDefault();


                // Guardar en el historial
                var historial = new HistorialDocumento
                {
                    Fecha = DateTime.Now,
                    TipoDocumento = "Inscripción de Vehículos",
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
                "Texto", tDocsInscripcionVehiculo?.CedulaAbogado);

            ViewData["CedulaCliente"] = new SelectList(
                _context.TGePersonas.Select(p => new
                {
                    Cedula = p.Cedula,
                    Texto = p.Nombre + " " + p.Apellido1 + " - " + p.Cedula
                }),
                "Cedula",
                "Texto",
                tDocsInscripcionVehiculo?.CedulaCliente);
            //ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tDocsInscripcionVehiculo.CedulaAbogado);
            //ViewData["CedulaCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsInscripcionVehiculo.CedulaCliente);
            ViewData["EstiloVehiculo"] = new SelectList(_context.TDocsTipoVehiculos, "Id", "Nombre", tDocsInscripcionVehiculo.EstiloVehiculo);
            ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsInscripcionVehiculo.LugarFirma);
            ViewData["MarcaVehiculo"] = new SelectList(_context.TDocsMarcaVehiculos, "Id", "Nombre", tDocsInscripcionVehiculo.MarcaVehiculo);
            return View(tDocsInscripcionVehiculo);
        }

        [HttpGet]
        public async Task<IActionResult> PrevisualizarPDFInscripcionVehiculo(
                string nombreCliente,
                string cedulaCliente,
                string estadoCivilCliente,
                string profesionCliente,
                string direccionCliente,
                string marca,
                string estilo,
                string modelo,
                string categoria,
                string marcaMotor,
                string numeroMotor,
                string serieChasis,
                string vin,
                string anio,
                string carroceria,
                string pesoNeto,
                string pesoBruto,
                string potencia,
                string color,
                string capacidad,
                string combustible,
                string cilindraje,
                string lugarFirma,
                string fechaFirma,
                string nombreNotario,
                string cedulaAbogado
            )
        {
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DocsMachotes", "InscripcionDeVehiculoMachote.html");
            var htmlTemplate = System.IO.File.ReadAllText(templatePath);
            var abogado = await _buscarAbogado.buscar(int.Parse(cedulaAbogado));
            var cliente = await _buscarPersona.buscar(int.Parse(cedulaCliente));

            // Reemplazar marcadores con los datos del formulario
            htmlTemplate = htmlTemplate
                .Replace("{{NOMBRE_CLIENTE}}", cliente.Nombre + " " + cliente.Apellido1 + " " + cliente.Apellido2)
                .Replace("{{CEDULA_CLIENTE}}", cedulaCliente)
                .Replace("{{ESTADO_CIVIL_CLIENTE}}", cliente.EstadoCivil)
                .Replace("{{PROFESION_CLIENTE}}", cliente.Oficio)
                .Replace("{{DIRECCION_CLIENTE}}", cliente.Direccion2)
                .Replace("{{MARCA}}", marca)
                .Replace("{{ESTILO}}", estilo)
                .Replace("{{MODELO}}", modelo)
                .Replace("{{CATEGORIA}}", categoria)
                .Replace("{{MARCA_MOTOR}}", marcaMotor)
                .Replace("{{NUMERO_MOTOR}}", numeroMotor)
                .Replace("{{SERIE_CHASIS}}", serieChasis)
                .Replace("{{VIN}}", vin)
                .Replace("{{ANIO}}", anio)
                .Replace("{{CARROCERIA}}", carroceria)
                .Replace("{{PESO_NETO}}", pesoNeto)
                .Replace("{{PESO_BRUTO}}", pesoBruto)
                .Replace("{{POTENCIA}}", potencia)
                .Replace("{{COLOR}}", color)
                .Replace("{{CAPACIDAD}}", capacidad)
                .Replace("{{COMBUSTIBLE}}", combustible)
                .Replace("{{CILINDRAJE}}", cilindraje)
                .Replace("{{LUGAR_FIRMA}}", lugarFirma)
                .Replace("{{FECHA_FIRMA}}", fechaFirma)
                .Replace("{{NOMBRE_NOTARIO}}", abogado.CedulaNavigation.Nombre + " " + abogado.CedulaNavigation.Apellido1 + " " + abogado.CedulaNavigation.Apellido2)
                .Replace("{{CEDULA_ABOGADO}}", cedulaAbogado);

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

            if (historial == null || historial.TipoDocumento != "Inscripción de Vehículos" || historial.DocumentoIdOriginal == null)
            {
                return NotFound();
            }


            var docOriginal = await _context.TDocsInscripcionVehiculos
                .FirstOrDefaultAsync(d => d.IdDocumento == historial.DocumentoIdOriginal.Value);

            if (docOriginal == null)
            {
                return NotFound();
            }

            /*ViewData["ProvinciaFinca"] = new SelectList(_context.TCrProvincias, "IdProvincia", "NombreProvincia", docOriginal.ProvinciaFinca);
            ViewData["DistritoFinca"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", docOriginal.DistritoFinca);
            ViewData["CantonesFinca"] = new SelectList(_context.TCrCantones, "IdCanton", "NombreCanton", docOriginal.CantonFinca);*/


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

            return View("CreateDocsInscripcionVehiculo", model);

        }

    }
}