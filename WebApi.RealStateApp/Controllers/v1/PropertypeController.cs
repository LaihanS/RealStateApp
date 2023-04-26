using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Features.Propiedades.Commands.CreatePropertypeCommand;
using RealStateApp.Core.Application.Features.Propiedades.Commands.CreatePropiedad;
using RealStateApp.Core.Application.Features.Propiedades.Commands.DeletePropertypeById;
using RealStateApp.Core.Application.Features.Propiedades.Commands.DeletePropiedadById;
using RealStateApp.Core.Application.Features.Propiedades.Commands.UpdatePropertypeCommand;
using RealStateApp.Core.Application.Features.Propiedades.Commands.UpdatePropiedad;
using RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedades;
using RealStateApp.Core.Application.Features.Propiedades.Queries.GetPropiedadesByID.RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedades;
using RealStateApp.Core.Application.Features.VentaTypes.Commands.CreateVentaType;
using RealStateApp.Core.Application.Features.VentaTypes.Commands.UpdateVentaType;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.WebApi.Controllers;
using StockApp.Core.Application.Features.Categories.Queries.GetAllPropertypesQuery;
using StockApp.Core.Application.Features.Categories.Queries.GetPropertypeByIdQuery;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;
using System.Net.Mime;

namespace WebApi.RealStateApp.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de Tipos de Propiedad")]
    public class PropertypeController : BaseApiController
    {
        public PropertypeController(IServiceProvider services) : base(services)
        {
        }

        [Authorize(Roles = "Administrador, Desarrollador")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertypeViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Obtener los tipos de propiedad",
            Description = "Obtiene todos los tipos de propiedad del sistema"
         )]
        public async Task<IActionResult> Get()
        {
                return Ok(await Mediator.Send(new GetAllPropertypesQuery()));
        }

        [Authorize(Roles = "Administrador, Desarrollador")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertypeViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Obtener un tipo de propiedad por Id",
            Description = "Obtener un tipo de propiedad por Id del sistema"
         )]
        public async Task<IActionResult> Get(int id)
        {
           
                return Ok(await Mediator.Send(new GetPropertypeByIdQuery { id = id }));
           
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Crear tipo de propiedad",
            Description = "Crea un tipo de propiedad"
         )]
        public async Task<IActionResult> Post([FromBody] CreatePropertypeCommand command)
        {
           
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await Mediator.Send(command);
                return NoContent();

        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SavePropiedadViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Actualizar tipo de propiedad",
            Description = "Actualiza un tipo de propiedad"
         )]
        public async Task<IActionResult> Put(int id, [FromBody] UpdatePropertypeCommand command)
        {
            
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (id != command.id)
                {
                    return BadRequest();
                }

                return Ok(await Mediator.Send(command));
           
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Borrar tipo de propiedad",
            Description = "Borra un tipo de propiedad"
         )]
        public async Task<IActionResult> Delete(int id)
        {
                await Mediator.Send(new DeletePropertypeById { id = id });
                return NoContent();
           
        }
    }
}
