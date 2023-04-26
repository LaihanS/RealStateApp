using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels.Mejora;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Application.ViewModels.Venta;

namespace WebApp.RealStateApp.Controllers
{
    [Authorize(Roles = "Administrador")] 
    public class AdminController : Controller
    {
        private readonly IPropertyTypeService propertyType;
        private readonly IVentaTypeService ventaTypeService;
        private readonly IMejoraService mejoraService;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        public AdminController(IPropertyTypeService propertyType, IMejoraService mejoraService, IVentaTypeService ventaTypeService, IMapper mapper, IUserService userService)
        {
            this.propertyType = propertyType;
            this.userService = userService;
            this.mejoraService = mejoraService;
            this.ventaTypeService = ventaTypeService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await userService.GetHomeViewModel());
        }

        public async Task<IActionResult> AgentMantainment()
        {
            List<UserViewModel> users = await userService.GetAllUsersAsyncJoined();
            users = users.Where(users => users.RoleList.Any(roles => roles.Equals(EnumRoles.Agente.ToString()))).ToList();
         
            return View(users);
        }

        public async Task<IActionResult> VentaTypeMantainment()
        {
            List<VentaTypeViewModel> ventas = await ventaTypeService.GetAsync();
            return View(ventas);
        }

        public async Task<IActionResult> DevMantainment()
        {
            List<UserViewModel> users = await userService.GetAllUsersAsyncJoined();
            users = users.Where(users => users.RoleList.Any(roles => roles.Equals(EnumRoles.Desarrollador.ToString()))).ToList();

            return View(users);
        }

        public async Task<IActionResult> AdminMantainment()
        {
            List<UserViewModel> users = await userService.GetAllUsersAsyncJoined();
            users = users.Where(users => users.RoleList.Any(roles => roles.Equals(EnumRoles.Administrador.ToString()))).ToList();

            return View(users);
        }

        public async Task<IActionResult> MejoraMantainment()
        {
            List<MejoraViewModel> mejoras = await mejoraService.GetAsync();
            return View(mejoras);
        }
        public async Task<IActionResult> PropertypeMantainment()
        {
            List<PropertypeViewModel> props = await propertyType.GetAsync();
            return View(props);
        }

        public async Task<IActionResult> ActivateOrInactivate(string Id)
        {
            return View("ChangeStatus", mapper.Map<SaveUserViewModel>(await userService.GetUserByIdWithRoles(Id)));
        }

