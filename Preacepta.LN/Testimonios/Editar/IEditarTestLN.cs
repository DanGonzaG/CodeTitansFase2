using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Testimonios.Editar
{
    public interface IEditarTestLN
    {
        Task<int> editar(TTestimonioDTO testDTO);
    }
}
