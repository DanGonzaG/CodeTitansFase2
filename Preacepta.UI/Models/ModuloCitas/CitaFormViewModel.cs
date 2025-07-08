using Microsoft.AspNetCore.Mvc.Rendering;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.UI.Models.ModuloCitas
{
    public class CitaFormViewModel
    {
        public CitasDTO Cita { get; set; }
        public List<SelectListItem> TiposCita { get; set; }
    }

}
