using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class ContactoModel
    {
        [Required(ErrorMessage = "Debe agregar su cédula")]
        [DisplayName("Cédula")]
        public string cedula { get; set; }
        [Required(ErrorMessage = "Debe agregar su nombre completo")]
        [DisplayName("Nombre Completo")]
        public string name { get; set; }
        [Required(ErrorMessage = "Debe agregar un correo electrónico")]
        [EmailAddress]
        [DisplayName("Correo electrónico")]
        public string email { get; set; }
        [Required(ErrorMessage = "Debe agregar un número telefónico")]
        [Phone]
        [DisplayName("Número telefónico")]
        public string phone_number { get; set; }

        [Required(ErrorMessage = "Debe agregar una descripción")]
        [DisplayName("Descripción del caso")]
        public string descripcionCaso { get; set; }

        [Required(ErrorMessage = "Debe aceptar los términos y condiciones")]
        public bool TerminosCondiciones { get; set; }
    }
}
