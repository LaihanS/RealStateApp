using AutoMapper;
using Banking.Core.Application.Helpers;
using MediatR;
using RealStateApp.Core.Application.Dtos.ImportantDto;
using RealStateApp.Core.Application.Enums;
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
using RealStateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Mejoras.Queries.GetAgenteById
{

    public class GetAgenteByIdQuery : IRequest<Response<UserViewModel>>
    {
        ///<example>weflwkeflwbfnljfnekjw</example>
        [SwaggerParameter(Description = "Id del Agente")]
        public string id { get; set; }
    }
    public class GetAgenteByIdQueryHandler : IRequestHandler<GetAgenteByIdQuery, Response<UserViewModel>>
    {
        private readonly IMapper mapper;
        Sequence6Digit sequence6Digit = new();
        private readonly IMejoraRepository mejoraRepository;
        private readonly IUserRepository userRepository;
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IUserService userService;
        private readonly IAccountService accountService;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;

        public GetAgenteByIdQueryHandler(IUserRepository userRepository, IAccountService accountService, IMejoraRepository mejoraRepository, IUserService userService, IPropiedadMejoraRepository propiedadMejoraRepository, IMapper mapper, IPropiedadRepository propiedadRepository)
        {
            this.mejoraRepository = mejoraRepository;
            this.userService = userService;
            this.propiedadMejoraRepository = propiedadMejoraRepository;
            this.propiedadRepository = propiedadRepository;
            this.mapper = mapper;
            this.accountService = accountService;
            this.userRepository = userRepository;
        }

        public async Task<Response<UserViewModel>> Handle(GetAgenteByIdQuery request, CancellationToken cancellationToken)
        {
            var categoriesViewModel = await GetAllUsersAsyncJoined();
            var agente = categoriesViewModel.FirstOrDefault(u => u.Id == request.id);
            if (agente == null) throw new ApiExceptions("Agent not found", (int)HttpStatusCode.NotFound);
            var agentevm = mapper.Map<UserViewModel>(agente);
            return new Response<UserViewModel>(agentevm);
        }

        private async Task<List<UserViewModel>> GetAllUsersAsyncJoined()
        {
            List<UserViewModel> userlist = mapper.Map<List<UserViewModel>>(await userRepository.GetAsync());
            List<string> userids = new();


            foreach (UserViewModel viewmodel in userlist)
            {
                userids.Add(viewmodel.Id);
            }
            List<UserViewModel> users = new();
            List<UserViewModel> userlistas = await accountService.GetUsers(userids);

           
            users = userlistas.ToList();

            users = users.Where(u => u.RoleList.Any(r => r.Equals(EnumRoles.Agente.ToString()))).ToList();
            List<RealStateApp.Core.Domain.Entities.Propiedades> props = await propiedadRepository.GetAsync();

            foreach (UserViewModel item in users)
            {
                item.CantidadPropiedades = props.Where(p => p.AgenteId == item.Id).Count();
            }

            return users;

        }
    }
}
