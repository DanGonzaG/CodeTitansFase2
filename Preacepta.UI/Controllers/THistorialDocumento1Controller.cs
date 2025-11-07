using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Preacepta.AD;
using Preacepta.LN.CrDireccion1.BuscarXid;
using Preacepta.LN.CrDireccion1.Listar;
using Preacepta.LN.DocPoderesEspecialesJudiciales.Buscar;
using Preacepta.LN.DocsAutorizacionRevisionExpediente.BuscarXid;
using Preacepta.LN.DocsAutorizacionRevisionExpediente.Editar;
using Preacepta.LN.DocsCombustible.Listar;
using Preacepta.LN.DocsMarcaVehiculo.Listar;
using Preacepta.LN.DocsOpcionCompraventaVehiculo.Buscar;
using Preacepta.LN.DocsPagare.Buscar;
using Preacepta.LN.DocsTipoVehiculo.Listar;
using Preacepta.LN.GeAbogado.BuscarXid;
using Preacepta.LN.DocsCompraventaFinca.BuscarXid;
using Preacepta.LN.DocsCompraventaFinca.Editar;
using Preacepta.LN.DocsContratoPrestacionServicios.BuscarXid;
using Preacepta.LN.DocsContratoPrestacionServicios.Editar;
using Preacepta.LN.DocsInscripcionVehiculo.BuscarXid;
using Preacepta.LN.DocsInscripcionVehiculo.Editar;
using Preacepta.LN.GeAbogado.Listar;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.LN.GePersona.Listar;
using Preacepta.LN.HistorialDocumentos.BuscarXid;
using Preacepta.LN.HistorialDocumentos.Crear;
using Preacepta.LN.HistorialDocumentos.Editar;
using Preacepta.LN.HistorialDocumentos.Eliminar;
using Preacepta.LN.HistorialDocumentos.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System.Globalization;

namespace Preacepta.UI.Controllers
{
    public class THistorialDocumento1Controller : Controller
    {
        private readonly IConverter _converter;

        //Historial
        private readonly IListarHistorialLN _listarHistorial;
        private readonly IBuscarHistorialLN _buscarHistorial;
        private readonly ICrearHistorialLN _crearHistorial;
        private readonly IEditarHistorialLN _editarHistorial;
        private readonly IELiminarHistorialLN _eliminarHistorial;

        //Clientes y abogados
        private readonly IBuscarXidGePersonaLN _buscarPersona;
        private readonly IListarAbogadoLN _listarAbogados;
        private readonly IListarGePersonaLN _listarGePersona;
        private readonly IBuscarAbogadoLN _buscarAbogado;

        //Autorizacion y revision de expedientes, insertar aqui las de los demas documentos
        private readonly IBuscarDocsAutorizacionRevisionExpedienteLN _buscarDocsAutorizacionRevision;
        private readonly IEditarDocsAutorizacionRevisionExpedienteLN _editardocsAutorizacionRevision;
        private readonly IBuscarPagareLN _buscarPagare;

        private readonly IBuscarDocCVLN _buscarcompraventaV;
        private readonly IListarDocsCombustibleLN _listarCombustibles;
        private readonly IListarDocsMarcaVehiculoLN _listarMarcas;//Deben de cambiarse por la linea 91
        private readonly IListarTipoVehiculoLN _listarTipos;//Deben de cambiarse por la linea 90

        private readonly IBuscarPoderJudLN _buscarPoderJud;

        //compra venta de fincas
        private readonly IBuscarDocsCompraventaFincaLN _buscarDocsCompraVentaFinca;
        private readonly IEditarDocsCompraventaFincaLN _editarDocsCompraVentaFinca;

        //prestacion de servicios
        private readonly IBuscarDocsContratoPrestacionServiciosLN _buscarDocsContratoPrestacionServicios;
        private readonly IEditarDocsContratoPrestacionServiciosLN _editarDocsContratoPrestacionServicios;

        //incripción de vehiculo
        private readonly IBuscarDocsInscripcionVehiculoLN _buscarDocsInscVehiculo;
        private readonly IEditarDocsInscripcionVehiculoLN _editarDocsInscVehiculo;
        private readonly IListarTipoVehiculoLN _listarTipoVehiculoDocsInscVehiculo;
        private readonly IListarDocsMarcaVehiculoLN _listarMarcaVehiculoDocsInscVehiculo;

        //listar direcciones
        private readonly IBuscarCrDireccion1LN _buscarDireccion;
        private readonly IListarCrDireccion1LN _listarDireccion;

        public THistorialDocumento1Controller(
         IConverter converter,
         IListarHistorialLN listarHistorial,
         IBuscarHistorialLN buscarHistorial,
         ICrearHistorialLN crearHistorial,
         IEditarHistorialLN editarHistorial,
         IELiminarHistorialLN eliminarHistorial,

         //Clientes y abogados
         IBuscarXidGePersonaLN buscarPersona,
         IListarAbogadoLN listarAbogados,
         IListarGePersonaLN listarGePersona,
         IBuscarAbogadoLN buscarAbogado,

         //Autorizacion y revision de expedientes
         IBuscarDocsAutorizacionRevisionExpedienteLN buscarDocsAutorizacionRevision,
         IEditarDocsAutorizacionRevisionExpedienteLN editardocsAutorizacionRevision,
         IBuscarPagareLN buscarPagare,

         //Compra venta de vehiculo
         IBuscarDocCVLN buscarcompraventaV,
         IListarDocsCombustibleLN listarCombustibles,
         IListarDocsMarcaVehiculoLN listarMarcas,
         IListarTipoVehiculoLN listarTipos,
         IBuscarPoderJudLN buscarPoderJud,

         //Direccion
         IBuscarDocsCompraventaFincaLN buscarDocsCompraVentaFinca,
         IEditarDocsCompraventaFincaLN editarDocsCompraVentaFinca,
         IBuscarCrDireccion1LN buscarDistritoDocsCompraVentaFinca,

         //prestacion de servicios
         IBuscarDocsContratoPrestacionServiciosLN buscarDocsContratoPrestacionServicios,
         IEditarDocsContratoPrestacionServiciosLN editarDocsContratoPrestacionServicios,

         //incripción de vehiculo
         IBuscarDocsInscripcionVehiculoLN buscarDocsInscVehiculo,
         IEditarDocsInscripcionVehiculoLN editarDocsInscVehiculo,
         IListarTipoVehiculoLN listarTipoVehiculoDocsInscVehiculo,
         IListarDocsMarcaVehiculoLN listarMarcaVehiculoDocsInscVehiculo,

         //listar y buscar direcciones
         IBuscarCrDireccion1LN buscarDireccion,
         IListarCrDireccion1LN listarDireccion
         )

