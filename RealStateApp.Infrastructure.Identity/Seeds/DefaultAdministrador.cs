
using Microsoft.AspNetCore.Identity;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Infrastructure.Identity.Seeds
{
    public static class DefaultAdministrador
    {
        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> role)
        {
            User defaultUser = new();
            defaultUser.UserName = "DefaultAdministradorUser";
            defaultUser.Email = "DefaultAdministradorUserUser@email.com";
            defaultUser.FirstName = "John";
            defaultUser.LastName = "Doe";
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumberConfirmed = true;
            defaultUser.Cedula = "12345678";

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!!");
                    //await userManager.AddToRoleAsync(defaultUser, EnumRoles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, EnumRoles.Administrador.ToString());
                    //await userManager.AddToRoleAsync(defaultUser, EnumRoles.SuperAdmin.ToString());
                }
            }

        }
    }
}
