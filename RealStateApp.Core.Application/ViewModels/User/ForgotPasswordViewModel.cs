﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.User
{
    public class ForgotPasswordViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Escriba su email")]
        public string Email { get; set;}
       public string? ErrorDetails  { get; set; }
       public bool HasError  { get; set; }
    }
}
