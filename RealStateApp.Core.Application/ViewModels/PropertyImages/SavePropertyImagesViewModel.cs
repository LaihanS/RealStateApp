using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.PropertyImages
{
    public class SavePropertyImagesViewModel
    {
        public int id { get; set; }
        public int IdPropiedad { get; set; }
        public string ImagePath { get; set; }

        public IFormFile? file { get; set; }

        public PropiedadViewModel? Propiedad { get; set; }
        public string? ErrorDetails { get; set; }
        public bool HasError { get; set; }

    }
}
