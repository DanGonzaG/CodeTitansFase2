using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.CrDireccion1.BuscarXid;
using Preacepta.LN.CrDireccion1.Listar;
using Preacepta.LN.DocsCompraventaFinca.BuscarXid;
using Preacepta.LN.DocsCompraventaFinca.Crear;
using Preacepta.LN.DocsCompraventaFinca.Editar;
using Preacepta.LN.DocsCompraventaFinca.Eliminar;
using Preacepta.LN.DocsCompraventaFinca.Listar;
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
    public class DocsCompraventaFincasController : Controller
    {
        private readonly Contexto _context;
        private readonly IConverter _converter;
        private readonly IBuscarDocsCompraventaFincaLN _buscar;
        private readonly ICrearDocsCompraventaFincaLN _crear;
        private readonly IEditarDocsCompraventaFincaLN _editar;
        private readonly IEliminarDocsCompraventaFincaLN _eliminar;
        private readonly IListarDocsCompraventaFincaLN _listar;
        private readonly IBuscarXidGePersonaLN _buscarPersona;
        private readonly IListarGePersonaLN _listarPersonas;
        private readonly ICrearHistorialLN _crearHistorialLN;
        private readonly IListarHistorialLN _listarHistorialLN;
        private readonly IBuscarHistorialLN _buscarHistorialLN;
        private readonly IELiminarHistorialLN _eLiminarHistorialLN;
        private readonly IBuscarCrDireccion1LN _buscarDistrito;
        private readonly IListarCrDireccion1LN _listarDistrito;
        private readonly ICrearEventosLN _bitacoraLN;

        public DocsCompraventaFincasController(IConverter converter,
            Contexto context,
            IBuscarDocsCompraventaFincaLN buscar,
            ICrearDocsCompraventaFincaLN crear,
            IEditarDocsCompraventaFincaLN editar,
            IEliminarDocsCompraventaFincaLN eliminar,
            IListarDocsCompraventaFincaLN listar,
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

        // GET: DocsCompraventaFincas
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Index()
        {
            return View(await _listar.listar());
        }

        // GET: DocsCompraventaFincas/Details/5
        [Authorize(Roles = "Gestor")]
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
        [Authorize(Roles = "Gestor")]
        public IActionResult Create()
        {
            ViewData["CedulaAbogado"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula");
            ViewData["CedulaComprador"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula");
            ViewData["CedulaVendedor"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula");
            ViewData["ProvinciaFinca"] = new SelectList(_listarDistrito.listarProvincias().Result, "IdProvincia", "NombreProvincia");
            ViewData["CantonFinca"] = new SelectList(_listarDistrito.listarCantones().Result, "IdCanton", "NombreCanton");
            ViewData["DistritoFinca"] = new SelectList(_listarDistrito.listarDistritos().Result, "IdDistrito", "NombreDistrito");
            ViewData["LugarFirma"] = new SelectList(_listarDistrito.listarDistritos().Result, "IdDistrito", "NombreDistrito");
            return View();
        }

        // POST: DocsCompraventaFincas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Create([Bind("IdDocumento,NumeroEscritura,CedulaAbogado,CedulaVendedor,CedulaComprador,MontoVenta,PartidoFinca,MatriculaFinca,NaturalezaFinca,DistritoFinca,CantonFinca,ProvinciaFinca,AreaFincaM2,PlanoCatastrado,ColindaNorte,ColindaSur,ColindaEste,ColindaOeste,FormaPago,MedioPago,OrigenFondos,LugarFirma,HoraFirma,FechaFirma")] DocsCompraventaFincaDTO tDocsCompraventaFinca)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tDocsCompraventaFinca);
                var Registros = await _listar.listar();
                var idDocumento = Registros.LastOrDefault();

                var usuario = User.Identity?.Name ?? "Desconocido";
                var tituloCorto = tDocsCompraventaFinca.NumeroEscritura.Length > 50
                    ? tDocsCompraventaFinca.NumeroEscritura.Substring(0, 47) + "..."
                    : tDocsCompraventaFinca.NumeroEscritura;
                var accion = $"Se creó documento CompraVenta Fincas '{tituloCorto}' con ID {idDocumento.IdDocumento}";
                await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_DocsCompraventaFinca", accion, idDocumento.IdDocumento);


                HistorialDocumentoDTO historialDocumentoDTO = new HistorialDocumentoDTO
                {
                    Cliente = tDocsCompraventaFinca.CedulaComprador,
                    Abogado = tDocsCompraventaFinca.CedulaAbogado,
                    Fecha = DateTime.Now.ToString(),
                    TipoDocumento = "CompraVenta Fincas.",
                    IdDocumento = idDocumento.IdDocumento,
                    Titulo = $"Doc.no.{idDocumento.IdDocumento} CompraVenta Fincas."

                };
                await _crearHistorialLN.Crear(historialDocumentoDTO);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CedulaAbogado"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula", tDocsCompraventaFinca.CedulaAbogado);
            ViewData["CedulaComprador"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula", tDocsCompraventaFinca.CedulaComprador);
            ViewData["CedulaVendedor"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula", tDocsCompraventaFinca.CedulaVendedor);
            ViewData["ProvinciaFinca"] = new SelectList(_listarDistrito.listarProvincias().Result, "IdProvincia", "NombreProvincia");
            ViewData["CantonFinca"] = new SelectList(_listarDistrito.listarCantones().Result, "IdCanton", "NombreCanton");
            ViewData["DistritoFinca"] = new SelectList(_listarDistrito.listarDistritos().Result, "IdDistrito", "NombreDistrito", tDocsCompraventaFinca.DistritoFinca);
            ViewData["LugarFirma"] = new SelectList(_listarDistrito.listarDistritos().Result, "IdDistrito", "NombreDistrito", tDocsCompraventaFinca.LugarFirma);
            return View(tDocsCompraventaFinca);
        }

        // GET: DocsCompraventaFincas/Edit/5
        [Authorize(Roles = "Gestor")]
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
            ViewData["CedulaAbogado"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula", tDocsCompraventaFinca.CedulaAbogado);
            ViewData["CedulaComprador"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Apellido1", tDocsCompraventaFinca.CedulaComprador);
            ViewData["CedulaVendedor"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Apellido1", tDocsCompraventaFinca.CedulaVendedor);
            ViewData["ProvinciaFinca"] = new SelectList(_listarDistrito.listarProvincias().Result, "IdProvincia", "NombreProvincia", tDocsCompraventaFinca.ProvinciaFinca);
            ViewData["CantonFinca"] = new SelectList(_listarDistrito.listarCantones().Result, "IdCanton", "NombreCanton", tDocsCompraventaFinca.CantonFinca);
            ViewData["DistritoFinca"] = new SelectList(_listarDistrito.listarDistritos().Result, "IdDistrito", "NombreDistrito", tDocsCompraventaFinca.DistritoFinca);
            ViewData["LugarFirma"] = new SelectList(_listarDistrito.listarDistritos().Result, "IdDistrito", "NombreDistrito", tDocsCompraventaFinca.LugarFirma);
            return View(tDocsCompraventaFinca);
        }

        // POST: DocsCompraventaFincas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
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
                    // Registrar en bitácora
                    var usuario = User.Identity?.Name ?? "Desconocido";
                    var tituloCorto = tDocsCompraventaFinca.NumeroEscritura.Length > 50
                        ? tDocsCompraventaFinca.NumeroEscritura.Substring(0, 47) + "..."
                        : tDocsCompraventaFinca.NumeroEscritura;
                    var accion = $"Se editó documento CompraVenta Fincas '{tituloCorto}' con ID {tDocsCompraventaFinca.IdDocumento}";
                    await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_DocsCompraventaFinca", accion, tDocsCompraventaFinca.IdDocumento);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CedulaAbogado"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Cedula", tDocsCompraventaFinca.CedulaAbogado);
            ViewData["CedulaComprador"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Apellido1", tDocsCompraventaFinca.CedulaComprador);
            ViewData["CedulaVendedor"] = new SelectList(_listarPersonas.listar().Result, "Cedula", "Apellido1", tDocsCompraventaFinca.CedulaVendedor);
            ViewData["ProvinciaFinca"] = new SelectList(_listarDistrito.listarProvincias().Result, "IdProvincia", "NombreProvincia", tDocsCompraventaFinca.ProvinciaFinca);
            ViewData["CantonFinca"] = new SelectList(_listarDistrito.listarCantones().Result, "IdCanton", "NombreCanton", tDocsCompraventaFinca.CantonFinca);
            ViewData["DistritoFinca"] = new SelectList(_listarDistrito.listarDistritos().Result, "IdDistrito", "NombreDistrito", tDocsCompraventaFinca.DistritoFinca);
            ViewData["LugarFirma"] = new SelectList(_listarDistrito.listarDistritos().Result, "IdDistrito", "NombreDistrito", tDocsCompraventaFinca.LugarFirma);
            return View(tDocsCompraventaFinca);
        }

        // GET: DocsCompraventaFincas/Delete/5
        [Authorize(Roles = "Gestor")]
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

        // POST: TDocsAutorizacionRevisionDaniel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tDocsCompraventaFinca = await _buscar.buscar(id);
            if (tDocsCompraventaFinca != null)
            {
                await _eliminar.Eliminar(id);
                int buscarHistorial = await _buscarHistorialLN.BuscarXidDocumento(id, "CompraVenta Fincas.");

                await _eLiminarHistorialLN.Eliminar(buscarHistorial); 
            }
            return RedirectToAction(nameof(Index));
        }

        // DE AQUI EN ADELNATE VAN MIS METODOS

        // GET: DocsCompraventaFincas/Create
        [HttpGet]
        [Authorize(Roles = "Abogado")]
        public async Task<IActionResult> CreateDocsCompraventaFincas(int cedulaComprador, int cedulaVendedor)
        {

            var comprador = await _buscarPersona.buscar(cedulaComprador);
            var vendedor = await _buscarPersona.buscar(cedulaVendedor);
            var abogado = await _buscarPersona.buscarXcorreo(User.Identity.Name);

            ViewBag.CompradorCedula = comprador.Cedula;
            ViewBag.CompradorNombre = comprador.Nombre;
            ViewBag.CompradorApellido1 = comprador.Apellido1;
            ViewBag.CompradorApellido2 = comprador.Apellido2;
            ViewBag.Dash = " - ";

            ViewBag.VendedorCedula = vendedor.Cedula;
            ViewBag.VendedorNombre = vendedor.Nombre;
            ViewBag.VendedorApellido1 = vendedor.Apellido1;
            ViewBag.VendedorApellido2 = vendedor.Apellido2;

            ViewBag.AbogadoCedula = abogado.Cedula;
            ViewBag.AbogadoNombre = abogado.Nombre;
            ViewBag.AbogadoApellido1 = abogado.Apellido1;
            ViewBag.AbogadoApellido2 = abogado.Apellido2;

            ViewBag.LugarFirma = new SelectList(
                (await _listarDistrito.listarDistritos()),
                "IdDistrito",
                "NombreDistrito"
            );
            return View();
        }

        // POST: DocsCompraventaFincas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Abogado")]
        public async Task<IActionResult> CreateDocsCompraventaFincas([Bind("IdDocumento,NumeroEscritura,CedulaAbogado,CedulaVendedor,CedulaComprador,MontoVenta,PartidoFinca,MatriculaFinca,NaturalezaFinca,DistritoFinca,CantonFinca,ProvinciaFinca,AreaFincaM2,PlanoCatastrado,ColindaNorte,ColindaSur,ColindaEste,ColindaOeste,FormaPago,MedioPago,OrigenFondos,LugarFirma,HoraFirma,FechaFirma")] DocsCompraventaFincaDTO tDocsCompraventaFinca)
        {

            var comprador = await _buscarPersona.buscar(tDocsCompraventaFinca.CedulaComprador);
            var vendedor = await _buscarPersona.buscar(tDocsCompraventaFinca.CedulaVendedor);
            var abogado = await _buscarPersona.buscarXcorreo(User.Identity.Name);
            if (ModelState.IsValid)
            {
                await _crear.Crear(tDocsCompraventaFinca);
                var Registros = await _listar.listar();
                var idDocumento = Registros.LastOrDefault();

                var usuario = User.Identity?.Name ?? "Desconocido";
                var tituloCorto = tDocsCompraventaFinca.NumeroEscritura.Length > 50
                    ? tDocsCompraventaFinca.NumeroEscritura.Substring(0, 47) + "..."
                    : tDocsCompraventaFinca.NumeroEscritura;
                var accion = $"Se creó documento CompraVenta Fincas '{tituloCorto}' con ID {idDocumento.IdDocumento}";
                await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_DocsCompraventaFinca", accion, idDocumento.IdDocumento);


                HistorialDocumentoDTO historialDocumentoDTO = new HistorialDocumentoDTO
                {
                    Cliente = tDocsCompraventaFinca.CedulaComprador,
                    Abogado = tDocsCompraventaFinca.CedulaAbogado,
                    Fecha = DateTime.Now.ToString(),
                    TipoDocumento = "CompraVenta Fincas.",
                    IdDocumento = idDocumento.IdDocumento,
                    Titulo = $"Doc.no.{idDocumento.IdDocumento} CompraVenta Fincas."

                };
                await _crearHistorialLN.Crear(historialDocumentoDTO);
                return RedirectToAction("DocsHistorial", "THistorialDocumento1");
            }
            ViewBag.CompradorCedula = comprador.Cedula;
            ViewBag.CompradorNombre = comprador.Nombre;
            ViewBag.CompradorApellido1 = comprador.Apellido1;
            ViewBag.CompradorApellido2 = comprador.Apellido2;
            ViewBag.Dash = " - ";

            ViewBag.VendedorCedula = vendedor.Cedula;
            ViewBag.VendedorNombre = vendedor.Nombre;
            ViewBag.VendedorApellido1 = vendedor.Apellido1;
            ViewBag.VendedorApellido2 = vendedor.Apellido2;

            ViewBag.AbogadoCedula = abogado.Cedula;
            ViewBag.AbogadoNombre = abogado.Nombre;
            ViewBag.AbogadoApellido1 = abogado.Apellido1;
            ViewBag.AbogadoApellido2 = abogado.Apellido2;
            return View(tDocsCompraventaFinca);
        }


        [HttpGet]
        [Authorize(Roles = "Abogado")]
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

            var vendedor = await _buscarPersona.buscar(int.Parse(cedulaVendedor));
            var comprador = await _buscarPersona.buscar(int.Parse(cedulaComprador));
            var abogado = await _buscarPersona.buscarXcorreo(User.Identity.Name);
            var prov = await _buscarDistrito.buscarProvincia(int.Parse(provinciaFinca));
            var cant = await _buscarDistrito.buscarCanton(int.Parse(cantonFinca));
            var distF = await _buscarDistrito.buscarDistrito(int.Parse(distritoFinca));
            var dist = await _buscarDistrito.buscarDistrito(int.Parse(lugarFirma));

            htmlTemplate = htmlTemplate
    .Replace("{{NUMERO}}", numeroEscritura)
    .Replace("{{NOMBRE_NOTARIO}}", abogado.Nombre + " " + abogado.Apellido1 + " " + abogado.Apellido2)
    .Replace("{{DIRECCION_NOTARIO}}", abogado.Direccion2)
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
    .Replace("{{DISTRITO_FINCA}}", distF.NombreDistrito)
    .Replace("{{CANTON_FINCA}}", cant.NombreCanton)
    .Replace("{{PROVINCIA_FINCA}}", prov.NombreProvincia)
    .Replace("{{AREA_FINCA}}", areaFincaM2)
    .Replace("{{PLANO_FINCA}}", planoCatastrado)
    .Replace("{{NORTE_FINCA}}", colindaNorte)
    .Replace("{{SUR_FINCA}}", colindaSur)
    .Replace("{{ESTE_FINCA}}", colindaEste)
    .Replace("{{OESTE_FINCA}}", colindaOeste)
    .Replace("{{FORMA_PAGO}}", formaPago)
    .Replace("{{MEDIO_PAGO}}", medioPago)
    .Replace("{{ORIGEN_FONDOS}}", origenFondos)
    .Replace("{{LUGAR}}", dist.NombreDistrito)
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

    }

}
