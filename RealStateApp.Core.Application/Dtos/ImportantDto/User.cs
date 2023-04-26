
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Dtos.ImportantDto
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cedula { get; set; }

        public ICollection<string> Roles { get; set; }
        //public ICollection<Propert> Productos { get; set; }
    }
}
