using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.Mappings;
using RealStateApp.Core.Application.ViewModels.Venta;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Settings;
using RealStateApp.Infrastructure.Identity.Contexts;
using RealStateApp.Infrastructure.Identity.Entities;
using RealStateApp.Infrastructure.Identity.Mappings;
using RealStateApp.Infrastructure.Identity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddIdentityRegistrationForApi(this IServiceCollection services, IConfiguration configuration)
        {
            #region contexts

            ContextConfiguration(services, configuration);

            #endregion

            #region identity
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User";
                options.AccessDeniedPath = "/User/AccessDenied";
            });

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },
                    OnChallenge = c =>
                    {
                        c.HandleResponse();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string>("You are not Authorized"));
                        return c.Response.WriteAsync(result);
                    },
                    OnForbidden = c =>
                    {
                        c.Response.StatusCode = 403;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string>("You are not Authorized to access this resource"));
                        return c.Response.WriteAsync(result);
                    }
                };

            });


            services.AddAuthentication();
            #endregion

            ServiceConfiguration(services);
        }

        public static void AddIdentityRegistrationForWeb(this IServiceCollection services, IConfiguration configuration)
        {
            #region contexts

            ContextConfiguration(services, configuration);

            #endregion

            #region identity
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User";
                options.AccessDeniedPath = "/User/AccessDenied";
            });

            services.AddAuthentication();
            #endregion

            ServiceConfiguration(services);
        }


        #region "Private methods"

        private static void ContextConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<IdentityContext>(o => o.UseInMemoryDatabase("RealDB"));
            }
            else
            {
                var connectionString = configuration.GetConnectionString("ConnectionDefault");
                services.AddDbContext<IdentityContext>(options =>
                {
                    options.UseSqlServer(connectionString, m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
                    options.EnableSensitiveDataLogging();
                });
            }
            #endregion
        }

        private static void ServiceConfiguration(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(IdentityProfile).Assembly);


            #region Injections
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAccountJWTService, AccountJWTService>();

            #endregion
        }
        #endregion
    }
}

