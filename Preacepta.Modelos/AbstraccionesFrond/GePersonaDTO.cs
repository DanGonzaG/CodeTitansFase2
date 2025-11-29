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
        [MaxLength(50, ErrorMessage = "El nombre no debe exceder los 50 caracteres")]
        [DisplayName("Nombre")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese el primer apellido de la persona")]
        [MaxLength(50, ErrorMessage = "El apellido no debe exceder los 50 caracteres")]
        [DisplayName("Primer apellido")]
        public string Apellido1 { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese el segundo apellido de la persona")]
        [MaxLength(50, ErrorMessage = "El segundo apellido no debe exceder los 50 caracteres")]
        [DisplayName("Segundo Apellido")]
        public string Apellido2 { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese la fecha de nacimiento de la persona")]
        [MaxLength(50, ErrorMessage = "La fecha no debe exceder los 50 caracteres")]
        [DisplayName("Fecha de nacimiento")]
        public string FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Debe ingresar una edad")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "Seleccione el estado civil de la persona")]
        [MaxLength(50, ErrorMessage = "El estado civil no debe exceder los 50 caracteres")]
        [DisplayName("Estado Civil")]
        public string EstadoCivil { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese la ocupación de la persona")]
        [MaxLength(50, ErrorMessage = "La ocupación no debe exceder los 50 caracteres")]
        [DisplayName("Ocupación")]
        public string Oficio { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese el distrito de la persona")]
        [DisplayName("Vecino de")]
        public int Direccion1 { get; set; }

        [Required(ErrorMessage = "Ingrese la dirección exacta")]
        [MaxLength(500, ErrorMessage = "El la direccion no debe exceder los 500 caracteres")]
        [DisplayName("Dirección exacta")]
        public string Direccion2 { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese un primer contacto")]
        [MaxLength(50, ErrorMessage = "El número de celular no debe exceder los 50 caracteres")]
        [DisplayName("Celular")]
        public string Telefono1 { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese un segundo contacto")]
        [MaxLength(50, ErrorMessage = "El contacto no debe exceder los 50 caracteres")]
        [DisplayName("Otro Contacto")]
        public string? Telefono2 { get; set; }

        [DisplayName("Fecha de registro")]
        public string? FechaRegistro { get; set; }

        public bool Activo { get; set; }

        [Required(ErrorMessage = "Seleccione un género")]
        [MaxLength(10, ErrorMessage = "El género no debe exceder los 10 caracteres")]
        public string Genero { get; set; } = null!;

        public string? ExpirationPassword { get; set; }

        [Required(ErrorMessage = "Debe de seleccionar el tipo de Identificación")]
        [MaxLength(100, ErrorMessage = "El tipo de identificación no debe exceder los 100 caracteres")]
        [DisplayName("Tipo de identificación")]
        public string? TipoIdentificacion { get; set; }

        [Required(ErrorMessage = "Debe de ingresar un número de Identificación")]
        [MaxLength(100, ErrorMessage = "El tipo de identificación no debe exceder los 100 caracteres")]
        [DisplayName("No. Idenficación")]
        public string? NumCedula { get; set; }

        //[Required]
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
        [Required (ErrorMessage ="El correo es un dato requerido")]
        [EmailAddress (ErrorMessage = "Correo no válido debe de tener @")]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Debe ingresar un contraseña")]
        [StringLength(18, ErrorMessage = "El {0} debe tener al menos {2} y como máximo {1} caracteres de longitud.", MinimumLength = 12)]
        [RegularExpression (@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",ErrorMessage ="La contraseña debe de tener al menos un numero, una mayuscula y un símbolo")] 

        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirmación de contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string? ConfirmPassword { get; set; }
        


    }
}
