using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Features.Propiedades.Commands.CreatePropiedad;
using RealStateApp.Core.Application.Features.Propiedades.Commands.UpdatePropiedad;
using RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedades;
using RealStateApp.Core.Application.Features.Propiedades.Queries.GetPropiedadesByID.RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedades;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using RealStateApp.Core.Application.Features.Propiedades.Commands.DeletePropiedadById;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de Propiedades")]

    public class PropiedadController : BaseApiController
    {
        public PropiedadController(IServiceProvider services) : base(services)
        {
        }

        [Authorize(Roles = "Administrador, Desarrollador")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropiedadViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Obtener todas las propiedades del sistema",
            Description = "Obtiene un listado de todas las propiedades del sistema"
         )]
        public async Task<IActionResult> Get([FromQuery] GetAllPropiedadParameters filters)
        {
           
                return Ok(await Mediator.Send(new GetAllPropiedadesQuery() { PropertyCode = filters.PropertyCode }));
           
        }


        [Authorize(Roles = "Administrador, Desarrollador")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SavePropiedadViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Obtener una propiedad por Id",
            Description = "Obtiene una propiedad por Id"
         )]
        public async Task<IActionResult> Get(int id)
        {
            
                return Ok(await Mediator.Send(new GetPropiedadByIdQuery { id = id }));
            
        }

        //[Authorize(Roles = "Administrador")]
        //[Authorize(Roles = "Desarrollador")]
        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Post(CreatePropertyCommand command)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest();
        //        }

        //        await Mediator.Send(command);
        //        return NoContent();

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}


        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SavePropiedadViewModel))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Put(int id, UpdatePropiedadCommand command)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest();
        //        }
        //        if(id != command.id)
        //        {
        //            return BadRequest();
        //        }

        //        return Ok(await Mediator.Send(command));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
           
                await Mediator.Send(new DeletePropiedadByID { id = id });
                return NoContent();
           
        }
    }
}
