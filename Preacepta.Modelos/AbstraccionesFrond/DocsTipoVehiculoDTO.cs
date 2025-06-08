using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class DocsTipoVehiculoDTO
    {
        [Display(Name = "ID del Tipo Vehiculo")]
        [Required(ErrorMessage = "Dato requerido")]
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Capacidad de Nombre excedida")]
        public string Nombre { get; set; } = null!;

        public virtual ICollection<TDocsInscripcionVehiculo> TDocsInscripcionVehiculos { get; set; } = new List<TDocsInscripcionVehiculo>();

        public virtual ICollection<TDocsOpcionCompraventaVehiculo> TDocsOpcionCompraventaVehiculos { get; set; } = new List<TDocsOpcionCompraventaVehiculo>();
    }
}
