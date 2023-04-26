
using RealStateApp.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using RealStateApp.Core.Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace RealStateApp.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationInfrastructure(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            #region ServicesDependency
            services.AddTransient(typeof(IGenericService<,,,>), typeof(GenericService<,,,>));
            services.AddTransient(typeof(IGenericAppService<,,>), typeof(GenericAppService<,,>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IVentaTypeService, VentaTypeService>();
            services.AddTransient<IMejoraService, MejoraService>();
            services.AddTransient<IPropertyTypeService, PropertyTypeService>();
            services.AddTransient<IPropiedadService, PropiedadService>();
            services.AddTransient<IPropertyImagesService, PropertyImagesService>();
            #endregion
        }
    }
}
