using RealStateApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Domain.Entities
{
    public class PropertyImages: AuditableBaseEntity
    {
        public int IdPropiedad { get; set; }
        public string ImagePath { get; set; }

        public Propiedades Propiedad { get; set; }
    }
}
