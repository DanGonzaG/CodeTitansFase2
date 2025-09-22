using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Preacepta.AD;
using Preacepta.LN.CrDireccion1.BuscarXid;
using Preacepta.LN.CrDireccion1.Listar;
using Preacepta.LN.DocsAutorizacionRevisionExpediente.BuscarXid;
using Preacepta.LN.DocsAutorizacionRevisionExpediente.Editar;
using Preacepta.LN.DocsCompraventaFinca.BuscarXid;
using Preacepta.LN.DocsCompraventaFinca.Editar;
using Preacepta.LN.DocsContratoPrestacionServicios.BuscarXid;
using Preacepta.LN.DocsContratoPrestacionServicios.Editar;
using Preacepta.LN.DocsInscripcionVehiculo.BuscarXid;
using Preacepta.LN.DocsInscripcionVehiculo.Editar;
using Preacepta.LN.DocsMarcaVehiculo.Listar;
using Preacepta.LN.DocsTipoVehiculo.Listar;
using Preacepta.LN.GeAbogado.Listar;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.LN.GePersona.Listar;
using Preacepta.LN.HistorialDocumentos.BuscarXid;
using Preacepta.LN.HistorialDocumentos.Crear;
using Preacepta.LN.HistorialDocumentos.Editar;
using Preacepta.LN.HistorialDocumentos.Eliminar;
using Preacepta.LN.HistorialDocumentos.Listar;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.UI.Controllers
{
    public class THistorialDocumento1Controller : Controller
    {
        private readonly Contexto _context;
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

        //Autorizacion y revision de expedientes, insertar aqui las de los demas documentos
        private readonly IBuscarDocsAutorizacionRevisionExpedienteLN _buscarDocsAutorizacionRevision;
        private readonly IEditarDocsAutorizacionRevisionExpedienteLN _editardocsAutorizacionRevision;

        //compra venta de fincas
        private readonly IBuscarDocsCompraventaFincaLN _buscarDocsCompraVentaFinca;
        private readonly IEditarDocsCompraventaFincaLN _editarDocsCompraVentaFinca;
        private readonly IBuscarCrDireccion1LN _buscarDistritoDocsCompraVentaFinca;
        private readonly IListarCrDireccion1LN _listarDistritoDocsCompraVentaFinca;

        //prestacion de servicios
        private readonly IBuscarDocsContratoPrestacionServiciosLN _buscarDocsContratoPrestacionServicios;
        private readonly IEditarDocsContratoPrestacionServiciosLN _editarDocsContratoPrestacionServicios;
        private readonly IBuscarCrDireccion1LN _buscarDistrito;


        //incripción de vehiculo
        private readonly IBuscarDocsInscripcionVehiculoLN _buscarDocsInscVehiculo;
        private readonly IEditarDocsInscripcionVehiculoLN _editarDocsInscVehiculo;
        private readonly IListarTipoVehiculoLN _listarTipoVehiculoDocsInscVehiculo;
        private readonly IListarDocsMarcaVehiculoLN _listarMarcaVehiculoDocsInscVehiculo;

        public THistorialDocumento1Controller(Contexto context,
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

         //Autorizacion y revision de expedientes
         IBuscarDocsAutorizacionRevisionExpedienteLN buscarDocsAutorizacionRevision,
         IEditarDocsAutorizacionRevisionExpedienteLN editardocsAutorizacionRevision,

         //compra venta de fincas
         IBuscarDocsCompraventaFincaLN buscarDocsCompraVentaFinca,
         IEditarDocsCompraventaFincaLN editarDocsCompraVentaFinca,
         IBuscarCrDireccion1LN buscarDistritoDocsCompraVentaFinca,
         IListarCrDireccion1LN listarDistritoDocsCompraVentaFinca,

         //prestacion de servicios
         IBuscarDocsContratoPrestacionServiciosLN buscarDocsContratoPrestacionServicios,
         IEditarDocsContratoPrestacionServiciosLN editarDocsContratoPrestacionServicios,
         IBuscarCrDireccion1LN buscarDistrito,

         //incripción de vehiculo
         IBuscarDocsInscripcionVehiculoLN buscarDocsInscVehiculo,
         IEditarDocsInscripcionVehiculoLN editarDocsInscVehiculo,
         IListarTipoVehiculoLN listarTipoVehiculoDocsInscVehiculo,
         IListarDocsMarcaVehiculoLN listarMarcaVehiculoDocsInscVehiculo
         )

        {
            _context = context;
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

            //Autorizacion y revision de expedientes
            _buscarDocsAutorizacionRevision = buscarDocsAutorizacionRevision;
            _editardocsAutorizacionRevision = editardocsAutorizacionRevision;

            //compra venta de fincas
            _buscarDocsCompraVentaFinca = buscarDocsCompraVentaFinca;
            _editarDocsCompraVentaFinca = editarDocsCompraVentaFinca;
            _buscarDistritoDocsCompraVentaFinca = buscarDistritoDocsCompraVentaFinca;
            _listarDistritoDocsCompraVentaFinca = listarDistritoDocsCompraVentaFinca;

            //prestacion de servicios
            _buscarDocsContratoPrestacionServicios = buscarDocsContratoPrestacionServicios;
            _editarDocsContratoPrestacionServicios = editarDocsContratoPrestacionServicios;
            _buscarDistrito = buscarDistrito;

            //incripción de vehiculo
            _buscarDocsInscVehiculo = buscarDocsInscVehiculo;
            _editarDocsInscVehiculo = editarDocsInscVehiculo;
            _listarTipoVehiculoDocsInscVehiculo = listarTipoVehiculoDocsInscVehiculo;
            _listarMarcaVehiculoDocsInscVehiculo = listarMarcaVehiculoDocsInscVehiculo;
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
                (await _listarDistritoDocsCompraVentaFinca.listarDistritos()),
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
                (await _listarDistritoDocsCompraVentaFinca.listarDistritos()),
                "IdDistrito",
                "NombreDistrito"
            );

            ViewBag.Provincia = new SelectList(
                (await _listarDistritoDocsCompraVentaFinca.listarProvincias()),
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
                (await _listarDistritoDocsCompraVentaFinca.listarDistritos()),
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
                    var prov = await _buscarDistrito.buscarProvincia(int.Parse(provinciaFinca));
                    var cant = await _buscarDistrito.buscarCanton(int.Parse(cantonFinca));
                    var distF = await _buscarDistrito.buscarDistrito(int.Parse(distritoFinca));
                    var dist = await _buscarDistrito.buscarDistrito(int.Parse(lugarFirma));

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
                    var prov1 = await _buscarDistrito.buscarProvincia(int.Parse(provincia));
                    var dist1 = await _buscarDistrito.buscarDistrito(int.Parse(ciudadFirma));
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
                    var dist2 = await _buscarDistrito.buscarDistrito(int.Parse(lugarFirma));


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

        // GET: THistorialDocumento1/Edit/5
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tHistorialDocumento = await _context.THistorialDocumentos.FindAsync(id);
            if (tHistorialDocumento == null)
            {
                return NotFound();
            }
            ViewData["Abogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tHistorialDocumento.Abogado);
            ViewData["Cliente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tHistorialDocumento.Cliente);
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
    }
}
