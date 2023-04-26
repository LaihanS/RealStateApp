using Banking.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Infrastructure.Persistence.Contexts;
using RealStateApp.Infrastructure.Persistence.Mappings;
using RealStateApp.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            #region contexts
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(o => o.UseInMemoryDatabase("RealDB"));
            }
            else
            {
                var connectionString = configuration.GetConnectionString("ConnectionDefault");
                services.AddDbContext<ApplicationContext>(options =>
                {
                    options.UseSqlServer(connectionString, m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName));
                    options.EnableSensitiveDataLogging();
                });
            }
            #endregion
            services.AddAutoMapper(typeof(PersistenceProfile).Assembly);


            #region repositories
            services.AddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddTransient(typeof(IGenericAppRepository<>), typeof(GenericAppRepository<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPropiedadRepository, PropiedadRepository>();
            services.AddTransient<IMejoraRepository, MejoraRepository>();
            services.AddTransient<IVentaTypeRepository, VentaTypeRepository>();
            services.AddTransient<IPropertyImagesRepository, PropertyImagesRepository>();
            services.AddTransient<IPropertyTypeRepository, PropertyTypeRepository>();
            services.AddTransient<IPropiedadMejoraRepository, PropiedadMejoraRepository>();

            #endregion
        }
    }
}
