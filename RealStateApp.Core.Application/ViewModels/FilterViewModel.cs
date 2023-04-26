using RealStateApp.Core.Application.ViewModels.PropertyType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels
{
    public class FilterViewModel
    {
        public int? PropiedadTipoID { get; set; }
        //public PropertypeViewModel? PropiedadTipo { get; set; }
        public int? PrecMax { get; set; }
        public string? PropCodigo { get; set; }
        public string? NombreAgente { get; set; }

        public int? PrecMin { get; set; }
        public int? BañoCantidad { get; set; }
        public int? HabiCantidad { get; set; }

    }
}