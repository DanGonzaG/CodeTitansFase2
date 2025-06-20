using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.Casos.BuscarXid;
using Preacepta.LN.Casos.Listar;
using Preacepta.LN.CasosEtapa.BuscarXid;
using Preacepta.LN.CasosEtapa.Crear;
using Preacepta.LN.CasosEtapa.Editar;
using Preacepta.LN.CasosEtapa.Eliminar;
using Preacepta.LN.CasosEtapa.Listar;
using Preacepta.LN.GeAbogado.BuscarXid;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.UI.Controllers
{

    public class CasosEtapaController : Controller
    {        
        private readonly IBuscarCasosEtapasLN _buscar;
        private readonly ICrearCasosEtapasLN _crear;
        private readonly IEditarCasosEtapasLN _editar;
        private readonly IEliminarCasosEtapasLN _eliminar;
        private readonly IListarCasosEtapasLN _listar;
        private readonly IBuscarCasosLN _buscarCaso;
        private readonly IListarCasosLN _listarCasos;
        
        private readonly IConverter _converter;

        public CasosEtapaController(IBuscarCasosEtapasLN buscar,
            ICrearCasosEtapasLN crear,
            IEditarCasosEtapasLN editar,
            IEliminarCasosEtapasLN eliminar,
            IListarCasosEtapasLN listar,
            IBuscarCasosLN buscarCaso,
            IListarCasosLN listarCasos,

            IConverter converter)
        {
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;
            _buscarCaso = buscarCaso;
            _listarCasos = listarCasos;

            _converter = converter;

        }



        /********************************************************************************************************************************************************************/
        /*-----------------------------------------------------------------//CONTROLLER METODOS PERSONALIZADOS\\------------------------------------------------------------*/
        /********************************************************************************************************************************************************************/


        #region Listado de etapas filtrado por caso
        [Authorize(Roles = "Gestor, Abogado, Cliente")]
        public async Task<IActionResult> EtapasPL(int id)
        {
            //var contexto = _context.TCasosEtapas.Include(t => t.IdCasoNavigation);
            //return View(await _listar.listarXcaso(id));

            var Caso = await _buscarCaso.buscar(id);

            var EtapaCaso = await _listar.listarXcaso(id);

            var viewModel = new CasoUnionEtapasPL
            {
                casoDTO = Caso,
                listarCasoEtapas = EtapaCaso                
            };
            if(viewModel.listarCasoEtapas.Count() == 0) 
            {
                TempData["CasoSinEtapas"] = $"El caso {Caso.Nombre} no cuenta con etapas de procesos legales";
            }                      
            return View(viewModel);
        }
        #endregion

        #region Creacion de una nueva etapa del proceso legal
        // GET: CasosEtapa/FormularioEtapaPL
        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<IActionResult> FormularioEtapaPL(int IdCaso)
        {
            var caso = await _buscarCaso.buscar(IdCaso);
            ViewBag.IdCaso = caso.IdCaso;
            ViewBag.NombreCaso = caso.Nombre;
            //ViewData["IdCaso"] = new SelectList(_context.TCasos, "IdCaso", "Nombre");
            //ViewData["IdCaso"] = new SelectList(_buscarCaso.buscar(IdCaso).Result, "IdCaso", "Nombre");
            //ViewData["IdCaso"] = IdCaso;
            return View();
        }

        // POST: CasosEtapa/FormularioEtapaPL
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<IActionResult> FormularioEtapaPL([Bind("IdEtapaPl,Fecha,Nombre,Descripcion,IdCaso,Activo")] CasosEtapaDTO tCasosEtapa, int IdCaso)
        {

            if (ModelState.IsValid)
            {
                await _crear.Crear(tCasosEtapa);
                return RedirectToAction("EtapasPL", "CasosEtapa", new { id = IdCaso });
            }
            var caso = await _buscarCaso.buscar(IdCaso);
            ViewBag.IdCaso = caso.IdCaso;
            ViewBag.NombreCaso = caso.Nombre;
            return View(tCasosEtapa);
        }
        #endregion

        #region Busqueda una etapa especifica
        //Este metodo muestra de manera detalla los registros de una etapa del proceso legal de manera detallada, se muestran en un modal
        // GET: CasosEtapa/DetalleEtapas/5
        [Authorize(Roles = "Gestor, Abogado, Cliente")]
        public async Task<IActionResult> DetalleEtapas(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCasosEtapa = await _buscar.buscar(id);
            var datos = new
            {
                /*se colocar los datos de tCasoEtapa en un objeto nuevo para poder manipularlos con un objeto json*/
                Id = tCasosEtapa.IdEtapaPl,
                CasoId = tCasosEtapa.IdCaso,
                NombreEtapa = tCasosEtapa.Nombre,
                FechaEtapa = tCasosEtapa.Fecha,
                DescripcioEtapa = tCasosEtapa.Descripcion

            };

            if (tCasosEtapa == null)
            {
                return NotFound();
            }
            return Json(datos);//se envia el objeto json y asi se pueden manipular los datos en el modal de la vista
        }
        #endregion

        #region Editar una etapa del proceso legal
        // GET: CasosEtapa/EditarEtapaPL/5
        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<IActionResult> EditarEtapaPL(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCasosEtapa = await _buscar.buscar(id);
            if (tCasosEtapa == null)
            {
                return NotFound();
            }
            var caso = await _buscarCaso.buscar(tCasosEtapa.IdCaso);
            ViewBag.IdCaso = caso.IdCaso;//muestra los datos en el formulario
            ViewBag.NombreCaso = caso.Nombre;
            //ViewData["IdCaso"] = new SelectList(_context.TCasos, "IdCaso", "Nombre", tCasosEtapa.IdCaso);
            return View(tCasosEtapa);
        }

        // POST: CasosEtapa/EditarEtapaPL/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<IActionResult> EditarEtapaPL(int IdEtapaPl, [Bind("IdEtapaPl,Fecha,Nombre,Descripcion,IdCaso,Activo")] CasosEtapaDTO tCasosEtapa)
        {
            if (IdEtapaPl != tCasosEtapa.IdEtapaPl)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.Editar(tCasosEtapa);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                TempData["MensajeModificacion"] = "La etapa ha sido modificada";
                return RedirectToAction("EtapasPL", "CasosEtapa", new { id = tCasosEtapa.IdCaso });
            }
            var caso = await _buscarCaso.buscar(tCasosEtapa.IdCaso);
            ViewBag.IdCaso = caso.IdCaso;//muestra los datos en el formulario
            ViewBag.NombreCaso = caso.Nombre;
            return View(tCasosEtapa);
        }
        #endregion

        #region Eliminacion de una etapa del proceso legal
        // GET: CasosEtapa/Delete/5
        [HttpGet]
        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<IActionResult> EliminarEtapaPL(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCasosEtapa = await _buscar.buscar(id);
            if (tCasosEtapa == null)
            {
                return NotFound();
            }

            return View(tCasosEtapa);
        }

        // POST: CasosEtapa/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor, Abogado")]
        public async Task<IActionResult> EliminarEtapaPLConfirmado(int IdEtapaPl)
        {
            var caso = await _buscar.buscar(IdEtapaPl);
            await _eliminar.Eliminar(IdEtapaPl);
            TempData["MensajeEliminacion"] = "La etapa ha sido borrada";
            return RedirectToAction("EtapasPL", "CasosEtapa", new { id = caso.IdCaso });
        }
        #endregion

        #region Descarga de EtapaPL
        [HttpGet]
        public async Task<IActionResult> DescargaPDFetapaPL(int id)
        {
            var imagenlogo = System.IO.File.ReadAllBytes("C:/Preacepta/Preacepta.UI/wwwroot/lyso/img/PreaceptaLogoColorNegro.png");
            var base64 = Convert.ToBase64String(imagenlogo);
            var etapaEncontrada = await _buscar.buscar(id);             
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lyso", "DescargaCasos", "DescargaEtapaPL.html");
            var htmlTemplate = System.IO.File.ReadAllText(templatePath);

            // Reemplazar marcadores con los datos del formulario
            htmlTemplate = htmlTemplate
                //encabezado
                .Replace("{{LOGO}}", base64)
                .Replace("{{CedulaJuridica}}", etapaEncontrada.IdCasoNavigation.IdAbogadoNavigation.CJuridica.ToString())
                .Replace("{{NombreBufete}}", etapaEncontrada.IdCasoNavigation.IdAbogadoNavigation.CJuridicaNavigation.Nombre)
                .Replace("{{TelefonoDespacho}}", etapaEncontrada.IdCasoNavigation.IdAbogadoNavigation.CJuridicaNavigation.Telefono)
                .Replace("{{EmailDespacho}}", etapaEncontrada.IdCasoNavigation.IdAbogadoNavigation.CJuridicaNavigation.Email)
                //Datos del caso y la etapa del caso
                .Replace("{{NombreCaso}}", etapaEncontrada.IdCasoNavigation.Nombre)
                .Replace("{{FechaEtapa}}", etapaEncontrada.Fecha)
                .Replace("{{NumeroEtapa}}", etapaEncontrada.IdEtapaPl.ToString())
                .Replace("{{NombreEtapa}}", etapaEncontrada.Nombre)
                .Replace("{{EtapaDescripcon}}", etapaEncontrada.Descripcion)
                //Datos del cliente y del abogado
                .Replace("{{NombreCliente}}", etapaEncontrada.IdCasoNavigation.IdClienteNavigation.Nombre)
                .Replace("{{NombreApellido1}}", etapaEncontrada.IdCasoNavigation.IdClienteNavigation.Apellido1)
                .Replace("{{NombreApellido2}}", etapaEncontrada.IdCasoNavigation.IdClienteNavigation.Apellido2)
                .Replace("{{IdCliente}}", etapaEncontrada.IdCasoNavigation.IdClienteNavigation.Cedula.ToString())
                .Replace("{{NombreAbogado}}", etapaEncontrada.IdCasoNavigation.IdAbogadoNavigation.CedulaNavigation.Nombre)
                .Replace("{{Carnet}}", etapaEncontrada.IdCasoNavigation.IdAbogadoNavigation.Carnet.ToString())
                //Feha y hora
                .Replace("{{nombreDIA}}", DateTime.Now.ToString("dddd"))
                .Replace("{{DIA}}", DateTime.Now.ToString("dd"))
                .Replace("{{NombreMes}}", DateTime.Now.ToString("MMMM"))
                .Replace("{{ANYO}}", DateTime.Now.ToString("yyyy"))
                .Replace("{{HoraActual}}", DateTime.Now.ToString("HH:mm"))
                ;



            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait
                },
                Objects = {
            new ObjectSettings
            {
                HtmlContent = htmlTemplate,
                WebSettings = { DefaultEncoding = "utf-8" }
            }
        }
            };

            var pdf = _converter.Convert(doc);

            return File(pdf, "application/pdf");
        }

        #endregion


        /********************************************************************************************************************************************************************/
        /*-----------------------------------------------------------------//CONTROLLER METODOS DE FRAMEWORK\\--------------------------------------------------------------*/
        /********************************************************************************************************************************************************************/

        // GET: CasosEtapa
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Index()
        {
            //var contexto = _context.TCasosEtapas.Include(t => t.IdCasoNavigation);
            return View(await _listar.listar());
        }

        // GET: CasosEtapa/Details/5
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCasosEtapa = await _buscar.buscar(id);
            if (tCasosEtapa == null)
            {
                return NotFound();
            }

            return View(tCasosEtapa);
        }

        // GET: CasosEtapa/Create
        [Authorize(Roles = "Gestor")]
        public IActionResult Create()
        {
            ViewData["IdCaso"] = new SelectList(_listarCasos.listar().Result, "IdCaso", "Nombre");
            return View();
        }

        // POST: CasosEtapa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Create([Bind("IdEtapaPl,Fecha,Nombre,Descripcion,IdCaso,Activo")] CasosEtapaDTO tCasosEtapa)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tCasosEtapa);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCaso"] = new SelectList(_listarCasos.listar().Result, "IdCaso", "Nombre", tCasosEtapa.IdCaso);
            return View(tCasosEtapa);
        }

        // GET: CasosEtapa/Edit/5
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCasosEtapa = await _buscar.buscar(id);
            if (tCasosEtapa == null)
            {
                return NotFound();
            }
            ViewData["IdCaso"] = new SelectList(_listarCasos.listar().Result, "IdCaso", "Nombre", tCasosEtapa.IdCaso);
            return View(tCasosEtapa);
        }

        // POST: CasosEtapa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int id, [Bind("IdEtapaPl,Fecha,Nombre,Descripcion,IdCaso,Activo")] CasosEtapaDTO tCasosEtapa)
        {
            if (id != tCasosEtapa.IdEtapaPl)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.Editar(tCasosEtapa);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCaso"] = new SelectList(_listarCasos.listar().Result, "IdCaso", "Nombre", tCasosEtapa.IdCaso);
            return View(tCasosEtapa);
        }

        // GET: CasosEtapa/Delete/5
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCasosEtapa = await _buscar.buscar(id);
            if (tCasosEtapa == null)
            {
                return NotFound();
            }

            return View(tCasosEtapa);
        }

        // POST: CasosEtapa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eliminar.Eliminar(id);
            return RedirectToAction(nameof(Index));
        } 
    }
}
