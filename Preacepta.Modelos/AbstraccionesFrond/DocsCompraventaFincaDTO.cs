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
        [StringLength(50)]
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
        [StringLength(50)]
        public string PartidoFinca { get; set; } = null!;

        [DisplayName("Matrícula de la Finca")]
        [Required(ErrorMessage = "Debe ingresar la matrícula de la finca")]
        [StringLength(50)]
        public string MatriculaFinca { get; set; } = null!;

        [DisplayName("Naturaleza de la Finca")]
        [Required(ErrorMessage = "Debe especificar la naturaleza de la finca")]
        [StringLength(100)]
        public string NaturalezaFinca { get; set; } = null!;

        [DisplayName("Distrito de la Finca")]
        public int DistritoFinca { get; set; }

        [DisplayName("Cantón de la Finca")]
        public int CantonFinca { get; set; }

        [DisplayName("Provincia de la Finca")]
        public int ProvinciaFinca { get; set; }

        [DisplayName("Área de la Finca (m²)")]
        public decimal AreaFincaM2 { get; set; }

        [DisplayName("Plano Catastrado")]
        [StringLength(100)]
        public string PlanoCatastrado { get; set; } = null!;

        [DisplayName("Colinda al Norte")]
        public string ColindaNorte { get; set; } = null!;

        [DisplayName("Colinda al Sur")]
        public string ColindaSur { get; set; } = null!;

        [DisplayName("Colinda al Este")]
        public string ColindaEste { get; set; } = null!;

        [DisplayName("Colinda al Oeste")]
        public string ColindaOeste { get; set; } = null!;

        [DisplayName("Forma de Pago")]
        [StringLength(100)]
        public string FormaPago { get; set; } = null!;

        [DisplayName("Medio de Pago")]
        [StringLength(100)]
        public string MedioPago { get; set; } = null!;

        [DisplayName("Origen de Fondos")]
        public string OrigenFondos { get; set; } = null!;

        [DisplayName("Lugar de Firma")]
        public int LugarFirma { get; set; }

        [DisplayName("Hora de Firma")]
        public TimeOnly HoraFirma { get; set; }

        [DisplayName("Fecha de Firma")]
        public DateOnly FechaFirma { get; set; }

        [DisplayName("Abogado")]
        public virtual TGeAbogado CedulaAbogadoNavigation { get; set; } = null!;

        [DisplayName("Comprador")]
        public virtual TGePersona CedulaCompradorNavigation { get; set; } = null!;

        [DisplayName("Vendedor")]
        public virtual TGePersona CedulaVendedorNavigation { get; set; } = null!;

        [DisplayName("Distrito de la Finca")]
        public virtual TCrDistrito DistritoFincaNavigation { get; set; } = null!;

        [DisplayName("Lugar de Firma")]
        public virtual TCrDistrito LugarFirmaNavigation { get; set; } = null!;
    }
}
