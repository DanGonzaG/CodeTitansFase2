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
