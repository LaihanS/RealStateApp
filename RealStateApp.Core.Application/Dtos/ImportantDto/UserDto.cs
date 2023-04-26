
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Dtos.ImportantDto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Cedula { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }
        public string ConfirmPassword { get; set; }
        public bool EmailConfirmed { get; set; }

        public List<string> RoleList { get; set; }


        // public ICollection<ProductViewModel> Productos { get; set; }
    }
}
