using Microsoft.AspNetCore.Mvc.Rendering;
using Preacepta.LN.GeAbogadoTipo.Listar;
using Preacepta.LN.GeNegocio.Listar;

namespace Preacepta.UI.Extensions
{
    public static class SelectListPersonas
    {
        public static List<SelectListItem> EstadoCivil => new()
        {
            new SelectListItem { Text = "Soltero", Value = "Soltero" },
            new SelectListItem { Text = "Casado", Value = "Casado" },
            new SelectListItem { Text = "Divorciado", Value = "Divorciado" },
            new SelectListItem { Text = "Viudo", Value = "Viudo" }
        };

        public static List<SelectListItem> Genero => new()
        {
            new SelectListItem { Text = "Femenino", Value = "Femenino" },
            new SelectListItem { Text = "Masculino", Value = "Masculino" }
        };

        public static List<SelectListItem> TipoIdentificacion => new()
        {
            new SelectListItem { Text = "Cédula física", Value = "Cedula" },
            new SelectListItem { Text = "DIMEX", Value = "DIMEX" },
            new SelectListItem { Text = "Pasaporte", Value = "Pasaporte" },
            new SelectListItem { Text = "Sin documento de identificación", Value = "SinDocumento" }
        };
        public static async Task<List<SelectListItem>> Negocios(IListarNegocioLN ListaDespachos)
        {
            var despachos = await ListaDespachos.listar();
            return despachos.Select(n => new SelectListItem
            {
                Value = n.CJuridica.ToString(),
                Text = $"{n.Nombre} - {n.CJuridica}"
            }).ToList();
        }


        public static async Task<List<SelectListItem>> TipoAbogado(IListarAbogadoTipoLN ListaAbogados)
        {
            var abogados = await ListaAbogados.listar();
            return abogados.Select(n => new SelectListItem
            {
                Value = n.IdTipoAbogado.ToString(),
                Text = $"{n.Nombre}"
            }).ToList();
        }
    }
}
