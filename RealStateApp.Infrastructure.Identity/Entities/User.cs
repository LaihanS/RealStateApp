using Microsoft.AspNetCore.Identity;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infrastructure.Identity.Entities
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string? ImagePath { get; set; }
        public string LastName { get; set; }
        public string? Cedula { get; set; }

        public ICollection<PropiedadViewModel> Propoedades { get; set; }
    }
}
