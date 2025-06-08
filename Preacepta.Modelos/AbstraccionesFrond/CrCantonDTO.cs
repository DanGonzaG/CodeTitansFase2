using Preacepta.Modelos.AbstraccionesBD;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class CrCantonDTO
    {
        [Required(ErrorMessage = "Debe ingresar el código de cantón")]
        [DisplayName("Código Cantón")]
        public int IdCanton { get; set; }

        [Required(ErrorMessage = "Seleccione el código de provincia")]
        [DisplayName("Código Provincia")]
        public int IdProvincia { get; set; }


        [Required(ErrorMessage = "Digite el nombre del cantón")]
        [DisplayName("Nombre")]
        [MaxLength(100, ErrorMessage = "Solo puede ingresar 100 caracteres")]
        public string? NombreCanton { get; set; }


        [DisplayName("Provincia")]
        public virtual TCrProvincia? IdProvinciaNavigation { get; set; }

        [InverseProperty("IdCatonNavigation")]
        public virtual ICollection<TCrDistrito> TCrDistritos { get; set; } = new List<TCrDistrito>();
    }
}
