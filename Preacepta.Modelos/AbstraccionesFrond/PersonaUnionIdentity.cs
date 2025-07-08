using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class PersonaUnionIdentity
    {
        public IndexModel? indexMdel { get; set; }

        public GePersonaDTO? personaDTO { get; set; }

        public GeAbogadoDTO? abogadoDTO { get; set; }
    }
}
