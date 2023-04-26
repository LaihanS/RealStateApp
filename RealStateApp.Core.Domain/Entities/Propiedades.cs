using RealStateApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Domain.Entities
{
    public class Propiedades : AuditableBaseEntity
    {
        public string UnicDigitSequence { get; set; }
        public string AgenteNombre { get; set; }
        public int TipoVenta { get; set; }
        public int Precio { get; set; }
        public int MtsTerrain { get; set; }
        public int QuantityHabitaciones { get; set; }
        public int QuantityBaños { get; set; }
        public int VentaTypeId { get; set; }
        public int PropertyTypeId { get; set; }
        public string AgenteId { get; set; }
        public string? UserClientId { get; set; }

        public string Descripcion { get; set; }
        public ICollection<PropiedadMejora> PropiedadMejoras { get; set; }
        public ICollection<PropertyImages> Imagenes { get; set; }
        public VentaType VentaType { get; set; }
        public PropertyType PropiedadType { get; set; }



    }
}
