using Preacepta.Modelos.AbstraccionesBD;
using System.ComponentModel.DataAnnotations;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class DocsPagareDTO
    {
        [Display(Name = "ID del Documento")]
        public int IdDocumento { get; set; }

        [Required(ErrorMessage = "El monto numérico es requerido.")]
        [Range(0.01, 999999999999.99, ErrorMessage = "El monto debe ser mayor que cero.")]
        [Display(Name = "Monto del Pagaré")]
        public decimal MontoNumerico { get; set; }

        [Display(Name = "Cédula Deudor")]
        public int CedulaDeudor { get; set; }

        [Display(Name = "Sociedad Deudor")]
        [MaxLength(150, ErrorMessage = "Capacidad de Sociedad Deudor excedida")]
        public string SociedadDeudor { get; set; } = null!;

        [Display(Name = "Cédula Jurídica de la Sociedad")]
        [MaxLength(20, ErrorMessage = "Capacidad de Cedula JuridicaSociedad excedida")]
        public string CedulaJuridicaSociedad { get; set; } = null!;

        [Display(Name = "Nombre del Acreedor")]
        [MaxLength(150, ErrorMessage = "Capacidad de Acreedor Nombre excedida")]
        public string AcreedorNombre { get; set; } = null!;

        [Display(Name = "Cédula Jurídica del Acreedor")]
        [Required(ErrorMessage = "La cédula jurídica del acreedor es requerida.")]
        [MaxLength(20, ErrorMessage = "Capacidad de CedulaJuridicaAcreedor excedida")]
        public string CedulaJuridicaAcreedor { get; set; } = null!;

        [Display(Name = "Domicilio del Acreedor")]
        public string AcreedorDomicilio { get; set; } = null!;

        [Display(Name = "Fecha de Firma")]
        public string? FechaFirma { get; set; }

        [Display(Name = "Hora de Firma")]
        public string? HoraFirma { get; set; }

        [Display(Name = "Fecha de Vencimiento")]
        public string FechaVencimiento { get; set; } = null!;

        [Display(Name = "Fórmula de Interés")]
        public string InteresFormula { get; set; } = null!;

        [Required(ErrorMessage = "La tasa de interés actual es requerida.")]
        [Range(0.01, 100.0, ErrorMessage = "La tasa de interés debe ser mayor a 0.")]
        [Display(Name = "Tasa de Interés Actual")]
        public decimal InteresTasaActual { get; set; }

        [Display(Name = "Interés Base")]
        [MaxLength(100, ErrorMessage = "Capacidad de InteresBase excedida")]
        public string InteresBase { get; set; } = null!;

        [Required(ErrorMessage = "Debe seleccionar un distrito.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un distrito válido.")]
        [Display(Name = "Lugar de Pago")]
        public int LugarPago { get; set; }

        [Required(ErrorMessage = "La cédula del fiador es requerida.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un fiador válido.")]
        [Display(Name = "Cédula Fiador")]
        public int CedulaFiador { get; set; }

        [Required(ErrorMessage = "La ubicación de la firma es requerida.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar una ubicación válida.")]
        [Display(Name = "Ubicación de Firma")]
        public int UbicacionFirma { get; set; }

        [Required(ErrorMessage = "La cédula del abogado es requerida.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un abogado válido.")]
        [Display(Name = "Cédula Abogado")]
        public int CedulaAbogado { get; set; }

        [Display(Name = "Tipo de Sociedad")]
        [MaxLength(1000, ErrorMessage = "Capacidad de Tipo de Sociedad excedida")]
        public string? TipoSociedad { get; set; }

        [Display(Name = "Ubicación de la Sociedad")]
        [MaxLength(100, ErrorMessage = "Capacidad de Ubicación de la Sociedad excedida")]
        public string? UbicacionSociedad { get; set; }

        [Display(Name = "Cedula del deudor en Navigation")]
        public virtual TGePersona? CedulaDeudorNavigation { get; set; }

        [Display(Name = "Cedula del Fiador en Navigation")]
        public virtual TGePersona? CedulaFiadorNavigation { get; set; }

        [Display(Name = "Lugar de pago en Navigation")]
        public virtual TCrDistrito? LugarPagoNavigation { get; set; }

        public virtual TGeAbogado? CedulaAbogadoNavigation { get; set; }
    }
}
