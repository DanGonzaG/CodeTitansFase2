using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Citas.ObtenerDatos

{

    public class ObtenerDatosCitasLN : IObtenerDatosCitasLN
    {
        private readonly Contexto _contexto;
        public ObtenerDatosCitasLN(Contexto contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }
        public CitasDTO ObtenerDeDB(TCita baseDatos)
        {
            return new CitasDTO
            {
                IdCita = baseDatos.IdCita,
                Fecha = baseDatos.Fecha,
                Hora = baseDatos.Hora,
                IdTipoCita = baseDatos.IdTipoCita,
                LinkVideo = baseDatos.LinkVideo,
                Anfitrion = baseDatos.Anfitrion,
                NombreTipoCita = baseDatos.IdTipoCitaNavigation?.Nombre
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TCita ObtenerDeFront(CitasDTO Formulario)
        {
            return new TCita
            {
                IdCita = Formulario.IdCita,
                Fecha = Formulario.Fecha,
                Hora = Formulario.Hora,
                IdTipoCita = Formulario.IdTipoCita,
                LinkVideo = Formulario.LinkVideo,
                Anfitrion = Formulario.Anfitrion,
            };
        }
        public async Task<List<CitasDTO>> ListarCitasAsync()
        {
            var citasDb = await _contexto.TCitas
                .Include(c => c.IdTipoCitaNavigation) // si quieres incluir la navegación para NombreTipoCita
                .ToListAsync();

            return citasDb.Select(c => ObtenerDeDB(c)).ToList();
        }
    }
}
