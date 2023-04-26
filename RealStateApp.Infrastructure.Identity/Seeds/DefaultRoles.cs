
using Microsoft.AspNetCore.Identity;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> role)
        {
           await role.CreateAsync(new IdentityRole(EnumRoles.Administrador.ToString()));
           await role.CreateAsync(new IdentityRole(EnumRoles.Agente.ToString()));
           await role.CreateAsync(new IdentityRole(EnumRoles.Cliente.ToString()));
            await role.CreateAsync(new IdentityRole(EnumRoles.Desarrollador.ToString()));

        }

    }
}
