using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.AD;

public partial class Contexto : DbContext
{
    public Contexto()
    {
    }

    public Contexto(DbContextOptions<Contexto> options)
        : base(options)
    {
    }

    public virtual DbSet<TCaso> TCasos { get; set; }

    public virtual DbSet<TCasosEtapa> TCasosEtapas { get; set; }

    public virtual DbSet<TCasosEvidencia> TCasosEvidencias { get; set; }

    public virtual DbSet<TCasosTipo> TCasosTipos { get; set; }

    public virtual DbSet<TCita> TCitas { get; set; }

    public virtual DbSet<TDocumentosCita> TDocumentosCita { get; set; }

    public virtual DbSet<TCitasCliente> TCitasClientes { get; set; }

    public virtual DbSet<TCitasTipo> TCitasTipos { get; set; }

    public virtual DbSet<TCrCantone> TCrCantones { get; set; }

    public virtual DbSet<TCrDistrito> TCrDistritos { get; set; }

    public virtual DbSet<TCrProvincia> TCrProvincias { get; set; }

    public virtual DbSet<TDocsAutorizacionRevisionExpediente> TDocsAutorizacionRevisionExpedientes { get; set; }

    public virtual DbSet<TDocsCombustible> TDocsCombustibles { get; set; }

    public virtual DbSet<TDocsCompraventaFinca> TDocsCompraventaFincas { get; set; }

    public virtual DbSet<TDocsContratoPrestacionServicio> TDocsContratoPrestacionServicios { get; set; }

    public virtual DbSet<TDocsInscripcionVehiculo> TDocsInscripcionVehiculos { get; set; }

    public virtual DbSet<TDocsMarcaVehiculo> TDocsMarcaVehiculos { get; set; }

    public virtual DbSet<TDocsOpcionCompraventaVehiculo> TDocsOpcionCompraventaVehiculos { get; set; }

    public virtual DbSet<TDocsPagare> TDocsPagares { get; set; }

    public virtual DbSet<TDocsPoderesEspecialesJudiciale> TDocsPoderesEspecialesJudiciales { get; set; }

    public virtual DbSet<TDocsTipoVehiculo> TDocsTipoVehiculos { get; set; }

    public virtual DbSet<TGeAbogado> TGeAbogados { get; set; }

    public virtual DbSet<TGeAbogadoTipo> TGeAbogadoTipos { get; set; }

    public virtual DbSet<TGeNegocio> TGeNegocios { get; set; }

    public virtual DbSet<TGePersona> TGePersonas { get; set; }

    public virtual DbSet<TGeRedesSociale> TGeRedesSociales { get; set; }

    public virtual DbSet<TTestimonio> TTestimonios { get; set; }

    public DbSet<HistorialDocumento> HistorialDocumentos { get; set; }



    //string Server = "Data Source=DANLAPTOPASUS\\DEVELOPERSERVER;Initial Catalog=PreaceptaBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"; //Conexion Daniel
    //string Server = "Data Source=ANDY;Initial Catalog=PreaceptaBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True"; // Conexion Andy
    //string Server = "Data Source=DESKTOP-BREQ0TF\\SQLEXPRESS;Initial Catalog=PreaceptaBD;Integrated Security=True;Trust Server Certificate=True"; //Conexion Alonso

    //string Server = "Data Source=DESKTOP-L8MJ1I5\\SQLEXPRESS03;Initial Catalog=PreaceptaBD;User ID=db_connect;Password=1357;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True";
    string Server = "Data Source=DESKTOP-SN6P8CV;Initial Catalog=PreaceptaBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True";//Andy

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.


