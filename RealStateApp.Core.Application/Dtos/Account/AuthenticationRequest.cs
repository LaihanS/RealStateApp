using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Dtos.Account
{
    ///<summary>
    /// Datos de la autenticacion del usuario
    ///</summary>

    public class AuthenticationRequest
    {
        ///<example>
        /// usuario@gmail.com
        ///</example>
        [SwaggerParameter(Description = "Email del usuario")]
        public string Email { get; set; }

        ///<example>
        /// *******
        ///</example>
        [SwaggerParameter(Description = "Password del usuario")]
        public string Password { get; set; }
    }
}
