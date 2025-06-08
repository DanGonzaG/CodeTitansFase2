using Preacepta.Modelos.AbstraccionesBD;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class CasosTipoDTO
    {
        [DisplayName("Id tipo de caso")]
        public int IdTipoCaso { get; set; }

        [Required(ErrorMessage = "Debe de agregar el nombre a nuevo tipo de caso")]
        public string Nombre { get; set; } = null!;
        public virtual ICollection<TCaso> TCasos { get; set; } = new List<TCaso>();
    }
}