        {
            _converter = converter;
            _listarHistorial = listarHistorial;
            _buscarHistorial = buscarHistorial;
            _crearHistorial = crearHistorial;
            _editarHistorial = editarHistorial;
            _eliminarHistorial = eliminarHistorial;

            //Clientes y abogados
            _buscarPersona = buscarPersona;
            _listarAbogados = listarAbogados;
            _listarGePersona = listarGePersona;
            _buscarAbogado = buscarAbogado;


            //Autorizacion y revision de expedientes
            _buscarDocsAutorizacionRevision = buscarDocsAutorizacionRevision;
            _editardocsAutorizacionRevision = editardocsAutorizacionRevision;

            //compra venta de fincas
            _buscarDocsCompraVentaFinca = buscarDocsCompraVentaFinca;
            _editarDocsCompraVentaFinca = editarDocsCompraVentaFinca;

            //prestacion de servicios
            _buscarDocsContratoPrestacionServicios = buscarDocsContratoPrestacionServicios;
            _editarDocsContratoPrestacionServicios = editarDocsContratoPrestacionServicios;

            //incripción de vehiculo
            _buscarDocsInscVehiculo = buscarDocsInscVehiculo;
            _editarDocsInscVehiculo = editarDocsInscVehiculo;
            _listarTipoVehiculoDocsInscVehiculo = listarTipoVehiculoDocsInscVehiculo;
            _listarMarcaVehiculoDocsInscVehiculo = listarMarcaVehiculoDocsInscVehiculo;

            //compra y vent de vehiculos
            _buscarcompraventaV = buscarcompraventaV;
            _listarMarcas = listarMarcaVehiculoDocsInscVehiculo;
            _listarTipos = listarTipoVehiculoDocsInscVehiculo;
            _listarCombustibles = listarCombustibles;

            //poderes judiciales
            _buscarPoderJud = buscarPoderJud;
            _buscarPagare = buscarPagare;

            //listar y buscar direcciones
            _buscarDireccion = buscarDireccion;
            _listarDireccion = listarDireccion;
        }

        [Authorize(Roles = "Abogado")]
        public async Task<IActionResult> DocsHistorial()
        {
            ViewBag.TipoDocumento = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Atorización RE.", Value = "Atorización RE." },
                    new SelectListItem { Text = "CompraVenta Fincas.", Value = "CompraVenta Fincas." },
                    new SelectListItem { Text = "Contrato Prestación Servicios", Value = "Contrato Prestación Servicios" },
                    new SelectListItem { Text = "Inscripción de vehiculo", Value = "Inscripción de vehiculo" },
                    new SelectListItem { Text = "Compra y venta de vehículos", Value = "Compra y venta de vehículos" },
                    new SelectListItem { Text = "Pagaré", Value = "Pagaré" },
                    new SelectListItem { Text = "Poderes especiales judiciales", Value = "Poderes especiales judiciales" }
                };
            return View(await _listarHistorial.listar());
        }

        [HttpGet]
        [Authorize(Roles = "Abogado")]
        public async Task<IActionResult> EditarDesdeHistorial(int id)
        {

            var resultadoHistorial = await _buscarHistorial.Buscar(id);

            if (resultadoHistorial == null)
            {
                return NotFound();
            }

            string tipoDoc = resultadoHistorial.TipoDocumento;

            switch (tipoDoc)
            {
                case "Atorización RE.":
                    var resultadoAutorizacionRevisionE = await _buscarDocsAutorizacionRevision
                                                            .buscar(resultadoHistorial.IdDocumento);

                    if (resultadoAutorizacionRevisionE == null)
                    {
                        return NotFound();
                    }

                    return RedirectToAction("CreateDocsAREDesdeHistorial", resultadoAutorizacionRevisionE);

                case "CompraVenta Fincas.":
                    var resultadoFincas = await _buscarDocsCompraVentaFinca
                                                            .buscar(resultadoHistorial.IdDocumento);

                    if (resultadoFincas == null)
                    {
                        return NotFound();
                    }

                    return RedirectToAction("CreateDocsFincaDesdeHistorial", resultadoFincas);

                case "Contrato Prestación Servicios":
                    var resultadoPrestacionServicios = await _buscarDocsContratoPrestacionServicios
                                                            .buscar(resultadoHistorial.IdDocumento);

                    if (resultadoPrestacionServicios == null)
                    {
                        return NotFound();
                    }

                    return RedirectToAction("CreateDocsCPSDesdeHistorial", resultadoPrestacionServicios);

                case "Inscripción de vehiculo":
                    var resultadoDocsInscVehiculo = await _buscarDocsInscVehiculo
                                                            .buscar(resultadoHistorial.IdDocumento);

                    if (resultadoDocsInscVehiculo == null)
                    {
                        return NotFound();
                    }

                    return RedirectToAction("CreateDocsInscripcionVehiculoDesdeHistorial", resultadoDocsInscVehiculo);

                case "Pagaré":
                    var resultadoPagare = await _buscarPagare.buscar(resultadoHistorial.IdDocumento);

                    if (resultadoPagare == null)
                    {
                        return NotFound();
                    }

                    return RedirectToAction("CreateDocsPGDesdeHitorial", resultadoPagare);

                case "Compra y venta de vehículos":
                    var resultadoCVvehiculo = await _buscarcompraventaV.buscar(resultadoHistorial.IdDocumento);

                    if (resultadoCVvehiculo == null)
                    {
                        return NotFound();
                    }

                    return RedirectToAction("CreateDocsOCVDesdeHistorial", resultadoCVvehiculo);

                case "Poderes especiales judiciales":
                    var resultadoPoderesesjudiciales = await _buscarPoderJud.buscar(resultadoHistorial.IdDocumento);

                    if (resultadoPoderesesjudiciales == null)
                    {
                        return NotFound();
                    }

                    return RedirectToAction("CreateDocsPoderesJudDesdeHistorial", resultadoPoderesesjudiciales);

                default:
                    return BadRequest($"El tipo de documento '{tipoDoc}' no está soportado.");
            }
        }



