using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Testimonios.Listar
{
    public interface IListarTestLN
    {
        Task<List<TTestimonioDTO>> Listar();
        Task<List<TTestimonioDTO>> ListarTodosSinFiltro();
    }
}
