using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class GePersonaDTO
    {
        public int Cedula { get; set; }
        
        public string Nombre { get; set; } = null!;
        
        public string Apellido1 { get; set; } = null!;
        
        public string Apellido2 { get; set; } = null!;

        public DateOnly FechaNacimiento { get; set; }

        public int Edad { get; set; }
       
        public string EstadoCivil { get; set; } = null!;
       
        public string Oficio { get; set; } = null!;

        public int Direccion1 { get; set; }
       
        public string Direccion2 { get; set; } = null!;
       
        public string Telefono1 { get; set; } = null!;
       
        public string? Telefono2 { get; set; }
        
        public DateTime FechaRegistro { get; set; }

        public bool Activo { get; set; }
        
        public string Email { get; set; } = null!;
        
        public virtual TCrDistrito Direccion1Navigation { get; set; } = null!;
       
        public virtual ICollection<TCaso> TCasos { get; set; } = new List<TCaso>();
       
        public virtual ICollection<TCitasCliente> TCitasClientes { get; set; } = new List<TCitasCliente>();
       
        public virtual ICollection<TDocsAutorizacionRevisionExpediente> TDocsAutorizacionRevisionExpedienteCedulaAsistenteNavigations { get; set; } = new List<TDocsAutorizacionRevisionExpediente>();
        
        public virtual ICollection<TDocsAutorizacionRevisionExpediente> TDocsAutorizacionRevisionExpedienteCedulaImputadoNavigations { get; set; } = new List<TDocsAutorizacionRevisionExpediente>();
       
        public virtual ICollection<TDocsCompraventaFinca> TDocsCompraventaFincaCedulaCompradorNavigations { get; set; } = new List<TDocsCompraventaFinca>();
        
        public virtual ICollection<TDocsCompraventaFinca> TDocsCompraventaFincaCedulaVendedorNavigations { get; set; } = new List<TDocsCompraventaFinca>();
        
        public virtual ICollection<TDocsContratoPrestacionServicio> TDocsContratoPrestacionServicios { get; set; } = new List<TDocsContratoPrestacionServicio>();
        
        public virtual ICollection<TDocsInscripcionVehiculo> TDocsInscripcionVehiculos { get; set; } = new List<TDocsInscripcionVehiculo>();
        
        public virtual ICollection<TDocsOpcionCompraventaVehiculo> TDocsOpcionCompraventaVehiculoCedulaCompradorNavigations { get; set; } = new List<TDocsOpcionCompraventaVehiculo>();
        
        public virtual ICollection<TDocsOpcionCompraventaVehiculo> TDocsOpcionCompraventaVehiculoCedulaPropietarioNavigations { get; set; } = new List<TDocsOpcionCompraventaVehiculo>();
       
        public virtual ICollection<TDocsPagare> TDocsPagareCedulaDeudorNavigations { get; set; } = new List<TDocsPagare>();
       
        public virtual ICollection<TDocsPagare> TDocsPagareCedulaFiadorNavigations { get; set; } = new List<TDocsPagare>();
        
        public virtual ICollection<TDocsPoderesEspecialesJudiciale> TDocsPoderesEspecialesJudiciales { get; set; } = new List<TDocsPoderesEspecialesJudiciale>();
       
        public virtual TGeAbogado? TGeAbogado { get; set; }
        
        public virtual ICollection<TTestimonio> TTestimonios { get; set; } = new List<TTestimonio>();
    }
}
