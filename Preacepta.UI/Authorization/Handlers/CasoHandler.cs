using Microsoft.AspNetCore.Authorization;
using Preacepta.AD;
using Preacepta.LN.Casos.BuscarXid;
using Preacepta.UI.Authorization.Requirements;
using System.Security.Claims;

namespace Preacepta.UI.Authorization.Handlers
{
    public class CasoHandler : AuthorizationHandler<CasoRequisito>
    {
        private readonly IBuscarCasosLN _buscarCasosLN;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CasoHandler(
            IBuscarCasosLN buscarCasosLN,
            IHttpContextAccessor httpContextAccessor)
        {
            _buscarCasosLN = buscarCasosLN;
            _httpContextAccessor = httpContextAccessor;
        }


            

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            CasoRequisito requirement)
        {

            var httpContext = _httpContextAccessor.HttpContext;
            var userAutenticado = context.User.FindFirst(ClaimTypes.Email) ?. Value;
            var userRol = context.User.FindFirst(ClaimTypes.Role)?.Value;

            if (httpContext == null || 
                string.IsNullOrEmpty(userAutenticado) || 
                string.IsNullOrEmpty(userRol))
                return;

            int idCaso = int.Parse(httpContext.Request.Query["idCaso"].FirstOrDefault());
            var caso = await _buscarCasosLN.buscar(idCaso);

            switch (userRol) 
            {
                case "Abogado":
                    if (caso != null &&
                 caso.IdAbogadoNavigation.CedulaNavigation.Email == userAutenticado) 
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                    }
                    break;
                case "Cliente":
                    if (caso != null &&
                 caso.IdClienteNavigation.Email == userAutenticado)
                    {
                        context.Succeed(requirement);
                    }
                    else 
                    {
                        context.Fail();
                    }
                    break;
            }
        }
    }
}
