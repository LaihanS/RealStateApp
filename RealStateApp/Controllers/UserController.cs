using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Domain.Entities;
using System.Text;
using WebApp.RealStateApp.Middlewares;
using static System.Net.Mime.MediaTypeNames;

namespace WebApp.RealStateApp.Controllers
{
    public class UserController : Controller
    {
        //[Authorize(Roles = "SuperAdmin, Admin, Basic")] (autoriza a cualquiera que esté-
        //-logueado con esos roles redirect al user index)
        private readonly IMapper _mapper;
        private readonly IUserService userService;
        private readonly IHttpContextAccessor http;
        public UserController(IHttpContextAccessor http, IMapper _mapper, IUserService userService)
        {
            this.http = http;
            this._mapper = _mapper;
            this.userService = userService;

        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> Index()
        {
            return View(new LoginViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginvm)
        {
            if (!ModelState.IsValid)
            {
                return View(loginvm);
            }

            AuthenticationResponse response = await userService.LoginAsync(loginvm);
            if (response != null && response.HasError != true)
            {
                if (response.RoleList.Any(r => r.Equals(EnumRoles.Administrador.ToString())))
                {
                    http.HttpContext.Session.Set<AuthenticationResponse>("user", response);
                    return RedirectToRoute(new { controller = "Admin", action = "Index" });
                }
                else if (response.RoleList.Any(r => r.Equals(EnumRoles.Agente.ToString())))
                {
                    http.HttpContext.Session.Set<AuthenticationResponse>("user", response);
                    return RedirectToRoute(new { controller = "Agent", action = "Index" });
                }
                else if (response.RoleList.Any(r => r.Equals(EnumRoles.Cliente.ToString())))
                {
                    http.HttpContext.Session.Set<AuthenticationResponse>("user", response);
                    return RedirectToRoute(new { controller = "Client", action = "Index" });
                }
                else
                {
                    return RedirectToRoute(new { controller = "User", action = "Index" });
                }
            }
            else
            {
                loginvm.HasError = response.HasError;
                loginvm.ErrorDetails = response.ErrorDetails;
                return View(loginvm);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await userService.SignOutAsync();
            http.HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> ForgotPassword()
        {

            return View(new ForgotPasswordViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotpassvm)
        {
            if (!ModelState.IsValid)
            {
                return View("ForgotPassword", forgotpassvm);

            }
            string origin = Request.Headers["origin"];
            ForgotPassworResponse responseforgot = await userService.ForgotPasswordAsync(forgotpassvm, origin);
            if (responseforgot.HasError)
            {
                forgotpassvm.HasError = responseforgot.HasError;
                forgotpassvm.ErrorDetails = responseforgot.ErrorDetails;
                return View("ForgotPassword", forgotpassvm);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });

        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
        {
            return View(vm);
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> ResetPasswordPost(ResetPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("ForgotPassword", vm);

            }
            string origin = Request.Headers["origin"];
            ResetPasswordResponse responsereset = await userService.ResetPasswordAsync(vm, origin);
            if (responsereset.HasError)
            {
                vm.HasError = responsereset.HasError;
                vm.ErrorDetails = responsereset.ErrorDetails;
                return View("ForgotPassword", responsereset);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public async Task<IActionResult> AccessDenied(ForgotPasswordViewModel passwordViewModel)
        {

            return View(passwordViewModel.ErrorDetails);
        }

        public async Task<IActionResult> Register()
        {
            return View(new SaveUserViewModel());
        }

        [HttpPost] 
        public async Task<IActionResult> Register(SaveUserViewModel uservm)
        {
            
            if (!ModelState.IsValid)
            {
                return View("Register", uservm);
            }

            string origin = Request.Headers["origin"];
            RegisterResponse registeresponse = await userService.RegisterAsync(uservm, origin);
            SaveUserViewModel userResponse = await userService.GetEditAsync(registeresponse.UserID);
            if (registeresponse.HasError)
            {
                uservm.ErrorDetails = registeresponse.ErrorDetails;
                uservm.HasError = registeresponse.HasError;
                return View("Register", uservm);
            }
            if (!uservm.IsAdmin)
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            if (userResponse != null && userResponse.Id != null)
            {
                userResponse.ImagePath = ReturnImageUrl(uservm.file, userResponse.UserName);
                userResponse.Password = uservm.Password;
                await userService.EditUser(userResponse);
            }

            return RedirectToRoute(new {controller = "User", action = "Index"});
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await userService.ConfirmAsync(userId, token);
            return View("ConfirmEmail", response);
        }

        private string ReturnImageUrl(IFormFile file, string id, string url = "", bool editmode = false)
        {

            if (editmode && file == null)
            {
                return url;
            }

            if (file == null)
            {
                return "";
            }

            //Crear directorio para la imagen actual
            string basepath = $"/Images/Usuario/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basepath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Crear la ruta de la imagen actual
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string filename = guid + fileInfo.Extension;

            string filePath = Path.Combine(path, filename);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (editmode)
            {
                string[] oldpath = url.Split("/");
                string odlImgageName = oldpath[^1];
                string OldfilePath = Path.Combine(path, odlImgageName);

                if (System.IO.File.Exists(OldfilePath))
                {
                    System.IO.File.Delete(OldfilePath);
                }
            }

            return $"{basepath}/{filename}";
        }

    }
}
