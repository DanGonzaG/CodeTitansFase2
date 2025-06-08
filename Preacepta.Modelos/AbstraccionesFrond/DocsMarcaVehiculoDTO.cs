using Preacepta.Modelos.AbstraccionesBD;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class DocsMarcaVehiculoDTO
    {
        [DisplayName("ID")]
        [Required(ErrorMessage = "Debe indicar el ID de la marca")]
        public int Id { get; set; }

        [DisplayName("Nombre de la Marca")]
        [Required(ErrorMessage = "Debe ingresar el nombre de la marca")]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        [DisplayName("Inscripciones de Vehículo")]
        public virtual ICollection<TDocsInscripcionVehiculo> TDocsInscripcionVehiculos { get; set; } = new List<TDocsInscripcionVehiculo>();

        [DisplayName("Opciones de Compraventa como Marca del Motor")]
        public virtual ICollection<TDocsOpcionCompraventaVehiculo> TDocsOpcionCompraventaVehiculoMarcaMotorNavigations { get; set; } = new List<TDocsOpcionCompraventaVehiculo>();

        [DisplayName("Opciones de Compraventa como Marca del Vehículo")]
        public virtual ICollection<TDocsOpcionCompraventaVehiculo> TDocsOpcionCompraventaVehiculoMarcaVehiculoNavigations { get; set; } = new List<TDocsOpcionCompraventaVehiculo>();
    }
}
