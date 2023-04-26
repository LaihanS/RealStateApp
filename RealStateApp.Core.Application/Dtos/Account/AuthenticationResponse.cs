using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Dtos.Account
{
    public class AuthenticationResponse
    {
        public string id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> RoleList { get; set; }
        public bool Verified { get; set; }
        public bool HasError { get; set; }
        public string ErrorDetails { get; set; }
    }
}
