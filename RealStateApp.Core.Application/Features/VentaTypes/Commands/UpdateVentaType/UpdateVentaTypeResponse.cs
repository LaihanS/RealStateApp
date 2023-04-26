using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.VentaTypes.Commands.UpdateVentaType
{
    public class UpdateVentaTypeResponse
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
