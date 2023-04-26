using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace RealStateApp.Core.Application.Helpers
{
  

   public class ValidarDoP : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; 
            }

            int precio = (int)value;

            if (precio <= 0)
            {
                return new ValidationResult("El precio debe ser mayor a 0");
            }

            return ValidationResult.Success;
        }
    }

}
