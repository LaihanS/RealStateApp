using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.Home
{
    public class HomeViewModel
    {
       public List<UserViewModel> AgentesActivos { get; set; }
        public List<UserViewModel> AgentesInactivos { get; set; }
        public List<UserViewModel> DesarrolladoresActivos { get; set; }
        public List<UserViewModel> DesarrolladoresInactivos { get; set; }

        public List<PropiedadViewModel> Propiedades{ get; set; }

       public List<UserViewModel> ClientesActivos { get; set; }
        public List<UserViewModel> ClientesInactivos { get; set; }


    }
}
