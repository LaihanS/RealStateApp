using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Application.ViewModels.User;
using WebApp.RealStateApp.Middlewares;

namespace WebApp.RealStateApp.Controllers
{
    public class HomeController : Controller
    {
            private readonly IUserService userService;
            private readonly IVentaTypeService ventaType;
            private readonly IPropertyTypeService propertyTypeService;
            private readonly IPropiedadService propiedadService;
            private readonly IPropertyImagesService propertyImages;
            private readonly IMapper mapper;
            private readonly IHttpContextAccessor httpContextAccessor;
            private readonly IMejoraService mejoraService;

            AuthenticationResponse User = new();
            public HomeController(IMejoraService mejoraService, IHttpContextAccessor httpContextAccessor, IVentaTypeService ventaType, IPropertyTypeService propertyTypeService, IPropertyImagesService propertyImages, IPropiedadService propiedadService, IMapper mapper, IUserService userService)
            {
                this.mejoraService = mejoraService;
                this.propertyImages = propertyImages;
                this.ventaType = ventaType;
                this.propertyTypeService = propertyTypeService;
                this.propiedadService = propiedadService;
                this.userService = userService;
                this.mapper = mapper;
                this.httpContextAccessor = httpContextAccessor;
                User = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            }


        public async Task<IActionResult> Index(FilterViewModel filter)
        {
            List<PropiedadViewModel> propi = await propiedadService.GetAsyncJoin();
            List<PropiedadViewModel> propiedad = await propiedadService.GetPropertiesFiltro(filter);
            ViewBag.PropiedadesTipos = await propertyTypeService.GetAsync();
            ViewBag.ProperPrecios = propi.Select(prec => prec.Precio);
            ViewBag.Habitaciones = propi.Select(prec => prec.QuantityHabitaciones);
            ViewBag.Baños = propi.Select(prec => prec.QuantityBaños);

            return View(propiedad.OrderByDescending(o => o.created).ToList());
        }

        public async Task<IActionResult> PropertyDetails(int id)
        {
            List<PropiedadViewModel> propiedad = await propiedadService.GetAsyncJoin();
            PropiedadViewModel prop = propiedad.Find(p => p.id == id);
            return View(prop);
        }

        public async Task<IActionResult> AgentMantainment(FilterViewModel filter)
        {
            List<UserViewModel> users = await userService.GetAllUsersAsyncJoinedFiltered(filter);
            users = users.Where(users => users.RoleList.Any(roles => roles.Equals(EnumRoles.Agente.ToString()))).ToList();

            return View(users.OrderByDescending(o => o.FirstName).ToList());
        }

        public async Task<IActionResult> AgentDetails(string id)
        {
            List<UserViewModel> users = await userService.GetAllUsersAsyncJoined();
            users = users.Where(users => users.RoleList.Any(roles => roles.Equals(EnumRoles.Agente.ToString()))).ToList();
            UserViewModel us = users.Find(u => u.Id == id);
            return View(us);
        }


    }
}
