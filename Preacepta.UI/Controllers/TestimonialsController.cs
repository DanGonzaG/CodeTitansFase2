using Microsoft.AspNetCore.Mvc;
using Praecepta.UI.Models;

namespace Praecepta.UI.Controllers
{
    public class TestimonialsController : Controller
    {
        public IActionResult TestimonialsLista()
        {
            List < Testimonials > lista = ListaQuemadaTestimonials;
            return View(lista);
        }


        public List<Testimonials> ListaQuemadaTestimonials = new List<Testimonials>()
{
    new Testimonials()
    {
        fecha = DateTime.Now,
        NomCliente = "Daniel Elizondo",
        Identificacion = 145689632,
        Telefono= "22859632",
        Calificacion = 4,
        Reportar = false,
        Comentario = "Quiero agradecer profundamente el excelente servicio que recibí. Desde el principio, el equipo fue muy profesional, atento y eficiente. ¡Sin duda recomendaría sus servicios!"
    },
    new Testimonials()
    {
        fecha = DateTime.Now.AddDays(-1),
        NomCliente = "Laura Jiménez",
        Identificacion = 123456789,
        Telefono= "22114455",
        Calificacion = 5,
        Reportar = false,
        Comentario = "¡Excelente trato y atención! Me hicieron sentir muy valorada como cliente. Definitivamente volveré a usar sus servicios."
    },
    new Testimonials()
    {
        fecha = DateTime.Now.AddDays(-2),
        NomCliente = "Carlos Fernández",
        Identificacion = 987654321,
        Telefono= "88997766",
        Calificacion = 3,
        Reportar = true,
        Comentario = "Buen servicio, aunque hubo algunos detalles que podrían mejorar. Espero que lo consideren en el futuro."
    },
    new Testimonials()
    {
        fecha = DateTime.Now.AddDays(-3),
        NomCliente = "Ana María Rojas",
        Identificacion = 147258369,
        Telefono= "22334455",
        Calificacion = 5,
        Reportar = false,
        Comentario = "Un servicio impecable. No puedo expresar lo feliz que estoy con los resultados. ¡Muchas gracias!"
    },
    new Testimonials()
    {
        fecha = DateTime.Now.AddDays(-4),
        NomCliente = "Fernando López",
        Identificacion = 456789123,
        Telefono= "22776688",
        Calificacion = 4,
        Reportar = false,
        Comentario = "Todo salió como esperaba. La atención fue cálida y profesional. Definitivamente lo recomiendo."
    },
    new Testimonials()
    {
        fecha = DateTime.Now.AddDays(-5),
        NomCliente = "Lucía Gómez",
        Identificacion = 852741963,
        Telefono= "22446688",
        Calificacion = 5,
        Reportar = false,
        Comentario = "¡Increíble experiencia! Desde el inicio hasta el final, el equipo mostró un compromiso admirable. ¡Gracias!"
    },
    new Testimonials()
    {
        fecha = DateTime.Now.AddDays(-6),
        NomCliente = "José Mora",
        Identificacion = 963852741,
        Telefono= "88994455",
        Calificacion = 4,
        Reportar = false,
        Comentario = "Estoy muy satisfecho con el servicio. Fue puntual y eficiente, justo lo que necesitaba."
    },
    new Testimonials()
    {
        fecha = DateTime.Now.AddDays(-7),
        NomCliente = "Andrea Castro",
        Identificacion = 789456123,
        Telefono= "22668844",
        Calificacion = 5,
        Reportar = false,
        Comentario = "Nunca había recibido un trato tan excelente. Cada detalle fue cuidado con esmero. ¡Mil gracias!"
    },
    new Testimonials()
    {
        fecha = DateTime.Now.AddDays(-8),
        NomCliente = "Luis Rodríguez",
        Identificacion = 159753846,
        Telefono= "22779966",
        Calificacion = 4,
        Reportar = false,
        Comentario = "La experiencia fue muy buena en general. Definitivamente lo consideraré en el futuro."
    },
    new Testimonials()
    {
        fecha = DateTime.Now.AddDays(-9),
        NomCliente = "Marcela Pérez",
        Identificacion = 741258963,
        Telefono= "22553377",
        Calificacion = 5,
        Reportar = false,
        Comentario = "¡Un servicio excepcional! No tengo ninguna queja, solo gratitud por el esfuerzo y dedicación."
    },
    new Testimonials()
    {
        fecha = DateTime.Now.AddDays(-10),
        NomCliente = "Carlos Vargas",
        Identificacion = 654321987,
        Telefono= "22119988",
        Calificacion = 3,
        Reportar = true,
        Comentario = "Aunque hubo algunos inconvenientes, aprecio el esfuerzo del equipo para solucionarlos."
    },
    new Testimonials()
    {
        fecha = DateTime.Now.AddDays(-11),
        NomCliente = "Julieta Ramírez",
        Identificacion = 123789456,
        Telefono= "22998877",
        Calificacion = 5,
        Reportar = false,
        Comentario = "¡Totalmente recomendado! La calidad del servicio no tiene comparación. ¡Increíble!"
    },
    new Testimonials()
    {
        fecha = DateTime.Now.AddDays(-12),
        NomCliente = "Ricardo Solano",
        Identificacion = 321654987,
        Telefono= "22115566",
        Calificacion = 4,
        Reportar = false,
        Comentario = "Todo salió bien y dentro del plazo esperado. Definitivamente volveré."
    },
    new Testimonials()
    {
        fecha = DateTime.Now.AddDays(-13),
        NomCliente = "Patricia Álvarez",
        Identificacion = 987321654,
        Telefono= "22774455",
        Calificacion = 5,
        Reportar = false,
        Comentario = "Gracias por un trabajo impecable. ¡No tengo palabras para expresar mi agradecimiento!"
    },
    new Testimonials()
    {
        fecha = DateTime.Now.AddDays(-14),
        NomCliente = "Juan López",
        Identificacion = 654987321,
        Telefono= "22997744",
        Calificacion = 4,
        Reportar = false,
        Comentario = "Todo estuvo perfecto, excepto por un pequeño detalle que fue solucionado rápidamente. ¡Buen trabajo!"
    },
    new Testimonials()
    {
        fecha = DateTime.Now.AddDays(-15),
        NomCliente = "Cristina Quesada",
        Identificacion = 852963741,
        Telefono= "22445599",
        Calificacion = 5,
        Reportar = false,
        Comentario = "No puedo estar más contenta con los resultados. ¡Gracias por el esfuerzo y dedicación!"
    },
    new Testimonials()
    {
        fecha = DateTime.Now.AddDays(-16),
        NomCliente = "Daniela Monge",
        Identificacion = 963741852,
        Telefono= "22237744",
        Calificacion = 4,
        Reportar = false,
        Comentario = "Muy buen servicio. Fue eficiente y cumplió con mis expectativas. ¡Gracias!"
    },
    new Testimonials()
    {
        fecha = DateTime.Now.AddDays(-17),
        NomCliente = "Andrés León",
        Identificacion = 147852369,
        Telefono= "22996688",
        Calificacion = 5,
        Reportar = false,
        Comentario = "No tengo más que palabras de agradecimiento. Fue una experiencia excepcional desde principio a fin."
    },
    new Testimonials()
    {
        fecha = DateTime.Now.AddDays(-18),
        NomCliente = "Katherine Ramírez",
        Identificacion = 963147852,
        Telefono= "22449977",
        Calificacion = 4,
        Reportar = false,
        Comentario = "Todo se manejó con profesionalismo y eficiencia. Definitivamente lo recomendaría."
    },
    new Testimonials()
    {
        fecha = DateTime.Now.AddDays(-19),
        NomCliente = "Tomás Herrera",
        Identificacion = 369852147,
        Telefono= "22338877",
        Calificacion = 5,
        Reportar = false,
        Comentario = "Un servicio de alta calidad, manejado con atención al detalle y dedicación. ¡Gracias por todo!"
    }
};

    }
}
