using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.IServices;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RealStateApp.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountJWTService _accountService;

        public AccountController(IAccountJWTService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("authenticate")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Login de usuario",
            Description = "Obtiene un JWT que te permitirá acceder a las funcionalidades del sistema"
         )]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationJWTResponse res = await _accountService.AuthAsync(request);
            if (res.RoleList.Any(r => r.Equals(EnumRoles.Agente.ToString())) || res.RoleList.Any(r => r.Equals(EnumRoles.Cliente.ToString())))
            {
                return StatusCode(StatusCodes.Status403Forbidden, $"Este rol {res.RoleList.FirstOrDefault()} no está autorizado a acceder a la api");
            }
            else
            {
               return Ok(res);
            }
         
        }

        [HttpPost("register")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Registro de usuario",
            Description = "Obtiene un nuevo usuario que te permitirá acceder a las funcionalidades del sistema"
         )]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterBasicUserAsync(request, origin, request.IsAdmin));
        }

        //[HttpGet("confirm-email")]
        //public async Task<IActionResult> RegisterAsync([FromQuery] string userId, [FromQuery] string token)
        //{
        //    return Ok(await _accountService.ConfirmUserAsync(userId, token));
        //}


        //[HttpPost("forgot-password")]
        //public async Task<IActionResult> ForgotPasswordAsync(ForgotPassworRequest request)
        //{
        //    var origin = Request.Headers["origin"];
        //    return Ok(await _accountService.ForgotPasswordAsync(request, origin));
        //}

        //[HttpPost("reset-password")]
        //public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest request)
        //{
        //    return Ok(await _accountService.ResetPasswordAsync(request));
        //}
    }
}
