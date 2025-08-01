using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.AD.Casos.BuscarXid;
using Preacepta.AD.Casos.Crear;
using Preacepta.AD.Casos.Editar;
using Preacepta.AD.Casos.Eliminar;
using Preacepta.AD.Casos.Listar;
using Preacepta.AD.CasosEtapa.BuscarXid;
using Preacepta.AD.CasosEtapa.Crear;
using Preacepta.AD.CasosEtapa.Editar;
using Preacepta.AD.CasosEtapa.Eliminar;
using Preacepta.AD.CasosEtapa.Listar;
using Preacepta.AD.CasosEvidencia.BuscarXid;
using Preacepta.AD.CasosEvidencia.Crear;
using Preacepta.AD.CasosEvidencia.Editar;
using Preacepta.AD.CasosEvidencia.Eliminar;
using Preacepta.AD.CasosEvidencia.Listar;
using Preacepta.AD.CasosTipo.BuscarXid;
using Preacepta.AD.CasosTipo.Crear;
using Preacepta.AD.CasosTipo.Editar;
using Preacepta.AD.CasosTipo.Eliminar;
using Preacepta.AD.CasosTipo.Listar;
using Preacepta.AD.CrDireccion1.BuscarXid;
using Preacepta.AD.CrDireccion1.Crear;
using Preacepta.AD.CrDireccion1.Editar;
using Preacepta.AD.CrDireccion1.Eliminar;
using Preacepta.AD.CrDireccion1.Listar;
using Preacepta.AD.DocPoderesEspecialesJudiciales.Buscar;
using Preacepta.AD.DocPoderesEspecialesJudiciales.Crear;
using Preacepta.AD.DocPoderesEspecialesJudiciales.Editar;
using Preacepta.AD.DocPoderesEspecialesJudiciales.Eliminar;
using Preacepta.AD.DocPoderesEspecialesJudiciales.Listar;
using Preacepta.AD.DocsOpcionCompraventaVehiculo.Buscar;
using Preacepta.AD.DocsOpcionCompraventaVehiculo.Crear;
using Preacepta.AD.DocsOpcionCompraventaVehiculo.Editar;
using Preacepta.AD.DocsOpcionCompraventaVehiculo.Eliminar;
using Preacepta.AD.DocsOpcionCompraventaVehiculo.Listar;
using Preacepta.AD.DocsPagare.Buscar;
using Preacepta.AD.DocsPagare.Crear;
using Preacepta.AD.DocsPagare.Editar;
using Preacepta.AD.DocsPagare.Eliminar;
using Preacepta.AD.DocsPagare.Listar;
using Preacepta.AD.DocsTipoVehiculo.Buscar;
using Preacepta.AD.DocsTipoVehiculo.Crear;
using Preacepta.AD.DocsTipoVehiculo.Editar;
using Preacepta.AD.DocsTipoVehiculo.Eliminar;
using Preacepta.AD.DocsTipoVehiculo.Listar;
using Preacepta.AD.DocsAutorizacionRevisionExpediente.BuscarXid;
using Preacepta.AD.DocsAutorizacionRevisionExpediente.Crear;
using Preacepta.AD.DocsAutorizacionRevisionExpediente.Editar;
using Preacepta.AD.DocsAutorizacionRevisionExpediente.Eliminar;
using Preacepta.AD.DocsAutorizacionRevisionExpediente.Listar;
using Preacepta.AD.DocsCombustible.BuscarXid;
using Preacepta.AD.DocsCombustible.Crear;
using Preacepta.AD.DocsCombustible.Editar;
using Preacepta.AD.DocsCombustible.Eliminar;
using Preacepta.AD.DocsCombustible.Listar;
using Preacepta.AD.DocsCompraventaFinca.BuscarXid;
using Preacepta.AD.DocsCompraventaFinca.Crear;
using Preacepta.AD.DocsCompraventaFinca.Editar;
using Preacepta.AD.DocsCompraventaFinca.Eliminar;
using Preacepta.AD.DocsCompraventaFinca.Listar;
using Preacepta.AD.DocsContratoPrestacionServicios.BuscarXid;
using Preacepta.AD.DocsContratoPrestacionServicios.Crear;
using Preacepta.AD.DocsContratoPrestacionServicios.Editar;
using Preacepta.AD.DocsContratoPrestacionServicios.Eliminar;
using Preacepta.AD.DocsContratoPrestacionServicios.Listar;
using Preacepta.AD.DocsInscripcionVehiculo.BuscarXid;
using Preacepta.AD.DocsInscripcionVehiculo.Crear;
using Preacepta.AD.DocsInscripcionVehiculo.Editar;
using Preacepta.AD.DocsInscripcionVehiculo.Eliminar;
using Preacepta.AD.DocsInscripcionVehiculo.Listar;
using Preacepta.AD.DocsMarcaVehiculo.BuscarXid;
using Preacepta.AD.DocsMarcaVehiculo.Crear;
using Preacepta.AD.DocsMarcaVehiculo.Editar;
using Preacepta.AD.DocsMarcaVehiculo.Eliminar;
using Preacepta.AD.DocsMarcaVehiculo.Listar;
using Preacepta.AD.GeAbogado.BuscarXid;
using Preacepta.AD.GeAbogado.Crear;
using Preacepta.AD.GeAbogado.Editar;
using Preacepta.AD.GeAbogado.Eliminar;
using Preacepta.AD.GeAbogado.Listar;
using Preacepta.AD.GeAbogadoTipo.BuscarXid;
using Preacepta.AD.GeAbogadoTipo.Crear;
using Preacepta.AD.GeAbogadoTipo.Editar;
using Preacepta.AD.GeAbogadoTipo.Eliminar;
using Preacepta.AD.GeAbogadoTipo.Listar;
using Preacepta.AD.GeNegocio.BuscarXId;
using Preacepta.AD.GeNegocio.Crear;
using Preacepta.AD.GeNegocio.Editar;
using Preacepta.AD.GeNegocio.Eliminar;
using Preacepta.AD.GeNegocio.Listar;
using Preacepta.AD.GePersona.BuscarXid;
using Preacepta.AD.GePersona.Crear;
using Preacepta.AD.GePersona.Editar;
using Preacepta.AD.GePersona.Eliminar;
using Preacepta.AD.GePersona.Listar;
using Preacepta.AD.Testimonios.Buscar;
using Preacepta.AD.Testimonios.Crear;
using Preacepta.AD.Testimonios.Editar;
using Preacepta.AD.Testimonios.Eliminar;
using Preacepta.AD.Testimonios.Listar;
using Preacepta.AD.GeRedesSociales.BuscarXid;
using Preacepta.AD.GeRedesSociales.Crear;
using Preacepta.AD.GeRedesSociales.Editar;
using Preacepta.AD.GeRedesSociales.Eliminar;
using Preacepta.AD.GeRedesSociales.Listar;
using Preacepta.LN.Casos.BuscarXid;
using Preacepta.LN.Casos.Crear;
using Preacepta.LN.Casos.Editar;
using Preacepta.LN.Casos.Eliminar;
using Preacepta.LN.Casos.Listar;
using Preacepta.LN.Casos.ObtenerDatos;
using Preacepta.LN.CasosEtapa.BuscarXid;
using Preacepta.LN.CasosEtapa.Crear;
using Preacepta.LN.CasosEtapa.Editar;
using Preacepta.LN.CasosEtapa.Eliminar;
using Preacepta.LN.CasosEtapa.Listar;
using Preacepta.LN.CasosEtapa.ObtenerDatos;
using Preacepta.LN.CasosEvidencia.BuscarXid;
using Preacepta.LN.CasosEvidencia.Crear;
using Preacepta.LN.CasosEvidencia.Editar;
using Preacepta.LN.CasosEvidencia.Eliminar;
using Preacepta.LN.CasosEvidencia.Listar;
using Preacepta.LN.CasosEvidencia.ObtenerDatos;
using Preacepta.LN.CasosTipo.BuscarXid;
using Preacepta.LN.CasosTipo.Crear;
using Preacepta.LN.CasosTipo.Editar;
using Preacepta.LN.CasosTipo.Eliminar;
using Preacepta.LN.CasosTipo.Listar;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.CrDireccion1.BuscarXid;
using Preacepta.LN.CrDireccion1.Crear;
using Preacepta.LN.CrDireccion1.Editar;
using Preacepta.LN.CrDireccion1.Eliminar;
using Preacepta.LN.CrDireccion1.Listar;
using Preacepta.LN.CrDireccion1.ObtenerDatos;
using Preacepta.LN.DocPoderesEspecialesJudiciales.Buscar;
using Preacepta.LN.DocPoderesEspecialesJudiciales.Crear;
using Preacepta.LN.DocPoderesEspecialesJudiciales.Editar;
using Preacepta.LN.DocPoderesEspecialesJudiciales.Eliminar;
using Preacepta.LN.DocPoderesEspecialesJudiciales.Listar;
using Preacepta.LN.DocPoderesEspecialesJudiciales.ObtenerDatos;
using Preacepta.LN.DocsOpcionCompraventaVehiculo.Buscar;
using Preacepta.LN.DocsOpcionCompraventaVehiculo.Crear;
using Preacepta.LN.DocsOpcionCompraventaVehiculo.Editar;
using Preacepta.LN.DocsOpcionCompraventaVehiculo.Eliminar;
using Preacepta.LN.DocsOpcionCompraventaVehiculo.Listar;
using Preacepta.LN.DocsOpcionCompraventaVehiculo.ObtenerDatos;
using Preacepta.LN.DocsPagare.Buscar;
using Preacepta.LN.DocsPagare.Crear;
using Preacepta.LN.DocsPagare.Editar;
using Preacepta.LN.DocsPagare.Eliminar;
using Preacepta.LN.DocsPagare.Listar;
using Preacepta.LN.DocsPagare.ObtenerDatos;
using Preacepta.LN.DocsTipoVehiculo.Buscar;
using Preacepta.LN.DocsTipoVehiculo.Crear;
using Preacepta.LN.DocsTipoVehiculo.Editar;
using Preacepta.LN.DocsTipoVehiculo.Eliminar;
using Preacepta.LN.DocsTipoVehiculo.Listar;
using Preacepta.LN.DocsTipoVehiculo.ObtenerDatos;
using Preacepta.LN.DocsAutorizacionRevisionExpediente.BuscarXid;
using Preacepta.LN.DocsAutorizacionRevisionExpediente.Crear;
using Preacepta.LN.DocsAutorizacionRevisionExpediente.Editar;
using Preacepta.LN.DocsAutorizacionRevisionExpediente.Eliminar;
using Preacepta.LN.DocsAutorizacionRevisionExpediente.Listar;
using Preacepta.LN.DocsAutorizacionRevisionExpediente.ObtenerDatos;
using Preacepta.LN.DocsCombustible.BuscarXid;
using Preacepta.LN.DocsCombustible.Crear;
using Preacepta.LN.DocsCombustible.Editar;
using Preacepta.LN.DocsCombustible.Eliminar;
using Preacepta.LN.DocsCombustible.Listar;
using Preacepta.LN.DocsCombustible.ObtenerDatos;
using Preacepta.LN.DocsCompraventaFinca.BuscarXid;
using Preacepta.LN.DocsCompraventaFinca.Crear;
using Preacepta.LN.DocsCompraventaFinca.Editar;
using Preacepta.LN.DocsCompraventaFinca.Eliminar;
using Preacepta.LN.DocsCompraventaFinca.Listar;
using Preacepta.LN.DocsCompraventaFinca.ObtenerDatos;
using Preacepta.LN.DocsContratoPrestacionServicios.BuscarXid;
using Preacepta.LN.DocsContratoPrestacionServicios.Crear;
using Preacepta.LN.DocsContratoPrestacionServicios.Editar;
using Preacepta.LN.DocsContratoPrestacionServicios.Eliminar;
using Preacepta.LN.DocsContratoPrestacionServicios.Listar;
using Preacepta.LN.DocsContratoPrestacionServicios.ObtenerDatos;
using Preacepta.LN.DocsInscripcionVehiculo.BuscarXid;
using Preacepta.LN.DocsInscripcionVehiculo.Crear;
using Preacepta.LN.DocsInscripcionVehiculo.Editar;
using Preacepta.LN.DocsInscripcionVehiculo.Eliminar;
using Preacepta.LN.DocsInscripcionVehiculo.Listar;
using Preacepta.LN.DocsInscripcionVehiculo.ObtenerDatos;
using Preacepta.LN.DocsMarcaVehiculo.BuscarXid;
using Preacepta.LN.DocsMarcaVehiculo.Crear;
using Preacepta.LN.DocsMarcaVehiculo.Editar;
using Preacepta.LN.DocsMarcaVehiculo.Eliminar;
using Preacepta.LN.DocsMarcaVehiculo.Listar;
using Preacepta.LN.DocsMarcaVehiculo.ObtenerDatos;
using Preacepta.LN.GeAbogado.BuscarXid;
using Preacepta.LN.GeAbogado.Crear;
using Preacepta.LN.GeAbogado.Editar;
using Preacepta.LN.GeAbogado.Eliminar;
using Preacepta.LN.GeAbogado.Listar;
using Preacepta.LN.GeAbogado.ObtenerDatos;
using Preacepta.LN.GeAbogadoTipo.BuscarXid;
using Preacepta.LN.GeAbogadoTipo.Crear;
using Preacepta.LN.GeAbogadoTipo.Editar;
using Preacepta.LN.GeAbogadoTipo.Eliminar;
using Preacepta.LN.GeAbogadoTipo.Listar;
using Preacepta.LN.GeAbogadoTipo.ObtenerDatos;
using Preacepta.LN.GeNegocio.BuscarXId;
using Preacepta.LN.GeNegocio.Crear;
using Preacepta.LN.GeNegocio.Editar;
using Preacepta.LN.GeNegocio.Eliminar;
using Preacepta.LN.GeNegocio.Listar;
using Preacepta.LN.GeNegocio.ObtenerDatos;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.LN.GePersona.Crear;
using Preacepta.LN.GePersona.Editar;
using Preacepta.LN.GePersona.Eliminar;
using Preacepta.LN.GePersona.Listar;
using Preacepta.LN.GePersona.ObtenerDatos;
using Preacepta.LN.Testimonios.Buscar;
using Preacepta.LN.Testimonios.Crear;
using Preacepta.LN.Testimonios.Editar;
using Preacepta.LN.Testimonios.Eliminar;
using Preacepta.LN.Testimonios.Listar;
using Preacepta.LN.Testimonios.ObtenerDatos;
using Preacepta.LN.GeRedesSociales.BuscarXid;
using Preacepta.LN.GeRedesSociales.Crear;
using Preacepta.LN.GeRedesSociales.Editar;
using Preacepta.LN.GeRedesSociales.Eliminar;
using Preacepta.LN.GeRedesSociales.Listar;
using Preacepta.LN.GeRedesSociales.ObtenerDatos;
using Preacepta.UI.Data;
using Preacepta.AD.CitasTipo.BuscarXid;
using Preacepta.AD.CitasTipo.Crear;
using Preacepta.AD.CitasTipo.Editar;
using Preacepta.AD.CitasTipo.Eliminar;
using Preacepta.AD.CitasTipo.Listar;
using Preacepta.LN.CitasTipo.BuscarXid;
using Preacepta.LN.CitasTipo.Crear;
using Preacepta.LN.CitasTipo.Editar;
using Preacepta.LN.CitasTipo.Eliminar;
using Preacepta.LN.CitasTipo.Listar;
using Preacepta.LN.CitasTipo.ObtenerDatos;
using Preacepta.AD.Citas.BuscarXid;
using Preacepta.AD.Citas.Crear;
using Preacepta.AD.Citas.Editar;
using Preacepta.AD.Citas.Eliminar;
using Preacepta.AD.Citas.Listar;
using Preacepta.LN.Citas.BuscarXid;
using Preacepta.LN.Citas.Crear;
using Preacepta.LN.Citas.Editar;
using Preacepta.LN.Citas.Eliminar;
using Preacepta.LN.Citas.Listar;
using Preacepta.LN.Citas.ObtenerDatos;
using Preacepta.AD.DocumentosCitas.DocumentosCitas;
using Preacepta.LN.DocumentosCita;
using DinkToPdf;
using DinkToPdf.Contracts;
using Preacepta.UI.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Preacepta.UI.Services.MensajesPersonalizados; // importa servicio