        => optionsBuilder.UseSqlServer(/*Server*//*"Server=DANLAPTOPASUS\\DEVELOPERSERVER;Database=PreaceptaBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True"*/);




       
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TCaso>(entity =>
        {
            entity.HasOne(d => d.IdAbogadoNavigation).WithMany(p => p.TCasos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Casos_T_GeAbogados");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.TCasos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Casos_T_GePersonas");

            entity.HasOne(d => d.IdTipoCasoNavigation).WithMany(p => p.TCasos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Casos_T_CasosTipos");
        });

        modelBuilder.Entity<TCasosEtapa>(entity =>
        {
            entity.HasKey(e => e.IdEtapaPl).HasName("PK_T_EtapaPL");

            entity.HasOne(d => d.IdCasoNavigation).WithMany(p => p.TCasosEtapas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_CasosEtapas_T_Casos");
        });

        modelBuilder.Entity<TCasosEvidencia>(entity =>
        {
            entity.HasOne(d => d.IdCasoNavigation).WithMany(p => p.TCasosEvidencia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_CasosEvidencias_T_Casos");

            entity.HasOne(d => d.IdCaso1).WithMany(p => p.TCasosEvidencia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_CasosEvidencias_T_CasosEtapas");
        });

        modelBuilder.Entity<TCasosTipo>(entity =>
        {
            entity.HasKey(e => e.IdTipoCaso).HasName("PK_T_TipoCasos");
        });

        modelBuilder.Entity<TCita>(entity =>
        {
            entity.HasOne(d => d.AnfitrionNavigation).WithMany(p => p.TCita)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Citas_T_GeAbogados");

            entity.HasOne(d => d.IdTipoCitaNavigation).WithMany(p => p.TCita)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Citas_T_CitasTipos");
        });

        modelBuilder.Entity<TDocumentosCita>(entity =>
        {
            entity.ToTable("T_DocumentosCita");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.NombreArchivo)
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(e => e.RutaArchivo)
                .HasMaxLength(500)
                .IsRequired();

            entity.Property(e => e.FechaSubida)
                .HasDefaultValueSql("GETDATE()");

            entity.HasOne(d => d.Cita)
                .WithMany(p => p.DocumentosCita)
                .HasForeignKey(d => d.IdCita)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_T_DocumentosCita_T_Citas");
        });


        modelBuilder.Entity<TCitasCliente>(entity =>
        {
            entity.HasOne(d => d.IdCitaNavigation).WithMany(p => p.TCitasClientes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_CitasClientes_T_Citas");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.TCitasClientes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_CitasClientes_T_GePersonas");
        });

        modelBuilder.Entity<TCitasTipo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_T_TipoCita");
        });

        modelBuilder.Entity<TCrCantone>(entity =>
        {
            entity.HasKey(e => e.IdCanton).HasName("PK_T_Cantones");

            entity.Property(e => e.IdCanton).ValueGeneratedNever();

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.TCrCantones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_CrCantones_T_CrProvincias");
        });

        modelBuilder.Entity<TCrDistrito>(entity =>
        {
            entity.HasKey(e => e.IdDistrito).HasName("PK_T_Distritos");

            entity.Property(e => e.IdDistrito).ValueGeneratedNever();

            entity.HasOne(d => d.IdCatonNavigation).WithMany(p => p.TCrDistritos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_CrDistritos_T_CrCantones");
        });

        modelBuilder.Entity<TCrProvincia>(entity =>
        {
            entity.HasKey(e => e.IdProvincia).HasName("PK_T_provincia");

            entity.Property(e => e.IdProvincia).ValueGeneratedNever();
        });

        modelBuilder.Entity<TDocsAutorizacionRevisionExpediente>(entity =>
        {
            entity.HasKey(e => e.IdDocumento).HasName("PK__T_DocsAu__B79DF372F42F6683");

            entity.HasOne(d => d.CedulaAbogadoNavigation).WithMany(p => p.TDocsAutorizacionRevisionExpedientes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDARE_cedula_abogado");

            entity.HasOne(d => d.CedulaAsistenteNavigation).WithMany(p => p.TDocsAutorizacionRevisionExpedienteCedulaAsistenteNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDARE_cedula_asistente");

            entity.HasOne(d => d.CedulaImputadoNavigation).WithMany(p => p.TDocsAutorizacionRevisionExpedienteCedulaImputadoNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDARE_cedula_imputado");
        });

        modelBuilder.Entity<TDocsCombustible>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_T_Combustible");
        });

        modelBuilder.Entity<TDocsCompraventaFinca>(entity =>
        {
            entity.HasKey(e => e.IdDocumento).HasName("PK__T_DocsCo__B79DF372A6CEC896");

            entity.HasOne(d => d.CedulaAbogadoNavigation).WithMany(p => p.TDocsCompraventaFincas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDCF_cedula_abogado");

            entity.HasOne(d => d.CedulaCompradorNavigation).WithMany(p => p.TDocsCompraventaFincaCedulaCompradorNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDCF_cedula_comprador");

            entity.HasOne(d => d.CedulaVendedorNavigation).WithMany(p => p.TDocsCompraventaFincaCedulaVendedorNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDCF_cedula_vendedor");

            entity.HasOne(d => d.DistritoFincaNavigation).WithMany(p => p.TDocsCompraventaFincaDistritoFincaNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDCF_distrito_finca");

            entity.HasOne(d => d.LugarFirmaNavigation).WithMany(p => p.TDocsCompraventaFincaLugarFirmaNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDCF_lugar_firma");
        });

        modelBuilder.Entity<TDocsContratoPrestacionServicio>(entity =>
        {
            entity.HasKey(e => e.IdDocumento).HasName("PK__T_DocsCo__B79DF372EAA42CDE");

            entity.HasOne(d => d.CedulaAbogadoNavigation).WithMany(p => p.TDocsContratoPrestacionServicios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDPS_cedula_abogado");

            entity.HasOne(d => d.CedulaClienteNavigation).WithMany(p => p.TDocsContratoPrestacionServicios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDPS_cedula_cliente");

            entity.HasOne(d => d.CiudadFirmaNavigation).WithMany(p => p.TDocsContratoPrestacionServicios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDPS_ciudad_firma");

            entity.HasOne(d => d.ProvinciaNavigation).WithMany(p => p.TDocsContratoPrestacionServicios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDPS_provincia");
        });

        modelBuilder.Entity<TDocsInscripcionVehiculo>(entity =>
        {
            entity.HasKey(e => e.IdDocumento).HasName("PK__T_DocsIn__B79DF372AC8F0937");

            entity.HasOne(d => d.CedulaAbogadoNavigation).WithMany(p => p.TDocsInscripcionVehiculos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDIV_cedula_abogado");

            entity.HasOne(d => d.CedulaClienteNavigation).WithMany(p => p.TDocsInscripcionVehiculos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDIV_cedula_cliente");

            entity.HasOne(d => d.EstiloVehiculoNavigation).WithMany(p => p.TDocsInscripcionVehiculos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDIV_estilo_vehiculo");

            entity.HasOne(d => d.LugarFirmaNavigation).WithMany(p => p.TDocsInscripcionVehiculos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDIV_lugar_firma");

            entity.HasOne(d => d.MarcaVehiculoNavigation).WithMany(p => p.TDocsInscripcionVehiculos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDIV_marca_vehiculo");
        });

        modelBuilder.Entity<TDocsMarcaVehiculo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_T_MarcaVehiculo");
        });

        modelBuilder.Entity<TDocsOpcionCompraventaVehiculo>(entity =>
        {
            entity.HasKey(e => e.IdDocumento).HasName("PK__T_DocsOp__B79DF372A755DA7E");

            entity.HasOne(d => d.CedulaAbogadoNavigation).WithMany(p => p.TDocsOpcionCompraventaVehiculos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDOCV_cedula_abogado");

            entity.HasOne(d => d.CedulaCompradorNavigation).WithMany(p => p.TDocsOpcionCompraventaVehiculoCedulaCompradorNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDOCV_cedula_comprador");

            entity.HasOne(d => d.CedulaPropietarioNavigation).WithMany(p => p.TDocsOpcionCompraventaVehiculoCedulaPropietarioNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDOCV_cedula_propietario");

            entity.HasOne(d => d.CombustibleNavigation).WithMany(p => p.TDocsOpcionCompraventaVehiculos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDOCV_combustible");

            entity.HasOne(d => d.LugarFirmaNavigation).WithMany(p => p.TDocsOpcionCompraventaVehiculos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDOCV_lugar_firma");

            entity.HasOne(d => d.MarcaMotorNavigation).WithMany(p => p.TDocsOpcionCompraventaVehiculoMarcaMotorNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDOCV_marca_motor");

            entity.HasOne(d => d.MarcaVehiculoNavigation).WithMany(p => p.TDocsOpcionCompraventaVehiculoMarcaVehiculoNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDOCV_marca_vehiculo");

            entity.HasOne(d => d.TipoVehiculoNavigation).WithMany(p => p.TDocsOpcionCompraventaVehiculos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDOCV_tipo_vehiculo");
        });

        modelBuilder.Entity<TDocsPagare>(entity =>
        {
            entity.HasKey(e => e.IdDocumento).HasName("PK__T_DocsPa__B79DF372BE9D476B");

            entity.HasOne(d => d.CedulaDeudorNavigation).WithMany(p => p.TDocsPagareCedulaDeudorNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_DocsPagare_T_GePersonas");

            entity.HasOne(d => d.CedulaFiadorNavigation).WithMany(p => p.TDocsPagareCedulaFiadorNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_DocsPagare_T_GePersonas1");

            entity.HasOne(d => d.LugarPagoNavigation).WithMany(p => p.TDocsPagares)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_DocsPagare_T_CrDistritos");
        });

        modelBuilder.Entity<TDocsPoderesEspecialesJudiciale>(entity =>
        {
            entity.HasOne(d => d.IdAbogadoNavigation).WithMany(p => p.TDocsPoderesEspecialesJudiciales)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_DocsPoderesEspecialesJudiciales_T_GeAbogados");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.TDocsPoderesEspecialesJudiciales)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_DocsPoderesEspecialesJudiciales_T_GePersonas");
        });

        modelBuilder.Entity<TDocsTipoVehiculo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_T_TipoVehiculo");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TGeAbogado>(entity =>
        {
            entity.Property(e => e.Cedula).ValueGeneratedNever();

            entity.HasOne(d => d.CJuridicaNavigation).WithMany(p => p.TGeAbogados)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_GeAbogados_T_GeNegocios");

            entity.HasOne(d => d.CedulaNavigation).WithOne(p => p.TGeAbogado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_GeAbogados_T_GePersonas");

            entity.HasOne(d => d.IdTipoAbogadoNavigation).WithMany(p => p.TGeAbogados)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_GeAbogados_T_GeAbogadoTipo");
        });

        modelBuilder.Entity<TGeNegocio>(entity =>
        {
            entity.HasKey(e => e.CJuridica).HasName("PK_T_Negocios");
        });

        modelBuilder.Entity<TGePersona>(entity =>
        {
            entity.HasKey(e => e.Cedula).HasName("PK_T_Pesonas");

            entity.Property(e => e.Cedula).ValueGeneratedNever();

            entity.HasOne(d => d.Direccion1Navigation).WithMany(p => p.TGePersonas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_GePersonas_T_CrDistritos");
        });

        modelBuilder.Entity<TGeRedesSociale>(entity =>
        {
            entity.HasOne(d => d.CedulaNavigation).WithMany(p => p.TGeRedesSociales)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_GeRedesSociales_T_GeAbogados");
        });

        modelBuilder.Entity<TTestimonio>(entity =>
        {
            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.TTestimonios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Testimonios_T_GePersonas");

            entity.Property(e => e.Activo)
                .IsRequired();

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
