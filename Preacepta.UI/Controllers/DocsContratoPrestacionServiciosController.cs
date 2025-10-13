using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.CrDireccion1.BuscarXid;
using Preacepta.LN.CrDireccion1.Listar;
using Preacepta.LN.DocsContratoPrestacionServicios.BuscarXid;
using Preacepta.LN.DocsContratoPrestacionServicios.Crear;
using Preacepta.LN.DocsContratoPrestacionServicios.Editar;
using Preacepta.LN.DocsContratoPrestacionServicios.Eliminar;
using Preacepta.LN.DocsContratoPrestacionServicios.Listar;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.LN.GePersona.Listar;
using Preacepta.LN.HistorialDocumentos.BuscarXid;
using Preacepta.LN.HistorialDocumentos.Crear;
using Preacepta.LN.HistorialDocumentos.Eliminar;
using Preacepta.LN.HistorialDocumentos.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using Preacepta.LN.BitacoraEventos.Crear;

namespace Preacepta.UI.Controllers
{
    public class DocsContratoPrestacionServiciosController : Controller
    {
        private readonly Contexto _context;
        private readonly IConverter _converter;
        private readonly IBuscarDocsContratoPrestacionServiciosLN _buscar;
        private readonly ICrearDocsContratoPrestacionServiciosLN _crear;
        private readonly IEditarDocsContratoPrestacionServiciosLN _editar;
        private readonly IEliminarDocsContratoPrestacionServiciosLN _eliminar;
        private readonly IListarDocsContratoPrestacionServiciosLN _listar;
        private readonly IBuscarXidGePersonaLN _buscarPersona;
        private readonly IListarGePersonaLN _listarPersonas;
        private readonly ICrearHistorialLN _crearHistorialLN;
        private readonly IListarHistorialLN _listarHistorialLN;
        private readonly IBuscarHistorialLN _buscarHistorialLN;
        private readonly IELiminarHistorialLN _eLiminarHistorialLN;
        private readonly IBuscarCrDireccion1LN _buscarDistrito;
        private readonly IListarCrDireccion1LN _listarDistrito;
        private readonly ICrearEventosLN _bitacoraLN;


        public DocsContratoPrestacionServiciosController(IConverter converter,
            Contexto context,
            IBuscarDocsContratoPrestacionServiciosLN buscar,
            ICrearDocsContratoPrestacionServiciosLN crear,
            IEditarDocsContratoPrestacionServiciosLN editar,
            IEliminarDocsContratoPrestacionServiciosLN eliminar,
            IListarDocsContratoPrestacionServiciosLN listar,
            IBuscarXidGePersonaLN buscarPersona,
            IListarGePersonaLN listarPersonas,
            ICrearHistorialLN crearHistorialLN,
            IListarHistorialLN listarHistorialLN,
            IBuscarHistorialLN buscarHistorialLN,
            IELiminarHistorialLN eLiminarHistorialLN,
            IBuscarCrDireccion1LN buscarDistrito,
            IListarCrDireccion1LN listarDistrito,
            ICrearEventosLN bitacora)
        {
            _converter = converter;
            _context = context;
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
            _bitacoraLN = bitacora;
        }

        // GET: ContratoPrestacionServicios
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Index()
        {
            return View(await _listar.listar());
        }

