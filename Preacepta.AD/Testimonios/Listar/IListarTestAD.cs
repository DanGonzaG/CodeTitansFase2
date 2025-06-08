using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Testimonios.Listar
{
    public interface IListarTestAD
    {
        Task<List<TTestimonioDTO>> Listar();
        Task<List<TTestimonioDTO>> listar2();
    }
}
