using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.Mejora
{
    public class SaveMejoraViewModel
    {
        public int id { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Escriba el Nombre...")]
        public string Nombre { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Escriba la descripcion...")]
        public string Descripcion { get; set; }

        public int? PropiedadId { get; set; }

        public string? ErrorDetails { get; set; }

        public bool HasError { get; set; }

    }
}
