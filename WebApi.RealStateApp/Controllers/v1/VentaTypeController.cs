using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Features.Propiedades.Commands.CreatePropiedad;
using RealStateApp.Core.Application.Features.Propiedades.Commands.DeletePropiedadById;
using RealStateApp.Core.Application.Features.Propiedades.Commands.UpdatePropertypeCommand;
using RealStateApp.Core.Application.Features.Propiedades.Commands.UpdatePropiedad;
using RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedades;
using RealStateApp.Core.Application.Features.Propiedades.Queries.GetPropiedadesByID.RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedades;
using RealStateApp.Core.Application.Features.VentaTypes.Commands.CreateVentaType;
using RealStateApp.Core.Application.Features.VentaTypes.Commands.DeleteVentaType;
using RealStateApp.Core.Application.Features.VentaTypes.Commands.UpdateVentaType;
using RealStateApp.Core.Application.Features.VentaTypes.Queries.GetAllVentaTypes;
using RealStateApp.Core.Application.Features.VentaTypes.Queries.GetVentaTypeById;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Application.ViewModels.Venta;
using RealStateApp.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;
using System.Net.Mime;

namespace WebApi.RealStateApp.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de Tipos de Venta")]
    public class VentaTypeController : BaseApiController
    {
        public VentaTypeController(IServiceProvider services) : base(services)
        {
        }

        [Authorize(Roles = "Administrador, Desarrollador")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VentaTypeViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Obtener todas los tipos de venta",
            Description = "Obtiene el listado de todos los tipos de venta del sistema"
         )]
        public async Task<IActionResult> Get()
        {

                return Ok(await Mediator.Send(new GetAllVentaTypesQuery()));
           
        }

        [Authorize(Roles = "Administrador, Desarrollador")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VentaTypeViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Obtener tipo de venta por id",
            Description = "Obtiene un tipo de venta por id"
         )]
        public async Task<IActionResult> Get(int id)
        {
            
                return Ok(await Mediator.Send(new GetVentaTypeByIdQuery { id = id }));
           
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Crear tipo de venta",
            Description = "Crea un tipo de venta nuevo"
         )]
        public async Task<IActionResult> Post([FromBody] CreateVentatTypeCommand command)
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
            Summary = "Actualizar tipo de venta",
            Description = "Actualiza un tipo de venta nuevo"
         )]
        public async Task<IActionResult> Put(int id,[FromBody] UpdateVentaTypeCommand command)
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
            Summary = "Eliminar tipo de venta",
            Description = "Elimina un tipo de venta nuevo"
         )]
        public async Task<IActionResult> Delete(int id)
        {
          
                await Mediator.Send(new DeleteVentaTypeById { id = id });
                return NoContent();
           
        }
    }
}