        [HttpPost]
        public async Task<IActionResult> ActivateOrInactivate(SaveUserViewModel userview)
        {
            await userService.ActivateOrInactivate(userview.Id);
            UserViewModel uservm = await userService.GetUserByIdWithRoles(userview.Id);
            if (uservm.RoleList.Any(r => r.Equals(EnumRoles.Administrador.ToString())))
            {
                return RedirectToRoute(new { controller = "Admin", action = "AdminMantainment" });
            }
            else if (uservm.RoleList.Any(r => r.Equals(EnumRoles.Desarrollador.ToString())))
            {
                return RedirectToRoute(new { controller = "Admin", action = "DevMantainment" });
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "AgentMantainment" });
            }
        }

        public async Task<IActionResult> Delete(string Id)
        {
            return View("DeleteUser", await userService.GetEditAsync(Id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SaveUserViewModel saveUserView)
        {
            SaveUserViewModel getuser = await userService.GetEditAsync(saveUserView.Id);
            await userService.DeleteUserAsync(mapper.Map<UserViewModel>(saveUserView));
            string basepath = $"/Images/Usuario/{getuser.UserName}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basepath}");
            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }

                foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
                {
                    directory.Delete();
                }

                Directory.Delete(path);
            }
            return RedirectToRoute(new { controller = "Agent", action = "PropertyMantainment" });
        }

        public async Task<IActionResult> DeleteVenta(int id)
        {
            return View("DeleteVenta", await ventaTypeService.GetEditAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteVenta(SaveVentaTypeViewModel saveUserView)
        {
            await ventaTypeService.Delete(saveUserView, saveUserView.id);
            return RedirectToRoute(new { controller = "Admin", action = "VentaTypeMantainment" });
        }

        public async Task<IActionResult> DeleteMejora(int id)
        {
            return View("DeleteMejora", await mejoraService.GetEditAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMejora(SaveMejoraViewModel saveUserView)
        {
            await mejoraService.Delete(saveUserView, saveUserView.id);
            return RedirectToRoute(new { controller = "Admin", action = "MejoraMantainment" });
        }

        public async Task<IActionResult> DeletePropertype(int id)
        {
            return View("DeletePropertype", await propertyType.GetEditAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePropertype(SavePropertypeViewModel savePropertype)
        {
            await propertyType.Delete(savePropertype, savePropertype.id);
            return RedirectToRoute(new { controller = "Admin", action = "PropertypeMantainment" });
        }

        public async Task<IActionResult> CreateMejora()
        {
            return View("CreateMejora", new SaveMejoraViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateMejora(SaveMejoraViewModel saveMejora)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateMejora", saveMejora);
            }
            await mejoraService.AddAsync(saveMejora);
            return RedirectToRoute(new { controller = "Admin", action = "MejoraMantainment" });
        }

        public async Task<IActionResult> CreatePropertype()
        {
            return View("CreatePropertype", new SavePropertypeViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePropertype(SavePropertypeViewModel saveMejora)
        {
            if (!ModelState.IsValid)
            {
                return View("CreatePropertype", saveMejora);
            }
            await propertyType.AddAsync(saveMejora);
            return RedirectToRoute(new { controller = "Admin", action = "PropertypeMantainment" });
        }

        public async Task<IActionResult> EditPropertype (int id)
        {
            return View("CreatePropertype", await propertyType.GetEditAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> EditPropertype(SavePropertypeViewModel saveVenta)
        {
            if (!ModelState.IsValid)
            {
                return View("CreatePropertype", saveVenta);
            }
            await propertyType.EditAsync(saveVenta, saveVenta.id);
            return RedirectToRoute(new { controller = "Admin", action = "PropertypeMantainment" });
        }

        public async Task<IActionResult> EditMejora(int id)
        {
            return View("CreateMejora", await mejoraService.GetEditAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> EditMejora(SaveMejoraViewModel saveVenta)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateMejora", saveVenta);
            }
            await mejoraService.EditAsync(saveVenta, saveVenta.id);
            return RedirectToRoute(new { controller = "Admin", action = "MejoraMantainment" });
        }

        public async Task<IActionResult> CreateVenta()
        {
            return View("CreateVenta", new SaveVentaTypeViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateVenta(SaveVentaTypeViewModel saveVenta)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateVenta", saveVenta);
            }
            await ventaTypeService.AddAsync(saveVenta);
            return RedirectToRoute(new { controller = "Admin", action = "VentaTypeMantainment" });
        }

        public async Task<IActionResult> EditVenta(int id)
        {
            return View("CreateVenta", await ventaTypeService.GetEditAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> EditVenta(SaveVentaTypeViewModel saveVenta)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateVenta", saveVenta);
            }
            await ventaTypeService.EditAsync(saveVenta, saveVenta.id);
            return RedirectToRoute(new { controller = "Admin", action = "VentaTypeMantainment" });
        }

        public async Task<IActionResult> Create()
        {
            return View("Create", new SaveUserViewModel());
        }

        public async Task<IActionResult> CreateAdmin()
        {
            return View("CreateAdmin", new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveUserViewModel saveUserView)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", saveUserView);
            }

            List<string> Roles = new() {EnumRoles.Desarrollador.ToString()};
            saveUserView.RoleList = Roles;
            RegisterResponse registeresponse = await userService.RegisterAdminOrDev(saveUserView);
            if (registeresponse.HasError) 
            {
                saveUserView.ErrorDetails = registeresponse.ErrorDetails;
                saveUserView.HasError = registeresponse.HasError;
                return View("Create", saveUserView);
            }
            return RedirectToRoute(new { controller = "Admin", action = "DevMantainment" });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdmin(SaveUserViewModel saveUserView)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", saveUserView);
            }

            List<string> Roles = new() { EnumRoles.Administrador.ToString() };
            saveUserView.RoleList = Roles;
            RegisterResponse registeresponse = await userService.RegisterAdminOrDev(saveUserView);
            if (registeresponse.HasError)
            {
                saveUserView.ErrorDetails = registeresponse.ErrorDetails;
                saveUserView.HasError = registeresponse.HasError;
                return View("CreateAdmin", saveUserView);
            }
            return RedirectToRoute(new { controller = "Admin", action = "AdminMantainment" });
        }

        public async Task<IActionResult> Edit(string Id)
        {
            SaveUserViewModel saveUserView = await userService.GetEditAsync(Id);
           return View("Create", saveUserView);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveUserViewModel saveUserView)
        {
            UserViewModel userrio = await userService.GetUserByIdWithRoles(saveUserView.Id);

            if (!ModelState.IsValid)
            {
                saveUserView.RoleList = userrio.RoleList.ToList();
                return View("Create", saveUserView);
            }

            SaveUserViewModel editresponse = await userService.EditUser(saveUserView);
            UserViewModel user = await userService.GetUserByIdWithRoles(editresponse.Id);
            /*users.Find(use => use.Id == editresponse.Id)*/;
            if (editresponse.HasError && user.RoleList.Any(r => r.Equals(EnumRoles.Desarrollador.ToString())))
            {
                editresponse.RoleList = user.RoleList.ToList();
                return View("Create", editresponse);
            }

            if (editresponse.HasError && user.RoleList.Any(r => r.Equals(EnumRoles.Administrador.ToString())))
            {
                editresponse.RoleList = user.RoleList.ToList();
                return View("CreateAdmin", editresponse);
            }
            if (user.RoleList.Any(r => r.Equals(EnumRoles.Administrador.ToString())))
            {
                return RedirectToRoute(new { controller = "Admin", action = "AdminMantainment" });
            }
            else
            {
                return RedirectToRoute(new { controller = "Admin", action = "DevMantainment" });
            }
        }
    }
} 
