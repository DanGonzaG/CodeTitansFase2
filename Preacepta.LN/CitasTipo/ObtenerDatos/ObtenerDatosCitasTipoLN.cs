using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CitasTipo.ObtenerDatos

{
    public class ObtenerDatosCitasTipoLN : IObtenerDatosCitasTipoLN
    {
        private readonly Contexto _contexto;
        public ObtenerDatosCitasTipoLN(Contexto contexto)
        {
            _contexto = contexto;
        }
        public CitasTipoDTO ObtenerDeDB(TCitasTipo baseDatos)
        {
            return new CitasTipoDTO
            {
                 Id = baseDatos.Id,
                 Nombre = baseDatos.Nombre
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TCitasTipo ObtenerDeFront(CitasTipoDTO Formulario)
        {
            return new TCitasTipo
            {
                Id = Formulario.Id,
                Nombre = Formulario.Nombre
            };
        }
        public async Task<List<CitasTipoDTO>> ListarTiposAsync()
        {
            var tipos = await _contexto.TCitasTipos.ToListAsync();
            return tipos.Select(t => ObtenerDeDB(t)).ToList();
        }

    }
}
