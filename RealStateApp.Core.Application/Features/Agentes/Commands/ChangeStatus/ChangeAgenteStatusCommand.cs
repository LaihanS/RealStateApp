using AutoMapper;
using Banking.Core.Application.Helpers;
using MediatR;
using RealStateApp.Core.Application.Dtos.ImportantDto;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Features.Mejoras.Commands.UpdateMejorasCommand;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.Services;
using RealStateApp.Core.Application.ViewModels.Mejora;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Application.ViewModels.Venta;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Agentes.Commands.ChangeStatus
{

    public class ChangeAgenteStatusCommand : IRequest<Response<int>>
    {
        ///<example>weflwkeflwbfnljfnekjw</example>
        [SwaggerParameter(Description = "Id del desarrollador")]
        public string id { get; set; }

        [SwaggerParameter(Description = "Activo O Inactivo")]
        public bool Activo { get; set; }
    }
    public class ChangeAgenteStatusCommandHandler : IRequestHandler<ChangeAgenteStatusCommand, Response<int>>
    {
        private readonly IMapper mapper;
        Sequence6Digit sequence6Digit = new();
        private readonly IMejoraRepository mejoraRepository;
        private readonly IUserRepository userRepository;
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IUserService userService;
        private readonly IAccountService accountService;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;

        public ChangeAgenteStatusCommandHandler(IUserRepository userRepository, IAccountService accountService, IMejoraRepository mejoraRepository, IUserService userService, IPropiedadMejoraRepository propiedadMejoraRepository, IMapper mapper, IPropiedadRepository propiedadRepository)
        {
            this.mejoraRepository = mejoraRepository;
            this.userService = userService;
            this.propiedadMejoraRepository = propiedadMejoraRepository;
            this.propiedadRepository = propiedadRepository;
            this.mapper = mapper;
            this.accountService = accountService;
            this.userRepository = userRepository;
        }

        public async Task<Response<int>> Handle(ChangeAgenteStatusCommand request, CancellationToken cancellationToken)
        {
          var user = await accountService.ActivateOrInactivateUserManually(request.id, request.Activo);
          if(user == null || user.Id == null) throw new ApiExceptions("An error occurred while changing agent status", (int)HttpStatusCode.InternalServerError);
          return new Response<int>((int)HttpStatusCode.NoContent);
        }


    }
}
