using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Praecepta.UI.Models;

namespace Praecepta.UI.Controllers
{
    public class CitasController : Controller
    {
        // GET: CitasController
        public ActionResult Calendar()
        {
            List<ModuloCitasVideo> lista = listaCitas;
            return View(lista);
        }


        public ActionResult CalendarPasado()
        {
            var lista = listaCitas.Where(a => a.FechaHora < DateTime.Now);
            return View(lista);
        }

        public ActionResult _CitaFuturo()
        {
            var lista = listaCitas.Where(a => a.FechaHora > DateTime.Now);
            return View(lista);
        }






        List<ModuloCitasVideo> listaCitas = new List<ModuloCitasVideo>()
{
    new ModuloCitasVideo()
    {
        NumCita = 1,
        Evento = "Revisión de contrato laboral",
        FechaHora = new DateTime(2025, 03, 31, 09, 00, 00),
        tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 2,
        Evento = "Consulta con cliente sobre propiedad",
        FechaHora = new DateTime(2025, 03, 31, 10, 30, 00),
         tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 3,
        Evento = "Audiencia judicial civil",
        FechaHora = new DateTime(2025, 04, 03, 13, 00, 00),
        tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 4,
        Evento = "Redacción de escritura pública",
        FechaHora = new DateTime(2025, 04, 05, 15, 45, 00),
         tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 5,
        Evento = "Revisión de contrato de alquiler",
        FechaHora = new DateTime(2025, 04, 07, 08, 15, 00),
        tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 6,
        Evento = "Consulta sobre herencias",
        FechaHora = new DateTime(2025, 04, 10, 11, 00, 00),
         tipo = "Virtual",
    },
    new ModuloCitasVideo()
    {
        NumCita = 7,
        Evento = "Firma de acuerdo judicial",
        FechaHora = new DateTime(2025, 04, 12, 14, 30, 00),
         tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 8,
        Evento = "Elaboración de apelación",
        FechaHora = new DateTime(2025, 04, 15, 16, 00, 00),
         tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 9,
        Evento = "Audiencia de conciliación",
        FechaHora = new DateTime(2025, 04, 18, 09, 45, 00),
         tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 10,
        Evento = "Negociación de contrato comercial",
        FechaHora = new DateTime(2025, 04, 20, 10, 15, 00),
         tipo = "Virtual",
    },
    new ModuloCitasVideo()
    {
        NumCita = 11,
        Evento = "Preparación de documentos notariales",
        FechaHora = new DateTime(2025, 04, 23, 15, 00, 00),
         tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 12,
        Evento = "Revisión de demandas",
        FechaHora = new DateTime(2025, 04, 25, 13, 15, 00),
         tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 13,
        Evento = "Firma de documentos de compraventa",
        FechaHora = new DateTime(2025, 04, 28, 09, 30, 00),
         tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 14,
        Evento = "Consulta con cliente corporativo",
        FechaHora = new DateTime(2025, 05, 01, 11, 45, 00),
         tipo = "Virtual",
    },
    new ModuloCitasVideo()
    {
        NumCita = 15,
        Evento = "Audiencia penal",
        FechaHora = new DateTime(2025, 05, 05, 10, 00, 00),
         tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 16,
        Evento = "Revisión de contrato de servicio",
        FechaHora = new DateTime(2025, 05, 08, 14, 30, 00),
        tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 17,
        Evento = "Asesoría legal sobre bienes raíces",
        FechaHora = new DateTime(2025, 05, 12, 08, 45, 00),
        tipo = "Virtual",
    },
    new ModuloCitasVideo()
    {
        NumCita = 18,
        Evento = "Firma de documentos judiciales",
        FechaHora = new DateTime(2025, 05, 15, 13, 00, 00),
        tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 19,
        Evento = "Estrategia para un caso civil",
        FechaHora = new DateTime(2025, 05, 20, 15, 15, 00),
        tipo = "Presencial",
    },
    new ModuloCitasVideo()
    {
        NumCita = 20,
        Evento = "Consulta sobre derecho fiscal",
        FechaHora = new DateTime(2025, 05, 31, 10, 30, 00),
        tipo = "Virtual",
    }
};


    }
}
