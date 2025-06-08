using Preacepta.Modelos.AbstraccionesBD;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class DocsCombustibleDTO
    {
        [Required(ErrorMessage = "Debe de proporcionar el identificador del tipo de combustible")]
        [DisplayName("ID")]
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "El nombre no debe exceder los 50 caracteres")]
        [DisplayName("Nombre del combustible")]
        public string? Nombre { get; set; }

        [DisplayName("Documentos de opción de compraventa del vehículo")]
        public virtual ICollection<TDocsOpcionCompraventaVehiculo> TDocsOpcionCompraventaVehiculos { get; set; } = new List<TDocsOpcionCompraventaVehiculo>();
    }
}