var builder = WebApplication.CreateBuilder(args);

// Agrega la conexion en un var
var connectionString = builder.Configuration.GetConnectionString("Server")
    ?? throw new InvalidOperationException("Conexion a base datos bajo el nombre Server no funciona");
Console.WriteLine($"Cadena de conexi�n utilizada: {connectionString}"); //muestra el mensaje

#region Base de Datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Servicio de contenexion con Autenticacion de Entity
/*builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddErrorDescriber<SpanishIdentityErrorDescriber>();*/


builder.Services.AddDefaultIdentity<IdentityUser>(
    options => options.SignIn.RequireConfirmedAccount = false)    
    .AddRoles<IdentityRole>() //activa el servicio de roles
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;
});

builder.Services.AddControllersWithViews();

//Servicio de contenexion con Base de datos PreaceptaBD
builder.Services.AddDbContext<Contexto>(options =>
    options.UseSqlServer(connectionString));
#endregion

#region Inyeccion de modulos
/*Inseccion de servicios modulo de general-personas*/
builder.Services.AddScoped<IListarGePersonaAD, ListarGePersonaAD>();
builder.Services.AddScoped<IListarGePersonaLN, ListarGePersonaLN>();
builder.Services.AddScoped<IObtenerDatosLN, ObtenerDatosLN>();
builder.Services.AddScoped<IBuscarXidGePersonaAD, BuscarXidGePersonaAD>();
builder.Services.AddScoped<IBuscarXidGePersonaLN, BuscarXidGePersonaLN>();
builder.Services.AddScoped<ICrearGePersonaAD, CrearGePersonaAD>();
builder.Services.AddScoped<ICrearGePersonaLN, CrearGePersonaLN>();
builder.Services.AddScoped<IEditarGePersonaAD, EditarGePersonaAD>();
builder.Services.AddScoped<IEditarGePersonaLN, EditarGePersonaLN>();
builder.Services.AddScoped<IEliminarPersonaAD, EliminarPersonaAD>();
builder.Services.AddScoped<IEliminarPersonaLN, EliminarPersonaLN>();
/*Inseccion de servicios modulo general-Abogados Tipos*/
builder.Services.AddScoped<IBuscarAbogadoTipoAD, BuscarAbogadoTipoAD>();
builder.Services.AddScoped<IBuscarAbogadoTipoLN, BuscarAbogadoTipoLN>();
builder.Services.AddScoped<ICrearAbogadoTipoAD, CrearAbogadoTipoAD>();
builder.Services.AddScoped<ICrearAbogadoTipoLN, CrearAbogadoTipoLN>();
builder.Services.AddScoped<IEditarAbogadoTipoAD, EditarAbogadoTipoAD>();
builder.Services.AddScoped<IEditarAbogadoTipoLN, EditarAbogadoTipoLN>();
builder.Services.AddScoped<IEliminarAbogadoTipoAD, EliminarAbogadoTipoAD>();
builder.Services.AddScoped<IEliminarAbogadoTipoLN, EliminarAbogadoTipoLN>();
builder.Services.AddScoped<IListarAbogadoTipoAD, ListarAbogadoTipoAD>();
builder.Services.AddScoped<IListarAbogadoTipoLN, ListarAbogadoTipoLN>();
builder.Services.AddScoped<IObtenerDatosAbogadoTipoLN, ObtenerDatosAbogadoTipoLN>();
/*Inseccion de servicios modulo Casos-Casos Tipo*/
builder.Services.AddScoped<IBuscarCasosTiposAD, BuscarCasosTiposAD>();
builder.Services.AddScoped<ICrearCasosTiposAD, CrearCasosTiposAD>();
builder.Services.AddScoped<IEditarCasosTiposAD, EditarCasosTiposAD>();
builder.Services.AddScoped<IEliminarCasosTipoAD, EliminarCasosTipoAD>();
builder.Services.AddScoped<IListarCasosTipoAD, ListarCasosTipoAD>();
builder.Services.AddScoped<IObtenerDatosCasosTipoLN, ObtenerDatosCasosTipoLN>();
builder.Services.AddScoped<IBuscarCasosTiposLN, BuscarCasosTiposLN>();
builder.Services.AddScoped<ICrearCasosTiposLN, CrearCasosTiposLN>();
builder.Services.AddScoped<IEditarCasosTiposLN, EditarCasosTiposLN>();
builder.Services.AddScoped<IEliminarCasosTipoLN, EliminarCasosTipoLN>();
builder.Services.AddScoped<IListarCasosTipoLN, ListarCasosTipoLN>();
/*Inseccion de servicios modulo general- Abogados*/
builder.Services.AddScoped<IBuscarAbogadoAD, BuscarAbogadoAD>();
builder.Services.AddScoped<ICrearAbogadoAD, CrearAbogadoAD>();
builder.Services.AddScoped<IEditarAbogadoAD, EditarAbogadoAD>();
builder.Services.AddScoped<IEliminarAbogadoAD, EliminarAbogadoAD>();
builder.Services.AddScoped<IListarAbogadoAD, ListarAbogadoAD>();
builder.Services.AddScoped<IBuscarAbogadoLN, BuscarAbogadoLN>();
builder.Services.AddScoped<ICrearAbogadoLN, CrearAbogadoLN>();
builder.Services.AddScoped<IEditarAbogadoLN, EditarAbogadoLN>();
builder.Services.AddScoped<IEliminarAbogadoLN, EliminarAbogadoLN>();
builder.Services.AddScoped<IListarAbogadoLN, ListarAbogadoLN>();
builder.Services.AddScoped<IObtenerDatosAbogadoLN, ObtenerDatosAbogadoLN>();
/*Inseccion de servicios modulo general- GeNegocio*/
builder.Services.AddScoped<IBuscarNegocioAD, BuscarNegocioAD>();
builder.Services.AddScoped<ICrearNegocioAD, CrearNegocioAD>();
builder.Services.AddScoped<IEditarNegocioAD, EditarNegocioAD>();
builder.Services.AddScoped<IEliminarNegocioAD, EliminarNegocioAD>();
builder.Services.AddScoped<IListarNegocioAD, ListarNegocioAD>();
builder.Services.AddScoped<IBuscarNegocioLN, BuscarNegocioLN>();
builder.Services.AddScoped<ICrearNegocioLN, CrearNegocioLN>();
builder.Services.AddScoped<IEditarNegocioLN, EditarNegocioLN>();
builder.Services.AddScoped<IEliminarNegocioLN, EliminarNegocioLN>();
builder.Services.AddScoped<IListarNegocioLN, ListarNegocioLN>();
builder.Services.AddScoped<IObtenerDatosNegocioLN, ObtenerDatosNegocioLN>();
/*Inseccion de servicios tablas Provincias,Cantones y Distritos*/
builder.Services.AddScoped<IBuscarCrDireccion1AD, BuscarCrDireccion1AD>();
builder.Services.AddScoped<ICrearCrDireccion1AD, CrearCrDireccion1AD>();
builder.Services.AddScoped<IEditarCrDireccion1AD, EditarCrDireccion1AD>();
builder.Services.AddScoped<IEliminarCrDireccion1AD, EliminarCrDireccion1AD>();
builder.Services.AddScoped<IListarCrDireccion1AD, ListarCrDireccion1AD>();
builder.Services.AddScoped<IBuscarCrDireccion1LN, BuscarCrDireccion1LN>();
builder.Services.AddScoped<ICrearCrDireccion1LN, CrearCrDireccion1LN>();
builder.Services.AddScoped<IEditarCrDireccion1LN, EditarCrDireccion1LN>();
builder.Services.AddScoped<IEliminarCrDireccion1LN, EliminarCrDireccion1LN>();
builder.Services.AddScoped<IListarCrDireccion1LN, ListarCrDireccion1LN>();
builder.Services.AddScoped<IObtenerDatosDireccion1LN, ObtenerDatosDireccion1LN>();
/*Inseccion de servicios modulo de Casos - casos*/
builder.Services.AddScoped<IBuscarCasosAD, BuscarCasosAD>();
builder.Services.AddScoped<ICrearCasosAD, CrearCasosAD>();
builder.Services.AddScoped<IEditarCasosAD, EditarCasosAD>();
builder.Services.AddScoped<IELiminarCasosAD, ELiminarCasosAD>();
builder.Services.AddScoped<IListarCasosAD, ListarCasosAD>();
builder.Services.AddScoped<IBuscarCasosLN, BuscarCasosLN>();
builder.Services.AddScoped<ICrearCasosLN, CrearCasosLN>();
builder.Services.AddScoped<IEditarCasosLN, EditarCasosLN>();
builder.Services.AddScoped<IELiminarCasosLN, ELiminarCasosLN>();
builder.Services.AddScoped<IListarCasosLN, ListarCasosLN>();
builder.Services.AddScoped<IObtenerDatosCasoLN, ObtenerDatosCasoLN>();
/*Inseccion de servicios modulo de General - Tablas y modelos de redes sociales*/
builder.Services.AddScoped<IBuscarRedesSocialesAD, BuscarRedesSocialesAD>();
builder.Services.AddScoped<ICrearRedesSocialesAD, CrearRedesSocialesAD>();
builder.Services.AddScoped<IEditarRedesSocialesAD, EditarRedesSocialesAD>();
builder.Services.AddScoped<IEliminarRedesSocialesAD, EliminarRedesSocialesAD>();
builder.Services.AddScoped<IListarRedesSocialesAD, ListarRedesSocialesAD>();
builder.Services.AddScoped<IObtenerDatosRedesSocialesLN, ObtenerDatosRedesSocialesLN>();
builder.Services.AddScoped<IBuscarRedesSocialesLN, BuscarRedesSocialesLN>();
builder.Services.AddScoped<ICrearRedesSocialesLN, CrearRedesSocialesLN>();
builder.Services.AddScoped<IEditarRedesSocialesLN, EditarRedesSocialesLN>();
builder.Services.AddScoped<IEliminarRedesSocialesLN, EliminarRedesSocialesLN>();
builder.Services.AddScoped<IListarRedesSocialesLN, ListarRedesSocialesLN>();
/*Inseccion de servicios modulo de Casos - Tablas y modelos de CatosEtapas*/
builder.Services.AddScoped<IBuscarCasosEtapasAD, BuscarCasosEtapasAD>();
builder.Services.AddScoped<ICrearCasosEtapasAD, CrearCasosEtapasAD>();
builder.Services.AddScoped<IEditarCasosEtapasAD, EditarCasosEtapasAD>();
builder.Services.AddScoped<IEliminarCasosEtapasAD, EliminarCasosEtapasAD>();
builder.Services.AddScoped<IListarCasosEtapasAD, ListarCasosEtapasAD>();
builder.Services.AddScoped<IObtnerDatosCasoEtapaLN, ObtnerDatosCasoEtapaLN>();
builder.Services.AddScoped<IBuscarCasosEtapasLN, BuscarCasosEtapasLN>();
builder.Services.AddScoped<ICrearCasosEtapasLN, CrearCasosEtapasLN>();
builder.Services.AddScoped<IEditarCasosEtapasLN, EditarCasosEtapasLN>();
builder.Services.AddScoped<IEliminarCasosEtapasLN, EliminarCasosEtapasLN>();
builder.Services.AddScoped<IListarCasosEtapasLN, ListarCasosEtapasLN>();
/*Inseccion de servicios modulo de Casos - Tablas y modelos de CatosEvidencias*/
builder.Services.AddScoped<IBuscarCasosEvidenciaAD, BuscarCasosEvidenciaAD>();
builder.Services.AddScoped<ICrearCasosEvidenciaAD, CrearCasosEvidenciaAD>();
builder.Services.AddScoped<IEditarCasosEvidenciaAD, EditarCasosEvidenciaAD>();
builder.Services.AddScoped<IEliminarCasosEvidenciaAD, EliminarCasosEvidenciaAD>();
builder.Services.AddScoped<IListarCasosEvidenciaAD, ListarCasosEvidenciaAD>();
builder.Services.AddScoped<IObtnerDatosCasoEvidenciaLN, ObtnerDatosCasoEvidenciaLN>();
builder.Services.AddScoped<IBuscarCasosEvidenciaLN, BuscarCasosEvidenciaLN>();
builder.Services.AddScoped<ICrearCasosEvidenciaLN, CrearCasosEvidenciaLN>();
builder.Services.AddScoped<IEditarCasosEvidenciaLN, EditarCasosEvidenciaLN>();
builder.Services.AddScoped<IEliminarCasosEvidenciaLN, EliminarCasosEvidenciaLN>();
builder.Services.AddScoped<IListarCasosEvidenciaLN, ListarCasosEvidenciaLN>();
/*Inseccion de servicios modulo de CitasTipo*/
builder.Services.AddScoped<IBuscarCitasTipoAD, BuscarCitasTipoAD>();
builder.Services.AddScoped<ICrearCitasTipoAD, CrearCitasTipoAD>();
builder.Services.AddScoped<IEditarCitasTipoAD, EditarCitasTipoAD>();
builder.Services.AddScoped<IEliminarCitasTipoAD, EliminarCitasTipoAD>();
builder.Services.AddScoped<IListarCitasTipoAD, ListarCitasTipoAD>();
builder.Services.AddScoped<IBuscarCitasTipoLN, BuscarCitasTipoLN>();
builder.Services.AddScoped<ICrearCitasTipoLN, CrearCitasTipoLN>();
builder.Services.AddScoped<IEditarCitasTipoLN, EditarCitasTipoLN>();
builder.Services.AddScoped<IEliminarCitasTipoLN, EliminarCitasTipoLN>();
builder.Services.AddScoped<IListarCitasTipoLN, ListarCitasTipoLN>();
builder.Services.AddScoped<IObtenerDatosCitasTipoLN, ObtenerDatosCitasTipoLN>();
/*Inseccion de servicios modulo de Citas*/
builder.Services.AddScoped<IBuscarCitasAD, BuscarCitasAD>();
builder.Services.AddScoped<ICrearCitasAD, CrearCitasAD>();
builder.Services.AddScoped<IEditarCitasAD, EditarCitasAD>();
builder.Services.AddScoped<IEliminarCitasAD, EliminarCitasAD>();
builder.Services.AddScoped<IListarCitasAD, ListarCitasAD>();
builder.Services.AddScoped<IBuscarCitasLN, BuscarCitasLN>();
builder.Services.AddScoped<ICrearCitasLN, CrearCitasLN>();
builder.Services.AddScoped<IEditarCitasLN, EditarCitasLN>();
builder.Services.AddScoped<IEliminarCitasLN, EliminarCitasLN>();
builder.Services.AddScoped<IListarCitasLN, ListarCitasLN>();
builder.Services.AddScoped<IObtenerDatosCitasLN, ObtenerDatosCitasLN>();
/*Inseccion de servicios modulo de Documentos Citas*/
builder.Services.AddScoped<IDocumentosCitaAD, DocumentosCitaAD>();
builder.Services.AddScoped<IDocumentosCitaLN, DocumentosCitaLN>();
/*Inseccion de servicios modulo de documento de prestacion de servicios*/
builder.Services.AddScoped<IBuscarDocsContratoPrestacionServiciosAD, BuscarDocsContratoPrestacionServiciosAD>();
builder.Services.AddScoped<ICrearDocsContratoPrestacionServiciosAD, CrearDocsContratoPrestacionServiciosAD>();
builder.Services.AddScoped<IEditarDocsContratoPrestacionServiciosAD, EditarDocsContratoPrestacionServiciosAD>();
builder.Services.AddScoped<IEliminarDocsContratoPrestacionServiciosAD, EliminarDocsContratoPrestacionServiciosAD>();
builder.Services.AddScoped<IListarDocsContratoPrestacionServiciosAD, ListarDocsContratoPrestacionServiciosAD>();
builder.Services.AddScoped<IObtenerDatosDocsContratoPrestacionServiciosLN, ObtenerDatosDocsContratoPrestacionServiciosLN>();
builder.Services.AddScoped<IBuscarDocsContratoPrestacionServiciosLN, BuscarDocsContratoPrestacionServiciosLN>();
builder.Services.AddScoped<ICrearDocsContratoPrestacionServiciosLN, CrearDocsContratoPrestacionServiciosLN>();
builder.Services.AddScoped<IEditarDocsContratoPrestacionServiciosLN, EditarDocsContratoPrestacionServiciosLN>();
builder.Services.AddScoped<IEliminarDocsContratoPrestacionServiciosLN, EliminarDocsContratoPrestacionServiciosLN>();
builder.Services.AddScoped<IListarDocsContratoPrestacionServiciosLN, ListarDocsContratoPrestacionServiciosLN>();
/*Inseccion de servicios modulo de documento de autorizacion revision expediente*/
builder.Services.AddScoped<IBuscarDocsAutorizacionRevisionExpedienteAD, BuscarDocsAutorizacionRevisionExpedienteAD>();
builder.Services.AddScoped<ICrearDocsAutorizacionRevisionExpedienteAD, CrearDocsAutorizacionRevisionExpedienteAD>();
builder.Services.AddScoped<IEditarDocsAutorizacionRevisionExpedienteAD, EditarDocsAutorizacionRevisionExpedienteAD>();
builder.Services.AddScoped<IEliminarDocsAutorizacionRevisionExpedienteAD, EliminarDocsAutorizacionRevisionExpedienteAD>();
builder.Services.AddScoped<IListarDocsAutorizacionRevisionExpedienteAD, ListarDocsAutorizacionRevisionExpedienteAD>();
builder.Services.AddScoped<IObtenerDocsAutorizacionRevisionExpedienteLN, ObtenerDocsAutorizacionRevisionExpedienteLN>();
builder.Services.AddScoped<IBuscarDocsAutorizacionRevisionExpedienteLN, BuscarDocsAutorizacionRevisionExpedienteLN>();
builder.Services.AddScoped<ICrearDocsAutorizacionRevisionExpedienteLN, CrearDocsAutorizacionRevisionExpedienteLN>();
builder.Services.AddScoped<IEditarDocsAutorizacionRevisionExpedienteLN, EditarDocsAutorizacionRevisionExpedienteLN>();
builder.Services.AddScoped<IEliminarDocsAutorizacionRevisionExpedienteLN, EliminarDocsAutorizacionRevisionExpedienteLN>();
builder.Services.AddScoped<IListarDocsAutorizacionRevisionExpedienteLN, ListarDocsAutorizacionRevisionExpedienteLN>();
/*Inseccion de servicios modulo de documento de combustibles*/
builder.Services.AddScoped<IBuscarDocsCombustibleAD, BuscarDocsCombustibleAD>();
builder.Services.AddScoped<ICrearDocsCombustibleAD, CrearDocsCombustibleAD>();
builder.Services.AddScoped<IEditarDocsCombustibleAD, EditarDocsCombustibleAD>();
builder.Services.AddScoped<IEliminarDocsCombustibleAD, EliminarDocsCombustibleAD>();
builder.Services.AddScoped<IListarDocsCombustibleAD, ListarDocsCombustibleAD>();
builder.Services.AddScoped<IObtenerDatosDocsCombustibleLN, ObtenerDatosDocsCombustibleLN>();
builder.Services.AddScoped<IBuscarDocsCombustibleLN, BuscarDocsCombustibleLN>();
builder.Services.AddScoped<ICrearDocsCombustibleLN, CrearDocsCombustibleLN>();
builder.Services.AddScoped<IEditarDocsCombustibleLN, EditarDocsCombustibleLN>();
builder.Services.AddScoped<IEliminarDocsCombustibleLN, EliminarDocsCombustibleLN>();
builder.Services.AddScoped<IListarDocsCombustibleLN, ListarDocsCombustibleLN>();
/*Inseccion de servicios modulo de documento de compraventafinca*/
builder.Services.AddScoped<IBuscarDocsCompraventaFincaAD, BuscarDocsCompraventaFincaAD>();
builder.Services.AddScoped<ICrearDocsCompraventaFincaAD, CrearDocsCompraventaFincaAD>();
builder.Services.AddScoped<IEditarDocsCompraventaFincaAD, EditarDocsCompraventaFincaAD>();
builder.Services.AddScoped<IEliminarDocsCompraventaFincaAD, EliminarDocsCompraventaFincaAD>();
builder.Services.AddScoped<IListarDocsCompraventaFincaAD, ListarDocsCompraventaFincaAD>();
builder.Services.AddScoped<IObtenerDatosDocsCompraventaFincaLN, ObtenerDatosDocsCompraventaFincaLN>();
builder.Services.AddScoped<IBuscarDocsCompraventaFincaLN, BuscarDocsCompraventaFincaLN>();
builder.Services.AddScoped<ICrearDocsCompraventaFincaLN, CrearDocsCompraventaFincaLN>();
builder.Services.AddScoped<IEditarDocsCompraventaFincaLN, EditarDocsCompraventaFincaLN>();
builder.Services.AddScoped<IEliminarDocsCompraventaFincaLN, EliminarDocsCompraventaFincaLN>();
builder.Services.AddScoped<IListarDocsCompraventaFincaLN, ListarDocsCompraventaFincaLN>();
/*Inseccion de servicios modulo de documento de incripcion de vehiculo*/
builder.Services.AddScoped<IBuscarDocsInscripcionVehiculoAD, BuscarDocsInscripcionVehiculoAD>();
builder.Services.AddScoped<ICrearDocsInscripcionVehiculoAD, CrearDocsInscripcionVehiculoAD>();
builder.Services.AddScoped<IEditarDocsInscripcionVehiculoAD, EditarDocsInscripcionVehiculoAD>();
builder.Services.AddScoped<IEliminarDocsInscripcionVehiculoAD, EliminarDocsInscripcionVehiculoAD>();
builder.Services.AddScoped<IListarDocsInscripcionVehiculoAD, ListarDocsInscripcionVehiculoAD>();
builder.Services.AddScoped<IObtenerDatosDocsInscripcionVehiculoTipoLN, ObtenerDatosDocsInscripcionVehiculoTipoLN>();
builder.Services.AddScoped<IBuscarDocsInscripcionVehiculoLN, BuscarDocsInscripcionVehiculoLN>();
builder.Services.AddScoped<ICrearDocsInscripcionVehiculoLN, CrearDocsInscripcionVehiculoLN>();
builder.Services.AddScoped<IEditarDocsInscripcionVehiculoLN, EditarDocsInscripcionVehiculoLN>();
builder.Services.AddScoped<IEliminarDocsInscripcionVehiculoLN, EliminarDocsInscripcionVehiculoLN>();
builder.Services.AddScoped<IListarDocsInscripcionVehiculoLN, ListarDocsInscripcionVehiculoLN>();
/*Inseccion de servicios modulo de documento de marcas de vehiculo*/
builder.Services.AddScoped<IBuscarDocsMarcaVehiculoAD, BuscarDocsMarcaVehiculoAD>();
builder.Services.AddScoped<ICrearDocsMarcaVehiculoAD, CrearDocsMarcaVehiculoAD>();
builder.Services.AddScoped<IEditarDocsMarcaVehiculoAD, EditarDocsMarcaVehiculoAD>();
builder.Services.AddScoped<IEliminarDocsMarcaVehiculoAD, EliminarDocsMarcaVehiculoAD>();
builder.Services.AddScoped<IListarDocsMarcaVehiculoAD, ListarDocsMarcaVehiculoAD>();
builder.Services.AddScoped<IObtenerDatosDocsMarcaVehiculoLN, ObtenerDatosDocsMarcaVehiculoLN>();
builder.Services.AddScoped<IBuscarDocsMarcaVehiculoLN, BuscarDocsMarcaVehiculoLN>();
builder.Services.AddScoped<ICrearDocsMarcaVehiculoLN, CrearDocsMarcaVehiculoLN>();
builder.Services.AddScoped<IEditarDocsMarcaVehiculoLN, EditarDocsMarcaVehiculoLN>();
builder.Services.AddScoped<IEliminarDocsMarcaVehiculoLN, EliminarDocsMarcaVehiculoLN>();
builder.Services.AddScoped<IListarDocsMarcaVehiculoLN, ListarDocsMarcaVehiculoLN>();
/*Insersion de servicios modulo de testimonios*/
builder.Services.AddScoped<IBuscarTestAD, BuscarTestAD>();
builder.Services.AddScoped<ICrearTestAD, CrearTestAD>();
builder.Services.AddScoped<IEditarTestAD, EditarTestAD>();
builder.Services.AddScoped<IEliminarTestAD, EliminarTestAD>();
builder.Services.AddScoped<IListarTestAD, ListarTestAD>();
builder.Services.AddScoped<IBuscarTestLN, BuscarTestLN>();
builder.Services.AddScoped<ICrearTestLN, CrearTestLN>();
builder.Services.AddScoped<IEditarTestLN, EditarTestLN>();
builder.Services.AddScoped<IEliminarTestLN, EliminarTestLN>();
builder.Services.AddScoped<IListarTestLN, ListarTestLN>();
builder.Services.AddScoped<IObtenerDatosTestLN, ObtenerDatosTestLN>();
// Servicios DocPoderesEspecialesJudiciales
builder.Services.AddScoped<IBuscarPoderJudAD, BuscarPoderJudAD>();
builder.Services.AddScoped<ICrearPoderJudAD, CrearPoderJudAD>();
builder.Services.AddScoped<IEditarPoderJudAD, EditarPoderJudAD>();
builder.Services.AddScoped<IEliminarPoderJudAD, EliminarPoderJudAD>();
builder.Services.AddScoped<IListarPoderJudAD, ListarPoderJudAD>();
builder.Services.AddScoped<IBuscarPoderJudLN, BuscarPoderJudLN>();
builder.Services.AddScoped<ICrearPoderJudLN, CrearPoderJudLN>();
builder.Services.AddScoped<IEditarPoderJudLN, EditarPoderJudLN>();
builder.Services.AddScoped<IEliminarPoderJudLN, EliminarPoderJudLN>();
builder.Services.AddScoped<IListarPoderJudLN, ListarPoderJudLN>();
builder.Services.AddScoped<IObtenerDatosPoderJudLN, ObtenerDatosPoderJudLN>();
// Servicios DocsOpcionCompraventaVehiculo
builder.Services.AddScoped<IBuscarDocCVAD, BuscarDocCVAD>();
builder.Services.AddScoped<ICrearDocCVAD, CrearDocCVAD>();
builder.Services.AddScoped<IEditarDocCVAD, EditarDocCVAD>();
builder.Services.AddScoped<IEliminarDocCVAD, EliminarDocCVAD>();
builder.Services.AddScoped<IListarDocCVAD, ListarDocCVAD>();
builder.Services.AddScoped<IBuscarDocCVLN, BuscarDocCVLN>();
builder.Services.AddScoped<ICrearDocCVLN, CrearDocCVLN>();
builder.Services.AddScoped<IEditarDocCVLN, EditarDocCVLN>();
builder.Services.AddScoped<IEliminarDocCVLN, EliminarDocCVLN>();
builder.Services.AddScoped<IListarDocCVLN, ListarDocCVLN>();
builder.Services.AddScoped<IObtenerDatosDocsCV, ObtenerDatosDocsCV>();
// Servicios DocsPagare
builder.Services.AddScoped<IBuscarPagareAD, BuscarPagareAD>();
builder.Services.AddScoped<ICrearPagareAD, CrearPagareAD>();
builder.Services.AddScoped<IEditarPagareAD, EditarPagareAD>();
builder.Services.AddScoped<IEliminarPagareAD, EliminarPagareAD>();
builder.Services.AddScoped<IListarPagareAD, ListarPagareAD>();
builder.Services.AddScoped<IBuscarPagareLN, BuscarPagareLN>();
builder.Services.AddScoped<ICrearPagareLN, CrearPagareLN>();
builder.Services.AddScoped<IEditarPagareLN, EditarPagareLN>();
builder.Services.AddScoped<IEliminarPagareLN, EliminarPagareLN>();
builder.Services.AddScoped<IListarPagareLN, ListarPagareLN>();
builder.Services.AddScoped<IObtenerDatosPagareLN, ObtenerDatosPagareLN>();

