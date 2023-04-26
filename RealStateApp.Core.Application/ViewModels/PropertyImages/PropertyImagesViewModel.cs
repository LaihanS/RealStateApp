using RealStateApp.Core.Application.ViewModels.Propiedades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.PropertyImages
{
    public class PropertyImagesViewModel
    {
        public int id { get; set; }
        public int IdPropiedad { get; set; }
        public string ImagePath { get; set; }

        public PropiedadViewModel? Propiedad { get; set; }
    }
}
