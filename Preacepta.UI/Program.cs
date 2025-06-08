using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
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
using Preacepta.UI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Server") ?? throw new InvalidOperationException("Conexion a base datos bajo el nombre Server no funciona");
Console.WriteLine($"Cadena de conexión utilizada: {connectionString}");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Servicio de contenexion con Autenticacion de Entity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

//Servicio de contenexion con Base de datos PreaceptaBD
builder.Services.AddDbContext<Contexto>(options =>
    options.UseSqlServer(connectionString));

/*Inseccion de servicios modulo de personas*/
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
/*Inseccion de servicios modulo de Abogados Tipos*/
builder.Services.AddScoped<IBuscarAbogadoTipoAD, BuscarAbogadoTipoAD>();
builder.Services.AddScoped<IBuscarAbogadoTipoLN, BuscarAbogadoTipoLN> ();
builder.Services.AddScoped<ICrearAbogadoTipoAD, CrearAbogadoTipoAD>();
builder.Services.AddScoped<ICrearAbogadoTipoLN, CrearAbogadoTipoLN>();
builder.Services.AddScoped<IEditarAbogadoTipoAD, EditarAbogadoTipoAD>();
builder.Services.AddScoped<IEditarAbogadoTipoLN, EditarAbogadoTipoLN>();
builder.Services.AddScoped<IEliminarAbogadoTipoAD, EliminarAbogadoTipoAD>();
builder.Services.AddScoped<IEliminarAbogadoTipoLN, EliminarAbogadoTipoLN>();
builder.Services.AddScoped<IListarAbogadoTipoAD, ListarAbogadoTipoAD>();
builder.Services.AddScoped<IListarAbogadoTipoLN, ListarAbogadoTipoLN>();
builder.Services.AddScoped<IObtenerDatosAbogadoTipoLN, ObtenerDatosAbogadoTipoLN>();
/*Inseccion de servicios modulo de Casos Tipo*/
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
/*Inseccion de servicios modulo de Abogados*/
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
/*Inseccion de servicios modulo de GeNegocio*/
builder.Services.AddScoped<IBuscarNegocioAD, BuscarNegocioAD>();
builder.Services.AddScoped< ICrearNegocioAD, CrearNegocioAD>();
builder.Services.AddScoped< IEditarNegocioAD, EditarNegocioAD>();
builder.Services.AddScoped<IEliminarNegocioAD, EliminarNegocioAD>();
builder.Services.AddScoped<IListarNegocioAD, ListarNegocioAD>();
builder.Services.AddScoped<IBuscarNegocioLN, BuscarNegocioLN>();
builder.Services.AddScoped<ICrearNegocioLN, CrearNegocioLN>();
builder.Services.AddScoped<IEditarNegocioLN, EditarNegocioLN>();
builder.Services.AddScoped<IEliminarNegocioLN, EliminarNegocioLN>();
builder.Services.AddScoped<IListarNegocioLN, ListarNegocioLN>();
builder.Services.AddScoped<IObtenerDatosNegocioLN, ObtenerDatosNegocioLN>();
/*Inseccion de servicios modulo de Provincias,Cantones y Distritos*/
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

var app = builder.Build();

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
