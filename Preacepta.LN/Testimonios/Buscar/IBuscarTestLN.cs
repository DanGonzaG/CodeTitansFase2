using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Testimonios.Buscar
{
    public interface IBuscarTestLN
    {
        Task<TTestimonioDTO?> buscar(int id);

        Task<GePersonaDTO?> BuscarClientePorId(int idCliente);
    }
}
