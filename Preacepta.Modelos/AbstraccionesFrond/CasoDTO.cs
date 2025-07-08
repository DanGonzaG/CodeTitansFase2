using Preacepta.Modelos.AbstraccionesBD;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class CasoDTO
    {
        [DisplayName("Identificador")]
        public int IdCaso { get; set; }

        [Required(ErrorMessage = "El nombre del caso es un dato requerido")]
        [DisplayName("Nombre del Caso")]
        public string Nombre { get; set; }

        public string? Fecha { get; set; }

        [DisplayName("Id Tipo de caso")]
        public int IdTipoCaso { get; set; }

        [Required(ErrorMessage = "Debe de agregar una descripción del caso")]
        public string Descripcion { get; set; } = null!;

        [Required(ErrorMessage = "Seleccione un funcionario")]
        [DisplayName("Id Abogado")]
        public int IdAbogado { get; set; }

        [DisplayName("Id Cliente")]
        public int IdCliente { get; set; }

        public bool Activo { get; set; }


        [DisplayName("Abogado")]
        public virtual TGeAbogado? IdAbogadoNavigation { get; set; } = null!;

        [DisplayName("Cliente")]
        public virtual TGePersona? IdClienteNavigation { get; set; } = null!;

        [DisplayName("Tipo de caso")]
        public virtual TCasosTipo? IdTipoCasoNavigation { get; set; } = null!;

        public virtual ICollection<TCasosEtapa> TCasosEtapas { get; set; } = new List<TCasosEtapa>();

        public virtual ICollection<TCasosEvidencia> TCasosEvidencia { get; set; } = new List<TCasosEvidencia>();
    }
}