//builder.Services.AddScoped<Preacepta.AD.DocsPagare.Eliminar.EliminarPagareAD>();

// Servicios DocsTipoVehiculo
builder.Services.AddScoped<IBuscarTipoVehiculoAD, BuscarTipoVehiculoAD>();
builder.Services.AddScoped<ICrearTipoVehiculoAD, CrearTipoVehiculoAD>();
builder.Services.AddScoped<IEditarTipoVehiculoAD, EditarTipoVehiculoAD>();
builder.Services.AddScoped<IEliminarTipoVehiculoAD, EliminarTipoVehiculoAD>();
builder.Services.AddScoped<IListarTipoVehiculoAD, ListarTipoVehiculoAD>();
builder.Services.AddScoped<IBuscarTipoVehiculoLN, BuscarTipoVehiculoLN>();
builder.Services.AddScoped<ICrearTipoVehiculoLN, CrearTipoVehiculoLN>();
builder.Services.AddScoped<IEditarTipoVehiculoLN, EditarTipoVehiculoLN>();
builder.Services.AddScoped<IEliminarTipoVehiculoLN, EliminarTipoVehiculoLN>();
builder.Services.AddScoped<IListarTipoVehiculoLN, ListarTipoVehiculoLN>();
builder.Services.AddScoped<IObtenerDatosTipoVehiculoLN, ObtenerDatosTipoVehiculoLN>();

