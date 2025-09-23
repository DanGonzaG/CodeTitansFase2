using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Preacepta.AD;
using Preacepta.LN.CrDireccion1.BuscarXid;
using Preacepta.LN.CrDireccion1.Listar;
using Preacepta.LN.DocsCombustible.Listar;
using Preacepta.LN.DocsMarcaVehiculo.Listar;
using Preacepta.LN.DocsOpcionCompraventaVehiculo.Buscar;
using Preacepta.LN.DocsOpcionCompraventaVehiculo.Crear;
using Preacepta.LN.DocsOpcionCompraventaVehiculo.Editar;
using Preacepta.LN.DocsOpcionCompraventaVehiculo.Eliminar;
using Preacepta.LN.DocsOpcionCompraventaVehiculo.Listar;
using Preacepta.LN.DocsTipoVehiculo.Listar;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.LN.HistorialDocumentos.Crear;
using Preacepta.LN.HistorialDocumentos.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Preacepta.UI.Controllers
{
    public class DocsOpcionCompraventaVehiculoesController : Controller
    {
        private readonly IConverter _converter;
        private readonly Contexto _context;

        // Documento
        private readonly IBuscarDocCVLN _buscar;
        private readonly ICrearDocCVLN _crear;
        private readonly IEditarDocCVLN _editar;
        private readonly IEliminarDocCVLN _eliminar;
        private readonly IListarDocCVLN _listar;

        // Personas
        private readonly IBuscarXidGePersonaLN _buscarPersona;

        // Historial
        private readonly ICrearHistorialLN _crearHistorial;
        private readonly IListarHistorialLN _listarHistorial;

        // Catálogos
        private readonly IListarDocsCombustibleLN _listarCombustibles;
        private readonly IListarDocsMarcaVehiculoLN _listarMarcas;
        private readonly IListarTipoVehiculoLN _listarTipos;

        //Direccion
        private readonly IListarCrDireccion1LN _listarDirecciones;
        private readonly IBuscarCrDireccion1LN _buscarDistrito;

        public DocsOpcionCompraventaVehiculoesController(
            IConverter converter,
            Contexto context,
            IBuscarDocCVLN buscar,
            ICrearDocCVLN crear,
            IEditarDocCVLN editar,
            IEliminarDocCVLN eliminar,
            IListarDocCVLN listar,
            IBuscarXidGePersonaLN buscarPersona,
            ICrearHistorialLN crearHistorial,
            IListarHistorialLN listarHistorial,
            IListarDocsCombustibleLN listarCombustibles,
            IListarDocsMarcaVehiculoLN listarMarcas,
            IListarTipoVehiculoLN listarTipos,
            IListarCrDireccion1LN listarDirecciones,
            IBuscarCrDireccion1LN buscarDistrito
        )
        {
            _converter = converter;
            _context = context;

            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;

            _buscarPersona = buscarPersona;

            _crearHistorial = crearHistorial;
            _listarHistorial = listarHistorial;

            _listarCombustibles = listarCombustibles;
            _listarMarcas = listarMarcas;
            _listarTipos = listarTipos;

            _listarDirecciones = listarDirecciones;

            _buscarDistrito = buscarDistrito;
        }

        // ================= CRUD base =================

        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Index()
            => View(await _listar.Listar());

        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _buscar.buscar(id);
            if (dto == null) return NotFound();
            return View(dto);
        }

        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Create()
        {
            var marcas = await _listarMarcas.listar();
            var tipos = await _listarTipos.Listar();
            var combustibles = await _listarCombustibles.listar();

            ViewData["MarcaVehiculo"] = new SelectList(marcas, "Id", "Nombre");
            ViewData["MarcaMotor"] = new SelectList(marcas, "Id", "Nombre");
            ViewData["TipoVehiculo"] = new SelectList(tipos, "Id", "Nombre");
            ViewData["Combustible"] = new SelectList(combustibles, "Id", "Nombre");

            ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito");

            var model = new DocsOpcionCompraventaVehiculoDTO
            {
                FechaInicio = DateTime.Today.ToString("yyyy-MM-dd"),
                FechaFirma = DateTime.Today.ToString("yyyy-MM-dd"),
                HoraFirma = DateTime.Now.ToString("HH:mm")
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Create(
            [Bind("IdDocumento,NumeroEscritura,CedulaAbogado,CedulaPropietario,CedulaComprador,PlacaVehiculo,MarcaVehiculo,TipoVehiculo,ModeloVehiculo,Carroceria,Categoria,Chasis,Serie,Vin,MarcaMotor,NumeroMotor,Color,Combustible,Anio,Capacidad,Cilindraje,Precio,MonedaPrecio,PlazoOpcionAnios,FechaInicio,MontoSenal,MonedaSenal,MontoADevolver,MontoAPerder,MonedaMontoPerdido,GastosTraspasoPagadosPor,LugarFirma,HoraFirma,FechaFirma")]
            DocsOpcionCompraventaVehiculoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                // recargar combos
                var marcas = await _listarMarcas.listar();
                var tipos = await _listarTipos.Listar();
                var combustibles = await _listarCombustibles.listar();

                ViewData["MarcaVehiculo"] = new SelectList(marcas, "Id", "Nombre", dto.MarcaVehiculo);
                ViewData["MarcaMotor"] = new SelectList(marcas, "Id", "Nombre", dto.MarcaMotor);
                ViewData["TipoVehiculo"] = new SelectList(tipos, "Id", "Nombre", dto.TipoVehiculo);
                ViewData["Combustible"] = new SelectList(combustibles, "Id", "Nombre", dto.Combustible);
                ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", dto.LugarFirma);
                return View(dto);
            }

            await _crear.crear(dto);

            // Crear historial (estilo Autorización)
            var ultimo = (await _listar.Listar())?.OrderByDescending(x => x.IdDocumento).FirstOrDefault();
            if (ultimo != null)
            {
                var historial = new HistorialDocumentoDTO
                {
                    Fecha = DateTime.Now.ToString("dd/MM/yyyy"),
                    TipoDocumento = "Compra y venta de vehículos",
                    Cliente = dto.CedulaComprador,
                    Abogado = dto.CedulaAbogado,
                    IdDocumento = ultimo.IdDocumento,
                    Titulo = $"Doc.no.{ultimo.IdDocumento} Compraventa Vehículo"
                };
                await _crearHistorial.Crear(historial);
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _buscar.buscar(id);
            if (dto == null) return NotFound();

            var marcas = await _listarMarcas.listar();
            var tipos = await _listarTipos.Listar();
            var combustibles = await _listarCombustibles.listar();

            ViewData["MarcaVehiculo"] = new SelectList(marcas, "Id", "Nombre", dto.MarcaVehiculo);
            ViewData["MarcaMotor"] = new SelectList(marcas, "Id", "Nombre", dto.MarcaMotor);
            ViewData["TipoVehiculo"] = new SelectList(tipos, "Id", "Nombre", dto.TipoVehiculo);
            ViewData["Combustible"] = new SelectList(combustibles, "Id", "Nombre", dto.Combustible);
            ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", dto.LugarFirma);

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("IdDocumento,NumeroEscritura,CedulaAbogado,CedulaPropietario,CedulaComprador,PlacaVehiculo,MarcaVehiculo,TipoVehiculo,ModeloVehiculo,Carroceria,Categoria,Chasis,Serie,Vin,MarcaMotor,NumeroMotor,Color,Combustible,Anio,Capacidad,Cilindraje,Precio,MonedaPrecio,PlazoOpcionAnios,FechaInicio,MontoSenal,MonedaSenal,MontoADevolver,MontoAPerder,MonedaMontoPerdido,GastosTraspasoPagadosPor,LugarFirma,HoraFirma,FechaFirma")]
            DocsOpcionCompraventaVehiculoDTO dto)
        {
            if (id != dto.IdDocumento) return NotFound();

            if (!ModelState.IsValid)
            {
                var marcas = await _listarMarcas.listar();
                var tipos = await _listarTipos.Listar();
                var combustibles = await _listarCombustibles.listar();

                ViewData["MarcaVehiculo"] = new SelectList(marcas, "Id", "Nombre", dto.MarcaVehiculo);
                ViewData["MarcaMotor"] = new SelectList(marcas, "Id", "Nombre", dto.MarcaMotor);
                ViewData["TipoVehiculo"] = new SelectList(tipos, "Id", "Nombre", dto.TipoVehiculo);
                ViewData["Combustible"] = new SelectList(combustibles, "Id", "Nombre", dto.Combustible);
                ViewData["LugarFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", dto.LugarFirma);
                return View(dto);
            }

            await _editar.editar(dto);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _buscar.buscar(id);
            if (dto == null) return NotFound();
            return View(dto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eliminar.eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        // ================ MIS MÉTODOS =================

        // GET personalizado
        [HttpGet]
        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<IActionResult> CreateDocsOpcionCompraventaVehiculoes(int CedulaPropietario, int CedulaComprador)
        {
            var propietario = await _buscarPersona.buscar(CedulaPropietario);
            var comprador = await _buscarPersona.buscar(CedulaComprador);
            var abogado = await _buscarPersona.buscarXcorreo(User.Identity.Name);

            ViewBag.ClienteCedula = comprador?.Cedula ?? 0;
            ViewBag.ClienteNombre = comprador?.Nombre ?? "";
            ViewBag.ClienteApellido1 = comprador?.Apellido1 ?? "";
            ViewBag.ClienteApellido2 = comprador?.Apellido2 ?? "";
            ViewBag.AbogadoCedula = abogado?.Cedula ?? 0;

            ViewBag.PropietarioCedula = propietario?.Cedula ?? 0;
            ViewBag.PropietarioNombre = propietario?.Nombre ?? "";
            ViewBag.PropietarioApellido1 = propietario?.Apellido1 ?? "";
            ViewBag.PropietarioApellido2 = propietario?.Apellido2 ?? "";

            

            var distritos = await _listarDirecciones.listarDistritos();
            ViewBag.UbicacionFirma = new SelectList(distritos, "IdDistrito", "NombreDistrito");

            var marcas = await _listarMarcas.listar();
            var tipos = await _listarTipos.Listar();
            var combs = await _listarCombustibles.listar();

            ViewData["MarcaVehiculo"] = new SelectList(marcas, "Id", "Nombre");
            ViewData["MarcaMotor"] = new SelectList(marcas, "Id", "Nombre");
            ViewData["TipoVehiculo"] = new SelectList(tipos, "Id", "Nombre");
            ViewData["Combustible"] = new SelectList(combs, "Id", "Nombre");

            var model = new DocsOpcionCompraventaVehiculoDTO
            {
                CedulaPropietario = propietario?.Cedula ?? 0,
                CedulaComprador = comprador?.Cedula ?? 0,
                CedulaAbogado = abogado?.Cedula ?? 0,
                FechaInicio = DateTime.Today.ToString("yyyy-MM-dd"),
                FechaFirma = DateTime.Today.ToString("yyyy-MM-dd"),
                HoraFirma = DateTime.Now.ToString("HH:mm")
            };

            return View("CreateDocsOpcionCompraventaVehiculoes", model);
        }


        // POST personalizado
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<IActionResult> CreateDocsOpcionCompraventaVehiculoes(
            [Bind("NumeroEscritura,CedulaAbogado,CedulaPropietario,CedulaComprador,PlacaVehiculo,MarcaVehiculo,TipoVehiculo,ModeloVehiculo,Carroceria,Categoria,Chasis,Serie,Vin,MarcaMotor,NumeroMotor,Color,Combustible,Anio,Capacidad,Cilindraje,Precio,MonedaPrecio,PlazoOpcionAnios,FechaInicio,MontoSenal,MonedaSenal,MontoADevolver,MontoAPerder,MonedaMontoPerdido,GastosTraspasoPagadosPor,LugarFirma,HoraFirma,FechaFirma")]
    DocsOpcionCompraventaVehiculoDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FechaFirma))
                dto.FechaFirma = DateTime.Today.ToString("yyyy-MM-dd");

            if (string.IsNullOrWhiteSpace(dto.HoraFirma))
                dto.HoraFirma = DateTime.Now.ToString("HH:mm");

            ModelState.Remove(nameof(dto.FechaFirma));
            ModelState.Remove(nameof(dto.HoraFirma));
            TryValidateModel(dto);

            if (!ModelState.IsValid)
            {
                var propietario = await _buscarPersona.buscar(dto.CedulaPropietario);
                var comprador = await _buscarPersona.buscar(dto.CedulaComprador);
                var abogado = await _buscarPersona.buscarXcorreo(User.Identity.Name);

                ViewBag.ClienteCedula = comprador?.Cedula ?? 0;
                ViewBag.ClienteNombre = comprador?.Nombre ?? "";
                ViewBag.ClienteApellido1 = comprador?.Apellido1 ?? "";
                ViewBag.ClienteApellido2 = comprador?.Apellido2 ?? "";
                ViewBag.AbogadoCedula = abogado?.Cedula ?? 0;

                ViewBag.PropietarioNombre = $"{propietario?.Nombre} {propietario?.Apellido1} {propietario?.Apellido2}".Trim();
                ViewBag.PropietarioCedula = propietario?.Cedula ?? 0;

                var marcas = await _listarMarcas.listar();
                var tipos = await _listarTipos.Listar();
                var combs = await _listarCombustibles.listar();

                ViewData["MarcaVehiculo"] = new SelectList(marcas, "Id", "Nombre", dto.MarcaVehiculo);
                ViewData["MarcaMotor"] = new SelectList(marcas, "Id", "Nombre", dto.MarcaMotor);
                ViewData["TipoVehiculo"] = new SelectList(tipos, "Id", "Nombre", dto.TipoVehiculo);
                ViewData["Combustible"] = new SelectList(combs, "Id", "Nombre", dto.Combustible);

                return View(dto);
            }

            await _crear.crear(dto);

            var ultimo = (await _listar.Listar())?.OrderByDescending(x => x.IdDocumento).FirstOrDefault();
            if (ultimo != null)
            {
                var historial = new HistorialDocumentoDTO
                {
                    Fecha = DateTime.Now.ToString("dd/MM/yyyy"),
                    TipoDocumento = "Compra y venta de vehículos",
                    Cliente = dto.CedulaComprador,
                    Abogado = dto.CedulaAbogado,
                    IdDocumento = ultimo.IdDocumento,
                    Titulo = $"Doc.no.{ultimo.IdDocumento} Compraventa Vehículo"
                };
                await _crearHistorial.Crear(historial);
            }

            return RedirectToAction("DocsHistorial", "THistorialDocumento1");
        }

        // -------- PDF --------
        [HttpGet]
        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<IActionResult> PrevisualizarPDF(
            string LugarFirma
            )
        {
            var htmlPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DocsMachotes", "OpcionCompraVentaVehiculos.html");
            var htmlTemplate = System.IO.File.ReadAllText(htmlPath);

            var q = Request.Query;

            string logoBase64 = "";
            string nombreBufete = "";
            string cedJuridica = "";
            string telDespacho = "";
            string emailDespacho = "";

            var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "img", "PreaceptaLogoColorNegro.png");
            if (System.IO.File.Exists(logoPath))
                logoBase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(logoPath));

            _ = int.TryParse(q["cedulaAbogado"], out var cedAbogado);
            _ = int.TryParse(q["cedulaPropietario"], out var cedProp);
            _ = int.TryParse(q["cedulaComprador"], out var cedComp);

            _ = int.TryParse(q["marcaVehiculo"], out var idMarcaVeh);
            _ = int.TryParse(q["tipoVehiculo"], out var idTipoVeh);
            _ = int.TryParse(q["marcaMotor"], out var idMarcaMotor);
            _ = int.TryParse(q["combustible"], out var idComb);

            var ab = await _buscarPersona.buscar(cedAbogado);
            var prop = await _buscarPersona.buscar(cedProp);
            var comp = await _buscarPersona.buscar(cedComp);

            var nombreNotario = ab != null ? $"{ab.Nombre} {ab.Apellido1} {(ab.Apellido2 ?? "")}".Trim() : q["cedulaAbogado"].ToString();
            var cedulaNotario = ab?.Cedula.ToString() ?? q["cedulaAbogado"].ToString();

            var nombreVendedor = prop != null ? $"{prop.Nombre} {prop.Apellido1} {(prop.Apellido2 ?? "")}".Trim() : q["cedulaPropietario"].ToString();
            var cedulaVendedor = prop?.Cedula.ToString() ?? q["cedulaPropietario"].ToString();

            var nombreComprador = comp != null ? $"{comp.Nombre} {comp.Apellido1} {(comp.Apellido2 ?? "")}".Trim() : q["cedulaComprador"].ToString();
            var cedulaComprador = comp?.Cedula.ToString() ?? q["cedulaComprador"].ToString();

            string marcaVehiculoNombre = q["marcaVehiculo"].ToString();
            string tipoVehiculoNombre = q["tipoVehiculo"].ToString();
            string marcaMotorNombre = q["marcaMotor"].ToString();
            string combustibleNombre = q["combustible"].ToString();

            try
            {
                var marcas = await _listarMarcas.listar();
                var tipos = await _listarTipos.Listar();
                var combs = await _listarCombustibles.listar();

                marcaVehiculoNombre = marcas?.FirstOrDefault(x => x.Id == idMarcaVeh)?.Nombre ?? marcaVehiculoNombre;
                tipoVehiculoNombre = tipos?.FirstOrDefault(x => x.Id == idTipoVeh)?.Nombre ?? tipoVehiculoNombre;
                marcaMotorNombre = marcas?.FirstOrDefault(x => x.Id == idMarcaMotor)?.Nombre ?? marcaMotorNombre;
                combustibleNombre = combs?.FirstOrDefault(x => x.Id == idComb)?.Nombre ?? combustibleNombre;
            }
            catch { }

            string lugarFirmaMostrar = LugarFirma.ToString() ?? "";
            var LugarFirmas = await _buscarDistrito.buscarDistrito(int.Parse(lugarFirmaMostrar));

            string fechaInicioMostrar = q["fechaInicio"];
            if (DateTime.TryParse(fechaInicioMostrar, out var fIni))
                fechaInicioMostrar = fIni.ToString("dd/MM/yyyy");

            string fechaFirmaMostrar = q["fechaFirma"];
            if (string.IsNullOrWhiteSpace(fechaFirmaMostrar))
                fechaFirmaMostrar = DateTime.Now.ToString("dd/MM/yyyy");
            else if (DateTime.TryParse(fechaFirmaMostrar, out var fFirma))
                fechaFirmaMostrar = fFirma.ToString("dd/MM/yyyy");

            string horaFirmaMostrar = q["horaFirma"];
            if (string.IsNullOrWhiteSpace(horaFirmaMostrar))
                horaFirmaMostrar = DateTime.Now.ToString("hh:mm tt", CultureInfo.InvariantCulture);
            else if (DateTime.TryParse(horaFirmaMostrar, out var dtHora))
                horaFirmaMostrar = dtHora.ToString("hh:mm tt", CultureInfo.InvariantCulture);
            else if (TimeSpan.TryParse(horaFirmaMostrar, out var tsHora))
                horaFirmaMostrar = DateTime.Today.Add(tsHora).ToString("hh:mm tt", CultureInfo.InvariantCulture);

            var gastosRaw = (q["gastosTraspasoPagadosPor"].ToString() ?? "").Trim().ToUpperInvariant();
            string gastosTexto = gastosRaw switch
            {
                "COMPRADOR" => nombreComprador,
                "VENDEDOR" => nombreVendedor,
                _ => string.IsNullOrWhiteSpace(gastosRaw) ? nombreComprador : gastosRaw
            };

            var html = htmlTemplate
                .Replace("{{LOGO}}", logoBase64)
                .Replace("{{NombreBufete}}", nombreBufete)
                .Replace("{{CedulaJuridica}}", cedJuridica)
                .Replace("{{TelefonoDespacho}}", telDespacho)
                .Replace("{{EmailDespacho}}", emailDespacho)

                .Replace("{{ID_DOCUMENTO}}", q["idDocumento"])
                .Replace("{{NUMERO_ESCRITURA}}", q["numeroEscritura"])
                .Replace("{{NOMBRE_NOTARIO}}", nombreNotario)
                .Replace("{{CEDULA_NOTARIO}}", cedulaNotario)
                .Replace("{{DIRECCION_NOTARIO}}", ab?.Direccion2 ?? "")

                .Replace("{{NOMBRE_VENDEDOR}}", nombreVendedor)
                .Replace("{{CEDULA_VENDEDOR}}", cedulaVendedor)
                .Replace("{{ESTADO_CIVIL_VENDEDOR}}", prop?.EstadoCivil ?? "")
                .Replace("{{OFICIO_VENDEDOR}}", prop?.Oficio ?? "")
                .Replace("{{DIRECCION_EXACTA_VENDEDOR}}", prop?.Direccion2 ?? "")

                .Replace("{{NOMBRE_COMPRADOR}}", nombreComprador)
                .Replace("{{CEDULA_COMPRADOR}}", cedulaComprador)
                .Replace("{{ESTADO_CIVIL_COMPRADOR}}", comp?.EstadoCivil ?? "")
                .Replace("{{OFICIO_COMPRADOR}}", comp?.Oficio ?? "")
                .Replace("{{DIRECCION_EXACTA_COMPRADOR}}", comp?.Direccion2 ?? "")

                .Replace("{{PLACA_VEHICULO}}", q["placaVehiculo"])
                .Replace("{{MARCA_VEHICULO}}", marcaVehiculoNombre)
                .Replace("{{TIPO_VEHICULO}}", tipoVehiculoNombre)
                .Replace("{{MODELO_VEHICULO}}", q["modeloVehiculo"])
                .Replace("{{CARROCERIA}}", q["carroceria"])
                .Replace("{{CATEGORIA}}", q["categoria"])
                .Replace("{{CHASIS}}", q["chasis"])
                .Replace("{{SERIE}}", q["serie"])
                .Replace("{{VIN}}", q["vin"])
                .Replace("{{MARCA_MOTOR}}", marcaMotorNombre)
                .Replace("{{NUMERO_MOTOR}}", q["numeroMotor"])
                .Replace("{{COLOR}}", q["color"])
                .Replace("{{COMBUSTIBLE}}", combustibleNombre)
                .Replace("{{ANIO}}", q["anio"])
                .Replace("{{CAPACIDAD}}", q["capacidad"])
                .Replace("{{CILINDRAJE}}", q["cilindraje"])
                .Replace("{{PRECIO}}", q["precio"])
                .Replace("{{MONEDA_PRECIO}}", q["monedaPrecio"])
                .Replace("{{PLAZO_OPCION_ANIOS}}", q["plazoOpcionAnios"])
                .Replace("{{FECHA_INICIO}}", fechaInicioMostrar)
                .Replace("{{MONTO_SENAL}}", q["montoSenal"])
                .Replace("{{MONEDA_SENAL}}", q["monedaSenal"])
                .Replace("{{MONTO_A_DEVOLVER}}", q["montoADevolver"])
                .Replace("{{MONTO_A_PERDER}}", q["montoAPerder"])
                .Replace("{{MONEDA_MONTO_PERDIDO}}", q["monedaMontoPerdido"])
                .Replace("{{GASTOS_TRASPASO_PAGADOS_POR}}", gastosTexto)
                .Replace("{{LUGAR_FIRMA}}", LugarFirmas.NombreDistrito)
                .Replace("{{HORA_FIRMA}}", horaFirmaMostrar)
                .Replace("{{FECHA_FIRMA}}", fechaFirmaMostrar);

            var doc = new HtmlToPdfDocument
            {
                GlobalSettings = new GlobalSettings { PaperSize = PaperKind.A4, Orientation = Orientation.Portrait },
                Objects = { new ObjectSettings { HtmlContent = html, WebSettings = { DefaultEncoding = "utf-8" } } }
            };

            var pdf = _converter.Convert(doc);
            return File(pdf, "application/pdf");
        }
    }
}
