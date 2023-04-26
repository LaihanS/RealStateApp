using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Propiedades.Commands.UpdatePropiedad
{
    public class PropiedadUpdateResponse
    {
        public int id { get; set; }

        public string UnicDigitSequence { get; set; }
        public string Tipo { get; set; }
        public string AgenteNombre { get; set; }
        public string TipoVenta { get; set; }
        public int Precio { get; set; }
        public int MtsTerrain { get; set; }
        public int QuantityHabitaciones { get; set; }
        public string UserClientId { get; set; }

        public int QuantityBaños { get; set; }
        public int VentaTypeId { get; set; }
        public int PropertyTypeId { get; set; }
        public string AgenteId { get; set; }
        public List<int>? IdMejoras { get; set; }
    }
}
