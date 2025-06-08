using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Testimonios.ObtenerDatos
{
    public interface IObtenerDatosTestLN
    {
        TTestimonioDTO ObtenerDeDB(TTestimonio test);
        TTestimonio ObtenerDeFront(TTestimonioDTO testDTO);
    }
}
