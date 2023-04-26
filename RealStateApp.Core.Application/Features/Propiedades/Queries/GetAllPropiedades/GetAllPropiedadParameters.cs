using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedades
{
    ///<summary>
    /// Parametro para obtener un producto específico
    ///</summary>
    public class GetAllPropiedadParameters
    {
        ///</example>234123<example>
        [SwaggerParameter(Description = "Código de la propiedad")]
        public string? PropertyCode { get; set; }
    }
}
