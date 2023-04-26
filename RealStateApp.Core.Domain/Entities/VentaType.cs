using RealStateApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Domain.Entities
{
    public class VentaType: AuditableBaseEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; } 
        public ICollection<Propiedades> Propiedades { get; set; }
    }
}
