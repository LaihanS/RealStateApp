using AutoMapper;
using Banking.Core.Application.Helpers;
using MediatR;
using RealStateApp.Core.Application.Dtos.ImportantDto;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Features.Mejoras.Commands.UpdateMejorasCommand;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels.Mejora;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Application.ViewModels.Venta;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Agentes.Queries.GetAllAgenteProperties
{

    public class GetAllAgentesPropertiesQuery : IRequest<Response<IEnumerable<PropiedadViewModel>>>
    {
        ///<example>weflwkeflwbfnljfnekjw</example>
        [SwaggerParameter(Description = "Id del Agente")]
        public string id { get; set; }
    }
    public class GetAllAgentePropertiesQueryHandler : IRequestHandler<GetAllAgentesPropertiesQuery, Response<IEnumerable<PropiedadViewModel>>>
    {
        private readonly IMapper mapper;
        Sequence6Digit sequence6Digit = new();
        private readonly IMejoraRepository mejoraRepository;
        private readonly IUserRepository userRepository;
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IUserService userService;
        private readonly IAccountService accountService;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;

        public GetAllAgentePropertiesQueryHandler(IUserRepository userRepository, IAccountService accountService, IMejoraRepository mejoraRepository, IUserService userService, IPropiedadMejoraRepository propiedadMejoraRepository, IMapper mapper, IPropiedadRepository propiedadRepository)
        {
            this.mejoraRepository = mejoraRepository;
            this.userService = userService;
            this.propiedadMejoraRepository = propiedadMejoraRepository;
            this.propiedadRepository = propiedadRepository;
            this.mapper = mapper;
            this.accountService = accountService;
            this.userRepository = userRepository;
        }

        public async Task<Response<IEnumerable<PropiedadViewModel>>> Handle(GetAllAgentesPropertiesQuery request, CancellationToken cancellationToken)
        {
            var propiedades = await propiedadRepository.GetAsync();
            var agente = accountService.FindByIdAsync(request.id);
            if (agente == null) throw new ApiExceptions("Agent not found", (int)HttpStatusCode.NotFound);
            var propieds = propiedades.Where(p => p.AgenteId == request.id);
            if (propieds == null || propieds.Count() == 0) throw new ApiExceptions("Properties not found", (int)HttpStatusCode.NotFound);
         
            return new Response<IEnumerable<PropiedadViewModel>>(mapper.Map<IEnumerable<PropiedadViewModel>>(propieds));
        }

    }
}
