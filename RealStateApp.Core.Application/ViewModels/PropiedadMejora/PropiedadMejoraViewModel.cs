using RealStateApp.Core.Application.ViewModels.Mejora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.PropiedadMejora
{
    public class PropiedadMejoraViewModel
    {
        public int id { get; set; }
        public int PropiedadId { get; set; }
        public int MejoraId { get; set; }

        public MejoraViewModel Mejora { get; set; }
    }
}
