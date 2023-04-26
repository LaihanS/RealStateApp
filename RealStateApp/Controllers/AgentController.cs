using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels;
using RealStateApp.Core.Application.ViewModels.PropertyImages;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Domain.Entities;

namespace WebApp.RealStateApp.Controllers
{
    [Authorize(Roles = "Agente")] 
    public class AgentController : Controller
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
        public AgentController(IMejoraService mejoraService, IHttpContextAccessor httpContextAccessor, IVentaTypeService ventaType, IPropertyTypeService propertyTypeService, IPropertyImagesService propertyImages, IPropiedadService propiedadService, IMapper mapper, IUserService userService)
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

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> PropertyMantainment(FilterViewModel filter)
        {
            List<PropiedadViewModel> propi = await propiedadService.GetAsyncJoin();
            List<PropiedadViewModel> propiedad = await propiedadService.GetPropertiesFiltro(filter);
            ViewBag.PropiedadesTipos = await propertyTypeService.GetAsync();
            ViewBag.ProperPrecios = propi.Select(prec => prec.Precio);
            ViewBag.Habitaciones = propi.Select(prec => prec.QuantityHabitaciones);
            ViewBag.Baños = propi.Select(prec => prec.QuantityBaños);

            return View(propiedad.OrderByDescending(o => o.created).ToList());
        }

        public async Task<IActionResult> PostImagesMantainment(int id)
        {
            List<PropertyImagesViewModel> images = await propertyImages.GetAsync();
            ViewBag.PropiedadID = id;
            return View("PostImages", images.Where(im => im.IdPropiedad == id).ToList());
        }

        public async Task<IActionResult> CreateProperty()
        {
            return View(new SavePropiedadViewModel()
            {
                TiposPropiedad = await propertyTypeService.GetAsync(),
                TiposVenta = await ventaType.GetAsync(),
                Mejoras = await mejoraService.GetAsync(),
            }); 
        }

        public async Task<IActionResult> CreateAgent()
        {
            SaveUserViewModel user = await userService.GetEditAsync(User.id);

            return View("CreateAgent", user);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAgent(SaveUserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateAgent", user);
            }
            string id = user.Password;
            SaveUserViewModel usuaresponse = await userService.GetEditAsync(user.Id);
            
            if (usuaresponse != null && usuaresponse.Id != null)
            {
                user.ImagePath = ReturnImageUrl(user.file, 0, usuaresponse.ImagePath, true, usuaresponse.Id); ;

                await userService.EditUser(user);
            }

            return RedirectToRoute(new { controller = "Agent", action = "Index" });

        }

        public async Task<IActionResult> EditProperty(int id)
        {
            SavePropiedadViewModel propie = await propiedadService.GetEditAsync(id);
            propie.TiposPropiedad = await propertyTypeService.GetAsync();
            propie.TiposVenta = await ventaType.GetAsync();
            propie.Mejoras = await mejoraService.GetAsync();
            ViewBag.SelectedMejoras = await propiedadService.GetMejoras(propie);
            
            return View("CreateProperty", propie);
        }


        [HttpPost] 
        public async Task<IActionResult> EditProperty(SavePropiedadViewModel savepropiedad)
        {
            if (!ModelState.IsValid)
            {
                SavePropiedadViewModel propie = await propiedadService.GetEditAsync(savepropiedad.id);
                savepropiedad.TiposPropiedad =  await propertyTypeService.GetAsync();
                savepropiedad.TiposVenta = await ventaType.GetAsync();
                savepropiedad.Mejoras = await mejoraService.GetAsync();
                ViewBag.SelectedMejoras = await propiedadService.GetMejoras(propie);

                return View("CreateProperty", savepropiedad);
            }

            await propiedadService.EditAsync(savepropiedad, savepropiedad.id);
      
            return RedirectToRoute(new { controller = "Agent", action = "Index" });

        }

