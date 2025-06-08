using Preacepta.Modelos.AbstraccionesBD;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class CrProvinciaDTO
    {
        [Required(ErrorMessage = "Debe ingresar el código de provincia")]
        [DisplayName("Código Provicia")]
        public int IdProvincia { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre de la provincia")]
        [DisplayName("Nombre")]
        public string? NombreProvincia { get; set; }


        public virtual ICollection<TCrCantone> TCrCantones { get; set; } = new List<TCrCantone>();


        public virtual ICollection<TDocsContratoPrestacionServicio> TDocsContratoPrestacionServicios { get; set; } = new List<TDocsContratoPrestacionServicio>();
    }
}
