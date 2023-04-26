using RealStateApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Domain.Entities
{
    public class PropiedadMejora: AuditableBaseEntity
    {
        public int? PropiedadId { get; set; }
        public int MejoraId { get; set; }

        public Propiedades Propiedad { get; set; }

        public Mejora Mejora { get; set; }
    }
}