//servicios para DInktoPdf, crear pdfs
builder.Services.AddSingleton(typeof(IConverter), PdfConverterService.GetConverter());

//builder.Services.AddScoped<Preacepta.AD.DocsTipoVehiculo.Eliminar.EliminarTipoVehiculoAD>();

//Servicio para mostrar mensajes personalizados
builder.Services.AddTransient<IValidacionesResetPassword, ValidacionesResetPassword>();
#endregion

#region Servicio de correo electronico
//Servicio de correo
builder.Services.AddTransient<IEmailSender, ServicioEmail>();
#endregion

var app = builder.Build();

#region Asignacion y creacion de roles
//Verifica se lo roles existen y si no los crea todo esto sucede en el incio de la aplicacion
using (var scope = app.Services.CreateScope())
{
    var roles = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    if (!await roles.RoleExistsAsync("Gestor") || !await roles.RoleExistsAsync("Abogado") || !await roles.RoleExistsAsync("Cliente"))
    {
        await roles.CreateAsync(new IdentityRole("Gestor"));
        await roles.CreateAsync(new IdentityRole("Abogado"));
        await roles.CreateAsync(new IdentityRole("Cliente"));
    }
}
#endregion

#region Creacion de usario root
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();    
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();


    var rootEmail = config["GestorSettings:Email"];
    var rootPassword = config["GestorSettings:Password"];

    // Asegurarse de que el rol Admin existe
    if (!await roleManager.RoleExistsAsync("Gestor"))
    {
        await roleManager.CreateAsync(new IdentityRole("Gestor"));
    }

    // Crear usuario root si no existe
    if (await userManager.FindByEmailAsync(rootEmail) == null)
    {
        var rootUser = new IdentityUser
        {
            UserName = rootEmail,
            Email = rootEmail,
            EmailConfirmed = true
        };

        await userManager.CreateAsync(rootUser, rootPassword);
        await userManager.AddToRoleAsync(rootUser, "Gestor");
    }    
}
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
