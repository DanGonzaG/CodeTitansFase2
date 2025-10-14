using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Preacepta.LN.CrDireccion1.BuscarXid;
using Preacepta.LN.CrDireccion1.Listar;
using Preacepta.LN.DocsPagare.Buscar;
using Preacepta.LN.DocsPagare.Crear;
using Preacepta.LN.DocsPagare.Editar;
using Preacepta.LN.DocsPagare.Eliminar;
using Preacepta.LN.DocsPagare.Listar;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.LN.HistorialDocumentos.BuscarXid;
using Preacepta.LN.HistorialDocumentos.Crear;
using Preacepta.LN.HistorialDocumentos.Eliminar;
using Preacepta.LN.HistorialDocumentos.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Globalization;
using Preacepta.LN.BitacoraEventos.Crear;

namespace Preacepta.UI.Controllers
{
    public class TDocsPagaresController : Controller
    {
        private readonly IConverter _converter;

        // LN de pagaré
        private readonly IBuscarPagareLN _buscar;
        private readonly ICrearPagareLN _crear;
        private readonly IEditarPagareLN _editar;
        private readonly IEliminarPagareLN _eliminar;
        private readonly IListarPagareLN _listar;

        // Personas
        private readonly IBuscarXidGePersonaLN _buscarPersona;

        // Historial
        private readonly ICrearHistorialLN _crearHistorialLN;
        private readonly IListarHistorialLN _listarHistorialLN;
        private readonly IBuscarHistorialLN _buscarHistorialLN;
        private readonly IELiminarHistorialLN _eliminarHistorialLN;

        // Direcciones
        private readonly IListarCrDireccion1LN _listarDirecciones;
        private readonly IBuscarCrDireccion1LN _buscarDistrito;

        private readonly ICrearEventosLN _bitacoraLN;

        public TDocsPagaresController(
            IConverter converter,
            IBuscarPagareLN buscar,
            ICrearPagareLN crear,
            IEditarPagareLN editar,
            IEliminarPagareLN eliminar,
            IListarPagareLN listar,
            IBuscarXidGePersonaLN buscarPersona,
            ICrearHistorialLN crearHistorialLN,
            IListarHistorialLN listarHistorialLN,
            IBuscarHistorialLN buscarHistorialLN,
            IELiminarHistorialLN eliminarHistorialLN,
            IListarCrDireccion1LN listarDirecciones,
            IBuscarCrDireccion1LN buscarDistrito,
            ICrearEventosLN bitacora
        )
        {
            _converter = converter;

            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;

            _buscarPersona = buscarPersona;

            _crearHistorialLN = crearHistorialLN;
            _listarHistorialLN = listarHistorialLN;
            _buscarHistorialLN = buscarHistorialLN;
            _eliminarHistorialLN = eliminarHistorialLN;

            _listarDirecciones = listarDirecciones;

            _buscarDistrito = buscarDistrito;
            _bitacoraLN = bitacora;
        }

        /********************************************************/
        // controller de Framework
        /********************************************************/

        #region Listar
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Index()
        {
            return View(await _listar.Listar());
        }
        #endregion

        #region Detalles
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _buscar.buscar(id);
            if (dto == null) return NotFound();
            return View(dto);
        }
        #endregion

