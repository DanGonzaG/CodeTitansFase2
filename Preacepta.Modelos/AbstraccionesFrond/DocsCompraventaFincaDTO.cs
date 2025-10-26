using Preacepta.Modelos.AbstraccionesBD;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class DocsCompraventaFincaDTO
    {
        [DisplayName("ID del Documento")]
        [Required(ErrorMessage = "Debe de proporcionar el ID del documento")]
        public int IdDocumento { get; set; }

        [DisplayName("Número de Escritura")]
        [Required(ErrorMessage = "Debe ingresar el número de escritura")]
        [MaxLength(50, ErrorMessage = "El nombre no debe exceder los 50 caracteres")]
        public string NumeroEscritura { get; set; } = null!;

        [DisplayName("Cédula del Abogado")]
        [Required(ErrorMessage = "Debe indicar la cédula del abogado")]
        public int CedulaAbogado { get; set; }

        [DisplayName("Cédula del Vendedor")]
        [Required(ErrorMessage = "Debe indicar la cédula del vendedor")]
        public int CedulaVendedor { get; set; }

        [DisplayName("Cédula del Comprador")]
        [Required(ErrorMessage = "Debe indicar la cédula del comprador")]
        public int CedulaComprador { get; set; }

        [DisplayName("Monto de la Venta")]
        [Required(ErrorMessage = "Debe ingresar el monto de la venta")]
        public decimal MontoVenta { get; set; }

        [DisplayName("Partido de la Finca")]
        [Required(ErrorMessage = "Debe ingresar el partido de la finca")]
        [MaxLength(50, ErrorMessage = "El nombre no debe exceder los 50 caracteres")]
        public string PartidoFinca { get; set; } = null!;

        [DisplayName("Matrícula de la Finca")]
        [Required(ErrorMessage = "Debe ingresar la matrícula de la finca")]
        [MaxLength(50, ErrorMessage = "El nombre no debe exceder los 50 caracteres")]
        public string MatriculaFinca { get; set; } = null!;

        [DisplayName("Naturaleza de la Finca")]
        [Required(ErrorMessage = "Debe especificar la naturaleza de la finca")]
        [MaxLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres")]
        public string NaturalezaFinca { get; set; } = null!;

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Distrito de la Finca")]
        public int DistritoFinca { get; set; }

        [DisplayName("Cantón de la Finca")]
        public int CantonFinca { get; set; }

        [DisplayName("Provincia de la Finca")]
        public int ProvinciaFinca { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Área de la Finca (m²)")]
        public decimal AreaFincaM2 { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Plano Catastrado")]
        [MaxLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres")]
        public string PlanoCatastrado { get; set; } = null!;

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Colinda al Norte")]
        [MaxLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres")]
        public string ColindaNorte { get; set; } = null!;

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Colinda al Sur")]
        [MaxLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres")]
        public string ColindaSur { get; set; } = null!;

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Colinda al Este")]
        [MaxLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres")]
        public string ColindaEste { get; set; } = null!;

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Colinda al Oeste")]
        [MaxLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres")]
        public string ColindaOeste { get; set; } = null!;

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Forma de Pago")]
        [MaxLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres")]
        public string FormaPago { get; set; } = null!;

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Medio de Pago")]
        [MaxLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres")]
        public string MedioPago { get; set; } = null!;

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Origen de Fondos")]
        [MaxLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres")]
        public string OrigenFondos { get; set; } = null!;

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Lugar de Firma")]
        public int LugarFirma { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Hora de Firma")]
        public TimeOnly HoraFirma { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Fecha de Firma")]
        [DataType(DataType.Date)]
        public DateOnly FechaFirma { get; set; }

        [DisplayName("Abogado")]
        public virtual TGeAbogado? CedulaAbogadoNavigation { get; set; } = null!;

        [DisplayName("Comprador")]
        public virtual TGePersona? CedulaCompradorNavigation { get; set; } = null!;

        [DisplayName("Vendedor")]
        public virtual TGePersona? CedulaVendedorNavigation { get; set; } = null!;

        [DisplayName("Distrito de la Finca")]
        public virtual TCrDistrito? DistritoFincaNavigation { get; set; } = null!;

        [DisplayName("Lugar de Firma")]
        public virtual TCrDistrito? LugarFirmaNavigation { get; set; } = null!;
    }
}
