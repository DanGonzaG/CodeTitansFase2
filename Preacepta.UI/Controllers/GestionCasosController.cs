using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Praecepta.UI.Models.ModuloCasos;

namespace Praecepta.UI.Controllers
{
    public class GestionCasosController : Controller
    {


        // GET: Casos
        public ActionResult CasosListado()
        {
            List<ModelCasos> lista = CasosQuemados;
            return View(lista);
        }

        public ActionResult EtapasPL()
        {
            List<ModelCasos> lista = EtapasQuemadas;
            return View(lista);
        }

        
        public ActionResult FormularioCaso()
        {
            return View();
        }

        
        public ActionResult CasosCerrados()
        {
            List<ModelCasos> lista = CasosQuemados.OrderBy(a => a.FechaInicio).ToList();
            return View(lista);
        }

        public ActionResult FormularioEtapaPL()
        {            
            return View();
        }

        public ActionResult SolicitudCaso()//historia tipo cliente
        {
            return View();
        }

        public ActionResult CasosListadoCliente()//historia tipo cliente
        {
            List<ModelCasos> lista = CasosQuemados;
            return View(lista);
        }



        // GET: Casos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Casos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Casos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Casos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public List<ModelCasos> CasosQuemados = new List<ModelCasos>()
        {
            new ModelCasos()
            {
                NumCaso = 1,
                TipoCaso = "Caso Penal",
                TituloCaso = "Intento de homicidio",
                Descripcion = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent vel ante quis tortor iaculis congue eget pretium enim. Etiam eget enim et sem molestie iaculis. Nunc ultrices turpis nulla, sit amet lacinia odio lobortis id. Nulla interdum fringilla quam, quis placerat dolor consectetur in. Quisque eget neque arcu. Fusce id.",
                PruebasCaso = "Sin Pruebas",
                FechaInicio = DateTime.Now,
                FechaFin = new DateTime(2025, 03, 15),
                Cliente = "Antonio Banderas",
                Abogado = "Lic Miguel Bustos",
                AbogadoLicencia =   27559


            },
            new ModelCasos()
    {
        NumCaso = 2,
        TipoCaso = "Caso Civil",
        TituloCaso = "Disputa de contrato",
        Descripcion = "Un conflicto legal entre dos empresas por el incumplimiento de un contrato firmado en el año anterior.",
        PruebasCaso = "Documentos legales y correos electrónicos",
        FechaInicio = new DateTime(2025, 03, 15),
        FechaFin = new DateTime(2023, 07, 10),
        Cliente = "Corporación XYZ",
        Abogado = "Lic Carmen Sánchez",
        AbogadoLicencia =   27559
    },
    new ModelCasos()
    {
        NumCaso = 3,
        TipoCaso = "Caso Penal",
        TituloCaso = "Fraude financiero",
        Descripcion = "Investigación sobre un esquema de fraude que afectó a más de 50 personas.",
        PruebasCaso = "Registros bancarios y testimonios",
        FechaInicio = new DateTime(2025, 01, 10),
        FechaFin = new DateTime(2026, 12, 05),
        Cliente = "Carlos Montenegro",
        Abogado = "Lic Antonio Ruiz",
        AbogadoLicencia =   27559
    },
    new ModelCasos()
    {
        NumCaso = 4,
        TipoCaso = "Caso de Familia",
        TituloCaso = "Custodia de menores",
        Descripcion = "Disputa entre dos padres sobre la custodia compartida de sus hijos.",
        PruebasCaso = "Informe psicológico y declaraciones judiciales",
        FechaInicio = new DateTime(2025, 02, 05),
        FechaFin = new DateTime(2024, 01, 25),
        Cliente = "María Gómez",
        Abogado = "Lic Laura Martínez",
        AbogadoLicencia =   27559
    },
    new ModelCasos()
    {
        NumCaso = 5,
        TipoCaso = "Caso Mercantil",
        TituloCaso = "Propiedad intelectual",
        Descripcion = "Conflicto por el uso indebido de una patente tecnológica.",
        PruebasCaso = "Registros de patentes y diseños originales",
        FechaInicio = new DateTime(2025, 03, 20),
        FechaFin = new DateTime(2025, 06, 18),
        Cliente = "Ingeniería Avanzada S.A.",
        Abogado = "Lic Roberto Castillo",
        AbogadoLicencia =   27559
    },
    new ModelCasos()
    {
        NumCaso = 6,
        TipoCaso = "Caso Laboral",
        TituloCaso = "Despido injustificado",
        Descripcion = "Un empleado demanda por haber sido despedido sin causa válida.",
        PruebasCaso = "Contrato laboral y correos entre el empleado y la empresa",
        FechaInicio = new DateTime(2025, 03, 25),
        FechaFin = new DateTime(2022, 11, 30),
        Cliente = "Ana López",
        Abogado = "Lic Jorge Fernández",
        AbogadoLicencia =   27559
    },
    new ModelCasos()
    {
        NumCaso = 7,
        TipoCaso = "Caso Administrativo",
        TituloCaso = "Permisos de construcción",
        Descripcion = "Problemas con los permisos para construir una nueva planta industrial.",
        PruebasCaso = "Documentos gubernamentales y planos de construcción",
        FechaInicio = new DateTime(2025, 02, 15),
        FechaFin = new DateTime(2027, 02, 14),
        Cliente = "Constructora ABC",
        Abogado = "Lic Andrea Torres",
        AbogadoLicencia =   27559
    },
    new ModelCasos()
    {
        NumCaso = 8,
        TipoCaso = "Caso Penal",
        TituloCaso = "Robo agravado",
        Descripcion = "Un individuo acusado de robo en un comercio durante horario nocturno.",
        PruebasCaso = "Videos de seguridad y testigos presenciales",
        FechaInicio = new DateTime(2025, 03, 28),
        FechaFin = new DateTime(2023, 04, 21),
        Cliente = "Pedro Hernández",
        Abogado = "Lic Patricia Ramírez",
        AbogadoLicencia =   27559
    },
    new ModelCasos()
    {
        NumCaso = 9,
        TipoCaso = "Caso de Familia",
        TituloCaso = "Divorcio complicado",
        Descripcion = "División de bienes entre una pareja después de 15 años de matrimonio.",
        PruebasCaso = "Registros de propiedades y cuentas compartidas",
        FechaInicio = new DateTime(2025, 03, 01),
        FechaFin = new DateTime(2024, 09, 07),
        Cliente = "Luis Castro",
        Abogado = "Lic Ana Beltrán",
        AbogadoLicencia =   27559
    },
    new ModelCasos()
    {
        NumCaso = 10,
        TipoCaso = "Caso Civil",
        TituloCaso = "Conflicto vecinal",
        Descripcion = "Disputa entre vecinos por el uso de espacios comunes y ruidos molestos.",
        PruebasCaso = "Fotografías y denuncias previas",
        FechaInicio = new DateTime(2025, 03, 10),
        FechaFin = new DateTime(2026, 08, 29),
        Cliente = "Susana Pérez",
        Abogado = "Lic Diego Morales",
        AbogadoLicencia =   27559
    },
    new ModelCasos()
    {
        NumCaso = 11,
        TipoCaso = "Caso Laboral",
        TituloCaso = "Acoso laboral",
        Descripcion = "Un empleado denuncia acoso constante por parte de su supervisor.",
        PruebasCaso = "Mensajes y reportes de recursos humanos",
        FechaInicio = new DateTime(2025, 03, 18),
        FechaFin = new DateTime(2023, 07, 10),
        Cliente = "Lucía Torres",
        Abogado = "Lic Andrés Gómez",
        AbogadoLicencia =   27559
    }
        };


