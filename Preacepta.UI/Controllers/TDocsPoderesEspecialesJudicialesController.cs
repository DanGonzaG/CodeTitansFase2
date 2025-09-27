using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Preacepta.AD.HistorialDocumentos.Eliminar;
using Preacepta.LN.DocPoderesEspecialesJudiciales.Buscar;
using Preacepta.LN.DocPoderesEspecialesJudiciales.Crear;
using Preacepta.LN.DocPoderesEspecialesJudiciales.Editar;
using Preacepta.LN.DocPoderesEspecialesJudiciales.Eliminar;
using Preacepta.LN.DocPoderesEspecialesJudiciales.Listar;
using Preacepta.LN.GeAbogado.BuscarXid;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.LN.HistorialDocumentos.BuscarXid;
using Preacepta.LN.HistorialDocumentos.Crear;
using Preacepta.LN.HistorialDocumentos.Editar;
using Preacepta.LN.HistorialDocumentos.Eliminar;
using Preacepta.LN.HistorialDocumentos.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System.Globalization;

namespace Preacepta.UI.Controllers
{
    public class TDocsPoderesEspecialesJudicialesController : Controller
    {
        private readonly IConverter _converter;

        // Documento
        private readonly IBuscarPoderJudLN _buscar;
        private readonly ICrearPoderJudLN _crear;
        private readonly IEditarPoderJudLN _editar;
        private readonly IEliminarPoderJudLN _eliminar;
        private readonly IListarPoderJudLN _listar;

        // Personas 
        private readonly IBuscarXidGePersonaLN _buscarPersona;

        // Historial 
        private readonly ICrearHistorialLN _crearHistorial;
        private readonly IBuscarHistorialLN _buscarHistorial;
        private readonly IListarHistorialLN _listarHistorial;
        private readonly IEditarHistorialLN _editarHistorial;
        private readonly IELiminarHistorialLN _eliminarHistorial;

        // Carnet (abogado)
        private readonly IBuscarAbogadoLN _buscarAbogado;

        public TDocsPoderesEspecialesJudicialesController(
            IBuscarPoderJudLN buscar,
            ICrearPoderJudLN crear,
            IEditarPoderJudLN editar,
            IEliminarPoderJudLN eliminar,
            IListarPoderJudLN listar,
            IConverter converter,
            IBuscarXidGePersonaLN buscarPersona,
            ICrearHistorialLN crearHistorial,
            IBuscarHistorialLN buscarHistorial,
            IListarHistorialLN listarHistorial,
            IBuscarAbogadoLN buscarAbogado,
            IEditarHistorialLN editarHistorial,
            IELiminarHistorialLN eliminarHistorial)
        {
            _converter = converter;

            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;

            _buscarPersona = buscarPersona;

            _crearHistorial = crearHistorial;
            _buscarHistorial = buscarHistorial;
            _listarHistorial = listarHistorial;

            _buscarAbogado = buscarAbogado;

            _editarHistorial = editarHistorial;
            _eliminarHistorial = eliminarHistorial;
        }

        // ================= CRUD base =================
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Index() => View(await _listar.Listar());

        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _buscar.buscar(id);
            if (dto == null) return NotFound();
            return View(dto);
        }

