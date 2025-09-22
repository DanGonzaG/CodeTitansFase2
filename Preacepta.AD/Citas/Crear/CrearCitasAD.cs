using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Citas.Crear
{
    public class CrearCitasAD : ICrearCitasAD
    {
        private readonly Contexto _contexto;

        public CrearCitasAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(TCita cita)
        {
            if (cita == null) return -1;

                await _contexto.TCitas.AddAsync(cita);
                await _contexto.SaveChangesAsync();

                return cita.IdCita; 
           
        }
        public async Task<bool> AsignarClienteAlaCita(int idCita, int idCliente)
        {
       
            var cita = await _contexto.TCitas.FindAsync(idCita);
            if (cita == null) return false;

            var relacion = new TCitasCliente
            {
                IdCita = idCita,
                IdCliente = idCliente
            };

            await _contexto.TCitasClientes.AddAsync(relacion);
            await _contexto.SaveChangesAsync();
            return true;
        }


    }
}