        public List<ModelCasos> EtapasQuemadas = new List<ModelCasos>
        {
            new ModelCasos()
            {
                NumEtapa = 10001,
                Fecha = DateTime.Now,
                DescripcionCaso = "Revisión inicial de documentos del caso.",
                PruebasEtapas = "Copia del contrato firmado"
            },

            new ModelCasos()
            {
                NumEtapa = 10002,
        Fecha = DateTime.Now.AddDays(-2),
        DescripcionCaso = "Citación inicial en el juzgado.",
        PruebasEtapas = "Testimonio del cliente"
    },
    new ModelCasos()
    {
        NumEtapa = 10003,
        Fecha = DateTime.Now.AddDays(-7),
        DescripcionCaso = "Presentación de evidencias preliminares.",
        PruebasEtapas = "Fotografías y correos electrónicos relevantes"
    },
    new ModelCasos()
    {
        NumEtapa = 10004,
        Fecha = DateTime.Now.AddDays(-14),
        DescripcionCaso = "Notificación formal a la parte demandada.",
        PruebasEtapas = "Documentos de notificación"
    },
    new ModelCasos()
    {
        NumEtapa = 10005,
        Fecha = DateTime.Now.AddDays(-20),
        DescripcionCaso = "Audiencia de medidas cautelares.",
        PruebasEtapas = "Declaraciones juradas y acta de audiencia"
    },
    new ModelCasos()
    {
        NumEtapa = 10006,
        Fecha = DateTime.Now.AddDays(-30),
        DescripcionCaso = "Análisis de pruebas aportadas por la defensa.",
        PruebasEtapas = "Reportes técnicos y declaraciones escritas"
    },
    new ModelCasos()
    {
        NumEtapa = 10007,
        Fecha = DateTime.Now.AddDays(-45),
        DescripcionCaso = "Solicitud de peritaje técnico.",
        PruebasEtapas = "Informe del perito designado"
    },
    new ModelCasos()
    {
        NumEtapa = 10008,
        Fecha = DateTime.Now.AddDays(-60),
        DescripcionCaso = "Revisión y aprobación de acuerdos extrajudiciales.",
        PruebasEtapas = "Documentos de acuerdos firmados por ambas partes"
    },
    new ModelCasos()
    {
        NumEtapa = 10009,
        Fecha = DateTime.Now.AddDays(-75),
        DescripcionCaso = "Preparación para la audiencia principal.",
        PruebasEtapas = "Lista de preguntas y evidencias"
    },
    new ModelCasos()
    {
        NumEtapa = 10010,
        Fecha = DateTime.Now.AddDays(-90),
        DescripcionCaso = "Audiencia principal y declaración de sentencia.",
        PruebasEtapas = "Acta de la audiencia y fallo del juez"
    }
};
    }
}
