using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Features.Agentes.Commands.ChangeStatus;
using RealStateApp.Core.Application.Features.Agentes.Queries.GetAllAgenteProperties;
using RealStateApp.Core.Application.Features.Mejoras.Queries.GetAgenteById;
using RealStateApp.Core.Application.Features.Mejoras.Queries.GetAllAgentesQuery;
using RealStateApp.Core.Application.Features.Mejoras.Queries.GetAllMejorasQuery;
using RealStateApp.Core.Application.Features.Propiedades.Commands.CreatePropiedad;
using RealStateApp.Core.Application.Features.Propiedades.Commands.DeletePropiedadById;
using RealStateApp.Core.Application.Features.Propiedades.Commands.UpdatePropiedad;
using RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedades;
using RealStateApp.Core.Application.Features.Propiedades.Queries.GetPropiedadesByID.RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedades;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.WebApi.Controllers;
using StockApp.Core.Application.Features.Categories.Queries.GetAllPropertypesQuery;
using StockApp.Core.Application.Features.Categories.Queries.GetPropertypeByIdQuery;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;
using System.Net.Mime;

namespace WebApi.RealStateApp.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de Usuarios Agentes")]
    public class AgenteController : BaseApiController
    {

        private readonly IUserService userService;
        private readonly IVentaTypeService ventaType;
        private readonly IPropertyTypeService propertyTypeService;
        private readonly IPropiedadService propiedadService;
        private readonly IPropertyImagesService propertyImages;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMejoraService mejoraService;

        public AgenteController(IServiceProvider services, IMejoraService mejoraService, IHttpContextAccessor httpContextAccessor, IVentaTypeService ventaType, IPropertyTypeService propertyTypeService, IPropertyImagesService propertyImages, IPropiedadService propiedadService, IMapper mapper, IUserService userService)  : base(services)
        {
            this.mejoraService = mejoraService;
            this.propertyImages = propertyImages;
            this.ventaType = ventaType;
            this.propertyTypeService = propertyTypeService;
            this.propiedadService = propiedadService;
            this.userService = userService;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        [Authorize(Roles = "Administrador, Desarrollador")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listado de Agentes",
            Description = "Obtiene todos los agentes"
         )]
        public async Task<IActionResult> Get()
        {
           
                return Ok(await Mediator.Send(new GetAllAgentesQuery()));
           
        }


        [Authorize(Roles = "Administrador, Desarrollador")]
        [HttpGet("AgentById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Obtener Agente por Id",
            Description = "Obtiene el agente del Id que se introduzca"
         )]
        public async Task<IActionResult> Get(string id)
        {
          
                return Ok(await Mediator.Send(new GetAgenteByIdQuery { id = id }));
            
         
        }


        [Authorize(Roles = "Administrador, Desarrollador")]
        [HttpGet("AgentPropiedades/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropiedadViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Obtener Popiedades del Agente por Id",
            Description = "Obtiene el las propiedades del agente del Id que se introduzca"
         )]
        public async Task<IActionResult> GetPropiedades(string id)
        {
           
                return Ok(await Mediator.Send(new GetAllAgentesPropertiesQuery { id = id }));
           
        }

        [Authorize(Roles = "Administrador")]
        [HttpPatch("ActivateAgent/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropiedadViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Cambiar status del agente",
            Description = "Cambia el estado del agente de inactivo a activo"
         )]
        public async Task<IActionResult> ChangeStatus(string id, bool active)
        {
      
                return Ok(await Mediator.Send(new ChangeAgenteStatusCommand { id = id, Activo = active }));
          
        }

    }
}
