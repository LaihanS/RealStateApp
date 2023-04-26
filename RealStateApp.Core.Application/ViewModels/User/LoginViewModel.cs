using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.User
{
    public class LoginViewModel
    {
        public string Password{ get; set; }
        public string Email { get; set; }
        public bool HasError { get; set; }
        public string? ErrorDetails { get; set; }

    }
}