        [HttpPost]
        public async Task<IActionResult> CreateProperty(SavePropiedadViewModel savepropiedad)
        {
            if (!ModelState.IsValid)
            {
                SavePropiedadViewModel propie = await propiedadService.GetEditAsync(savepropiedad.id);
                savepropiedad.TiposPropiedad = await propertyTypeService.GetAsync();
                savepropiedad.TiposVenta =  await ventaType.GetAsync();
                savepropiedad.Mejoras =  await mejoraService.GetAsync();

                return View("CreateProperty", savepropiedad);
            }
            SavePropertyImagesViewModel ImageProp = new();
            List<IFormFile> Files = new() {savepropiedad.file1, savepropiedad.file2, savepropiedad.file3, savepropiedad.file4};
            SavePropiedadViewModel propresult = await propiedadService.AddAsync(savepropiedad);
            if (propresult != null && propresult.id != null)
            {
                foreach (IFormFile item in Files)
                {
                    if (item != null)
                    { 
                        ImageProp.ImagePath = ReturnImageUrl(item, propresult.id);
                        ImageProp.IdPropiedad = propresult.id;
                        await propertyImages.AddAsync(ImageProp);
                    }
                }

            }
            return RedirectToRoute(new { controller = "Agent", action = "PropertyMantainment" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View("DeleteProperty", await propiedadService.GetEditAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SavePropiedadViewModel saveUserView)
        {
            await propiedadService.Delete(saveUserView, saveUserView.id);
            string basepath = $"/Images/Propiedad/{saveUserView.id}";
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

        public async Task<IActionResult> DeleteImage(int id)
        {
            return View("DeleteImages", await propertyImages.GetEditAsync(id));
        }
       
    

    [HttpPost]
        public async Task<IActionResult> DeleteImage(SavePropertyImagesViewModel saveUserView)
        {
            SavePropertyImagesViewModel imagen = await propertyImages.GetEditAsync(saveUserView.id);
            await propertyImages.Delete(saveUserView, saveUserView.id);
            string basepath = $"/Images/Propiedad/{imagen.IdPropiedad}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basepath}");
            string imagenpath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{imagen.ImagePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                FileInfo files = new(imagenpath);

                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    if (file.FullName.Equals(files.FullName, StringComparison.OrdinalIgnoreCase))
                    {
                        file.Delete();
                    }
                }

            }
            List<PropertyImagesViewModel> images = await propertyImages.GetAsync();
            return View("PostImages", images.Where(im => im.IdPropiedad == imagen.IdPropiedad).ToList());;
        }

        public async Task<IActionResult> EditImage(int id)
        {
            SavePropertyImagesViewModel imagen = await propertyImages.GetEditAsync(id);

            return View("CreateImage", imagen);
        }

        public async Task<IActionResult> CreateImage(int idprop)
        {
            return View("CreateImage", new SavePropertyImagesViewModel()
            {
                IdPropiedad = idprop
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateImage(SavePropertyImagesViewModel image)
        {
            PropiedadViewModel prop = propiedadService.GetAsyncJoin().Result.Find(prop => prop.id == image.IdPropiedad);
            if (prop.Imagenes.Count() == 4)
            {
                image.HasError = true;
                image.ErrorDetails = "No puede crrar más de 4 imágenes para una misma propiedad";
                return View("CreateImage", image);
            }
            image.ImagePath = "nullimagepath";
            SavePropertyImagesViewModel posteo = await propertyImages.AddAsync(image);

            if (posteo != null && posteo.id != null)
            {
                posteo.ImagePath = ReturnImageUrl(image.file, image.IdPropiedad);
                await propertyImages.EditAsync(posteo, posteo.id);
            }

            return RedirectToRoute(new { controller = "Agent", action = "PropertyMantainment" });
        }


        [HttpPost]
        public async Task<IActionResult> EditImage(SavePropertyImagesViewModel image)
        {

            SavePropertyImagesViewModel posteo = await propertyImages.GetEditAsync(image.id);
            posteo.ImagePath = ReturnImageUrl(image.file, posteo.IdPropiedad, posteo.ImagePath, true);
            await propertyImages.EditAsync(posteo, image.id);

            List<PropertyImagesViewModel> images = await propertyImages.GetAsync();
            return View("PostImages", images.Where(im => im.IdPropiedad == posteo.id).ToList());
        }


        private string ReturnImageUrl(IFormFile file, int id, string url = "", bool editmode = false, string idn = "")
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
            string basepath = "";
            if (idn != null || idn == "")
            {
               basepath = $"/Images/Propiedad/{idn}";

            }
            else if (id != null || id != 0)
            {
                basepath = $"/Images/Propiedad/{id}";
            }
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
