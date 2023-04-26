using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Dtos.Account
{
    ///<summary>
    ///  Datos para registrar usuario
    ///</summary>
    public class RegisterRequest
    {
        [SwaggerParameter(Description = "UserName del usuario")]
        public string UserName { get; set; }

        [SwaggerParameter(Description = "Apellido del usuario")]
        public string LastName { get; set; }

        [SwaggerParameter(Description = "Teléfono del usuario")]
        public string PhoneNumber { get; set; }

        [SwaggerParameter(Description = "Nombre del usuario")]
        public string FirstName { get; set; }

        [SwaggerParameter(Description = "Cedula")]
        public string Cedula { get; set; }

        [SwaggerParameter(Description = "Email del usuario")]
        public string Email { get; set; }

        [SwaggerParameter(Description = "Password del usuario")]
        public string Password { get; set; }

        [SwaggerParameter(Description = "Determinar si es administrador")]
        public bool IsAdmin { get; set; }
        public string? ImagePath { get; set; }

        [SwaggerParameter(Description = "Confirmar Contraseña")]
        public string ConfirmPassword { get; set; }
    }
}
