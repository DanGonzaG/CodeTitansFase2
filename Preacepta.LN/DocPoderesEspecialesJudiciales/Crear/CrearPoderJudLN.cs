using Preacepta.AD.DocPoderesEspecialesJudiciales.Crear;
using Preacepta.LN.DocPoderesEspecialesJudiciales.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocPoderesEspecialesJudiciales.Crear
{
    public class CrearPoderJudLN : ICrearPoderJudLN
    {
        private readonly ICrearPoderJudAD _crear;
        private readonly IObtenerDatosPoderJudLN _obtenerDatos;

        public CrearPoderJudLN(ICrearPoderJudAD crear, IObtenerDatosPoderJudLN obtenerDatos)
        {
            _crear = crear;
            _obtenerDatos = obtenerDatos;
        }

        public async Task<int> crear(DocsPoderesEspecialesJudicialeDTO poderJudDTO)
        {
            if (poderJudDTO == null)
            {
                Console.WriteLine("Error: Objeto nulo.");
                return 0;
            }

            try
            {
                var entidad = _obtenerDatos.ObtenerDeFront(poderJudDTO);
                var idNuevo = await _crear.crear(entidad);
                if (idNuevo <= 0)
                {
                    Console.WriteLine("Creación no exitosa.");
                    return 0;
                }
                return idNuevo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearPoderJudLN: {ex.Message}");
                return -1;
            }
        }
    }
}
