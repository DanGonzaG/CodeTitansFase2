using Preacepta.Modelos.AbstraccionesBD;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class GeRedesSocialeDTO
    {
        [DisplayName("Identificador")]
        public int IdRs { get; set; }

        [Required(ErrorMessage = "Debe de seleccionar un abogado")]
        [DisplayName("Cédula")]
        public int Cedula { get; set; }

        [Required(ErrorMessage = "Debe de agregar un link de acceso a las redes sociales")]
        [DisplayName("Link")]
        [MaxLength(5000, ErrorMessage = "El nombre no debe exceder los 5000 caracteres")]
        public string LinkRedSocila { get; set; } = null!;

        public virtual TGeAbogado? CedulaNavigation { get; set; } = null!;
    }
}
