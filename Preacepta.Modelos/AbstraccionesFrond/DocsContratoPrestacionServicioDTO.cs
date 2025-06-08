using Preacepta.Modelos.AbstraccionesBD;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class DocsContratoPrestacionServicioDTO
    {
        [DisplayName("ID del Documento")]
        public int IdDocumento { get; set; }

        [DisplayName("Razón Social de la Empresa")]
        [Required(ErrorMessage = "Debe ingresar la razón social de la empresa")]
        [StringLength(255)]
        public string RazonSocialEmpresa { get; set; } = null!;

        [DisplayName("Provincia")]
        public int Provincia { get; set; }

        [DisplayName("Cédula Jurídica de la Empresa")]
        [Required(ErrorMessage = "Debe ingresar la cédula jurídica")]
        [StringLength(20)]
        public string CedulaJuridicaEmpresa { get; set; } = null!;

        [DisplayName("Cédula del Abogado")]
        public int CedulaAbogado { get; set; }

        [DisplayName("Cédula del Cliente")]
        public int CedulaCliente { get; set; }

        [DisplayName("Tipo de Servicios")]
        [Required(ErrorMessage = "Debe especificar el tipo de servicios")]
        [StringLength(255)]
        public string TipoServicios { get; set; } = null!;

        [DisplayName("Fecha de Inicio")]
        [DataType(DataType.Date)]
        public DateOnly FechaInicio { get; set; }

        [DisplayName("Fecha Final")]
        [DataType(DataType.Date)]
        public DateOnly FechaFinal { get; set; }

        [DisplayName("Monto de Honorarios")]
        [DataType(DataType.Currency)]
        public decimal MontoHonorarios { get; set; }

        [DisplayName("Información Confidencial")]
        [Required(ErrorMessage = "Debe agregar la información confidencial")]
        public string InformacionConfidencial { get; set; } = null!;

        [DisplayName("Ciudad de Firma")]
        public int CiudadFirma { get; set; }

        [DisplayName("Hora de Firma")]
        public TimeOnly HoraFirma { get; set; }

        [DisplayName("Fecha de Firma")]
        [DataType(DataType.Date)]
        public DateOnly FechaFirma { get; set; }

        [DisplayName("Abogado")]
        public virtual TGeAbogado CedulaAbogadoNavigation { get; set; } = null!;

        [DisplayName("Cliente")]
        public virtual TGePersona CedulaClienteNavigation { get; set; } = null!;

        [DisplayName("Ciudad")]
        public virtual TCrDistrito CiudadFirmaNavigation { get; set; } = null!;

        [DisplayName("Provincia")]
        public virtual TCrProvincia ProvinciaNavigation { get; set; } = null!;
    }
}
