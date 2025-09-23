using Preacepta.AD.DocsPagare.Eliminar;
using System;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsPagare.Eliminar
{
    public class EliminarPagareLN : IEliminarPagareLN
    {
        private readonly IEliminarPagareAD _eliminar;

        public EliminarPagareLN(IEliminarPagareAD eliminar)
        {
            _eliminar = eliminar;
        }

        public async Task<int> eliminar(int id)
        {
            if (id <= 0)
            {
                Console.WriteLine("EliminarPagareLN: el valor de id debe ser mayor a 0.");
                return 0;
            }

            try
            {
                int resultado = await _eliminar.eliminar(id);

                switch (resultado)
                {
                    case -2:
                        Console.WriteLine("EliminarPagareLN: No se pudo eliminar por referencias (FK).");
                        break;
                    case -1:
                        Console.WriteLine("EliminarPagareLN: Error inesperado en AD.");
                        break;
                    case 0:
                        Console.WriteLine("EliminarPagareLN: No se encontró el registro a eliminar.");
                        break;
                    default:
                        Console.WriteLine($"EliminarPagareLN: Eliminado correctamente (filas afectadas: {resultado}).");
                        break;
                }

                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EliminarPagareLN: excepción no controlada. Detalle: {ex.Message}");
                return -1;
            }
        }
    }
}
