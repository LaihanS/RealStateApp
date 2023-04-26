using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.User
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string ImagePath { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        public string PhoneNumber { get; set; }
        
        public bool IsAdmin { get; set; }

        public string Cedula { get; set; }

        public string Email { get; set; }

        public int CantidadPropiedades { get; set; }

        public bool EmailConfirmed { get; set; }
        public string Password { get; set; }

        [NotMapped]
        public ICollection<string> RoleList { get; set; }
    }
}