        [HttpGet]
        [Authorize(Roles = "Abogado")]
        public async Task<IActionResult> CreateDocsAREDesdeHistorial(DocsAutorizacionRevisionExpedienteDTO resultadoAutorizacioRevisionE)
        {
            var cliente = await _buscarPersona.buscar(resultadoAutorizacioRevisionE.CedulaImputado);
            var asistente = await _buscarPersona.buscar(resultadoAutorizacioRevisionE.CedulaAsistente);
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

            ViewBag.CedulaAsistente = asistente.Cedula;
            ViewBag.NombreAsistente = asistente.Nombre;
            ViewBag.Apellido1Cliente = asistente.Apellido1;
            ViewBag.Apellido2Cliente = asistente.Apellido2;

            return View();

        }

        //Metodos Crear, documentos Andy
        [HttpGet]
        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<IActionResult> CreateDocsPGDesdeHitorial(DocsPagareDTO resultadoPagare)
        {
            var deudor = await _buscarPersona.buscar(resultadoPagare.CedulaDeudor);
            var fiador = await _buscarPersona.buscar(resultadoPagare.CedulaFiador);
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

            var distritos = await _listarDireccion.listarDistritos();
            ViewBag.UbicacionFirma = new SelectList(distritos, "IdDistrito", "NombreDistrito");

            var model = new DocsPagareDTO
            {
                CedulaDeudor = deudor?.Cedula ?? 0,
                CedulaFiador = fiador?.Cedula ?? 0,
                CedulaAbogado = abogado?.Cedula ?? 0,
                FechaFirma = DateTime.Today.ToString("yyyy-MM-dd"),
                HoraFirma = DateTime.Now.ToString("HH:mm")
            };

            return View("CreateDocsPGDesdeHitorial", model);
        }

        // Compraventa vehiculo
        [HttpGet]
        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<IActionResult> CreateDocsOCVDesdeHistorial(DocsOpcionCompraventaVehiculoDTO resultadoCVvehiculo)
        {
            var propietario = await _buscarPersona.buscar(resultadoCVvehiculo.CedulaPropietario);
            var comprador = await _buscarPersona.buscar(resultadoCVvehiculo.CedulaComprador);
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

            var distritos = await _listarDireccion.listarDistritos();
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

            return View("CreateDocsOCVDesdeHistorial", model);
        }

        [HttpGet]
        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<IActionResult> CreateDocsPoderesJudDesdeHistorial(DocsPoderesEspecialesJudicialeDTO resultadoPoderesesjudiciales)
        {
            var cliente = await _buscarPersona.buscar(resultadoPoderesesjudiciales.IdCliente);
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

            return View("CreateDocsPoderesJudDesdeHistorial", model);
        }

        [HttpGet]
        [Authorize(Roles = "Abogado")]
        public async Task<IActionResult> CreateDocsFincaDesdeHistorial(DocsCompraventaFincaDTO resultadoFincas)
        {

            var comprador = await _buscarPersona.buscar(resultadoFincas.CedulaComprador);
            var vendedor = await _buscarPersona.buscar(resultadoFincas.CedulaVendedor);
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
                (await _listarDireccion.listarDistritos()),
                "IdDistrito",
                "NombreDistrito"
            );
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Abogado")]
        public async Task<IActionResult> CreateDocsCPSDesdeHistorial(DocsContratoPrestacionServicioDTO resultadoPrestacionServicios)
        {
            var cliente = await _buscarPersona.buscar(resultadoPrestacionServicios.CedulaCliente);
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
                (await _listarDireccion.listarDistritos()),
                "IdDistrito",
                "NombreDistrito"
            );

            ViewBag.Provincia = new SelectList(
                (await _listarDireccion.listarProvincias()),
                "IdProvincia",
                "NombreProvincia"
            );

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Abogado")]
        public async Task<IActionResult> CreateDocsInscripcionVehiculoDesdeHistorial(DocsInscripcionVehiculoDTO resultadoDocsInscVehiculo)
        {
            var cliente = await _buscarPersona.buscar(resultadoDocsInscVehiculo.CedulaCliente);
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

            ViewBag.LugarFirma = new SelectList(
                (await _listarDireccion.listarDistritos()),
                "IdDistrito",
                "NombreDistrito"
            );

            ViewBag.EstiloVehiculo = new SelectList(
                (await _listarTipoVehiculoDocsInscVehiculo.Listar()),
                "Id",
                "Nombre"
            );

            ViewBag.MarcaVehiculo = new SelectList(
                (await _listarMarcaVehiculoDocsInscVehiculo.listar()),
                "Id",
                "Nombre"
            );

            return View();
        }


