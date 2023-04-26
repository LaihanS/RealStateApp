using RealStateApp.Core.Application.ViewModels.Propiedades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.Venta
{
    public class SaveVentaTypeViewModel
    {
        public int id { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Escriba el Nombre...")]
        public string Nombre { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Escriba la descripcion...")]
        public string Descripcion { get; set; }
        public bool HasError { get; set; }
        public string? ErrorDetails { get; set; }

        public ICollection<PropiedadViewModel>? Propiedades { get; set; }
    }
}