        // GET: ContratoPrestacionServicios/Details/5
        [Authorize(Roles = "Gestor")]
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
        [Authorize(Roles = "Gestor")]
        public IActionResult Create()
        {
            ViewData["CedulaAbogado"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula");
            ViewData["CedulaCliente"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula");
            ViewData["CiudadFirma"] = new SelectList(_listarDistrito.listarDistritos().Result, "IdDistrito", "NombreDistrito");
            ViewData["Provincia"] = new SelectList(_listarDistrito.listarProvincias().Result, "IdProvincia", "NombreProvincia");

            return View();
        }

        // POST: ContratoPrestacionServicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Create([Bind("IdDocumento,RazonSocialEmpresa,Provincia,CedulaJuridicaEmpresa,CedulaAbogado,CedulaCliente,TipoServicios,FechaInicio,FechaFinal,MontoHonorarios,InformacionConfidencial,CiudadFirma,HoraFirma,FechaFirma")] DocsContratoPrestacionServicioDTO tDocsContratoPrestacionServicio)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tDocsContratoPrestacionServicio);
                var Registros = await _listar.listar();
                var idDocumento = Registros.LastOrDefault();

                var usuario = User.Identity?.Name ?? "Desconocido";
                var tituloCorto = tDocsContratoPrestacionServicio.RazonSocialEmpresa.Length > 50
                    ? tDocsContratoPrestacionServicio.RazonSocialEmpresa.Substring(0, 47) + "..."
                    : tDocsContratoPrestacionServicio.RazonSocialEmpresa;
                var accion = $"Se creó Contrato Prestación de Servicios '{tituloCorto}' con ID {idDocumento.IdDocumento}";
                await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_DocsContratoPrestacionServicios", accion, idDocumento.IdDocumento);


                HistorialDocumentoDTO historialDocumentoDTO = new HistorialDocumentoDTO
                {
                    Cliente = tDocsContratoPrestacionServicio.CedulaCliente,
                    Abogado = tDocsContratoPrestacionServicio.CedulaAbogado,
                    Fecha = DateTime.Now.ToString(),
                    TipoDocumento = "Contrato Prestación Servicios",
                    IdDocumento = idDocumento.IdDocumento,
                    Titulo = $"Doc.no.{idDocumento.IdDocumento} Contrato Prestación de Servicios"

                };
                await _crearHistorialLN.Crear(historialDocumentoDTO);
                return RedirectToAction(nameof(Index));
            }

            ViewData["CedulaAbogado"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula", tDocsContratoPrestacionServicio.CedulaAbogado);
            ViewData["CedulaCliente"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula", tDocsContratoPrestacionServicio.CedulaCliente);
            ViewData["CiudadFirma"] = new SelectList(_listarDistrito.listarDistritos().Result, "IdDistrito", "NombreDistrito", tDocsContratoPrestacionServicio.CiudadFirma);
            ViewData["Provincia"] = new SelectList(_listarDistrito.listarProvincias().Result, "IdProvincia", "NombreProvincia", tDocsContratoPrestacionServicio.Provincia);

            return View(tDocsContratoPrestacionServicio);
        }

        // GET: ContratoPrestacionServicios/Edit/5
        [Authorize(Roles = "Gestor")]
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
            ViewData["CedulaAbogado"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula", tDocsContratoPrestacionServicio.CedulaAbogado);
            ViewData["CedulaCliente"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula", tDocsContratoPrestacionServicio.CedulaCliente);
            ViewData["CiudadFirma"] = new SelectList(_listarDistrito.listarDistritos().Result, "IdDistrito", "NombreDistrito", tDocsContratoPrestacionServicio.CiudadFirma);
            ViewData["Provincia"] = new SelectList(_listarDistrito.listarProvincias().Result, "IdProvincia", "NombreProvincia", tDocsContratoPrestacionServicio.Provincia);
            return View(tDocsContratoPrestacionServicio);
        }

        // POST: ContratoPrestacionServicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
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

                    var usuario = User.Identity?.Name ?? "Desconocido";
                    var tituloCorto = tDocsContratoPrestacionServicio.RazonSocialEmpresa.Length > 50
                        ? tDocsContratoPrestacionServicio.RazonSocialEmpresa.Substring(0, 47) + "..."
                        : tDocsContratoPrestacionServicio.RazonSocialEmpresa;
                    var accion = $"Se editó Contrato Prestación de Servicios '{tituloCorto}' con ID {tDocsContratoPrestacionServicio.IdDocumento}";
                    await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_DocsContratoPrestacionServicios", accion, tDocsContratoPrestacionServicio.IdDocumento);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CedulaAbogado"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula", tDocsContratoPrestacionServicio.CedulaAbogado);
            ViewData["CedulaCliente"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula", tDocsContratoPrestacionServicio.CedulaCliente);
            ViewData["CiudadFirma"] = new SelectList(_listarDistrito.listarDistritos().Result, "IdDistrito", "NombreDistrito", tDocsContratoPrestacionServicio.CiudadFirma);
            ViewData["Provincia"] = new SelectList(_listarDistrito.listarProvincias().Result, "IdProvincia", "NombreProvincia", tDocsContratoPrestacionServicio.Provincia);
            return View(tDocsContratoPrestacionServicio);
        }

        // GET: ContratoPrestacionServicios/Delete/5
        [Authorize(Roles = "Gestor")]
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

        // POST: TDocsAutorizacionRevisionDaniel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tDocsContratoPrestacionServicio = await _buscar.buscar(id);
            if (tDocsContratoPrestacionServicio != null)
            {
                await _eliminar.Eliminar(id);
                int buscarHistorial = await _buscarHistorialLN.BuscarXidDocumento(id, "Contrato Prestación Servicios");

                await _eLiminarHistorialLN.Eliminar(buscarHistorial); 
            }
            return RedirectToAction(nameof(Index));
        }

        //DE AQUI EN ADELNATE ESTAN MIS METODOS
        // GET: ContratoPrestacionServicios/Create
        [HttpGet]
        [Authorize(Roles = "Abogado")]
        public async Task<IActionResult> CreateDocsContratoPrestacionServicios(int id)
        {
            var cliente = await _buscarPersona.buscar(id);
            var abogado = await _buscarPersona.buscarXcorreo(User.Identity.Name);

            ViewBag.ClienteCedula = cliente.Cedula;
            ViewBag.ClienteNombre = cliente.Nombre;
            ViewBag.ClienteApellido1 = cliente.Apellido1;
            ViewBag.ClienteApellido2 = cliente.Apellido2;
            ViewBag.Dash = " - ";

            ViewBag.AbogadoCedula = abogado.Cedula;
            ViewBag.AbogadoNombre = abogado.Nombre;
            ViewBag.AbogadoApellido1 = abogado.Apellido1;
            ViewBag.AbogadoApellido2 = abogado.Apellido2;

            ViewBag.CiudadFirma = new SelectList(
                (await _listarDistrito.listarDistritos()),
                "IdDistrito",
                "NombreDistrito"
            );

            ViewBag.Provincia = new SelectList(
                (await _listarDistrito.listarProvincias()),
                "IdProvincia",
                "NombreProvincia"
            );

            return View();
        }

        // POST: ContratoPrestacionServicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Abogado")]
        public async Task<IActionResult> CreateDocsContratoPrestacionServicios([Bind("IdDocumento,RazonSocialEmpresa,Provincia,CedulaJuridicaEmpresa,CedulaAbogado,CedulaCliente,TipoServicios,FechaInicio,FechaFinal,MontoHonorarios,InformacionConfidencial,CiudadFirma,HoraFirma,FechaFirma")] DocsContratoPrestacionServicioDTO tDocsContratoPrestacionServicio)
        {
            var cliente = await _buscarPersona.buscar(tDocsContratoPrestacionServicio.CedulaCliente);
            var abogado = await _buscarPersona.buscarXcorreo(User.Identity.Name);

            if (ModelState.IsValid)
            {
                await _crear.Crear(tDocsContratoPrestacionServicio);
                var Registros = await _listar.listar();
                var idDocumento = Registros.LastOrDefault();

                var usuario = User.Identity?.Name ?? "Desconocido";
                var tituloCorto = tDocsContratoPrestacionServicio.RazonSocialEmpresa.Length > 50
                    ? tDocsContratoPrestacionServicio.RazonSocialEmpresa.Substring(0, 47) + "..."
                    : tDocsContratoPrestacionServicio.RazonSocialEmpresa;
                var accion = $"Se creó Contrato Prestación de Servicios '{tituloCorto}' con ID {idDocumento.IdDocumento}";
                await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_DocsContratoPrestacionServicios", accion, idDocumento.IdDocumento);


                HistorialDocumentoDTO historialDocumentoDTO = new HistorialDocumentoDTO
                {
                    Cliente = tDocsContratoPrestacionServicio.CedulaCliente,
                    Abogado = tDocsContratoPrestacionServicio.CedulaAbogado,
                    Fecha = DateTime.Now.ToString(),
                    TipoDocumento = "Contrato Prestación Servicios",
                    IdDocumento = idDocumento.IdDocumento,
                    Titulo = $"Doc.no.{idDocumento.IdDocumento} Contrato Prestación de Servicios"

                };
                await _crearHistorialLN.Crear(historialDocumentoDTO);
                return RedirectToAction("DocsHistorial", "THistorialDocumento1");
            }
            ViewBag.ClienteCedula = cliente.Cedula;
            ViewBag.ClienteNombre = cliente.Nombre;
            ViewBag.ClienteApellido1 = cliente.Apellido1;
            ViewBag.ClienteApellido2 = cliente.Apellido2;
            ViewBag.Dash = " - ";

            ViewBag.AbogadoCedula = abogado.Cedula;
            ViewBag.AbogadoNombre = abogado.Nombre;
            ViewBag.AbogadoApellido1 = abogado.Apellido1;
            ViewBag.AbogadoApellido2 = abogado.Apellido2;
            
            return View(tDocsContratoPrestacionServicio);
        }

        [HttpGet]
        [Authorize(Roles = "Abogado")]
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

            var cliente = await _buscarPersona.buscar(int.Parse(cedulaCliente));
            var abogado = await _buscarPersona.buscarXcorreo(User.Identity.Name);
            var prov = await _buscarDistrito.buscarProvincia(int.Parse(provincia));
            var dist = await _buscarDistrito.buscarDistrito(int.Parse(ciudadFirma));
            var fecha = DateTime.Parse(fechaFirma);

            htmlTemplate = htmlTemplate
                .Replace("{{RAZON_SOCIAL_EMPRESA}}", razonSocialEmpresa)
                .Replace("{{PROVINCIA}}", prov.NombreProvincia)
                .Replace("{{CEDULA_JURIDICA_EMPRESA}}", cedulaJuridicaEmpresa)
                .Replace("{{NOMBRE_ABOGADO}}", abogado.Nombre + " " + abogado.Apellido1 + " " + abogado.Apellido2)
                .Replace("{{ESTADO_CIVIL_ABOGADO}}", abogado.EstadoCivil)
                .Replace("{{OCUPACION_ABOGADO}}", abogado.Oficio)
                .Replace("{{DOMICILIO_ABOGADO}}", abogado.Direccion2)
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
                .Replace("{{CIUDAD_FIRMA}}", dist.NombreDistrito)
                .Replace("{{HORA_FIRMA}}", horaFirma)
                .Replace("{{DIA_FIRMA}}", fecha.Day.ToString())
                .Replace("{{MES_FIRMA}}", fecha.Month.ToString())
                .Replace("{{ANIO_FIRMA}}", fecha.Year.ToString());

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