using Preacepta.AD.GeAbogadoTipo.Eliminar;
using Preacepta.AD.GePersona.Eliminar;
using Preacepta.LN.GePersona.Eliminar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeAbogadoTipo.Eliminar
{
    public class EliminarAbogadoTipoLN : IEliminarAbogadoTipoLN
    {
            private readonly IEliminarAbogadoTipoAD _eliminar;

            public EliminarAbogadoTipoLN(IEliminarAbogadoTipoAD eliminar)
            {
                _eliminar = eliminar;
            }

            public async Task<int> eliminar(int id)
            {
                if (id < 0)
                {
                    Console.WriteLine("el valor de id en menor a 1");
                    return 0;
                }
                try
                {
                    int bandera = await _eliminar.eliminar(id);
                    return bandera;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en: EliminarPersonaLN {ex.Message}");
                    return -1;
                }


            }
        }
    }