        [HttpGet]
        [Authorize(Roles = "Abogado")]
        public async Task<IActionResult> PrevisualizarDesdeHistorial(int Id)
        {
            var resultadoHistorial = await _buscarHistorial.Buscar(Id);

            if (resultadoHistorial == null)
            {
                return NotFound();
            }

            string tipoDoc = resultadoHistorial.TipoDocumento;

            switch (tipoDoc)
            {
                case "Atorización RE.":
                    var resultadoAutorizacionRevisionE = await _buscarDocsAutorizacionRevision
                                                            .buscar(resultadoHistorial.IdDocumento);

                    if (resultadoAutorizacionRevisionE == null)
                    {
                        return NotFound();
                    }

                    var resultado = await _buscarDocsAutorizacionRevision.buscar(resultadoHistorial.IdDocumento);

                    string expediente = resultado.Expediente;
                    string delito = resultado.Delito;
                    string cedulaImputado = resultado.CedulaImputado.ToString();
                    string ofendido = resultado.Ofendido;
                    string cedulaAbogado = resultado.CedulaAbogado.ToString();
                    string cedulaAsistente = resultado.CedulaAsistente.ToString();

                    var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DocsMachotes", "AutorizacionExpedienteMachote.html");
                    var htmlTemplate = System.IO.File.ReadAllText(templatePath);

                    // Reemplazar marcadores con los datos del formulario
                    htmlTemplate = htmlTemplate
                        .Replace("{{EXPEDIENTE}}", expediente)
                        .Replace("{{DELITO}}", delito)
                        .Replace("{{CEDULA_IMPUTADO}}", cedulaImputado)
                        .Replace("{{OFENDIDO}}", ofendido)
                        .Replace("{{CEDULA_ABOGADO}}", cedulaAbogado)
                        .Replace("{{CEDULA_ASISTENTE}}", cedulaAsistente);

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

                case "CompraVenta Fincas.":
                    var resultadoFincas = await _buscarDocsCompraVentaFinca
                                                            .buscar(resultadoHistorial.IdDocumento);

                    if (resultadoFincas == null)
                    {
                        return NotFound();
                    }

                    var resultado2 = await _buscarDocsCompraVentaFinca.buscar(resultadoHistorial.IdDocumento);

                    string numeroEscritura = resultado2.NumeroEscritura;
                    cedulaAbogado = resultado2.CedulaAbogado.ToString();
                    string cedulaVendedor = resultado2.CedulaVendedor.ToString();
                    string cedulaComprador = resultado2.CedulaComprador.ToString();
                    string montoVenta = resultado2.MontoVenta.ToString();
                    string partidoFinca = resultado2.PartidoFinca;
                    string matriculaFinca = resultado2.MatriculaFinca;
                    string naturalezaFinca = resultado2.NaturalezaFinca;
                    string distritoFinca = resultado2.DistritoFinca.ToString();
                    string cantonFinca = resultado2.CantonFinca.ToString();
                    string provinciaFinca = resultado2.ProvinciaFinca.ToString();
                    string areaFincaM2 = resultado2.AreaFincaM2.ToString();
                    string planoCatastrado = resultado2.PlanoCatastrado;
                    string colindaNorte = resultado2.ColindaNorte;
                    string colindaSur = resultado2.ColindaSur;
                    string colindaEste = resultado2.ColindaEste;
                    string colindaOeste = resultado2.ColindaOeste;
                    string formaPago = resultado2.FormaPago;
                    string medioPago = resultado2.MedioPago;
                    string origenFondos = resultado2.OrigenFondos;
                    string lugarFirma = resultado2.LugarFirma.ToString();
                    string horaFirma = resultado2.HoraFirma.ToString();
                    string fechaFirma = resultado2.FechaFirma.ToString();

                    var templatePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DocsMachotes", "CompraVentaFincasMachote.html");
                    var htmlTemplate2 = System.IO.File.ReadAllText(templatePath2);

                    var vendedor = await _buscarPersona.buscar(int.Parse(cedulaVendedor));
                    var comprador = await _buscarPersona.buscar(int.Parse(cedulaComprador));
                    var abogado = await _buscarPersona.buscarXcorreo(User.Identity.Name);
                    var prov = await _buscarDireccion.buscarProvincia(int.Parse(provinciaFinca));
                    var cant = await _buscarDireccion.buscarCanton(int.Parse(cantonFinca));
                    var distF = await _buscarDireccion.buscarDistrito(int.Parse(distritoFinca));
                    var dist = await _buscarDireccion.buscarDistrito(int.Parse(lugarFirma));

                    // Reemplazar marcadores con los datos del formulario
                    htmlTemplate = htmlTemplate2
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

                    var doc2 = new HtmlToPdfDocument()
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

                    var pdf2 = _converter.Convert(doc2);

                    return File(pdf2, "application/pdf");

                case "Contrato Prestación Servicios":
                    var resultadoPrestacion = await _buscarDocsContratoPrestacionServicios
                                                            .buscar(resultadoHistorial.IdDocumento);

                    if (resultadoPrestacion == null)
                    {
                        return NotFound();
                    }

                    var resultado3 = await _buscarDocsContratoPrestacionServicios.buscar(resultadoHistorial.IdDocumento);

                    string razonSocialEmpresa = resultado3.RazonSocialEmpresa;
                    string provincia = resultado3.Provincia.ToString();
                    string cedulaJuridicaEmpresa = resultado3.CedulaJuridicaEmpresa;
                    cedulaAbogado = resultado3.CedulaAbogado.ToString();
                    string cedulaCliente = resultado3.CedulaCliente.ToString();
                    string tipoServicios = resultado3.TipoServicios;
                    string fechaInicio = resultado3.FechaInicio.ToString();
                    string fechaFinal = resultado3.FechaFinal.ToString();
                    string montoHonorarios = resultado3.MontoHonorarios.ToString();
                    string informacionConfidencial = resultado3.InformacionConfidencial;
                    string ciudadFirma = resultado3.CiudadFirma.ToString();
                    horaFirma = resultado3.HoraFirma.ToString();
                    fechaFirma = resultado3.FechaFirma.ToString();

                    var templatePath3 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DocsMachotes", "PrestacionServiciosMachote.html");
                    var htmlTemplate3 = System.IO.File.ReadAllText(templatePath3);

                    var cliente = await _buscarPersona.buscar(int.Parse(cedulaCliente));
                    abogado = await _buscarPersona.buscarXcorreo(User.Identity.Name);
                    var prov1 = await _buscarDireccion.buscarProvincia(int.Parse(provincia));
                    var dist1 = await _buscarDireccion.buscarDistrito(int.Parse(ciudadFirma));
                    var fecha = DateTime.Parse(fechaFirma);

                    // Reemplazar marcadores con los datos del formulario
                    htmlTemplate = htmlTemplate3
                        .Replace("{{RAZON_SOCIAL_EMPRESA}}", razonSocialEmpresa)
                        .Replace("{{PROVINCIA}}", prov1.NombreProvincia)
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
                        .Replace("{{CIUDAD_FIRMA}}", dist1.NombreDistrito)
                        .Replace("{{HORA_FIRMA}}", horaFirma)
                        .Replace("{{DIA_FIRMA}}", fecha.Day.ToString())
                        .Replace("{{MES_FIRMA}}", fecha.Month.ToString())
                        .Replace("{{ANIO_FIRMA}}", fecha.Year.ToString());

                    var doc3 = new HtmlToPdfDocument()
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

                    var pdf3 = _converter.Convert(doc3);

                    return File(pdf3, "application/pdf");

                case "Inscripción de vehiculo":
                    var resultadoInscVehiculo = await _buscarDocsInscVehiculo
                                                            .buscar(resultadoHistorial.IdDocumento);

                    if (resultadoInscVehiculo == null)
                    {
                        return NotFound();
                    }

                    var resultado4 = await _buscarDocsInscVehiculo.buscar(resultadoHistorial.IdDocumento);

                    string nombreCliente = resultado4.CedulaClienteNavigation.Nombre;
                    cedulaCliente = resultado4.CedulaCliente.ToString();
                    string estadoCivilCliente = resultado4.CedulaClienteNavigation.Cedula.ToString();
                    string profesionCliente = resultado4.CedulaClienteNavigation.Oficio;
                    string direccionCliente = resultado4.CedulaClienteNavigation.Direccion2;
                    string marca = resultado4.MarcaVehiculo.ToString();
                    string estilo = resultado4.EstiloVehiculo.ToString();
                    string modelo = resultado4.ModeloVehiculo.ToString();
                    string categoria = resultado4.Categoria;
                    string marcaMotor = resultado4.MarcaMotor;
                    string numeroMotor = resultado4.NumeroMotor;
                    string serieChasis = resultado4.NumeroSerieChasis;
                    string vin = resultado4.Vin;
                    string anio = resultado4.Anio.ToString();
                    string carroceria = resultado4.Carroceria;
                    string pesoNeto = resultado4.PesoNeto.ToString();
                    string pesoBruto = resultado4.PesoBruto.ToString();
                    string potencia = resultado4.Potencia.ToString();
                    string color = resultado4.Color;
                    string capacidad = resultado4.Capacidad.ToString();
                    string combustible = resultado4.Combustible;
                    string cilindraje = resultado4.Cilindraje;
                    lugarFirma = resultado4.LugarFirma.ToString();
                    fechaFirma = resultado4.FechaFirma.ToString();
                    cedulaAbogado = resultado4.CedulaAbogadoNavigation.Cedula.ToString();


                    var templatePath4 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DocsMachotes", "InscripcionDeVehiculoMachote.html");
                    var htmlTemplate4 = System.IO.File.ReadAllText(templatePath4);

                    cliente = await _buscarPersona.buscar(int.Parse(cedulaCliente));
                    abogado = await _buscarPersona.buscarXcorreo(User.Identity.Name);
                    var dist2 = await _buscarDireccion.buscarDistrito(int.Parse(lugarFirma));


                    // Reemplazar marcadores con los datos del formulario
                    htmlTemplate4 = htmlTemplate4
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
                        .Replace("{{LUGAR_FIRMA}}", dist2.NombreDistrito)
                        .Replace("{{FECHA_FIRMA}}", fechaFirma)
                        .Replace("{{NOMBRE_NOTARIO}}", abogado.Nombre + " " + abogado.Apellido1 + " " + abogado.Apellido2)
                        .Replace("{{CEDULA_ABOGADO}}", cedulaAbogado);

                    var doc4 = new HtmlToPdfDocument()
                    {
                        GlobalSettings = new GlobalSettings
                        {
                            PaperSize = PaperKind.A4,
                            Orientation = Orientation.Portrait
                        },
                        Objects = {
            new ObjectSettings
            {
                HtmlContent = htmlTemplate4,
                WebSettings = { DefaultEncoding = "utf-8" }
            }
        }
                    };

                    var pdf4 = _converter.Convert(doc4);

                    return File(pdf4, "application/pdf");

                case "Pagaré":
                    var resultadoPagare = await _buscarPagare.buscar(resultadoHistorial.IdDocumento);

                    if (resultadoPagare == null)
                    {
                        return NotFound();
                    }

                    var resultado5 = await _buscarPagare.buscar(resultadoHistorial.IdDocumento);


                    string idDocumento = resultado5.IdDocumento.ToString();
                    string montoNumerico = resultado5.MontoNumerico.ToString();
                    string cedulaDeudor = resultado5.CedulaDeudor.ToString();
                    string sociedadDeudor = resultado5.SociedadDeudor.ToString();
                    string cedulaJuridicaSociedad = resultado5.CedulaJuridicaSociedad;
                    string acreedorNombre = resultado5.AcreedorNombre;
                    string cedulaJuridicaAcreedor = resultado5.CedulaJuridicaAcreedor;
                    string acreedorDomicilio = resultado5.AcreedorDomicilio;
                    string? fechaFirmaNuevo = resultado5.FechaFirma.ToString();
                    string? horaFirmaNuevo = resultado5.HoraFirma.ToString();
                    string fechaVencimiento = resultado5.FechaVencimiento.ToString();
                    string interesFormula = resultado5.InteresFormula;
                    string interesTasaActual = resultado5.InteresTasaActual.ToString();
                    string interesBase = resultado5.InteresBase;
                    string lugarPago = resultado5.LugarPago.ToString();
                    string cedulaFiador = resultado5.CedulaFiador.ToString();
                    string ubicacionFirma = resultado5.UbicacionFirma.ToString();
                    string TipoSociedad = resultado5.TipoSociedad;
                    string UbicacionSociedad = resultado5.UbicacionSociedad;

                    var templatePath5 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DocsMachotes", "DocsPagare.html");
                    var htmlTemplate5 = System.IO.File.ReadAllText(templatePath5);

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

                    var ahora = DateTime.Now;
                    var fechaFirmaStr = !string.IsNullOrWhiteSpace(fechaFirmaNuevo)
                        ? fechaFirmaNuevo
                        : ahora.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                    var horaFirmaStr = !string.IsNullOrWhiteSpace(horaFirmaNuevo)
                        ? (DateTime.TryParse(horaFirmaNuevo, out var dt) ? dt.ToString("HH:mm")
                            : (TimeSpan.TryParse(horaFirmaNuevo, out var ts) ? ahora.Date.Add(ts).ToString("HH:mm") : horaFirmaNuevo))
                        : ahora.ToString("HH:mm");

                    string lugarPagoMostrar = resultadoPagare.LugarPago.ToString() ?? "";
                    var lugarPagos = await _buscarDireccion.buscarDistrito(int.Parse(lugarPagoMostrar));

                    string UbicacionFirmaMostrar = resultadoPagare.UbicacionFirma.ToString() ?? "";
                    var UbicacionFirma = await _buscarDireccion.buscarDistrito(int.Parse(UbicacionFirmaMostrar));

                    var abogadopagare = await _buscarPersona.buscarXcorreo(User.Identity?.Name ?? "");
                    var cedulaAbogadopagare = (abogadopagare?.Cedula ?? 0).ToString();

                    htmlTemplate5 = htmlTemplate5
                        .Replace("{{LOGO}}", logoBase64)
                        .Replace("{{NombreBufete}}", "")
                        .Replace("{{CedulaJuridica}}", "")
                        .Replace("{{TelefonoDespacho}}", "")
                        .Replace("{{EmailDespacho}}", "")

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
                        .Replace("{{UBICACION_FIRMA}}", UbicacionFirma.NombreDistrito)
                        .Replace("{{HORA_FIRMA_NUEVO}}", horaFirmaStr)
                        .Replace("{{FECHA_FIRMA_NUEVO}}", fechaFirmaStr)
                        .Replace("{{HORA_FIRMA_LETRAS}}", horaFirmaStr)
                        .Replace("{{FECHA_FIRMA_LETRAS}}", fechaFirmaStr)
                        .Replace("{{CEDULA_ABOGADO}}", cedulaAbogadopagare);

                    var doc5 = new HtmlToPdfDocument()
                    {
                        GlobalSettings = new GlobalSettings
                        {
                            PaperSize = PaperKind.A4,
                            Orientation = Orientation.Portrait
                        },
                        Objects = {
             new ObjectSettings
             {
                 HtmlContent = htmlTemplate5,
                 WebSettings = { DefaultEncoding = "utf-8" }
             }
         }
                    };

                    var pdf5 = _converter.Convert(doc5);

                    return File(pdf5, "application/pdf");

                case "Compra y venta de vehículos":
                    {
                        var resultadoCV = await _buscarcompraventaV.buscar(resultadoHistorial.IdDocumento);
                        if (resultadoCV == null) return NotFound();

                        var templatePathCV = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DocsMachotes", "OpcionCompraVentaVehiculos.html");
                        var htmlCV = System.IO.File.ReadAllText(templatePathCV);

                        string logoBase64CV = "";
                        var logoPathCV = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "img", "PreaceptaLogoColorNegro.png");
                        if (System.IO.File.Exists(logoPathCV))
                            logoBase64CV = Convert.ToBase64String(System.IO.File.ReadAllBytes(logoPathCV));

                        string idDocumentoCV = resultadoCV.IdDocumento.ToString();
                        string numeroEscrituraCV = resultadoCV.NumeroEscritura ?? "";
                        string cedulaAbogadoStr = resultadoCV.CedulaAbogado.ToString();
                        string cedulaPropStr = resultadoCV.CedulaPropietario.ToString();
                        string cedulaCompStr = resultadoCV.CedulaComprador.ToString();
                        string idMarcaVehStr = resultadoCV.MarcaVehiculo.ToString();
                        string idTipoVehStr = resultadoCV.TipoVehiculo.ToString();
                        string idMarcaMotorStr = resultadoCV.MarcaMotor.ToString();
                        string idCombStr = resultadoCV.Combustible.ToString();
                        string placaVehiculo = resultadoCV.PlacaVehiculo ?? "";
                        string modeloVehiculo = resultadoCV.ModeloVehiculo ?? "";
                        carroceria = resultadoCV.Carroceria ?? "";
                        categoria = resultadoCV.Categoria ?? "";
                        numeroMotor = resultadoCV.NumeroMotor ?? "";
                        string chasis = resultadoCV.Chasis ?? "";
                        string serie = resultadoCV.Serie ?? "";
                        vin = resultadoCV.Vin ?? "";
                        color = resultadoCV.Color ?? "";
                        anio = resultadoCV.Anio.ToString();
                        capacidad = resultadoCV.Capacidad?.ToString() ?? "";
                        cilindraje = resultadoCV.Cilindraje ?? "";
                        string precio = resultadoCV.Precio.ToString();
                        string monedaPrecio = resultadoCV.MonedaPrecio ?? "";
                        string plazoOpcionAnios = resultadoCV.PlazoOpcionAnios.ToString();
                        string montoSenal = resultadoCV.MontoSenal.ToString();
                        string monedaSenal = resultadoCV.MonedaSenal ?? "";
                        string montoADevolver = resultadoCV.MontoADevolver.ToString();
                        string montoAPerder = resultadoCV.MontoAPerder.ToString();
                        string monedaMontoPerd = resultadoCV.MonedaMontoPerdido ?? "";

                        _ = int.TryParse(cedulaAbogadoStr, out var cedAbogado);
                        _ = int.TryParse(cedulaPropStr, out var cedProp);
                        _ = int.TryParse(cedulaCompStr, out var cedComp);

                        var ab = cedAbogado > 0 ? await _buscarPersona.buscar(cedAbogado) : null;
                        var prop = cedProp > 0 ? await _buscarPersona.buscar(cedProp) : null;
                        var comp = cedComp > 0 ? await _buscarPersona.buscar(cedComp) : null;

                        string nombreNotario = ab != null ? $"{ab.Nombre} {ab.Apellido1} {(ab.Apellido2 ?? "")}".Trim() : cedulaAbogadoStr;
                        string cedulaNotario = ab?.Cedula.ToString() ?? cedulaAbogadoStr;

                        string nombreVendedor = prop != null ? $"{prop.Nombre} {prop.Apellido1} {(prop.Apellido2 ?? "")}".Trim() : cedulaPropStr;
                        cedulaVendedor = prop?.Cedula.ToString() ?? cedulaPropStr;

                        string nombreComprador = comp != null ? $"{comp.Nombre} {comp.Apellido1} {(comp.Apellido2 ?? "")}".Trim() : cedulaCompStr;
                        cedulaComprador = comp?.Cedula.ToString() ?? cedulaCompStr;

                        _ = int.TryParse(idMarcaVehStr, out var idMarcaVeh);
                        _ = int.TryParse(idTipoVehStr, out var idTipoVeh);
                        _ = int.TryParse(idMarcaMotorStr, out var idMarcaMotor);
                        _ = int.TryParse(idCombStr, out var idComb);

                        string marcaVehiculoNombre = idMarcaVehStr;
                        string tipoVehiculoNombre = idTipoVehStr;
                        string marcaMotorNombre = idMarcaMotorStr;
                        string combustibleNombre = idCombStr;

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

                        string fechaInicioMostrar =
                            DateTime.TryParse(resultadoCV.FechaInicio, out var fIni)
                                ? fIni.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                                : "";

                        string fechaFirmaMostrar =
                            DateTime.TryParse(resultadoCV.FechaFirma, out var fFirma)
                                ? fFirma.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                                : DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                        string horaFirmaMostrar = !string.IsNullOrWhiteSpace(resultadoCV.HoraFirma)
                            ? (DateTime.TryParse(resultadoCV.HoraFirma, out var dtHora) ? dtHora.ToString("hh:mm tt", CultureInfo.InvariantCulture)
                               : (TimeSpan.TryParse(resultadoCV.HoraFirma, out var tsHora) ? DateTime.Today.Add(tsHora).ToString("hh:mm tt", CultureInfo.InvariantCulture)
                               : resultadoCV.HoraFirma))
                            : DateTime.Now.ToString("hh:mm tt", CultureInfo.InvariantCulture);

                        string lugarFirmaNombre = "";
                        string lugarFirmaIdStr = resultadoCV.LugarFirma.ToString();
                        if (int.TryParse(lugarFirmaIdStr, out var idLugarFirma))
                        {
                            var lugar = await _buscarDireccion.buscarDistrito(idLugarFirma);
                            lugarFirmaNombre = lugar?.NombreDistrito ?? "";
                        }

                        var gastosRaw = (resultadoCV.GastosTraspasoPagadosPor ?? "").Trim().ToUpperInvariant();
                        string gastosTexto = gastosRaw switch
                        {
                            "COMPRADOR" => nombreComprador,
                            "VENDEDOR" => nombreVendedor,
                            _ => string.IsNullOrWhiteSpace(gastosRaw) ? nombreComprador : gastosRaw
                        };

                        htmlCV = htmlCV
                            .Replace("{{LOGO}}", logoBase64CV)
                            .Replace("{{NombreBufete}}", "")
                            .Replace("{{CedulaJuridica}}", "")
                            .Replace("{{TelefonoDespacho}}", "")
                            .Replace("{{EmailDespacho}}", "")

                            .Replace("{{ID_DOCUMENTO}}", idDocumentoCV)
                            .Replace("{{NUMERO_ESCRITURA}}", numeroEscrituraCV)
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

                            .Replace("{{PLACA_VEHICULO}}", placaVehiculo)
                            .Replace("{{MARCA_VEHICULO}}", marcaVehiculoNombre)
                            .Replace("{{TIPO_VEHICULO}}", tipoVehiculoNombre)
                            .Replace("{{MODELO_VEHICULO}}", modeloVehiculo)
                            .Replace("{{CARROCERIA}}", carroceria)
                            .Replace("{{CATEGORIA}}", categoria)
                            .Replace("{{CHASIS}}", chasis)
                            .Replace("{{SERIE}}", serie)
                            .Replace("{{VIN}}", vin)
                            .Replace("{{MARCA_MOTOR}}", marcaMotorNombre)
                            .Replace("{{NUMERO_MOTOR}}", numeroMotor)
                            .Replace("{{COLOR}}", color)
                            .Replace("{{COMBUSTIBLE}}", combustibleNombre)
                            .Replace("{{ANIO}}", anio)
                            .Replace("{{CAPACIDAD}}", capacidad)
                            .Replace("{{CILINDRAJE}}", cilindraje)
                            .Replace("{{PRECIO}}", precio)
                            .Replace("{{MONEDA_PRECIO}}", monedaPrecio)
                            .Replace("{{PLAZO_OPCION_ANIOS}}", plazoOpcionAnios)
                            .Replace("{{FECHA_INICIO}}", fechaInicioMostrar)
                            .Replace("{{MONTO_SENAL}}", montoSenal)
                            .Replace("{{MONEDA_SENAL}}", monedaSenal)
                            .Replace("{{MONTO_A_DEVOLVER}}", montoADevolver)
                            .Replace("{{MONTO_A_PERDER}}", montoAPerder)
                            .Replace("{{MONEDA_MONTO_PERDIDO}}", monedaMontoPerd)
                            .Replace("{{GASTOS_TRASPASO_PAGADOS_POR}}", gastosTexto)
                            .Replace("{{LUGAR_FIRMA}}", lugarFirmaNombre)
                            .Replace("{{HORA_FIRMA}}", horaFirmaMostrar)
                            .Replace("{{FECHA_FIRMA}}", fechaFirmaMostrar);

                        var docCV = new HtmlToPdfDocument
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
                                HtmlContent = htmlCV,
                                WebSettings = { DefaultEncoding = "utf-8" }
                            }
                        }
                                                };

                        var pdfCV = _converter.Convert(docCV);
                        return File(pdfCV, "application/pdf");
                    }

                case "Poderes especiales judiciales":
                    var resultadoPoderesesjudiciales = await _buscarPoderJud.buscar(resultadoHistorial.IdDocumento);

                    if (resultadoPoderesesjudiciales == null)
                    {
                        return NotFound();
                    }

                    string idDoc = resultadoPoderesesjudiciales.IdDoc.ToString();
                    string fechaA = resultadoPoderesesjudiciales.Fecha.ToString();
                    string idAbogado = resultadoPoderesesjudiciales.IdAbogado.ToString();
                    string idCliente = resultadoPoderesesjudiciales.IdCliente.ToString();
                    string texto = resultadoPoderesesjudiciales.Texto;
                    string NumCausa = resultadoPoderesesjudiciales.NumCausa;
                    {
                        var templatePath7 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DocsMachotes", "PoderesEspecialesJudiciales.html");
                        var htmlTemplate7 = System.IO.File.ReadAllText(templatePath7);

                        string logoBase64pj = "";
                        string nombreBufete = "";
                        string cedJuridica = "";
                        string telDespacho = "";
                        string emailDesp = "";

                        var logoPath7 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "img", "PreaceptaLogoColorNegro.png");
                        if (System.IO.File.Exists(logoPath7))
                            logoBase64pj = Convert.ToBase64String(System.IO.File.ReadAllBytes(logoPath7));

                        _ = int.TryParse(idAbogado, out var cedAbogado);
                        _ = int.TryParse(idCliente, out var cedCliente);

                        var personaAbogado = await _buscarPersona.buscar(cedAbogado);
                        var personaCliente = await _buscarPersona.buscar(cedCliente);

                        string nombreAbogado = personaAbogado != null
                            ? $"{personaAbogado.Nombre} {personaAbogado.Apellido1} {(personaAbogado.Apellido2 ?? "")}".Trim()
                            : idAbogado;

                        nombreCliente = personaCliente != null
                            ? $"{personaCliente.Nombre} {personaCliente.Apellido1} {(personaCliente.Apellido2 ?? "")}".Trim()
                            : idCliente;

                        string cedulaAbogadoStr = personaAbogado?.Cedula.ToString() ?? idAbogado;
                        string cedulaClienteStr = personaCliente?.Cedula.ToString() ?? idCliente;

                        var abogadoDetalle = (cedAbogado > 0) ? await _buscarAbogado.buscar(cedAbogado) : null;
                        string carnetProfesional = abogadoDetalle?.Carnet.ToString() ?? "";

                        string fechaMostrar = DateTime.TryParse(fechaA, out var f)
                            ? f.ToString("dd/MM/yyyy")
                            : DateTime.Today.ToString("dd/MM/yyyy");

                        string lugarDocumento = new[] {
        personaAbogado?.Direccion2,
        personaCliente?.Direccion2
    }.FirstOrDefault(s => !string.IsNullOrWhiteSpace(s)) ?? "San José";

                        htmlTemplate7 = htmlTemplate7
                            .Replace("{{LOGO}}", logoBase64pj)
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

                        var docPdf7 = new HtmlToPdfDocument
                        {
                            GlobalSettings = new GlobalSettings { PaperSize = PaperKind.A4, Orientation = Orientation.Portrait },
                            Objects = { new ObjectSettings { HtmlContent = htmlTemplate7, WebSettings = { DefaultEncoding = "utf-8" } } }
                        };

                        var pdf7 = _converter.Convert(docPdf7);
                        return File(pdf7, "application/pdf");
                    }

                default:
                    return BadRequest($"El tipo de documento '{tipoDoc}' no está soportado.");

            }
        }

        // GET: THistorialDocumento1
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Index()
        {
            return View(await _listarHistorial.listar());
        }

        // GET: THistorialDocumento1/Details/5
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tHistorialDocumento = await _buscarHistorial.Buscar(id);
            if (tHistorialDocumento == null)
            {
                return NotFound();
            }

            return View(tHistorialDocumento);
        }

        // GET: THistorialDocumento1/Create

        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Create()
        {
            ViewData["Abogado"] = (await _listarAbogados.listar())
                .Select(n => new SelectListItem
                {
                    Value = n.Cedula.ToString(),
                    Text = $"{n.Carnet} - Abogado(a) {n.CedulaNavigation.Nombre}"
                })
    .ToList();

            ViewData["Cliente"] = (await _listarGePersona.listar())
               .Select(n => new SelectListItem
               {
                   Value = n.Cedula.ToString(),
                   Text = $"{n.Cedula} - {n.Nombre} {n.Apellido1} {n.Apellido2}"
               })
               .ToList();
            return View();
        }


        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tHistorialDocumento = await _buscarHistorial.Buscar(id);

            if (tHistorialDocumento == null)
            {
                return NotFound();
            }
            ViewData["Abogado"] = new SelectList(_listarGePersona.listar().Result, "Cedula", "Cedula", tHistorialDocumento.Abogado);
            ViewData["Cliente"] = new SelectList(_listarGePersona.listar().Result, "Cedula", "Apellido1", tHistorialDocumento.Cliente);
            return View(tHistorialDocumento);
        }


        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tHistorialDocumento = await _buscarHistorial.Buscar(id);
            if (tHistorialDocumento == null)
            {
                return NotFound();
            }

            return View(tHistorialDocumento);
        }

        // POST: THistorialDocumento1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID no válido.");
            }
            try
            {
                await _eliminarHistorial.Eliminar(id);
                return RedirectToAction(nameof(Index));
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Error de referencia nula: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Otro error: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Tutorial()
        {
            return View();
        }
    }
}