using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.CrDireccion1.BuscarXid;
using Preacepta.LN.CrDireccion1.Listar;
using Preacepta.LN.DocsInscripcionVehiculo.BuscarXid;
using Preacepta.LN.DocsInscripcionVehiculo.Crear;
using Preacepta.LN.DocsInscripcionVehiculo.Editar;
using Preacepta.LN.DocsInscripcionVehiculo.Eliminar;
using Preacepta.LN.DocsInscripcionVehiculo.Listar;
using Preacepta.LN.DocsMarcaVehiculo.BuscarXid;
using Preacepta.LN.DocsMarcaVehiculo.Listar;
using Preacepta.LN.DocsTipoVehiculo.Buscar;
using Preacepta.LN.DocsTipoVehiculo.Listar;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.LN.GePersona.Listar;
using Preacepta.LN.HistorialDocumentos.BuscarXid;
using Preacepta.LN.HistorialDocumentos.Crear;
using Preacepta.LN.HistorialDocumentos.Eliminar;
using Preacepta.LN.HistorialDocumentos.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using Preacepta.LN.BitacoraEventos.Crear;
using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.UI.Controllers
{
    public class DocsInscripcionVehiculoController : Controller
    {
        private readonly IConverter _converter;
        private readonly IBuscarDocsInscripcionVehiculoLN _buscar;
        private readonly ICrearDocsInscripcionVehiculoLN _crear;
        private readonly IEditarDocsInscripcionVehiculoLN _editar;
        private readonly IEliminarDocsInscripcionVehiculoLN _eliminar;
        private readonly IListarDocsInscripcionVehiculoLN _listar;
        private readonly IBuscarXidGePersonaLN _buscarPersona;
        private readonly IListarGePersonaLN _listarPersonas;
        private readonly ICrearHistorialLN _crearHistorialLN;
        private readonly IListarHistorialLN _listarHistorialLN;
        private readonly IBuscarHistorialLN _buscarHistorialLN;
        private readonly IELiminarHistorialLN _eLiminarHistorialLN;
        private readonly IBuscarCrDireccion1LN _buscarDistrito;
        private readonly IListarCrDireccion1LN _listarDistrito;
        private readonly IBuscarTipoVehiculoLN _buscarTipoVehiculo;
        private readonly IListarTipoVehiculoLN _listarTipoVehiculo;
        private readonly IBuscarDocsMarcaVehiculoLN _buscarMarcaVehiculo;
        private readonly IListarDocsMarcaVehiculoLN _listarMarcaVehiculo;
        private readonly ICrearEventosLN _bitacoraLN;

        public DocsInscripcionVehiculoController(IConverter converter,
            IBuscarDocsInscripcionVehiculoLN buscar,
            ICrearDocsInscripcionVehiculoLN crear,
            IEditarDocsInscripcionVehiculoLN editar,
            IEliminarDocsInscripcionVehiculoLN eliminar,
            IListarDocsInscripcionVehiculoLN listar,
            IBuscarXidGePersonaLN buscarPersona,
            IListarGePersonaLN listarPersonas,
            ICrearHistorialLN crearHistorialLN,
            IListarHistorialLN listarHistorialLN,
            IBuscarHistorialLN buscarHistorialLN,
            IELiminarHistorialLN eLiminarHistorialLN,
            IBuscarCrDireccion1LN buscarDistrito,
            IListarCrDireccion1LN listarDistrito,
            IBuscarTipoVehiculoLN buscarTipoVehiculo,
            IListarTipoVehiculoLN listarTipoVehiculo,
            IBuscarDocsMarcaVehiculoLN buscarMarcaVehiculo,
            IListarDocsMarcaVehiculoLN listarMarcaVehiculo,
            ICrearEventosLN bitacora)
        {
            _converter = converter;
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;
            _buscarPersona = buscarPersona;
            _listarPersonas = listarPersonas;
            _crearHistorialLN = crearHistorialLN;
            _listarHistorialLN = listarHistorialLN;
            _buscarHistorialLN = buscarHistorialLN;
            _eLiminarHistorialLN = eLiminarHistorialLN;
            _buscarDistrito = buscarDistrito;
            _listarDistrito = listarDistrito;
            _buscarTipoVehiculo = buscarTipoVehiculo;
            _listarTipoVehiculo = listarTipoVehiculo;
            _buscarMarcaVehiculo = buscarMarcaVehiculo;
            _listarMarcaVehiculo = listarMarcaVehiculo;
            _bitacoraLN = bitacora;
        }

        // GET: TDocsInscripcionVehiculo
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Index()
        {
            return View(await _listar.listar());
        }

        // GET: TDocsInscripcionVehiculo/Details/5
        [Authorize(Roles = "Gestor")]
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
        [Authorize(Roles = "Gestor")]
        public IActionResult Create()
        {
            ViewData["CedulaAbogado"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula");
            ViewData["CedulaCliente"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Apellido1");
            ViewData["EstiloVehiculo"] = new SelectList(_listarTipoVehiculo.Listar().Result, "Id", "Nombre");
            ViewData["LugarFirma"] = new SelectList(_listarDistrito.listarDistritos().Result, "IdDistrito", "NombreDistrito");
            ViewData["MarcaVehiculo"] = new SelectList(_listarMarcaVehiculo.listar().Result, "Id", "Nombre");
            return View();
        }

        // POST: TDocsInscripcionVehiculo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Create([Bind("IdDocumento,CedulaCliente,CedulaAbogado,MarcaVehiculo,EstiloVehiculo,ModeloVehiculo,Categoria,MarcaMotor,NumeroMotor,NumeroSerieChasis,Vin,Anio,Carroceria,PesoNeto,PesoBruto,Potencia,Color,Capacidad,Combustible,Cilindraje,LugarFirma,FechaFirma")] DocsInscripcionVehiculoDTO tDocsInscripcionVehiculo)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tDocsInscripcionVehiculo);
                var Registros = await _listar.listar();
                var idDocumento = Registros.LastOrDefault();

               

                HistorialDocumentoDTO historialDocumentoDTO = new HistorialDocumentoDTO
                {
                    Cliente = tDocsInscripcionVehiculo.CedulaCliente,
                    Abogado = tDocsInscripcionVehiculo.CedulaAbogado,
                    Fecha = DateTime.Now.ToString(),
                    TipoDocumento = "Inscripción de vehiculo",
                    IdDocumento = idDocumento.IdDocumento,
                    Titulo = $"Doc.no.{idDocumento.IdDocumento} Inscripción de vehiculo"

                };
                await _crearHistorialLN.Crear(historialDocumentoDTO);

                var usuario = User.Identity?.Name ?? "Desconocido";
                var tituloCorto = $"Inscripción {tDocsInscripcionVehiculo.MarcaVehiculo} {tDocsInscripcionVehiculo.ModeloVehiculo}";
                var accion = $"Se creó documento '{tituloCorto}' con ID {idDocumento.IdDocumento}";
                await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_DocsInscripcionVehiculo", accion, idDocumento.IdDocumento);

                return RedirectToAction(nameof(Index));
            }
            ViewData["CedulaAbogado"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula", tDocsInscripcionVehiculo.CedulaAbogado);
            ViewData["CedulaCliente"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Apellido1", tDocsInscripcionVehiculo.CedulaCliente);
            ViewData["EstiloVehiculo"] = new SelectList(_listarTipoVehiculo.Listar().Result, "Id", "Nombre", tDocsInscripcionVehiculo.EstiloVehiculo);
            ViewData["LugarFirma"] = new SelectList(_listarDistrito.listarDistritos().Result, "IdDistrito", "NombreDistrito", tDocsInscripcionVehiculo.LugarFirma);
            ViewData["MarcaVehiculo"] = new SelectList(_listarMarcaVehiculo.listar().Result, "Id", "Nombre", tDocsInscripcionVehiculo.MarcaVehiculo);
            return View(tDocsInscripcionVehiculo);
        }

        // GET: TDocsInscripcionVehiculo/Edit/5
        [Authorize(Roles = "Gestor")]
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
            ViewData["CedulaAbogado"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula", tDocsInscripcionVehiculo.CedulaAbogado);
            ViewData["CedulaCliente"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Apellido1", tDocsInscripcionVehiculo.CedulaCliente);
            ViewData["EstiloVehiculo"] = new SelectList(_listarTipoVehiculo.Listar().Result, "Id", "Nombre", tDocsInscripcionVehiculo.EstiloVehiculo);
            ViewData["LugarFirma"] = new SelectList(_listarDistrito.listarDistritos().Result, "IdDistrito", "NombreDistrito", tDocsInscripcionVehiculo.LugarFirma);
            ViewData["MarcaVehiculo"] = new SelectList(_listarMarcaVehiculo.listar().Result, "Id", "Nombre", tDocsInscripcionVehiculo.MarcaVehiculo);
            return View(tDocsInscripcionVehiculo);
        }

        // POST: TDocsInscripcionVehiculo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
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
                    var usuario = User.Identity?.Name ?? "Desconocido";
                    var tituloCorto = $"Inscripción {tDocsInscripcionVehiculo.MarcaVehiculo} {tDocsInscripcionVehiculo.ModeloVehiculo}";
                    var accion = $"Se editó documento '{tituloCorto}' con ID {id}";
                    await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_DocsInscripcionVehiculo", accion, id);

                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CedulaAbogado"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula", tDocsInscripcionVehiculo.CedulaAbogado);
            ViewData["CedulaCliente"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Apellido1", tDocsInscripcionVehiculo.CedulaCliente);
            ViewData["EstiloVehiculo"] = new SelectList(_listarTipoVehiculo.Listar().Result, "Id", "Nombre", tDocsInscripcionVehiculo.EstiloVehiculo);
            ViewData["LugarFirma"] = new SelectList(_listarDistrito.listarDistritos().Result, "IdDistrito", "NombreDistrito", tDocsInscripcionVehiculo.LugarFirma);
            ViewData["MarcaVehiculo"] = new SelectList(_listarMarcaVehiculo.listar().Result, "Id", "Nombre", tDocsInscripcionVehiculo.MarcaVehiculo);
            return View(tDocsInscripcionVehiculo);
        }

        // GET: TDocsInscripcionVehiculo/Delete/5
        [Authorize(Roles = "Gestor")]
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
        [Authorize(Roles = "Gestor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tDocsInscripcionVehiculo = await _buscar.buscar(id);
            if (tDocsInscripcionVehiculo != null)
            {
                await _eliminar.Eliminar(id);
                int buscarHistorial = await _buscarHistorialLN.BuscarXidDocumento(id, "Inscripción de vehiculo");

                await _eLiminarHistorialLN.Eliminar(buscarHistorial); 
            }
            return RedirectToAction(nameof(Index));
        }


        // DE AQUI EN ADELANTE VAN MIS METODOS
        // GET: TDocsInscripcionVehiculo/Create
        [HttpGet]
        [Authorize(Roles = "Abogado")]
        public async Task<IActionResult> CreateDocsInscripcionVehiculo(string id)
        {
            var cliente = await _buscarPersona.buscarXnumCedula(id);
            var abogado = await _buscarPersona.buscarXcorreo(User.Identity.Name);

            ViewBag.ClienteNumCedula = cliente.NumCedula;
            ViewBag.ClienteCedula = cliente.Cedula;
            ViewBag.ClienteNombre = cliente.Nombre;
            ViewBag.ClienteApellido1 = cliente.Apellido1;
            ViewBag.ClienteApellido2 = cliente.Apellido2;
            ViewBag.Dash = " - ";

            ViewBag.AbogadoCedula = abogado.NumCedula;
            ViewBag.AbogadoNombre = abogado.Nombre;
            ViewBag.AbogadoApellido1 = abogado.Apellido1;
            ViewBag.AbogadoApellido2 = abogado.Apellido2;

            ViewBag.LugarFirma = new SelectList(
                (await _listarDistrito.listarDistritos()),
                "IdDistrito",
                "NombreDistrito"
            );

            ViewBag.EstiloVehiculo = new SelectList(
                (await _listarTipoVehiculo.Listar()),
                "Id",
                "Nombre"
            );

            ViewBag.MarcaVehiculo = new SelectList(
                (await _listarMarcaVehiculo.listar()),
                "Id",
                "Nombre"
            );

            return View();
        }

        // POST: TDocsInscripcionVehiculo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Abogado")]
        public async Task<IActionResult> CreateDocsInscripcionVehiculo([Bind("IdDocumento,CedulaCliente,CedulaAbogado,MarcaVehiculo,EstiloVehiculo,ModeloVehiculo,Categoria,MarcaMotor,NumeroMotor,NumeroSerieChasis,Vin,Anio,Carroceria,PesoNeto,PesoBruto,Potencia,Color,Capacidad,Combustible,Cilindraje,LugarFirma,FechaFirma")] DocsInscripcionVehiculoDTO tDocsInscripcionVehiculo)
        {
            var cliente = await _buscarPersona.buscar(tDocsInscripcionVehiculo.CedulaCliente);
            var abogado = await _buscarPersona.buscarXcorreo(User.Identity.Name);
            tDocsInscripcionVehiculo.CedulaAbogado = abogado.Cedula;
            if (ModelState.IsValid)
            {
                await _crear.Crear(tDocsInscripcionVehiculo);
                var Registros = await _listar.listar();
                var idDocumento = Registros.LastOrDefault();

                var usuario = User.Identity?.Name ?? "Desconocido";
                var tituloCorto = $"Inscripción {tDocsInscripcionVehiculo.MarcaVehiculo} {tDocsInscripcionVehiculo.ModeloVehiculo}";
                var accion = $"Se creó documento '{tituloCorto}' con ID {idDocumento.IdDocumento}";
                await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_DocsInscripcionVehiculo", accion, idDocumento.IdDocumento);


                HistorialDocumentoDTO historialDocumentoDTO = new HistorialDocumentoDTO
                {
                    Cliente = tDocsInscripcionVehiculo.CedulaCliente,
                    Abogado = tDocsInscripcionVehiculo.CedulaAbogado,
                    Fecha = DateTime.Now.ToString(),
                    TipoDocumento = "Inscripción de vehiculo",
                    IdDocumento = idDocumento.IdDocumento,
                    Titulo = $"Doc.no.{idDocumento.IdDocumento} Inscripción de vehiculo"

                };
                await _crearHistorialLN.Crear(historialDocumentoDTO);
                return RedirectToAction("DocsHistorial", "THistorialDocumento1");
            }
            ViewBag.ClienteNumCedula = cliente.Cedula;
            ViewBag.ClienteCedula = cliente.Cedula;
            ViewBag.ClienteNombre = cliente.Nombre;
            ViewBag.ClienteApellido1 = cliente.Apellido1;
            ViewBag.ClienteApellido2 = cliente.Apellido2;
            ViewBag.Dash = " - ";

            ViewBag.AbogadoCedula = abogado.Cedula;
            ViewBag.AbogadoNombre = abogado.Nombre;
            ViewBag.AbogadoApellido1 = abogado.Apellido1;
            ViewBag.AbogadoApellido2 = abogado.Apellido2;

            return View(tDocsInscripcionVehiculo);
        }

        [HttpGet]
        [Authorize(Roles = "Abogado")]
        public async Task<IActionResult> PrevisualizarPDFInscripcionVehiculo(
                string nombreCliente,
                int cedulaCliente,
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
                int cedulaAbogado
            )
        {
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DocsMachotes", "InscripcionDeVehiculoMachote.html");
            var htmlTemplate = System.IO.File.ReadAllText(templatePath);
            
            var cliente = await _buscarPersona.buscar(cedulaCliente);
            var abogado = await _buscarPersona.buscarXcorreo(User.Identity.Name);
            var dist = await _buscarDistrito.buscarDistrito(int.Parse(lugarFirma));

            htmlTemplate = htmlTemplate
                .Replace("{{NOMBRE_CLIENTE}}", cliente.Nombre + " " + cliente.Apellido1 + " " + cliente.Apellido2)
                .Replace("{{CEDULA_CLIENTE}}", cliente.NumCedula)
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
                .Replace("{{LUGAR_FIRMA}}", dist.NombreDistrito)
                .Replace("{{FECHA_FIRMA}}", fechaFirma)
                .Replace("{{NOMBRE_NOTARIO}}", abogado.Nombre + " " + abogado.Apellido1 + " " + abogado.Apellido2)
                .Replace("{{CEDULA_ABOGADO}}", abogado.NumCedula);

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
    }
}