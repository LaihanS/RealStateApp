using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Dtos.Account
{
    public class JwtResponse
    {
        public bool HasError { get; set; }
        public string ErrorDetails { get; set; }


    }
}