        #region Crear (Root)
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Create()
        {
            // Ubicación de firma como lista plana de distritos
            var distritos = await _listarDirecciones.listarDistritos();
            ViewBag.UbicacionFirma = new SelectList(distritos, "IdDistrito", "NombreDistrito");

            return View(new DocsPagareDTO
            {
                FechaFirma = DateTime.Today.ToString("yyyy-MM-dd"),
                HoraFirma = DateTime.Now.ToString("HH:mm")
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Create([Bind(
            "IdDocumento,MontoNumerico,CedulaDeudor,SociedadDeudor,CedulaJuridicaSociedad," +
            "AcreedorNombre,CedulaJuridicaAcreedor,AcreedorDomicilio,FechaFirma,HoraFirma," +
            "FechaVencimiento,InteresFormula,InteresTasaActual,InteresBase,LugarPago," +
            "CedulaFiador,UbicacionFirma,CedulaAbogado,TipoSociedad,UbicacionSociedad"
        )] DocsPagareDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _crear.crear(dto);

            // Obtener el último Id creado y registrar historial (mismo patrón que Autorización)
            var registros = await _listar.Listar();
            var ultimo = registros.LastOrDefault();
            if (ultimo != null)
            {
                var hist = new HistorialDocumentoDTO
                {
                    Cliente = dto.CedulaDeudor,
                    Abogado = dto.CedulaAbogado,
                    Fecha = DateTime.Now.ToString(),
                    TipoDocumento = "Pagaré",
                    IdDocumento = ultimo.IdDocumento,
                    Titulo = $"Doc.no.{ultimo.IdDocumento} Pagaré"
                };

                await _crearHistorialLN.Crear(hist);

                var usuario = User.Identity?.Name ?? "Desconocido";
                var tituloCorto = $"Pagaré {dto.CedulaDeudor}";
                var accion = $"Se creó documento '{tituloCorto}' con ID {ultimo.IdDocumento}";
                await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_DocsPagare", accion, ultimo.IdDocumento);
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Editar (Root)
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _buscar.buscar(id);
            if (dto == null) return NotFound();

            var distritos = await _listarDirecciones.listarDistritos();
            ViewBag.UbicacionFirma = new SelectList(distritos, "IdDistrito", "NombreDistrito", dto.UbicacionFirma);

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int id, [Bind(
            "IdDocumento,MontoNumerico,CedulaDeudor,SociedadDeudor,CedulaJuridicaSociedad," +
            "AcreedorNombre,CedulaJuridicaAcreedor,AcreedorDomicilio,FechaFirma,HoraFirma," +
            "FechaVencimiento,InteresFormula,InteresTasaActual,InteresBase,LugarPago," +
            "CedulaFiador,UbicacionFirma,CedulaAbogado,TipoSociedad,UbicacionSociedad"
        )] DocsPagareDTO dto)
        {
            if (id != dto.IdDocumento) return NotFound();
            if (!ModelState.IsValid) return View(dto);

            await _editar.editar(dto);

            var usuario = User.Identity?.Name ?? "Desconocido";
            var tituloCorto = $"Pagaré {dto.CedulaDeudor}";
            var accion = $"Se editó documento '{tituloCorto}' con ID {dto.IdDocumento}";
            await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_DocsPagare", accion, dto.IdDocumento);


            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Eliminar (Root)
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
            var doc = await _buscar.buscar(id);
            if (doc != null)
            {
                await _eliminar.eliminar(id);

                // Mismo patrón de Autorización: buscar id del historial por (id, tipo) y eliminar
                var idHist = await _buscarHistorialLN.BuscarXidDocumento(id, "Pagaré");
                if (idHist > 0)
                    await _eliminarHistorialLN.Eliminar(idHist);
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        /********************************************************/
        // controller personalizados
        /********************************************************/

        #region Crear Documento – Abogado
        [HttpGet]
        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<IActionResult> CreateDocsPagares(int CedulaDeudor, int CedulaFiador)
        {
            var deudor  = await _buscarPersona.buscar(CedulaDeudor);
            var fiador  = await _buscarPersona.buscar(CedulaFiador);
            var abogado = await _buscarPersona.buscarXcorreo(User.Identity.Name);

            ViewBag.DeudorCedula = deudor?.Cedula ?? 0;
            ViewBag.DeudorNombre = deudor?.Nombre ?? "";
            ViewBag.DeudorApellido1 = deudor?.Apellido1 ?? "";
            ViewBag.DeudorApellido2 = deudor?.Apellido2 ?? "";

            ViewBag.FiadorCedula = fiador?.Cedula ?? 0;
            ViewBag.FiadorNombre = fiador?.Nombre ?? "";
            ViewBag.FiadorApellido1 = fiador?.Apellido1 ?? "";
            ViewBag.FiadorApellido2 = fiador?.Apellido2 ?? "";

            ViewBag.FiadorNombreCompleto = $"{ViewBag.FiadorNombre} {ViewBag.FiadorApellido1} {ViewBag.FiadorApellido2}".Trim();

            ViewBag.AbogadoCedula = abogado?.Cedula ?? 0;

            var distritos = await _listarDirecciones.listarDistritos();
            ViewBag.UbicacionFirma = new SelectList(distritos, "IdDistrito", "NombreDistrito");

            var model = new DocsPagareDTO
            {
                CedulaDeudor  = deudor?.Cedula ?? 0,
                CedulaFiador  = fiador?.Cedula ?? 0,
                CedulaAbogado = abogado?.Cedula ?? 0,
                FechaFirma    = DateTime.Today.ToString("yyyy-MM-dd"),
                HoraFirma     = DateTime.Now.ToString("HH:mm")
            };

            return View("CreateDocsPagares", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<IActionResult> CreateDocsPagares(DocsPagareDTO dto)
        {
            dto.FechaFirma ??= DateTime.Now.ToString("yyyy-MM-dd");
            dto.FechaVencimiento ??= DateTime.Now.ToString("yyyy-MM-dd");

            if (!ModelState.IsValid)
            {
                var deudor = await _buscarPersona.buscar(dto.CedulaDeudor);
                var fiador = await _buscarPersona.buscar(dto.CedulaFiador);
                var abogado = await _buscarPersona.buscarXcorreo(User.Identity.Name);

                ViewBag.DeudorCedula = deudor?.Cedula ?? 0;
                ViewBag.DeudorNombre = deudor?.Nombre ?? "";
                ViewBag.DeudorApellido1 = deudor?.Apellido1 ?? "";
                ViewBag.DeudorApellido2 = deudor?.Apellido2 ?? "";

                ViewBag.FiadorCedula = fiador?.Cedula ?? 0;
                ViewBag.FiadorNombre = $"{fiador?.Nombre} {fiador?.Apellido1} {fiador?.Apellido2}".Trim();

                ViewBag.AbogadoCedula = abogado?.Cedula ?? 0;

                var distritos = await _listarDirecciones.listarDistritos();
                ViewBag.UbicacionFirma = new SelectList(distritos, "IdDistrito", "NombreDistrito");
            }

            await _crear.crear(dto);

            var registros = await _listar.Listar();
            var ultimo = registros.LastOrDefault();
            if (ultimo != null)
            {
                var historial = new HistorialDocumentoDTO
                {
                    Cliente = dto.CedulaDeudor,
                    Abogado = dto.CedulaAbogado,
                    Fecha = DateTime.Now.ToString(),
                    TipoDocumento = "Pagaré",
                    IdDocumento = ultimo.IdDocumento,
                    Titulo = $"Doc.no.{ultimo.IdDocumento} Pagaré"
                };
                await _crearHistorialLN.Crear(historial);

                var usuario = User.Identity?.Name ?? "Desconocido";
                var tituloCorto = $"Pagaré {dto.CedulaDeudor}";
                var accion = $"Se creó documento '{tituloCorto}' con ID {ultimo.IdDocumento}";
                await _bitacoraLN.RegistrarBitacoraAsync(usuario, "T_DocsPagare", accion, ultimo.IdDocumento);
            
        }

            return RedirectToAction("DocsHistorial", "THistorialDocumento1");
        }
        #endregion

        #region Previsualizar PDF
        [HttpGet]
        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<IActionResult> PrevisualizarPDF(
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
            string LugarPago,        
            string cedulaFiador,
            string UbicacionFirma,   
            string TipoSociedad,
            string UbicacionSociedad
        )
        {
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DocsMachotes", "DocsPagare.html");
            var htmlTemplate = System.IO.File.ReadAllText(templatePath);

            string logoBase64 = "";
            var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "img", "PreaceptaLogoColorNegro.png");
            if (System.IO.File.Exists(logoPath))
                logoBase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(logoPath));

            _ = int.TryParse(cedulaDeudor, out var cedDeudorInt);
            _ = int.TryParse(cedulaFiador, out var cedFiadorInt);

            var deudor = cedDeudorInt > 0 ? await _buscarPersona.buscar(cedDeudorInt) : null;
            var fiador = cedFiadorInt > 0 ? await _buscarPersona.buscar(cedFiadorInt) : null;

            var deudorNombre = deudor != null
                ? $"{deudor.Nombre} {deudor.Apellido1} {(deudor.Apellido2 ?? "")}".Trim()
                : "";

            var fiadorNombre = fiador != null
                ? $"{fiador.Nombre} {fiador.Apellido1} {(fiador.Apellido2 ?? "")}".Trim()
                : "";

            string lugarPagoMostrar = LugarPago.ToString() ?? "";
            var lugarPagos = await _buscarDistrito.buscarDistrito(int.Parse(lugarPagoMostrar));

            string UbicacionFirmaMostrar = UbicacionFirma.ToString() ?? "";
            var UbicacionFirmas = await _buscarDistrito.buscarDistrito(int.Parse(UbicacionFirmaMostrar));

            var ahora = DateTime.Now;
            var fechaFirmaStr = !string.IsNullOrWhiteSpace(fechaFirmaNuevo)
                ? fechaFirmaNuevo
                : ahora.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            var horaFirmaStr = !string.IsNullOrWhiteSpace(horaFirmaNuevo)
                ? (DateTime.TryParse(horaFirmaNuevo, out var dt) ? dt.ToString("HH:mm")
                    : (TimeSpan.TryParse(horaFirmaNuevo, out var ts) ? ahora.Date.Add(ts).ToString("HH:mm") : horaFirmaNuevo))
                : ahora.ToString("HH:mm");

            var abogado = await _buscarPersona.buscarXcorreo(User.Identity?.Name ?? "");
            var cedulaAbogado = (abogado?.Cedula ?? 0).ToString();

            htmlTemplate = htmlTemplate
                .Replace("{{LOGO}}", logoBase64)
                .Replace("{{ID_DOCUMENTO}}", idDocumento ?? "")
                .Replace("{{MONTO_NUMERICO}}", montoNumerico ?? "")
                .Replace("{{MONTO_LETRAS}}", montoNumerico ?? "")
                .Replace("{{NOMBRE_DEUDOR}}", deudorNombre)
                .Replace("{{CEDULA_DEUDOR}}", cedulaDeudor ?? "")
                .Replace("{{CEDULA_DEUDOR_LETRAS}}", cedulaDeudor ?? "")
                .Replace("{{ESTADO_CIVIL_DEUDOR}}", deudor?.EstadoCivil ?? "")
                .Replace("{{DOMICILIO_DEUDOR}}", deudor?.Direccion2 ?? "")
                .Replace("{{Oficio}}", deudor?.Oficio ?? "")
                .Replace("{{NOMBRE_FIADOR}}", fiadorNombre)
                .Replace("{{CEDULA_FIADOR}}", cedulaFiador ?? "")
                .Replace("{{SOCIEDAD_DEUDOR}}", sociedadDeudor ?? "")
                .Replace("{{SOCIEDAD_TIPO}}", TipoSociedad ?? "")
                .Replace("{{CEDULA_JURIDICA_SOCIEDAD}}", cedulaJuridicaSociedad ?? "")
                .Replace("{{CEDULA_JURIDICA_SOCIEDAD_LETRAS}}", cedulaJuridicaSociedad ?? "")
                .Replace("{{Ubicacion_Sociedad}}", UbicacionSociedad ?? "")
                .Replace("{{ACREEDOR_NOMBRE}}", acreedorNombre ?? "")
                .Replace("{{CEDULA_JURIDICA_ACREEDOR}}", cedulaJuridicaAcreedor ?? "")
                .Replace("{{CEDULA_JURIDICA_ACREEDOR_LETRAS}}", cedulaJuridicaAcreedor ?? "")
                .Replace("{{ACREEDOR_DOMICILIO}}", acreedorDomicilio ?? "")
                .Replace("{{INTERES_FORMULA}}", interesFormula ?? "")
                .Replace("{{INTERES_TASA_ACTUAL}}", interesTasaActual ?? "")
                .Replace("{{INTERES_BASE}}", interesBase ?? "")
                .Replace("{{FECHA_VENCIMIENTO}}", fechaVencimiento ?? "")
                .Replace("{{LUGAR_PAGO}}", lugarPagos.NombreDistrito)
                .Replace("{{UBICACION_FIRMA}}", UbicacionFirmas.NombreDistrito)
                .Replace("{{HORA_FIRMA_NUEVO}}", horaFirmaStr)
                .Replace("{{FECHA_FIRMA_NUEVO}}", fechaFirmaStr)
                .Replace("{{HORA_FIRMA_LETRAS}}", horaFirmaStr)
                .Replace("{{FECHA_FIRMA_LETRAS}}", fechaFirmaStr)
                .Replace("{{CEDULA_ABOGADO}}", cedulaAbogado);

            var doc = new HtmlToPdfDocument
            {
                GlobalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait
                },
                Objects =
                {
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
        #endregion
    }
}
