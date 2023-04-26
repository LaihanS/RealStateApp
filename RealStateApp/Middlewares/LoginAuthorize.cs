using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Helpers;
using WebApp.RealStateApp.Controllers;

namespace WebApp.RealStateApp.Middlewares
{
    public class LoginAuthorize : IAsyncActionFilter
    {
        private readonly ValidateUserSession _userSession;
        IHttpContextAccessor httpContextAccessor;
        AuthenticationResponse User = new();
        public LoginAuthorize(IHttpContextAccessor httpContextAccessor, ValidateUserSession userSession)
        {
            this.httpContextAccessor = httpContextAccessor;
            _userSession = userSession;
           User =  httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_userSession.HasUser())
            {
                var controller = (UserController)context.Controller;
                if (User.RoleList.Any(r => r.Equals(EnumRoles.Cliente.ToString()) && User.RoleList.Count == 1))
                {
                    context.Result = controller.RedirectToAction("Index", "Client");
                }
                else if (User.RoleList.Any(r => r.Equals(EnumRoles.Administrador.ToString())))
                {
                    context.Result = controller.RedirectToAction("Index", "Admin");
                }
                else if (User.RoleList.Any(r => r.Equals(EnumRoles.Agente.ToString())))
                {
                    context.Result = controller.RedirectToAction("Index", "Agent");
                }

            }
            else
            {
                await next();
            }
        }
    }
}
