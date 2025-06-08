using Preacepta.Modelos.AbstraccionesBD;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class CrDistritoDTO
    {
        [Required(ErrorMessage = "Debe ingresar el código de distrito")]
        [DisplayName("Código Distrito")]
        public int IdDistrito { get; set; }

        [Required(ErrorMessage = "Debe ingresar el código de cantón")]
        [DisplayName("Código Cantón")]
        public int IdCaton { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre del distrito")]
        [DisplayName("Nombre")]
        public string NombreDistrito { get; set; } = null!;


        [DisplayName("Código Cantón")]
        public virtual TCrCantone? IdCatonNavigation { get; set; } = null!;

        public virtual ICollection<TDocsCompraventaFinca> TDocsCompraventaFincaDistritoFincaNavigations { get; set; } = new List<TDocsCompraventaFinca>();

        public virtual ICollection<TDocsCompraventaFinca> TDocsCompraventaFincaLugarFirmaNavigations { get; set; } = new List<TDocsCompraventaFinca>();

        public virtual ICollection<TDocsContratoPrestacionServicio> TDocsContratoPrestacionServicios { get; set; } = new List<TDocsContratoPrestacionServicio>();

        public virtual ICollection<TDocsInscripcionVehiculo> TDocsInscripcionVehiculos { get; set; } = new List<TDocsInscripcionVehiculo>();

        public virtual ICollection<TDocsOpcionCompraventaVehiculo> TDocsOpcionCompraventaVehiculos { get; set; } = new List<TDocsOpcionCompraventaVehiculo>();

        public virtual ICollection<TDocsPagare> TDocsPagares { get; set; } = new List<TDocsPagare>();

        public virtual ICollection<TGePersona> TGePersonas { get; set; } = new List<TGePersona>();
    }
}
