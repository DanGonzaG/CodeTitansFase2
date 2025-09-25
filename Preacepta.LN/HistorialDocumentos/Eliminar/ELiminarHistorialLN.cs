using Preacepta.AD.HistorialDocumentos.Eliminar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.HistorialDocumentos.Eliminar
{
    public class ELiminarHistorialLN : IELiminarHistorialLN
    {
        private readonly IELiminarHistorialAD _eliminar;

        public ELiminarHistorialLN(IELiminarHistorialAD eliminar)
        {
            _eliminar = eliminar;
        }

        public async Task<int> Eliminar(int id)
        {
            if (id <= 0)
            {
                Console.WriteLine("EliminarHistorialLN: id inválido.");
                return 0;
            }

            try
            {
                int bandera = await _eliminar.Eliminar(id);
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EliminarHistorialLN Error: {ex.Message}");
                return -1;
            }
        }
    }
}
