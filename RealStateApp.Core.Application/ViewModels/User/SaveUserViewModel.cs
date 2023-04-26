using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.User
{
    public class SaveUserViewModel
    {
        public string? Id { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Escriba el Nombre...")]
        public string FirstName { get; set; }
        public string? ImagePath { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Escriba el apellido...")]
        public string LastName { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Escriba el teléfono...")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Escriba el username...")]
        public string? UserName { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Escriba la cédula...")]
        public string Cedula { get; set; }
        public List<string>? RoleList { get; set; }

        public bool IsAdmin { get; set; }

        public IFormFile? file { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Escriba el email...")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Escriba la contraseña...")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coinciden")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Escriba la confirmación")]
        public string ConfirmPassword { get; set; }


        public bool EmailConfirmed { get; set; }

        public string? ErrorDetails { get; set; }
        public bool HasError { get; set; }

    }
}
