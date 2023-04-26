using RealStateApp.Core.Application.ViewModels.Propiedades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.Venta
{
    public class VentaTypeViewModel
    {
        public int? id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public ICollection<PropiedadViewModel> Propiedades { get; set; }
    }
}
