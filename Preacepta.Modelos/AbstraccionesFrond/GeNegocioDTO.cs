using Preacepta.Modelos.AbstraccionesBD;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class GeNegocioDTO
    {
        [DisplayName("Cédula Jurídica")]
        public int CJuridica { get; set; }

        [Required(ErrorMessage = "Debe de agregar un nombre")]
        [MaxLength(50, ErrorMessage = "El nombre no debe exceder los 50 caracteres")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "Debe de agregar un número de teléfono")]
        [DisplayName("Teléfono")]
        [MaxLength(50, ErrorMessage = "El nombre no debe exceder los 50 caracteres")]
        public string Telefono { get; set; } = null!;

        [Required(ErrorMessage = "Debe de agragar un correo electrónico")]
        [MaxLength(50,ErrorMessage = "El nombre no debe exceder los 50 caracteres")]
        [DisplayName("Correo electrónico")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Debe de agragar un representante o dueño del despacho")]
        [MaxLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres")]
        public string? Representante { get; set; }


        [DisplayName("Fecha de Creación")]
        public string FechaConsolidacion { get; set; }

        [Required(ErrorMessage = "Debe de seleccionar un distrito")]
        [DisplayName("Ubicacion")]
        public int Direccion1 { get; set; }

        [Required(ErrorMessage = "Debe de agragar un representante o dueño del despacho")]
        [DisplayName("Direccion exacta")]
        [MaxLength(500, ErrorMessage = "El nombre no debe exceder los 500 caracteres")]
        public string Direccion2 { get; set; } = null!;

        public virtual ICollection<TGeAbogado> TGeAbogados { get; set; } = new List<TGeAbogado>();
    }
}
