using Preacepta.Modelos.AbstraccionesBD;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class GePersonaDTO
    {
        [Required(ErrorMessage = "Ingrese al cédula de la persona")]
        [DisplayName("Cédula")]
        public int Cedula { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre de la persona")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese el primer apellido de la persona")]
        [DisplayName("Primer apellido")]
        public string Apellido1 { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese el segundo apellido de la persona")]
        [DisplayName("Segundo Apellido")]
        public string Apellido2 { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese la fecha de nacimiento de la persona")]
        [DisplayName("Fecha de nacimiento")]
        public string FechaNacimiento { get; set; }


        public int Edad { get; set; }

        [Required(ErrorMessage = "Seleccione el estado civil de la persona")]
        [DisplayName("Estado Civil")]
        public string EstadoCivil { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese la ocupación de la persona")]
        [DisplayName("Ocupación")]
        public string Oficio { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese la ubicación de la persona")]
        [DisplayName("Vecino de")]
        public int Direccion1 { get; set; }

        [Required(ErrorMessage = "Ingrese la dirección exacta")]
        [DisplayName("Dirección exacta")]
        public string Direccion2 { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese un primer contacto")]
        [DisplayName("Telefono 1")]
        public string Telefono1 { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese un segundo contacto")]
        [DisplayName("Telefono 2")]
        public string? Telefono2 { get; set; }

        [DisplayName("Fecha de registro")]
        public string? FechaRegistro { get; set; }

        public bool Activo { get; set; }

        //public string Email { get; set; } = null!;

        [DisplayName("Vecino de")]
        public virtual TCrDistrito? Direccion1Navigation { get; set; } = null!;

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


        /*estas variables forman parte del modulo de auntenticacion y se colocan aca porque el modelo persona es el encargado de llevar el registro total*/
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }



    }
}