        [Authorize(Roles = "Gestor")]
        public IActionResult Create()
        {
            var model = new DocsPoderesEspecialesJudicialeDTO
            {
                Fecha = DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Create(
            [Bind("IdDoc,Fecha,IdAbogado,IdCliente,Texto,NumCausa")]
            DocsPoderesEspecialesJudicialeDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            dto.Fecha ??= DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            await _crear.crear(dto);

            var nuevoIdDoc = (await _listar.Listar())
                                ?.OrderByDescending(x => x.IdDoc)
                                .Select(x => x.IdDoc)
                                .FirstOrDefault() ?? 0;

            if (nuevoIdDoc > 0)
            {
                var historial = new HistorialDocumentoDTO
                {
                    Fecha = DateTime.Now.ToString("dd/MM/yyyy"),
                    TipoDocumento = "Poderes especiales judiciales",
                    Cliente = dto.IdCliente,
                    Abogado = dto.IdAbogado,
                    IdDocumento = nuevoIdDoc,
                    Titulo = $"Doc.no.{nuevoIdDoc} Poder especial judicial"
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
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("IdDoc,Fecha,IdAbogado,IdCliente,Texto,NumCausa")]
            DocsPoderesEspecialesJudicialeDTO dto)
        {
            if (id != dto.IdDoc) return NotFound();
            if (!ModelState.IsValid) return View(dto);

            dto.Fecha = string.IsNullOrWhiteSpace(dto.Fecha)
                ? DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                : dto.Fecha;

            await _editar.editar(dto);

            try
            {
                var historiales = await _listarHistorial.listar();
                var relacionados = historiales?
                    .Where(h => h.IdDocumento == dto.IdDoc &&
                                string.Equals(h.TipoDocumento, "Poderes especiales judiciales", StringComparison.OrdinalIgnoreCase))
                    .ToList() ?? new List<HistorialDocumentoDTO>();

                foreach (var h in relacionados)
                {
                    bool cambio = false;
                    if (h.Abogado != dto.IdAbogado) { h.Abogado = dto.IdAbogado; cambio = true; }
                    if (h.Cliente != dto.IdCliente) { h.Cliente = dto.IdCliente; cambio = true; }
                    if (cambio) await _editarHistorial.Editar(h);
                }
            }
            catch
            {
                TempData["HistorialWarn"] = "Se guardó el documento, pero no se pudo sincronizar el historial.";
            }

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
            var historiales = await _listarHistorial.listar();
            var relacionados = historiales?
                .Where(h => h.IdDocumento == id &&
                            string.Equals(h.TipoDocumento, "Poderes especiales judiciales", StringComparison.OrdinalIgnoreCase))
                .ToList() ?? new List<HistorialDocumentoDTO>();

            foreach (var h in relacionados)
                await _eliminarHistorial.Eliminar(h.Id);

            await _eliminar.eliminar(id);

            return RedirectToAction(nameof(Index));
        }

        // ================= MÉTODOS PERSONALIZADOS =================

        [HttpGet]
        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<IActionResult> CreateDocsPoderesEspecialesJudiciales(int id)
        {
            var cliente = await _buscarPersona.buscar(id);
            var abogado = await _buscarPersona.buscarXcorreo(User.Identity.Name);

            ViewBag.ClienteCedula = cliente?.Cedula ?? 0;
            ViewBag.ClienteNombre = cliente?.Nombre ?? "";
            ViewBag.ClienteApellido1 = cliente?.Apellido1 ?? "";
            ViewBag.ClienteApellido2 = cliente?.Apellido2 ?? "";
            ViewBag.AbogadoCedula = abogado?.Cedula ?? 0;

            var model = new DocsPoderesEspecialesJudicialeDTO
            {
                Fecha = DateTime.Today.ToString("yyyy-MM-dd"),
                IdCliente = cliente?.Cedula ?? 0,
                IdAbogado = abogado?.Cedula ?? 0
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<IActionResult> CreateDocsPoderesEspecialesJudiciales(DocsPoderesEspecialesJudicialeDTO dto)
        {
            dto.Fecha ??= DateTime.Now.ToString("yyyy-MM-dd");

            if (!ModelState.IsValid)
            {
                var c = await _buscarPersona.buscar(dto.IdCliente);
                var a = await _buscarPersona.buscar(dto.IdAbogado);

                ViewBag.ClienteCedula = c?.Cedula ?? 0;
                ViewBag.ClienteNombre = c?.Nombre ?? "";
                ViewBag.ClienteApellido1 = c?.Apellido1 ?? "";
                ViewBag.ClienteApellido2 = c?.Apellido2 ?? "";
                ViewBag.AbogadoCedula = a?.Cedula ?? 0;

                //return View(dto);
            }

            await _crear.crear(dto);

            var nuevoIdDoc = (await _listar.Listar())
                                ?.OrderByDescending(x => x.IdDoc)
                                .Select(x => x.IdDoc)
                                .FirstOrDefault() ?? 0;

            if (nuevoIdDoc > 0)
            {
                var historial = new HistorialDocumentoDTO
                {
                    Fecha = DateTime.Now.ToString("dd/MM/yyyy"),
                    TipoDocumento = "Poderes especiales judiciales",
                    Cliente = dto.IdCliente,
                    Abogado = dto.IdAbogado,
                    IdDocumento = nuevoIdDoc,
                    Titulo = $"Doc.no.{nuevoIdDoc} Poder especial judicial"
                };
                await _crearHistorial.Crear(historial);
            }

            return RedirectToAction("DocsHistorial", "THistorialDocumento1");
        }

        // ===================== PDF =====================

        [HttpGet]
        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<IActionResult> PrevisualizarPDF(
            string idDoc,
            string Fecha,
            string idAbogado,
            string idCliente,
            string texto,
            string NumCausa)
        {
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DocsMachotes", "PoderesEspecialesJudiciales.html");
            var htmlTemplate = System.IO.File.ReadAllText(templatePath);

            string logoBase64 = "";
            string nombreBufete = "";
            string cedJuridica = "";
            string telDespacho = "";
            string emailDesp = "";

            var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "img", "PreaceptaLogoColorNegro.png");
            if (System.IO.File.Exists(logoPath))
                logoBase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(logoPath));

            _ = int.TryParse(idAbogado, out var cedAbogado);
            _ = int.TryParse(idCliente, out var cedCliente);

            var personaAbogado = await _buscarPersona.buscar(cedAbogado);
            var personaCliente = await _buscarPersona.buscar(cedCliente);

            string nombreAbogado = personaAbogado != null
                ? $"{personaAbogado.Nombre} {personaAbogado.Apellido1} {(personaAbogado.Apellido2 ?? "")}".Trim()
                : idAbogado;

            string nombreCliente = personaCliente != null
                ? $"{personaCliente.Nombre} {personaCliente.Apellido1} {(personaCliente.Apellido2 ?? "")}".Trim()
                : idCliente;

            string cedulaAbogadoStr = personaAbogado?.Cedula.ToString() ?? idAbogado;
            string cedulaClienteStr = personaCliente?.Cedula.ToString() ?? idCliente;

            var abogadoDetalle = (cedAbogado > 0) ? await _buscarAbogado.buscar(cedAbogado) : null;
            string carnetProfesional = abogadoDetalle?.Carnet.ToString() ?? "";

            string fechaMostrar = DateTime.TryParse(Fecha, out var f)
                ? f.ToString("dd/MM/yyyy")
                : DateTime.Today.ToString("dd/MM/yyyy");

            string lugarDocumento = new[] {
                personaAbogado?.Direccion2,
                personaCliente?.Direccion2
            }.FirstOrDefault(s => !string.IsNullOrWhiteSpace(s)) ?? "San José";

            htmlTemplate = htmlTemplate
                .Replace("{{LOGO}}", logoBase64)
                .Replace("{{NombreBufete}}", nombreBufete)
                .Replace("{{CedulaJuridica}}", cedJuridica)
                .Replace("{{TelefonoDespacho}}", telDespacho)
                .Replace("{{EmailDespacho}}", emailDesp)

                .Replace("{{ID_DOC}}", idDoc ?? "")
                .Replace("{{FECHA}}", fechaMostrar)

                .Replace("{{NOMBRE_PODERDANTE}}", nombreCliente)
                .Replace("{{CEDULA_PODERDANTE}}", cedulaClienteStr)
                .Replace("{{CEDULA_PODERDANTE_LETRAS}}", cedulaClienteStr)
                .Replace("{{ESTADO_CIVIL_PODERDANTE}}", personaCliente?.EstadoCivil ?? "")
                .Replace("{{OFICIO_PODERDANTE}}", personaCliente?.Oficio ?? "")
                .Replace("{{DIRECCION_PODERDANTE}}", personaCliente?.Direccion2 ?? "")
                .Replace("{{CONDICION_MAYOR_PODERDANTE}}", "mayor")

                .Replace("{{NOMBRE_APODERADO}}", nombreAbogado)
                .Replace("{{CEDULA_APODERADO}}", cedulaAbogadoStr)
                .Replace("{{CEDULA_APODERADO_LETRAS}}", cedulaAbogadoStr)
                .Replace("{{ESTADO_CIVIL_APODERADO}}", personaAbogado?.EstadoCivil ?? "")
                .Replace("{{OFICIO_APODERADO}}", "abogado")
                .Replace("{{DIRECCION_APODERADO}}", personaAbogado?.Direccion2 ?? "")
                .Replace("{{OFICINA_APODERADO}}", personaAbogado?.Direccion2 ?? "")
                .Replace("{{CONDICION_MAYOR_APODERADO}}", "mayor")

                .Replace("{{CARNE_PROFESIONAL}}", carnetProfesional)
                .Replace("{{CARNE_PROFESIONAL_LETRAS}}", carnetProfesional)

                .Replace("{{PODER_ESPECIAL_JUDICIAL}}", texto ?? "")
                .Replace("{{TEXTO}}", texto ?? "")
                .Replace("{{NUMERO_CAUSA}}", NumCausa ?? "")

                .Replace("{{LUGAR_DOCUMENTO}}", lugarDocumento)
                .Replace("{{FECHA_EN_PALABRAS}}", fechaMostrar);

            var docPdf = new HtmlToPdfDocument
            {
                GlobalSettings = new GlobalSettings { PaperSize = PaperKind.A4, Orientation = Orientation.Portrait },
                Objects = { new ObjectSettings { HtmlContent = htmlTemplate, WebSettings = { DefaultEncoding = "utf-8" } } }
            };

            var pdf = _converter.Convert(docPdf);
            return File(pdf, "application/pdf");
        }
    }
}
