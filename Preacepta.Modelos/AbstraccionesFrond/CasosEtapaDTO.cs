using Preacepta.Modelos.AbstraccionesBD;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class CasosEtapaDTO
    {
        [DisplayName("Num. Etapa Proceso")]
        public int IdEtapaPl { get; set; }

        public string? Fecha { get; set; }

        [Required(ErrorMessage = "Debe de agregar un titulo a la etapa del proceso legal")]
        [DisplayName("Nombre de Etapa")]
        [MaxLength(100, ErrorMessage = "El valor máximo es de 100 letras")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "Debe de agregar una descripción de la etapa del proceso legal")]
        [DisplayName("Descripción")]
        [MaxLength(5000, ErrorMessage = "El valor máximo es de 5000 letras")]
        public string Descripcion { get; set; } = null!;

        [DisplayName("Id Caso")]

        public int IdCaso { get; set; }

        public bool Activo { get; set; }

        [Required(ErrorMessage = "Debe agregar pruebas")]
        [MaxLength(500, ErrorMessage = "El valor máximo es de 500 letras")]
        public string Pruebas { get; set; }

        [DisplayName("Nombre Caso")]
        public virtual TCaso? IdCasoNavigation { get; set; } = null!;

        public virtual ICollection<TCasosEvidencia> TCasosEvidencia { get; set; } = new List<TCasosEvidencia>();
    }
}

