using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.Modelos.AbstraccionesFrond.ViewModelsFront
{
    public class PersonaParcialDatosPersonales
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

        [Required(ErrorMessage = "Seleccione un género")]
        public string Genero { get; set; } = null!;
    }
}
