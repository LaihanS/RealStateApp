using RealStateApp.Core.Application.ViewModels.Mejora;
using RealStateApp.Core.Application.ViewModels.PropertyImages;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.PropiedadMejora;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Application.ViewModels.Venta;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.Propiedades
{
    public class PropiedadViewModel
    {
        public int id { get; set; }
        public string UnicDigitSequence { get; set; }
        public string Tipo { get; set; }
        public string AgenteNomre { get; set; }
        public int TipoVenta { get; set; }
        public int Precio { get; set; }
        public int MtsTerrain { get; set; }
        public int QuantityHabitaciones { get; set; }
        public int QuantityBaños { get; set; }
        public int VentaTypeId { get; set; }
        public int PropertyTypeId { get; set; }
        public string? UserClientId { get; set; }
        public DateTime created { get; set; }

        public string AgenteId { get; set; }
        public string Descripcion { get; set; }
        public UserViewModel Agente { get; set; }

        public ICollection<PropiedadMejoraViewModel> PropiedadMejoras { get; set; }
        public List<PropertyImagesViewModel> Imagenes { get; set; }
        public VentaTypeViewModel VentaType { get; set; }
        public PropertypeViewModel PropiedadType { get; set; }
    }
}
