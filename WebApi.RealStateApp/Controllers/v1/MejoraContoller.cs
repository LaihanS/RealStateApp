using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Features.Mejoras.Commands.CreateMejoraCommand;
using RealStateApp.Core.Application.Features.Mejoras.Commands.DeleteMejoraById;
using RealStateApp.Core.Application.Features.Mejoras.Commands.UpdateMejorasCommand;
using RealStateApp.Core.Application.Features.Mejoras.Queries.GetAllMejorasQuery;
using RealStateApp.Core.Application.Features.Mejoras.Queries.GetVentaTypeById;
using RealStateApp.Core.Application.ViewModels.Mejora;
using RealStateApp.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;
using System.Net.Mime;

namespace WebApi.RealStateApp.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de Mejoras")]
    public class MejoraController : BaseApiController
    {
        public MejoraController(IServiceProvider services) : base(services)
        {
        }

        [Authorize(Roles = "Administrador, Desarrollador")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MejoraViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Obtienener listado de mejoras",
            Description = "Obtiene todas las mejoras del sistema"
         )]
        public async Task<IActionResult> Get()
        {
           
                return Ok(await Mediator.Send(new GetAllMejorasQuery()));
           
        }

        [Authorize(Roles = "Administrador, Desarrollador")]
        [HttpGet("GetMejoraById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MejoraViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Obtienener mejora por Id",
            Description = "Obtiene la mejora que se especifique por ID"
         )]
        public async Task<IActionResult> Get(int id)
        {
                return Ok(await Mediator.Send(new GetMejorasByIdQuery { id = id }));
            
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Crear mejora",
            Description = "Ingresa una nueva mejora a la base de datos"
         )]
        public async Task<IActionResult> Post([FromBody] CreateMejoraCommand command)
        {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await Mediator.Send(command);
                return NoContent();
        }


        [Authorize(Roles = "Administrador")]
        [HttpPut("UpdateMejora/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveMejoraViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Actualizar mejora",
            Description = "Actualiza la mejora especificada a la base de datos"
         )]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateMejorasCommand command)
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
        [HttpDelete("DeleteMejora/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Eliminar mejora",
            Description = "Elimina la mejora especificada de la base de datos"
         )]
        public async Task<IActionResult> Delete(int id)
        {
           
                await Mediator.Send(new DeleteMejoraById { id = id });
                return NoContent();
           
        }
    }
}
