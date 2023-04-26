using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.Mejora
{
    public class MejoraViewModel
    {
        public int id { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public int PropiedadId { get; set; }
        public PropiedadViewModel Propiedad { get; set; }
    }
}
